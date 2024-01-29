using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Constantes
{
    public static class MensajesExcepciones
    {
        public const string ExtensionInvalida = "La extensión del archivo no es válida.";
        public const string ArchivoVacio = "El archivo se encuentra Vacio";
        public const string DepartamentoRequerido = "El departamento es un campo requerido.";
        public const string ErrorAlGuardar = "Ocurrio un error al intentar cargar el objeto en el DataWarehouse.";
        public const string NoHayRegistros = "No hay datos registrados para el departamento consultado";
    }
}
