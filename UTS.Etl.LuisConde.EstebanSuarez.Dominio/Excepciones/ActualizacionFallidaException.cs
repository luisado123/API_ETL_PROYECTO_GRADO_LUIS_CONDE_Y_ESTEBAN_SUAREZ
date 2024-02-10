using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Excepciones
{
    public class ActualizacionFallidaException : Exception
    {
        public ActualizacionFallidaException(string message) : base(message)
        {
        }
    }
}
