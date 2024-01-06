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

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddTransient<IProcesarArchivoServicio, ProcesarArchivoServicio>();
builder.Services.AddTransient<IRequestHandler<CrearObjetoDataLakeComando, RespuestaEtl>, CrearObjetoDataLakeManejador>();
//builder.Services.AddSingleton<IMongoConexionRepositorio>(provider =>
//{
//    var configuration = provider.GetRequiredService<IConfiguration>();
//    return new MongoConexionRepositorio(
//        configuration["MongoDb:ConnectionString"],
//        configuration["MongoDb:DatabaseName"]
//    );
//});


//builder.Services.AddTransient<GuardarObjetoEtlServicio>();
//builder.Services.AddTransient<IDataLakeRepositorio, DataLakeRepositorio>();




var loggerFactory = LoggerFactory.Create(builder =>
{
    // 2. Configuración del LoggerFactory
    builder
        .AddConsole() // Agregar el destino de registro de consola
        .SetMinimumLevel(LogLevel.Information); // Establecer el nivel de registro deseado
});

// 3. Creación del Logger
var logger = loggerFactory.CreateLogger<DataWarehouseHubClass>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IUnitOfWork ,UnitOfWork>();

//builder.Services.AddSingleton<MongoConnectionRepository>();
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
