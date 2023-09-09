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
using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Controllers;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public class SqlCollection : ISqlCollection
    {

        public SqlConnection CreateSqlConnection(string _connectionString)
        {
            return new SqlConnection(_connectionString);
        }


        public async  Task<string> ReadRemoteSqlEndpointAsync(string stringConnection)
        {
            try
            {
                string sqlQuery = @"
                SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
                var result = await this.GetTableNamesAsync(sqlQuery,stringConnection);
                string SQLDataToJSON = await this.CreateJsonFromTablesAsync(result,stringConnection);
                // Procesa los resultados para obtener la estructura de las tablas y sus columnas
             

                return SQLDataToJSON;

            }
            catch (Exception ex)
            {
                // Maneja las excepciones según tus necesidades
                throw ex;
            }
        }



        public async Task<List<string>> GetTableNamesAsync(string sqlQuery, string connectionString)
        {
            List<string> tableNames = new List<string>();

            try
            {
                using (SqlConnection connection = CreateSqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand tableCommand = new SqlCommand(sqlQuery, connection))
                    using (SqlDataReader tableReader = await tableCommand.ExecuteReaderAsync())
                    {
                        while (tableReader.Read()) // Para cada tabla
                        {
                            string tableNameWithSchema = $"{tableReader.GetString(0)}.{tableReader.GetString(1)}";
                            tableNames.Add(tableNameWithSchema);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine($"Error al obtener nombres de tablas: {ex.Message}");
                throw;
            }

            return tableNames;
        }



        public async Task<string> CreateJsonFromTablesAsync(List<string> tableNames, string connectionString)
        {
            // Crea y llena el JSON utilizando los nombres de las tablas
            JObject json = new JObject();

            try
            {
                using (SqlConnection connection = CreateSqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    foreach (string tableNameWithSchema in tableNames)
                    {
                        // Analizar el esquema y el nombre de la tabla
                        string[] parts = tableNameWithSchema.Split('.'); // Separar por el punto
                        string schema = "dbo"; // Establecer un valor predeterminado en caso de que no haya un esquema específico
                        string tableName = tableNameWithSchema; // El nombre de la tabla por defecto

                        if (parts.Length > 1)
                        {
                            schema = parts[0]; // Tomar el primer elemento como esquema
                            tableName = parts[1]; // Tomar el segundo elemento como nombre de la tabla
                        }

                        // Consulta para obtener todos los registros de la tabla actual con el esquema especificado
                        string dataQuery = $"SELECT * FROM [{schema}].[{tableName}]";

                        using (SqlCommand dataCommand = new SqlCommand(dataQuery, connection))
                        using (SqlDataReader dataReader = await dataCommand.ExecuteReaderAsync())
                        {
                            JArray tableData = new JArray();

                            while (dataReader.Read()) // Para cada registro en la tabla
                            {
                                JObject rowData = new JObject();

                                for (int i = 0; i < dataReader.FieldCount; i++)
                                {
                                    string columnName = dataReader.GetName(i);
                                    object columnValue = dataReader.GetValue(i);

                                    rowData[columnName] = JToken.FromObject(columnValue);
                                }

                                tableData.Add(rowData);
                            }

                            json[tableName] = tableData;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine($"Error al crear JSON desde tablas: {ex.Message}");
                throw;
            }

            JObject finalJson = new JObject();
            finalJson["data"] = json;

            return finalJson.ToString();
        }



    }
}
