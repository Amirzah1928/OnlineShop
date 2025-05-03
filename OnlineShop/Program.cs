using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Middlewares;
using OnlineShop.Models;
using OnlineShop.Repositories;
using OnlineShop.Services;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

var conecctionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OnlineShopDBContext>(options => options.UseSqlServer(conecctionstring));
// Add services to the container.
builder.Services.AddMemoryCache();



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/Cities", async (IUnitOfWork unitOfWork, CancellationToken cancellationToken) =>
{
    var cities = await unitOfWork.cityRepository.GetCitiesListAsync(cancellationToken);
    return cities;
}).WithTags("City");

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<RateLimitMiddleware>();
app.Run();
