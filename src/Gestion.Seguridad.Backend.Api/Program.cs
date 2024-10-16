using Gestion.Seguridad.Backend.Application.Mappers;
using Gestion.Seguridad.Backend.Application.Services;
using Gestion.Seguridad.Backend.Application.Services.Interfaces;
using Gestion.Seguridad.Backend.Infrastructure.Persistence.Configuration;
using Gestion.Seguridad.Backend.Infrastructure.Persistence.Repositories;
using Gestion.Seguridad.Backend.Infrastructure.Persistence.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
var configuration = builder.Configuration;
SqlServerDatabase.ConnectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Cors Policy
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();