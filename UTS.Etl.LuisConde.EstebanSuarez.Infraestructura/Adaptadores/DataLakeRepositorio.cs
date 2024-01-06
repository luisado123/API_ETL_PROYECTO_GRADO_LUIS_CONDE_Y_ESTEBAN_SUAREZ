using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Constantes;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;
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
        public async Task<IActionResult> GuardarUno(string objetoRawData)
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
                return new OkObjectResult(new { Message = MensajesExitosos.CargaEnDataWareHouseExitosa });
            }
            catch (Exception ex)
            {
                throw new GuardadoEnDataLakeFallidoExcepcion($"{MensajesExcepciones.ErrorAlGuardar}: {ex.Message}");
            }
        }

        public async Task<IActionResult> GuardarVarios(List<string> listaObjetosRawData)
        {
            try
            {
                var coleccion = _mongoConexionRepositorio.InicializarColeccion(NombreColeccion);
                var listadoRawDataFormatoBson = listaObjetosRawData.Select(objetoRawData => BsonDocument.Parse(objetoRawData)).ToList();
             
                foreach (var objetoFormatoBson in listadoRawDataFormatoBson)
                {
                    if (!objetoFormatoBson.Contains("_id"))
                    {
                        objetoFormatoBson["_id"] = ObjectId.GenerateNewId();
                    }
                }

                await coleccion.InsertManyAsync(listadoRawDataFormatoBson);
                return new OkObjectResult(new { Message = MensajesExitosos.CargaEnDataWareHouseExitosa });
            }
            catch (Exception ex)
            {
                throw new GuardadoEnDataLakeFallidoExcepcion($"{MensajesExcepciones.ErrorAlGuardar}: {ex.Message}");
            }
        }
    }
}
