using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql;
using ParkingLotSystem.Server.DataAccess.Contexts;
using ParkingLotSystem.Server.DataAccess.Repositories;
using ParkingLotSystem.Server.Business.Interfaces;
using ParkingLotSystem.Server.Business.Services;
using ParkingLotSystem.Server.DataAccess.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//  CORS Politikası Tanımlama (React Uygulaması İçin)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("https://localhost:59014")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
});

////  Veritabanı Bağlantısı
//builder.Services.AddDbContext<ParkingLotSystemDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// MySQL Bağlantısı
builder.Services.AddDbContext<ParkingLotSystemDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 40)) 
    ));

//  JWT Authentication Konfigürasyonu
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

//  Swagger Konfigürasyonu + JWT Desteği
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Parking Lot API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Token'ınızı girin. Örnek: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//  Controller ve API Endpoint'lerini Kaydetme
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//  Bağımlılıkları (Dependency Injection) Tanımlama
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<ISiteService, SiteService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();

var app = builder.Build();

//  Middleware Kullanımı
app.UseCors("AllowReactApp");

app.UseAuthentication();  // Kullanıcı kimlik doğrulaması
app.UseAuthorization();   // Yetkilendirme kontrolleri

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
