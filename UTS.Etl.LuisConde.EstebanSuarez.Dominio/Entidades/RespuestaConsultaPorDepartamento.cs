using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades
{
    public class RespuestaConsultaPorDepartamento
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public List<Dictionary<string, object>> Datos { get; set; }
        public string DepartamentoOrigen { get; set; }

        public RespuestaConsultaPorDepartamento(string departamento, List<Dictionary<string, object>> datos)
        {
            Datos = datos;
            DepartamentoOrigen = departamento;
        }
    }
}
