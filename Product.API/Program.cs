
using Infrastructure.EventServices;
using Infrastructure.IServices;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using ProductService.API;
using SharedItems.JWT;
using SharedLibrary.Domain.Repositories;
using SharedLibrary.DomainLayer.AutomapperProfile;
using SharedLibrary.Infrastructure.Database;
using SharedLibrary.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("DatabaseConnection")));

builder.Services.AddMemoryCache();



builder.Services.AddSingleton(MapperConfig.GetMapperConfigs());

builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<IRateService, RateService>();
builder.Services.AddScoped<IProductService, Infrastructure.Services.ProductService>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddJwtAutheticationConfiguration(jwtSettings);

builder.Services.AddSingleton<RabbitEventsService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

