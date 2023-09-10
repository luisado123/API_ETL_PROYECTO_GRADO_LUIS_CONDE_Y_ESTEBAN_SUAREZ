namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public interface IXslxCollection
    {
        Task<List<Dictionary<string, string>>> ReadExcelAsync(string filePath);
        Task<string> ExcelDataToMetadataJsonAsync(List<Dictionary<string, string>> excelData);
    }
}
