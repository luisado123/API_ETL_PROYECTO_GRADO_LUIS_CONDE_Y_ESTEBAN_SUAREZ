using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Infraestructura.Servicios;

namespace UTS.Etl.LuisConde.EstebanSuarez.Infraestructura.Extensiones
{
    public static  class LeerArchivoExcelExtension
    {
        public static async Task<List<Dictionary<string, object>>> LeerArchivoExcel(this IFormFile archivo)
        {
            using var archivoStream = archivo.OpenReadStream();
            using var documento = SpreadsheetDocument.Open(archivoStream, false);

            var contenido = await ProcesarArchivoExcelAsync(documento);

            return contenido;
        }
        public static async Task<List<Dictionary<string, object>>> ProcesarArchivoExcelAsync(SpreadsheetDocument spreadsheetDocument)
        {
            return await Task.Run(() =>
            {
                var excelDataList = new List<Dictionary<string, object>>();
                var workbookPart = spreadsheetDocument.WorkbookPart;
                var worksheetPart = workbookPart.WorksheetParts.FirstOrDefault();

                if (worksheetPart != null)
                {
                    var worksheet = worksheetPart.Worksheet;
                    var sheetData = worksheet.GetFirstChild<SheetData>();

                    var rows = sheetData.Elements<Row>();

                    foreach (var row in rows.Skip(1))
                    {
                        var excelData = new Dictionary<string, object>();
                        var cells = row.Elements<Cell>().ToList();

                        for (int col = 0; col < cells.Count; col++)
                        {
                            var columnHeaderCell = sheetData.Elements<Row>().First().Elements<Cell>().ElementAt(col);
                            var columnHeader = ObtenerCeldasExcel(workbookPart, columnHeaderCell);
                            var cellValue = ObtenerCeldasExcel(workbookPart, cells[col]);
                            excelData.Add(columnHeader, cellValue);
                        }

                        excelDataList.Add(excelData);
                    }
                }

                return excelDataList;
            });
        }



        private static  string ObtenerCeldasExcel(WorkbookPart workbookPart, Cell cell)
        {
            var cellValue = cell.CellValue;
            var text = (cellValue == null) ? cell.InnerText : cellValue.Text;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                var sharedStringTable = workbookPart.SharedStringTablePart.SharedStringTable;
                return sharedStringTable.ChildElements[int.Parse(text)].InnerText;
            }

            return text;
        }

    }
}
