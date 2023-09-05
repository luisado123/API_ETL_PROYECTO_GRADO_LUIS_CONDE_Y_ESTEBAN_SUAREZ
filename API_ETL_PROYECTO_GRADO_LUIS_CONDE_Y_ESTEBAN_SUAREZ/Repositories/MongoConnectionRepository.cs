using MongoDB.Bson;
using MongoDB.Driver;

namespace API_LUIS_CONDE_PERSONALSOFT.Repositories
{
    public class MongoConnectionRepository
    {

        public MongoClient clienteMongo;


        public IMongoDatabase db;

        public MongoConnectionRepository()
        {

            try
            {
                clienteMongo = new MongoClient("mongodb+srv://luisado:Condeeiemlda123@cluster0.abcj6sj.mongodb.net/?retryWrites=true&w=majority");
                db = clienteMongo.GetDatabase("Luisconde");

                // Resto del código para trabajar con la base de datos

                Console.WriteLine("Conexión exitosa a la base de datos");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la conexión a la base de datos: " + ex.Message);
                // Aquí puedes realizar alguna acción adicional en caso de que la conexión falle, como mostrar un mensaje de error al usuario o realizar un registro de errores.
            }

        }
    }
}
