using System.Text;
using FleetFlow.Api.Data;
using FleetFlow.Api.Models;
using FleetFlow.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Por padrão o projeto roda SEM PostgreSQL, usando banco em memória.
// Para usar PostgreSQL depois, altere "DatabaseProvider" no appsettings.json.
var databaseProvider = builder.Configuration["DatabaseProvider"] ?? "InMemory";

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (databaseProvider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string do PostgreSQL não configurada.");

        options.UseNpgsql(connectionString);
    }
    else
    {
        options.UseInMemoryDatabase("FleetFlow");
    }
});

builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<TokenService>();

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Jwt:Key não configurada.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Desenvolvimento", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("Desenvolvimento");
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Cria o usuário administrador inicial tanto no InMemory quanto no PostgreSQL.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var passwordService = scope.ServiceProvider.GetRequiredService<PasswordService>();

    if (databaseProvider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
    {
        db.Database.EnsureCreated();
    }

    if (!db.Usuarios.Any())
    {
        db.Usuarios.Add(new Usuario
        {
            Nome = "Administrador FleetFlow",
            Email = "admin@fleetflow.com",
            SenhaHash = passwordService.GerarHash("123456"),
            Perfil = "Administrador",
            Ativo = true
        });

        db.SaveChanges();
    }
}

app.Run();
