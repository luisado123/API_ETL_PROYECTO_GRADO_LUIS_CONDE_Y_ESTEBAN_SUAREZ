using Microsoft.Data.Analysis;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public interface ICsvCollection
    {
        Task<DataFrame> ReadCsvAsync(string filePath);
        Task<string> DataFrameToJsonAsync(DataFrame df);
    }
}
