using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Dto;
using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Analysis;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Controllers
{
    public class ExtractionController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public ExtractionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("read-csv/{filePath}")]
        public async Task<IActionResult> ReadCsv(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    return BadRequest("La ruta del archivo CSV es requerida.");
                }

                var csvData = await _unitOfWork.CsvCollection.ReadCsvAsync(filePath);


                //List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
                string JsonDataFrame =await _unitOfWork.CsvCollection.DataFrameToJsonAsync(csvData);

                return Ok(JsonDataFrame);
            }
            catch (Exception ex)
            {
                // Maneja las excepciones según tus necesidades
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("read-xlsx/{filePath}")]
        public async Task<IActionResult> ReadXslxAsync(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    return BadRequest("La ruta del archivo es obligatoria.");
                }

                var result = await _unitOfWork.XslxCollection.ReadExcelAsync(filePath);
                var finalJsonObjectResult= new { data = result };
                string XslxlJsonFormat = Newtonsoft.Json.JsonConvert.SerializeObject(finalJsonObjectResult);

                return Ok(XslxlJsonFormat);
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción según tus necesidades
                return StatusCode(500, $"Error en la operación: {ex.Message}");
            }
        }

        [HttpGet("read-sql/{filePath}")]
        public async Task<IActionResult> ReadLocalScripSqlAsync(string filePath)
        {
            try
            {

                // Llama al método para ejecutar el script SQL
                List<Dto.TableSqlDto> tablasScript = new List<TableSqlDto>();
                 tablasScript= await _unitOfWork.SqlCollection.ReadLocalScriptSqlAsync(filePath);

                if (tablasScript!=null && tablasScript.Count()>0)
                {
                    return Ok("Extracción de el script finalizada.");
                }
                else
                {
                    return BadRequest("El script esta vacio.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpGet("ReadSqlEndpoint")]
        public async Task<IActionResult> ReadRemoteSqlEndpoint(SqlEndpointParameters parameters)
        {
            try
            {
                // Verifica si connectionString y databaseName son nulos o vacíos
                if (string.IsNullOrEmpty(parameters.ConnectionString) || string.IsNullOrEmpty(parameters.DatabaseName))
                {
                    return BadRequest("Los parámetros connectionString y databaseName son requeridos.");
                }

                // Combina el databaseName con el connectionString
                var connectionStringWithDatabase = $"{parameters.ConnectionString};Database={parameters.DatabaseName}";

                // Crea una instancia de SqlConnectionRepository
                var sqlConnectionContext = new SqlConnectionRepository(connectionStringWithDatabase);

                var sqlCollection = _unitOfWork.SqlCollection;
            
                List<TableSqlDto> tablesCollectionRaw= new List<TableSqlDto>();
                tablesCollectionRaw = await sqlCollection.ReadRemoteSqlEndpointAsync(sqlConnectionContext);

                // Realizar operaciones con la instancia de unitOfWork
                // ...

                return Ok("Operación exitosa");
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción según tus necesidades
                return StatusCode(500, $"Error en la operación: {ex.Message}");
            }
        }
    }
}
