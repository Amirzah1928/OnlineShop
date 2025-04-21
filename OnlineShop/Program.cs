using Microsoft.EntityFrameworkCore;
using OnlineShop;

var builder = WebApplication.CreateBuilder(args);

var conecctionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OnlineShopDBContext>(options => options.UseSqlServer(conecctionstring));
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
