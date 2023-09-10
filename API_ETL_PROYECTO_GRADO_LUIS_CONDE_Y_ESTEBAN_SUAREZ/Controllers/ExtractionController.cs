using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Dto;
using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories;
using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Utils.HubSignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.Analysis;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtractionController : Controller
    {



        private readonly IUnitOfWork _unitOfWork;

        private readonly IHubContext<DataWarehouseHubClass> _hubContext;

        public ExtractionController(IUnitOfWork unitOfWork, IHubContext<DataWarehouseHubClass> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> ExtractDataFromMultipleSourcesAsync(List<DataSource> dataSources)
        {
            var tasks = new List<Task<string>>(); // Lista para almacenar tareas de extracción de datos

            foreach (var dataSource in dataSources)
            {
                switch (dataSource.FormatOrigin.ToLower())
                {
                    case "csv":
                        tasks.Add(ExtractDataFromCsvAsync(dataSource.Path));
                        break;
                    case "xlsx":
                        tasks.Add(ExtractDataFromXslxAsync(dataSource.Path));
                        break;
                    case "sql":
                        tasks.Add(ExtractDataFromSqlServerAsync(dataSource.Path));
                        break;
                    default:
                        // Manejar un formato no válido (por ejemplo, devolver un error)
                        return BadRequest($"Formato no válido para la ruta: {dataSource.Path}");
                }
            }

            while (tasks.Count > 0)
            {
                var completedTask = await Task.WhenAny(tasks);
                tasks.Remove(completedTask);
                await _hubContext.Clients.All.SendAsync("ExtractionCompleted", "Extracción de datos completada");
            }

            // Aquí puedes procesar los resultados, si es necesario
            var extractedDataList = tasks.Select(t => t.Result).ToList();

            if (extractedDataList.Count == 1)
            {
                // Llama al método cuando tienes un solo JSON
                bool success = await _unitOfWork.DataWarehouseRepository.LoadSingleRawDataIntoStagingArea(extractedDataList[0]);
                await _hubContext.Clients.All.SendAsync("DataSaved", success);
                return Ok("Carga de datos completada para un solo JSON");
            }
            else if (extractedDataList.Count > 1)
            {
                // Llama a otro método cuando tienes más de un JSON
                bool success = await _unitOfWork.DataWarehouseRepository.LoadRawDataIntoStagingArea(extractedDataList);
                await _hubContext.Clients.All.SendAsync("DataSaved", success);
                return Ok("Carga de datos completada para múltiples JSON");
            }
            else
            {
                // Devuelve un mensaje si no se encontraron datos para cargar
                return Ok("No se encontraron datos para cargar");
            }



        }




        [HttpPost("read-csv/{filePath}")]
        public async Task<string> ExtractDataFromCsvAsync(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    return "BadRequest: La ruta del archivo CSV es requerida.";
                }

                var csvData = await _unitOfWork.CsvCollection.ReadCsvAsync(filePath);

                string JsonDataFrame = await _unitOfWork.CsvCollection.DataFrameToJsonAsync(csvData);
                await _unitOfWork.DataWarehouseRepository.LoadSingleRawDataIntoStagingArea(JsonDataFrame);

                return JsonDataFrame; // Devuelve el JsonDataFrame en caso de éxito
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}"; // Devuelve un mensaje de error en caso de excepción
            }
        }



        [HttpPost("read-xlsx/{filePath}")]
        public async Task<string> ExtractDataFromXslxAsync(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    return "BadRequest: La ruta del archivo es obligatoria.";
                }

                var RawDataExcel = await _unitOfWork.XslxCollection.ReadExcelAsync(filePath);
                string XslxlJsonFormat = await _unitOfWork.XslxCollection.ExcelDataToMetadataJsonAsync(RawDataExcel);

                return XslxlJsonFormat; // Devuelve el resultado como una cadena
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}"; // Devuelve un mensaje de error en caso de excepción
            }
        }




        [HttpPost("ReadSqlEndpoint")]
        public async Task<string> ExtractDataFromSqlServerAsync(string ConnectionString)
        {
            try
            {
                // Verifica si connectionString y databaseName son nulos o vacíos
                if (string.IsNullOrEmpty(ConnectionString))
                {
                    return "BadRequest: la cadena de conexion es necesaria.";
                }

                //var sqlCollection = _unitOfWork.SqlCollection;
                string SQLDataToJSON = await _unitOfWork.SqlCollection.ReadRemoteSqlEndpointAsync(ConnectionString);
                await _unitOfWork.DataWarehouseRepository.LoadSingleRawDataIntoStagingArea(SQLDataToJSON);

                return "Operación exitosa"; // Devuelve un mensaje de éxito como una cadena
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}"; // Devuelve un mensaje de error en caso de excepción
            }
        }


        [HttpGet("Hola")]
        public IActionResult ObtenerSaludo()
        {
            return Ok("Hola");
        }


    }
}
