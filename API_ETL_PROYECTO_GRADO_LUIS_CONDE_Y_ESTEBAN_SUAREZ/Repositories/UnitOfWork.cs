using API_LUIS_CONDE_PERSONALSOFT.Repositories;
using MongoDB.Driver;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        internal ICsvCollection _csvCollection;
        internal ISqlCollection _sqlCollection;
        internal  IXslxCollection _xslxCollection;
        internal IDataWarehouseRepository _dataWarehouseRepository;

        //private readonly IMongoDatabase _database;



        //public UnitOfWork(MongoConnectionRepository mongoConnectionRepository)
        //{
        //    var clienteMongo = mongoConnectionRepository.clienteMongo;
        //    var db = mongoConnectionRepository.db;

        //    Console.WriteLine("Conexión exitosa a la base de datos MongoDB");
        //}


        public ICsvCollection CsvCollection { get; set; } = new CsvCollection();
        public ISqlCollection SqlCollection { get; } = new SqlCollection();
        public IXslxCollection XslxCollection { get; set; } = new XslxCollection();

        public IDataWarehouseRepository DataWarehouseRepository { get; set; } = new DataWarehouseRepository();


    }
}
