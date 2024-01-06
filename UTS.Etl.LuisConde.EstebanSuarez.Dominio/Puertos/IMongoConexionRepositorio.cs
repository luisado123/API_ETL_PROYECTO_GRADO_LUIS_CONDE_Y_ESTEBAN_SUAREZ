using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Puertos
{
    public interface IMongoConexionRepositorio : IDisposable
    {
        MongoClient ClienteMongo { get; }
        IMongoDatabase Db { get; }
        IMongoCollection<BsonDocument> InicializarColeccion(string collectionName);

    }
}
