using MongoDB.Bson;
using MongoDB.Driver;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Puertos;

namespace UTS.Etl.LuisConde.EstebanSuarez.Infraestructura.Adaptadores.MongoDb
{
    public class MongoConexionRepositorio : IMongoConexionRepositorio
    {
        public MongoClient ClienteMongo { get; private set; }
        public IMongoDatabase Db { get; private set; }

        public MongoConexionRepositorio(string connectionString, string databaseName)
        {
            try
            {
                ClienteMongo = new MongoClient(connectionString);
                Db = ClienteMongo.GetDatabase(databaseName);

                Console.WriteLine("Conexión exitosa a la base de datos");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la conexión a la base de datos: " + ex.Message);
                // Aquí puedes realizar alguna acción adicional en caso de que la conexión falle.
                throw;
            }
        }


        public void Dispose()
        {
            // Cierre de conexiones, liberación de recursos, etc.
            ClienteMongo = null;
            Db = null;
        }

        public IMongoCollection<BsonDocument> InicializarColeccion(string collectionName)
        {
            return Db.GetCollection<BsonDocument>(collectionName);
        }
    }

}
