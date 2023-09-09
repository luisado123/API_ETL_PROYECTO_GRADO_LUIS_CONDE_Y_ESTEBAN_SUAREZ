using API_LUIS_CONDE_PERSONALSOFT.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Repositories
{
    public class DataWarehouseRepository : IDataWarehouseRepository
    {
        internal MongoConnectionRepository _repository = new MongoConnectionRepository();

        private IMongoCollection<BsonDocument> Collection;

        public DataWarehouseRepository()
        {
            // No inicialices la colección aquí
        }

        // Agrega un método privado para inicializar la colección cuando sea necesario
        private void InitializeCollection()
        {
            if (Collection == null)
            {
                Collection = _repository.db.GetCollection<BsonDocument>("DW_RAW_DATA_COLLECTION");
            }
        }

        public async Task LoadRawDataIntoStagingArea(List<string> rawJsonDataList)
        {
            InitializeCollection(); // Inicializa la colección si aún no se ha hecho

            List<BsonDocument> bsonData = new List<BsonDocument>();

            foreach (string json in rawJsonDataList)
            {
                BsonDocument rawDataBson = BsonDocument.Parse(json);
                bsonData.Add(rawDataBson);
            }

            await Collection.InsertManyAsync(bsonData);
        }

        public async Task LoadSingleRawDataIntoStagingArea(string transformedDataJson)
        {
            InitializeCollection(); // Inicializa la colección si aún no se ha hecho

            BsonDocument rawSingleDataBson = BsonDocument.Parse(transformedDataJson);

            if (!rawSingleDataBson.Contains("_id"))
            {
                rawSingleDataBson["_id"] = ObjectId.GenerateNewId();
            }
            // Implementa aquí la lógica para guardar el dato transformado
            await Collection.InsertOneAsync(rawSingleDataBson);
        }
    }

}
