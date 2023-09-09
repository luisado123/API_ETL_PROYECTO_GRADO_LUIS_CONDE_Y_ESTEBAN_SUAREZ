using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Dto;
using System.Data.SqlClient;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public interface ISqlCollection
    {
        SqlConnection CreateSqlConnection(string stringConnection);
        Task<string> ReadRemoteSqlEndpointAsync( string connectionString);

    }
}
