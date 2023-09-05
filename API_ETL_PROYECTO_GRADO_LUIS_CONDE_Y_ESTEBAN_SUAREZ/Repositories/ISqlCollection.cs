using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Dto;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public interface ISqlCollection
    {
        Task<List<TableSqlDto>> ReadLocalScriptSqlAsync(string scriptFilePath);
        Task<List<Dto.TableSqlDto>> ReadRemoteSqlEndpointAsync( SqlConnectionRepository dbContext);


    }
}
