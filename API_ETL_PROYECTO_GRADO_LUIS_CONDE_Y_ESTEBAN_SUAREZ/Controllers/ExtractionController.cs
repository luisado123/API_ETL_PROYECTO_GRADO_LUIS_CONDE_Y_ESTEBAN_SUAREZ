using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Dto;
using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Analysis;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

                string JsonDataFrame =await _unitOfWork.CsvCollection.DataFrameToJsonAsync(csvData);
               await _unitOfWork.DataWarehouseRepository.LoadSingleRawDataIntoStagingArea(JsonDataFrame);
                return Ok(JsonDataFrame);
            }
            catch (Exception ex)
            {
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
                var finalJsonObjectResult= new { data = result, origin = "CSV" };
                string XslxlJsonFormat = Newtonsoft.Json.JsonConvert.SerializeObject(finalJsonObjectResult);

                return Ok(XslxlJsonFormat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en la operación: {ex.Message}");
            }
        }



        [HttpPost("ReadSqlEndpoint")]
        public async Task<IActionResult> ReadRemoteSqlEndpoint(SqlEndpointParameters parameters)
        {
            try
            {
                // Verifica si connectionString y databaseName son nulos o vacíos
                if (string.IsNullOrEmpty(parameters.ConnectionString) || string.IsNullOrEmpty(parameters.DatabaseName))
                {
                    return BadRequest("Los parámetros connectionString y databaseName son requeridos.");
                }

                var sqlCollection = _unitOfWork.SqlCollection;
                string SQLDataToJSON =  await sqlCollection.ReadRemoteSqlEndpointAsync(parameters.ConnectionString);
                await _unitOfWork.DataWarehouseRepository.LoadSingleRawDataIntoStagingArea(SQLDataToJSON);
                return Ok("Operación exitosa");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en la operación: {ex.Message}");
            }
        }
    }
}
