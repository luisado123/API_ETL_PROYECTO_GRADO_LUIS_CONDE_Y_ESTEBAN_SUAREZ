using Amazon.Runtime.Internal;
using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.Etl.LuisConde.EstebanSuarez.Infraestructura.Extensiones
{
    public static class LeerArchivoCsvExtension
    {
        public static async Task<List<Dictionary<string, object>>> LeerArchivoCsv(this IFormFile archivo)
        {
            using var documento = archivo.OpenReadStream();
            var contenido = await ProcesarArchivoCsvAsync(documento);
            return contenido;
        }

        private static async Task<List<Dictionary<string, object>>> ProcesarArchivoCsvAsync(Stream stream)
        {
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            // Lee el archivo CSV y convierte las filas en un diccionario
            var records = await Task.Run(() => csv.GetRecords<dynamic>().ToList());
            var result = new List<Dictionary<string, object>>();

            foreach (var record in records)
            {
                var dictionary = new Dictionary<string, object>();

                foreach (var keyValuePair in record)
                {
                    dictionary.Add(keyValuePair.Key, keyValuePair.Value);
                }

                result.Add(dictionary);
            }

            return result;
        }

    }
}
