using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hospital_Management_system.Api.Middleware;
using Hospital_Management_system.Api.Validators;
using Hospital_Management_system.Database.Entities;
using Hospital_Management_system.Models.Mapping;
using Hospital_Management_system.Services;
using Hospital_Management_system.Services.AuthRepository;
using Hospital_Management_system.Services.PatientRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using Serilog.Events;
using Microsoft.ApplicationInsights.Extensibility;
using Hospital_Management_system.Services.DoctorRepository;

// 🟢 ADDED: Initial bootstrap logging configuration
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        path: "Logs/hospital-api-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.ApplicationInsights(
        TelemetryConfiguration.CreateDefault(),
        TelemetryConverter.Traces)
    .CreateLogger();

try
{
    Log.Information("Starting up the Hospital Management API...");

    var builder = WebApplication.CreateBuilder(args);

    // 🟢 ADDED: Tell ASP.NET Core to use Serilog for all application logs
    builder.Host.UseSerilog();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    // Add services
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientValidator>();
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

    builder.Services.AddScoped<IPatientService, PatientService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IDoctorService, DoctorService>();

    // Configure AutoMapper explicitly via configuration action
    builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

    // 🔐 Configure JWT Authentication Schemes
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
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

    var app = builder.Build();

    // Configure middleware pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hospital API v1");
            c.RoutePrefix = string.Empty;
        });
    }

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseHttpsRedirection();

    // 🔐 Ordered Security Pipeline
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application failed to start correctly.");
}
finally
{
    Log.CloseAndFlush();
}