using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataWarehouseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DataWarehouseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task<IActionResult> LoadRawDataIntoStagingAreaa(List<string> rawJsonDataList)
        //{
        //    if (categoria == null)
        //    {
        //        return BadRequest();
        //    }

        //    categoria.Id = ObjectId.GenerateNewId().ToString();

        //    await mongoDb.CreateCategoria(categoria);

        //    return Created("Created", true);
        //}

        [HttpGet("SaveSingleRawData")]

        public async Task LoadSingleRawDataIntoStagingArea(string transformedDataJson)
        {

            // Implementa aquí la lógica para guardar el dato transformado
            await _unitOfWork.DataWarehouseRepository.LoadSingleRawDataIntoStagingArea(transformedDataJson);
        }

    }
}
