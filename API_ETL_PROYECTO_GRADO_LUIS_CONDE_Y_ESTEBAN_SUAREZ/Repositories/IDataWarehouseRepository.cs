namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public interface IDataWarehouseRepository
    {
        Task<bool> LoadRawDataIntoStagingArea(List<string> dataJson);
        Task<bool> LoadSingleRawDataIntoStagingArea(string singleDataJson);
    }
}
