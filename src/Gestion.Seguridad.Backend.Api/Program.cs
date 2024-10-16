using FluentValidation;
using Gestion.Seguridad.Backend.Application.Mappers;
using Gestion.Seguridad.Backend.Application.Services;
using Gestion.Seguridad.Backend.Application.Services.Interfaces;
using Gestion.Seguridad.Backend.Application.Validators.Usuario;
using Gestion.Seguridad.Backend.Domain.DTOs.Usuario;
using Gestion.Seguridad.Backend.Domain.Helpers;
using Gestion.Seguridad.Backend.Infrastructure.Persistence.Configuration;
using Gestion.Seguridad.Backend.Infrastructure.Persistence.Repositories;
using Gestion.Seguridad.Backend.Infrastructure.Persistence.Repositories.Interfaces;
using Serilog.Events;
using Serilog;
using Gestion.Seguridad.Backend.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
var configuration = builder.Configuration;
SqlServerDatabase.ConnectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

// Validators with Fluent Validation
builder.Services.AddScoped<IValidator<CrearUsuarioDto>, CrearUsuarioValidator>();
builder.Services.AddScoped<IValidator<EditarUsuarioDto>, EditarUsuarioValidator>();
builder.Services.AddScoped<IValidator<LoginUsuarioDto>, LoginUsuarioValidator>();

// Dependecy Injection for Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Dependecy Injection for Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

// Cors
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

// Serilog
var directory = Path.Combine(AppContext.BaseDirectory, "Logs");
var fileName = "Log.txt";
if (!Directory.Exists(directory))
{
    Directory.CreateDirectory(directory);
}
var path = Path.Combine(directory, fileName);
var helper = new SerilogHelper();
helper.JsonFileConfiguration(path, LogEventLevel.Error, RollingInterval.Day);
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(helper.Logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Cors Policy
app.UseCors("CorsPolicy");

// Error Handling Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();