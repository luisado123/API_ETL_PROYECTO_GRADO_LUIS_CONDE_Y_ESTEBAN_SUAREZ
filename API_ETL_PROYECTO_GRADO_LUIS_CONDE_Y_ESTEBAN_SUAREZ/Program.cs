using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Utils.HubSignalR;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using OfficeOpenXml;
using System.Reflection;
using MediatR;
using UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.ArchivoXlsx.Comandos.Crear;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;
using UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Puertos;
using UTS.Etl.LuisConde.EstebanSuarez.Infraestructura.Servicios;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.servicios.ArchivoXlsx;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Puertos;
using UTS.Etl.LuisConde.EstebanSuarez.Infraestructura.Adaptadores;
using UTS.Etl.LuisConde.EstebanSuarez.Infraestructura.Adaptadores.MongoDb;
using UTS.Etl.LuisConde.EstebanSuarez.Infraestructura.FormateoRespuesta;
using UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.Carga;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuracion = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddTransient<IProcesarArchivoServicio, ProcesarArchivoServicio>();
builder.Services.AddTransient<IRequestHandler<CrearObjetoDataLakeComando, RespuestaEtl>, CrearObjetoDataLakeManejador>();
builder.Services.AddSingleton<IMongoClient>(_ =>
{
    var connectionString = configuracion.GetConnectionString("MongoDbConnection");
    return new MongoClient(connectionString);
});

builder.Services.AddSingleton<IMongoConexionRepositorio, MongoConexionRepositorio>(_ =>
{
    var connectionString = configuracion.GetConnectionString("MongoDbConnection");
    var databaseName = configuracion.GetConnectionString("MongoDbDatabaseName");
    return new MongoConexionRepositorio(connectionString, databaseName);
});
builder.Services.AddSingleton<IDataLakeRepositorio, DataLakeRepositorio>();
builder.Services.AddTransient<GuardarObjetoEtlServicio>();
builder.Services.AddTransient<IRequestHandler<CargarObjetosDataLakeComando,IActionResult>, CargarObjetosDataLakeManejador>();


var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddConsole() // Agregar el destino de registro de consola
        .SetMinimumLevel(LogLevel.Information); // Establecer el nivel de registro deseado
});

var logger = loggerFactory.CreateLogger<DataWarehouseHubClass>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("https://localhost:7229") // Cambia el puerto si es necesario
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<ExceptionMiddleware>();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowLocalhost");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<DataWarehouseHubClass>("/DataWarehouseHubClass"); // Reemplaza YourHubClass con la clase de tu hub
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
