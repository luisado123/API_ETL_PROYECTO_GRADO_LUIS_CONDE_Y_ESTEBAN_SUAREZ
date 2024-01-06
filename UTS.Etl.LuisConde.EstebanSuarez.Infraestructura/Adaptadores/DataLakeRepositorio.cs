using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Excepciones;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Puertos;

namespace UTS.Etl.LuisConde.EstebanSuarez.Infraestructura.Adaptadores
{
    public class DataLakeRepositorio : IDataLakeRepositorio
    {
        private readonly IMongoConexionRepositorio _mongoConexionRepositorio;

        private readonly string NombreColeccion = "DW_RAW_DATA_COLLECTION";
        public DataLakeRepositorio(IMongoConexionRepositorio mongoConexionRepositorio)
        {
            _mongoConexionRepositorio = mongoConexionRepositorio;
        }
        public async Task<bool> GuardarUno(string objetoRawData)
        {
            try
            {
               var coleccion = _mongoConexionRepositorio.InicializarColeccion(NombreColeccion);
                BsonDocument rawSingleDataBson = BsonDocument.Parse(objetoRawData);
                if (!rawSingleDataBson.Contains("_id"))
                {
                    rawSingleDataBson["_id"] = ObjectId.GenerateNewId();
                }
                await coleccion.InsertOneAsync(rawSingleDataBson);
                return true; // Indica que la operación fue exitosa
            }
            catch (Exception ex)
            {
                throw new GuardadoEnDataLakeFallidoExcepcion($"Error al guardar el dato transformado: {ex.Message}");
            }
        }

        public Task<bool> GuardarVarios(IEnumerable<string> listaRawData)
        {
            throw new NotImplementedException();
        }
    }
}
