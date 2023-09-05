using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Dto;
using MongoDB.Driver.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public class SqlCollection : ISqlCollection
    {
   

        public async Task<List<Dto.TableSqlDto>> ReadLocalScriptSqlAsync(string scriptFilePath)
        {
            try
            {
                string script;

                // Lee el contenido del archivo SQL
                using (StreamReader reader = new StreamReader(scriptFilePath))
                {
                    script = await reader.ReadToEndAsync();
                }
                var a= ExtractTuples(script);
                // Procesa el script SQL y extrae la información de las tablas
                List<Dto.TableSqlDto> tableInfoList = new List<Dto.TableSqlDto>();

                // Implementa la lógica para analizar el script SQL y obtener la información de las tablas.
                // Por ejemplo, puedes buscar patrones en el script que representen definiciones de tablas y columnas.

                // Ejemplo de cómo agregar información de una tabla a la lista:
                var table = new TableSqlDto
                {
                    TableName = "NombreDeLaTabla",
                    Columns = new List<string> { "Columna1", "Columna2", "Columna3" }
                    // Agrega las columnas según corresponda
                };
                tableInfoList.Add(table);

                Console.WriteLine("Script SQL local ejecutado con éxito de forma asíncrona.");

                // Devuelve la lista de información de tablas en formato JSON
                return tableInfoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ejecutar el script SQL local de forma asíncrona: " + ex.Message);
                return null; // Maneja el error según sea necesario
            }
        }

        public Dictionary<string, List<string>> ExtractTuples(string sqlScript)
        {
            var tableColumns = new Dictionary<string, List<string>>();

            // Encuentra todas las definiciones de tablas en el script SQL
            var tableMatches = Regex.Matches(sqlScript, @"CREATE TABLE ([^\(]+)\((.*?)\);", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            foreach (Match tableMatch in tableMatches)
            {
                string tableName = tableMatch.Groups[1].Value.Trim();
                string columns = tableMatch.Groups[2].Value;

                // Divide las columnas por comas y limpia los espacios en blanco
                var columnList = new List<string>(
                    columns.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(column => column.Trim())
                );

                // Agrega la tabla y sus columnas al diccionario
                tableColumns[tableName] = columnList;
            }

            return tableColumns;
        }


        public async Task<List<Dto.TableSqlDto>> ReadRemoteSqlEndpointAsync( SqlConnectionRepository _dbContext)
        {
            try
            {
                // Construye la consulta SQL para obtener información de todas las tablas
                string sqlQuery = @"
                SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

                // Ejecuta la consulta utilizando SqlConnectionRepository
                var result = await _dbContext.ExecuteQueryAsync(sqlQuery);
                string jsonData = JsonConvert.SerializeObject(result);
                // Procesa los resultados para obtener la estructura de las tablas y sus columnas
                List <TableSqlDto> tableInfos = new List<TableSqlDto>();
                var currentTableInfo = new TableSqlDto();
                string currentTableName = null;

                // Agrega la última tabla a la lista
                if (currentTableName != null)
                {
                    tableInfos.Add(currentTableInfo);
                }

                return tableInfos;
            }
            catch (Exception ex)
            {
                // Maneja las excepciones según tus necesidades
                throw ex;
            }
        }



    }
}
