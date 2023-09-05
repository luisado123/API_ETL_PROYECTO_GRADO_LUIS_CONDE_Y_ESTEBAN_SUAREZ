namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Dto
{
    public class TableRow
    {
        public Dictionary<string, object> Values { get; set; } = new Dictionary<string, object>();

        public object this[string columnName]
        {
            get => Values.ContainsKey(columnName) ? Values[columnName] : null;
            set => Values[columnName] = value;
        }
    }
}
