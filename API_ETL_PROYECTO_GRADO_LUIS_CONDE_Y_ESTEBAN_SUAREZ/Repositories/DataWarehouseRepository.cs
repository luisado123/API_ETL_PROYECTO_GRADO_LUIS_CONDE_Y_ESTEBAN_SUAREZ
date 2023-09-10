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

        public async Task<bool> LoadRawDataIntoStagingArea(List<string> rawJsonDataList)
        {
            InitializeCollection(); // Inicializa la colección si aún no se ha hecho

            List<BsonDocument> bsonData = new List<BsonDocument>();

            foreach (string json in rawJsonDataList)
            {
                try
                {
                    BsonDocument rawDataBson = BsonDocument.Parse(json);
                    bsonData.Add(rawDataBson);
                }
                catch (Exception ex)
                {
                    // Puedes registrar el error si lo deseas
                    Console.WriteLine($"Error al parsear el JSON: {ex.Message}");
                    return false; // Retorna false en caso de error
                }
            }

            try
            {
                await Collection.InsertManyAsync(bsonData);
                return true; // Retorna true si se guardó con éxito
            }
            catch (Exception ex)
            {
                // Puedes registrar el error si lo deseas
                Console.WriteLine($"Error al guardar los datos: {ex.Message}");
                return false; // Retorna false en caso de error
            }
        }


        public async Task<bool> LoadSingleRawDataIntoStagingArea(string transformedDataJson)
        {
            try
            {
                InitializeCollection(); // Inicializa la colección si aún no se ha hecho

                BsonDocument rawSingleDataBson = BsonDocument.Parse(transformedDataJson);

                if (!rawSingleDataBson.Contains("_id"))
                {
                    rawSingleDataBson["_id"] = ObjectId.GenerateNewId();
                }

                // Implementa aquí la lógica para guardar el dato transformado
                await Collection.InsertOneAsync(rawSingleDataBson);

                return true; // Indica que la operación fue exitosa
            }
            catch (Exception ex)
            {
                // Puedes registrar el error si lo deseas
                Console.WriteLine($"Error al guardar el dato transformado: {ex.Message}");
                return false; // Indica que la operación falló
            }
        }

    }

}
