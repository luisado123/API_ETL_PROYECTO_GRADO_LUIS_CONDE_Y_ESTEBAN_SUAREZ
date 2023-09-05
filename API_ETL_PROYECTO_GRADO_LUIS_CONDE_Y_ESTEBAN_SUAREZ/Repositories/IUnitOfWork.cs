namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public interface IUnitOfWork
    {
        ICsvCollection CsvCollection { get; set; }
        ISqlCollection SqlCollection { get; }
        IXslxCollection XslxCollection { get; set; }

        //llamados a las demas interfaces en caso de tener mas formatos
        void SaveUnprocessedDataToWarehousee();
    }
}
