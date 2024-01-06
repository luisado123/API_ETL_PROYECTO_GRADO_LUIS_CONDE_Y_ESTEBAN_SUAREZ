using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Dto;
using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Utils.HubSignalR;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.Analysis;
using UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.ArchivoXlsx.Comandos.Crear;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtractionController : Controller
    {

        private readonly IHubContext<DataWarehouseHubClass> _hubContext;
        readonly IMediator _mediator;
        public ExtractionController(IHubContext<DataWarehouseHubClass> hubContext,IMediator mediator)
        {
            _hubContext = hubContext;
            _mediator = mediator;
        }

        [HttpPost("ProbarExtraccionExcel")]
        public async Task<RespuestaEtl> ExtraerYTransformarXlsx([FromForm] CrearObjetoDataLakeComando solicitud) => await _mediator.Send(solicitud); 

        //[HttpPost("LoadRawDataIntoDatawarehouse")]
        //public async Task<IActionResult> LoadDataAsync([FromBody] List<DataSource> dataSources)
        //{

        //    var extractedDataList = await ExtractDataAsync(dataSources);

        //    if (extractedDataList.Count == 0)
        //    {
        //        return Ok("No se encontraron datos para cargar");
        //    }

        //    bool success = false;

        //    if (extractedDataList.Count == 1)
        //    {
        //        success = await _unitOfWork.DataWarehouseRepository.LoadSingleRawDataIntoStagingArea(extractedDataList[0]);
        //        await _hubContext.Clients.All.SendAsync("NotifyDataSaved", success);
        //    }
        //    else
        //    {
        //        success = await _unitOfWork.DataWarehouseRepository.LoadRawDataIntoStagingArea(extractedDataList);
        //        await _hubContext.Clients.All.SendAsync("NotifyDataSaved", success);
        //    }


        //    return Ok(success ? "Carga de datos completada" : "Error al cargar datos");
        //}

        //[HttpPost]
        //private async Task<List<string>> ExtractDataAsync(List<DataSource> dataSources)
        //{
        //    var tasks = new List<Task<string>>(); // Lista para almacenar tareas de extracción de datos

        //    foreach (var dataSource in dataSources)
        //    {
        //        switch (dataSource.FormatOrigin.ToLower())
        //        {
        //            case "csv":
        //                tasks.Add(ExtractDataFromCsvAsync(dataSource.Path));
        //                break;
        //            case "xlsx":
        //                tasks.Add(ExtractDataFromXslxAsync(dataSource.Path));
        //                break;
        //            case "sql":
        //                tasks.Add(ExtractDataFromSqlServerAsync(dataSource.Path));
        //                break;
        //            default:
        //                // Manejar un formato no válido (por ejemplo, devolver un error)
        //                return new List<string>(); // Devuelve una lista vacía si hay un formato no válido
        //        }
        //    }

        //    var extractedDataList = new List<string>(); // Lista para almacenar los resultados

        //    while (tasks.Count > 0)
        //    {
        //        var completedTask = await Task.WhenAny(tasks);
        //        tasks.Remove(completedTask);
        //        var result = await completedTask; // Obtener el resultado de la tarea completada
        //        extractedDataList.Add(result); // Agregar el resultado a la lista
        //        await _hubContext.Clients.All.SendAsync("NotifyExtractionCompleted", true);
        //    }

        //    // Ahora puedes procesar los resultados en extractedDataList, si es necesario
        //    return extractedDataList;
        //}





        //[HttpPost("read-csv/{filePath}")]
        //public async Task<string> ExtractDataFromCsvAsync(string filePath)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(filePath))
        //        {
        //            return "BadRequest: La ruta del archivo CSV es requerida.";
        //        }

        //        var csvData = await _unitOfWork.CsvCollection.ReadCsvAsync(filePath);

        //        string JsonDataFrame = await _unitOfWork.CsvCollection.DataFrameToJsonAsync(csvData);
        //        await _unitOfWork.DataWarehouseRepository.LoadSingleRawDataIntoStagingArea(JsonDataFrame);

        //        return JsonDataFrame; // Devuelve el JsonDataFrame en caso de éxito
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"Error: {ex.Message}"; // Devuelve un mensaje de error en caso de excepción
        //    }
        //}







        [HttpGet("Hola")]
        public IActionResult ObtenerSaludo()
        {
            return Ok("Hola");
        }


    }
}
