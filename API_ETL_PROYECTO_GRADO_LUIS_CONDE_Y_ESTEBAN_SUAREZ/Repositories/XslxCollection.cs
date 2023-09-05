using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Dto;
using OfficeOpenXml;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public class XslxCollection : IXslxCollection
    {
        public async Task<IEnumerable<Dictionary<string, string>>> ReadExcelAsync(string filePath)
        {
            var excelDataList = new List<Dictionary<string, string>>();

            await Task.Run(() =>
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault(); // Obtiene la primera hoja del archivo

                    if (worksheet != null)
                    {
                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;

                        for (int row = 2; row <= rowCount; row++) // Suponemos que la primera fila es de encabezados
                        {
                            var excelData = new Dictionary<string, string>();

                            for (int col = 1; col <= colCount; col++)
                            {
                                var columnHeader = worksheet.Cells[1, col].Text;
                                var cellValue = worksheet.Cells[row, col].Text;

                                excelData.Add(columnHeader, cellValue);
                            }

                            excelDataList.Add(excelData);
                        }
                    }
                }
            });

            return excelDataList;
        }


    }
}
