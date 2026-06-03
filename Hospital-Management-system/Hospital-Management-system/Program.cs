using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hospital_Management_system.Api.Middleware;
using Hospital_Management_system.Api.Validators;
using Hospital_Management_system.Database.Entities;
using Hospital_Management_system.Models.Mapping;
using Hospital_Management_system.Services.PatientRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
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
// Configure AutoMapper by adding the profile explicitly via configuration action
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
var app = builder.Build();

// Configure middleware
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

app.UseAuthorization();

app.MapControllers();

app.Run();