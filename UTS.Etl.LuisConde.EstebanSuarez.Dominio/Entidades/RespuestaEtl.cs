using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades
{
    public class RespuestaEtl
    {
        public string Departamento { get; set; } = default!;
        public string ObjetoCreado { get; set; } = default!;

        public RespuestaEtl(string departamento,string objetoCreado) {
            Departamento = departamento;
            ObjetoCreado= objetoCreado;
        }
    }
}
