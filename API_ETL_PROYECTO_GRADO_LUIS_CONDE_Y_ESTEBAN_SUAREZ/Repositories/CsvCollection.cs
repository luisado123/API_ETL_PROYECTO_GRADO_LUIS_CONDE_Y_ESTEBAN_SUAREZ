using Amazon.Runtime.Internal;
using CsvHelper;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using System.Globalization;
using Microsoft.Data.Analysis;
using MongoDB.Bson.IO;
using System.Xml;
using Newtonsoft.Json;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public class CsvCollection:ICsvCollection
    {

        public async Task<DataFrame> ReadCsvAsync(string filePath)
        {
            var dataFrames = new List<DataFrame>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<dynamic>().ToList();

                var dataFrame = new DataFrame();
                foreach (var propertyName in ((IDictionary<string, object>)records.First()).Keys)
                {
                    dataFrame.Columns.Add(new StringDataFrameColumn(propertyName, records.Select(r => ((IDictionary<string, object>)r)[propertyName]?.ToString())));
                }

                dataFrames.Add(dataFrame);
            }


            return dataFrames[0];
        }

        public  async Task<string> DataFrameToJsonAsync(DataFrame df)
        {
            var data = new List<Dictionary<string, object>>();

            for (int rowIndex = 0; rowIndex < df.Rows.Count(); rowIndex++)
            {
                var rowData = new Dictionary<string, object>();
                rowData["fila"] = (rowIndex + 1).ToString();

                for (int colIndex = 0; colIndex < df.Columns.Count; colIndex++)
                {
                    string columnName = df.Columns[colIndex].Name;
                    object columnValue = df[rowIndex, colIndex];

                    rowData[columnName] = columnValue.ToString();
                }

                data.Add(rowData);
            }

   
            var finalJsonRaw = new
            {
                data,
                origin="CSV"
            };

            return await Task.FromResult(Newtonsoft.Json.JsonConvert.SerializeObject(finalJsonRaw));
        }

    }
}
