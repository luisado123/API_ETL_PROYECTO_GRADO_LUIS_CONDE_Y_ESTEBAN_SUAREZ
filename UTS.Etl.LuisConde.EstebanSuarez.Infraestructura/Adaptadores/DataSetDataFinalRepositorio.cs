using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Constantes;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Excepciones;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Puertos;

namespace UTS.Etl.LuisConde.EstebanSuarez.Infraestructura.Adaptadores
{
    public class DataSetDataFinalRepositorio : IDataSetDataFinalRepositorio
    {

        private readonly IMongoConexionRepositorio _mongoConexionRepositorio;

        private readonly string NombreColeccion = "DW_FINAL_DATA_SET_COLLECTION";
        public DataSetDataFinalRepositorio(IMongoConexionRepositorio mongoConexionRepositorio)
        {
            _mongoConexionRepositorio = mongoConexionRepositorio;
        }
        public async Task<IActionResult> ActualizarPorDepartamento(string nombreDepartamento, string objetoRawData)
        {
            try
            {
                var coleccion = _mongoConexionRepositorio.InicializarColeccion(NombreColeccion);
                BsonDocument filtro = new BsonDocument("DepartamentoOrigen", nombreDepartamento);
                BsonDocument rawSingleDataBson = BsonDocument.Parse(objetoRawData);

                var replaceResult = await coleccion.ReplaceOneAsync(filtro, rawSingleDataBson);

                if (replaceResult.ModifiedCount == 0)
                {
                    throw new ActualizacionFallidaException("No se encontraron documentos para actualizar.");
                }

                return new OkObjectResult(new { Message = MensajesExitosos.ActualizacionExitosa });
            }
            catch (Exception ex)
            {
                throw new ActualizacionFallidaException($"{MensajesExcepciones.ErrorAlActualizar}: {ex.Message}");
            }
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

        public async Task<RespuestaConsultaPorDepartamento> ObtenerPorDepartamento(string nombreDepartamento)
        {
            try
            {
                var coleccion = _mongoConexionRepositorio.InicializarColeccion(NombreColeccion);

                var builder = Builders<BsonDocument>.Filter;
                var filtro = builder.Eq("DepartamentoOrigen", nombreDepartamento);

                var resultado = await coleccion.Find(filtro).FirstOrDefaultAsync();

                if (resultado == null)
                    throw new ObtensionPorDepartamentoException(MensajesExcepciones.NoHayRegistros);

                return BsonSerializer.Deserialize<RespuestaConsultaPorDepartamento>(resultado.ToJson());
            }
            catch (Exception ex)
            {
                throw new ObtensionPorDepartamentoException(ex.Message);
            }
        }
    }
}
