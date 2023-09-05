using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Dto;
using System.Data;
using System.Data.SqlClient;
namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public class SqlConnectionRepository
    {
        private readonly string _connectionString;

        public SqlConnectionRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }


        public async Task<List<Dictionary<string, object>>> ExecuteQueryAsync(string sqlQuery)
        {
            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand tableCommand = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader tableReader = await tableCommand.ExecuteReaderAsync())
                        {
                            while (tableReader.Read()) // Para cada tabla
                            {
                                string tableNameWithSchema = $"{tableReader.GetString(0)}.{tableReader.GetString(1)}";

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
                                {
                                    using (SqlDataReader dataReader = await dataCommand.ExecuteReaderAsync())
                                    {
                                        List<Dictionary<string, object>> tableData = new List<Dictionary<string, object>>();

                                        while (dataReader.Read()) // Para cada registro en la tabla
                                        {
                                            Dictionary<string, object> rowData = new Dictionary<string, object>();

                                            for (int i = 0; i < dataReader.FieldCount; i++)
                                            {
                                                string columnName = dataReader.GetName(i);
                                                object columnValue = dataReader.GetValue(i);

                                                rowData[columnName] = columnValue;
                                            }

                                            tableData.Add(rowData);
                                        }

                                        // Agregar los datos de la tabla actual al resultado
                                        Dictionary<string, object> tableResult = new Dictionary<string, object>
                                {
                                    { "TableName", tableName },
                                    { "Data", tableData }
                                };

                                        dataList.Add(tableResult);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine($"Error al obtener datos: {ex.Message}");
                throw;
            }

            return dataList;
        }

    }
}
