using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades
{
    public class ObjetoDataLake
    {
        public string Id { get; set; } = default!;
        public List<Dictionary<string, object>> Datos { get; set; }
        public string DepartamentoOrigen { get; set; }

        public ObjetoDataLake(List<Dictionary<string, object>> datos, string departmento)
        {
            Datos = datos;
            DepartamentoOrigen = departmento;
        }

        public ObjetoDataLake()
        {
            // Constructor sin parámetros requerido para la deserialización.
        }

        public void AsignarDepartamento(string departamento)
        {
            DepartamentoOrigen = departamento;
        }

        public string ConvertirAJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public RespuestaEtl CrearRespuestaEtl(string departamento,string objetoCreado)
        {
            return new RespuestaEtl(departamento, objetoCreado);

        }

        public void AsignarDatosAMetaData(List<Dictionary<string, object>> datosObjetoDataLake)
        {
            if (datosObjetoDataLake.Count > 0)
            {
                Datos = datosObjetoDataLake;
            }
            else
            {
                Datos = new List<Dictionary<string, object>>();
            }
        }
    }

}
