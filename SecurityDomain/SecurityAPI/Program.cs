

using Microsoft.EntityFrameworkCore;
using SharedItems.JWT;
using SecurityDomain.Repositories;
using SecurityInfrastructure.Database;
using SecurityInfrastructure.Repositories;
using SecurityDomain.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;

builder.Services.AddDbContext<SecurityDbContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("DatabaseConnection")));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddIdentity<AppUser, Role>()
          .AddEntityFrameworkStores<SecurityDbContext>()
          .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
});



builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();





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

