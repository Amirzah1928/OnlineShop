using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using OnlineShop;
using System.Reflection;
using System.Net;
using OnlineShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DomainService.Data;
using OnlineShop.DomainService.Services;
using OnlineShop.DomainService.Repositories;
using OnlineShop.DomainService.Proxies;
using OnlineShop.Infrastructure.Proxies;
using OnlineShop.Infrastructure.Repositories;
using OnlineShop.Application.Behaviores;
using OnlineShop.API.Middlewares;
using OnlineShop.DomainModel.Attributes;
using OnlineShop.DomainService.Helper;
using OnlineShop.APIs.Features;
using OnlineShop.APIs.Middlewares;
using Polly;
using OnlineShop.DomainModel.Models;
using Hangfire;
using OnlineShop.Application.Jobs;
using Hangfire.MemoryStorage;
using OnlineShop.APIs.OperationFilters;
using System.Text.Json.Serialization;
using Microsoft.FeatureManagement;
using OnlineShop.DomainService.Resolvers;
using OnlineShop.Infrastructure.Resolvers;
using OnlineShop.DomainService.Failovers;


var builder = WebApplication.CreateBuilder(args);

var conecctionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OnlineShopDBContext>(options => options.UseSqlServer(conecctionstring));

var readConnectionString = builder.Configuration.GetConnectionString("ReadConnection");
builder.Services.AddDbContext<OnlineShopReadDBContext>(options => options.UseSqlServer(readConnectionString));


// Add services to the container.
builder.Services.AddMemoryCache();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AddAcceptLanguageHeaderParameter>();
});



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IReadUnitOfWork, ReadUnitOfWork>();


builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserReadRepository, UserReadRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();


builder.Services.AddScoped<ITrackingCodeProxy, TrackingCodeProxy>();
// Strategy pattern
builder.Services.AddScoped<TrackingCodeProxy>();
builder.Services.AddScoped<LocalTrackingCodeProxy>();
builder.Services.AddScoped<ITrackingCodeResolver, TrackingCodeResolver>();


// Failover pattern
builder.Services.AddScoped<ITrackingCodeProxy, TrackingCodeProxy>();
builder.Services.AddScoped<ITrackingCodeProxy, LocalTrackingCodeProxy>();
builder.Services.AddScoped<FallbackTrackingCodeProxy>();



builder.Services.AddHttpClient<ITrackingCodeProxy, TrackingCodeProxy>((serviceProvider, client) =>
{
    var option = serviceProvider.GetRequiredService<IOptions<Settings>>();
    var settings = option.Value.TrackingCode;

    client.BaseAddress = new Uri(settings.BaseURL);
})
.AddPolicyHandler(serviceProvider =>
{
    var retryPolicy = Policy<HttpResponseMessage>
        .Handle<HttpRequestException>()
        .OrResult(r => !r.IsSuccessStatusCode)
        .WaitAndRetryAsync(
            retryCount: 3,
            sleepDurationProvider: attempt => TimeSpan.FromSeconds(2 * attempt),
            onRetry: (response, delay, retryCount, context) =>
            {
                Console.WriteLine($"Retry {retryCount} after {delay.TotalSeconds}s due to " +
                    $"{response.Exception?.Message ?? response.Result?.StatusCode.ToString()}");
            });

    var circuitBreakerPolicy = Policy<HttpResponseMessage>
        .Handle<HttpRequestException>()
        .OrResult(r => !r.IsSuccessStatusCode)
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(5)
        );

    var fallbackPolicy = Policy<HttpResponseMessage>
    .Handle<Exception>()
    .OrResult(r => !r.IsSuccessStatusCode)
    .FallbackAsync(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable));


    return fallbackPolicy.WrapAsync(circuitBreakerPolicy).WrapAsync(retryPolicy);
});







builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
    options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);


builder.Services.AddTransient<UserTrackingCodeJob>();

builder.Services.AddHangfire(config =>
{
    config.UseMemoryStorage();
});

builder.Services.AddHangfireServer();

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));


builder.Services.AddEndpointsApiExplorer();


builder.Services.AddLocalization();

builder.Services.AddFeatureManagement();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();



var supportedLanguages = Enum
    .GetValues<Languages>()
    .Select(x => x.ToString())
    .ToArray();

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedLanguages[0])
    .AddSupportedCultures(supportedLanguages)
    .AddSupportedUICultures(supportedLanguages);

app.UseRequestLocalization(localizationOptions);


app.MapGet("/Cities", async (IUnitOfWork unitOfWork, CancellationToken cancellationToken) =>
{
    var cities = await unitOfWork.CityRepository.GetCitiesListAsync(cancellationToken);
    return cities;
}).WithTags("City");



var enumTypes = typeof(BaseModel).Assembly.GetTypes()
    .Where(t => t.IsEnum &&
    t.Namespace != null &&
    t.Namespace.Contains("OnlineShop.DomainModel.Enums") &&
    t.GetCustomAttributes(typeof(EnumEndpointAttribute), false).Length != 0
    ).ToList();

foreach (var enumType in enumTypes)
{
    var attribute = (enumType.GetCustomAttribute(typeof(EnumEndpointAttribute)) as EnumEndpointAttribute)!;

    app.MapGet(attribute.Route, () =>
    {
        var enumValues = Enum.GetValues(enumType).Cast<Enum>();
        var viewModel = enumValues.ToVieModel();

        return BaseResult.Success(viewModel);
    })
        .WithTags("Enums");
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<RateLimitMiddleware>();

app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<UserTrackingCodeJob>(
    "get-Users-tacking-code-job",
    job => job.Get(),
    Cron.Hourly()
    );

app.Run();
