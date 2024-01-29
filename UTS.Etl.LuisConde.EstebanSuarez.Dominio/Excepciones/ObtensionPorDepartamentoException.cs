using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Excepciones
{
    public class ObtensionPorDepartamentoException:Exception
    {
        public ObtensionPorDepartamentoException(string message) : base(message)
        {
        }
    }
}
