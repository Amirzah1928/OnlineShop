using FastEndpoints;
using UrlShortener.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<UrlShortnerDBContext>();

builder.Services.AddEndpointsApiExplorer();
// Add services to the container.
builder.Services.AddFastEndpoints();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseFastEndpoints();

app.Run();
