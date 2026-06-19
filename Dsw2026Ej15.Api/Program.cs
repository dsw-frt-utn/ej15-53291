using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Registramos tu persistencia en memoria como Singleton (Punto 3.f del enunciado)
builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();

// 2. Registramos el servicio básico de Health Checks (Punto 3.j del enunciado)
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ⚠️ ESPACIO RESERVADO PARA PERSONA 2:
// Aquí tu compañero va a meter la línea de su middleware de errores más adelante.

app.UseAuthorization();
app.MapControllers();

// 3. Mapeamos la ruta del Health Check básico para el sondeo de estado
app.MapHealthChecks("/health-check");

app.Run();