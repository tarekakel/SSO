using Domain.Entities;
using Infrastructure.Auth;
using Infrastructure.Db;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SSO.Extensions;
using Asp.Versioning;
using Persistence.Repositories;
using Domain.Interfaces.Repository;
using Application.Services;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Options;
using Infrastructure.Auth.Authorization;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);



// Add API versioning using Asp.Versioning.Mvc
builder.Services.AddApiVersioning(options =>
{
    // Set the default API version (you can change this to your preferred version)
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // Optionally, assume default version when not specified
    options.AssumeDefaultVersionWhenUnspecified = true;

    // Report API versions in response headers
    options.ReportApiVersions = true;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
// JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);

builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<JwtSettings>>().Value);



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PermissionPolicy:Admin:User.Read", policy =>
      policy.Requirements.Add(new PermissionRequirement(
          new[] { "Admin" }, new[] { "User.Read" })));

    options.AddPolicy("PermissionPolicy:Admin:User.Read,User.Write", policy =>
       policy.Requirements.Add(new PermissionRequirement(
           new[] { "Admin" }, new[] { "User.Read", "User.Write" })));
    // You can dynamically add more from DB if needed
});

builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddServices();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var settings = jwtSettings.Get<JwtSettings>();
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = settings!.Issuer,
        ValidAudience = settings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key))
    };
});

var app = builder.Build();
await app.MigrateAndSeedDatabaseAsync();
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



