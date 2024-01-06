using Microsoft.AspNetCore.SignalR;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Utils.HubSignalR
{
    public class DataWarehouseHubClass:Hub
    {
        private readonly ILogger<DataWarehouseHubClass> _logger;

        public DataWarehouseHubClass(ILogger<DataWarehouseHubClass> logger)
        {
            _logger = logger;
        }
        public async Task NotifyExtractionCompleted(bool success)
        {
            var message = success ? "Extracción de datos completada" : "Extracción de datos fallida";
            _logger.LogInformation(message);
           await Clients.All.SendAsync("ExtractionCompleted", new { Message = message, Success = success });
        }

        public async Task NotifyDataSaved(bool success)
        {
            var message = success ? "Datos guardados con éxito" : "Error al guardar los datos";
            _logger.LogInformation(message);
            await Clients.All.SendAsync("DataSaved", new { Message = message, Success = success });
        }


    }
}
