namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public interface IXslxCollection
    {
        Task<IEnumerable<Dictionary<string, string>>> ReadExcelAsync(string filePath);
    }
}
