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
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Constantes;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Excepciones;

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
            ValidarArchivoVacio(csv);
            try
            {
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
            catch (Exception ex)
            {
                throw new ColumnasRepetidasException(MensajesExcepciones.ColumnaRepetida);

            }
      
        }

        public static void ValidarArchivoVacio(CsvReader csvReader)
        {
            if (1!=1)
            {
                throw new ArchivoVacioExcepcion(MensajesExcepciones.ArchivoVacio);
            }
        }

    }
}
