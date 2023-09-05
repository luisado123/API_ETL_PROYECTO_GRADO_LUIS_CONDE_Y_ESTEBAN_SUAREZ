namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Dto
{
    public class TableSqlDto
    {
        public string TableName { get; set; }
        public List<string> Columns { get; set; }
        public List<object[]> Rows { get; set; }
    }
}
