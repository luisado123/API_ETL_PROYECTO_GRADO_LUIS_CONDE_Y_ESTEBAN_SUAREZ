using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Puertos;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Excepciones;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Constantes;
using UTS.Etl.LuisConde.EstebanSuarez.Infraestructura.Extensiones;

namespace UTS.Etl.LuisConde.EstebanSuarez.Infraestructura.Servicios
{
    public class ProcesarArchivoServicio : IProcesarArchivoServicio
    {
        private List<Dictionary<string, object>> _datosMetaData;

        public ProcesarArchivoServicio()
        {
            _datosMetaData = new List<Dictionary<string, object>>();
        }
        public async Task<List<Dictionary<string, object>>> ProcesarAsync(IFormFile archivo)
        {
            var elementosArchivo = Path.GetExtension(archivo.FileName) switch
            {
                ".xlsx" or ".xls" => await ProcesarArchivoExcel(archivo),
                ".csv" => await ProcesarArchivoPlano(archivo),
                _ => throw new ExtensionArchivoNoValidaException(MensajesExcepciones.ExtensionInvalida)
            };
            return elementosArchivo;
        }

        private async Task<List<Dictionary<string, object>>> ProcesarArchivoExcel(IFormFile archivo)
        {
            _datosMetaData = await archivo.LeerArchivoExcel();
            return _datosMetaData;
        }

        private async Task<List<Dictionary<string, object>>> ProcesarArchivoPlano(IFormFile archivo)
        {
            _datosMetaData = await archivo.LeerArchivoCsv();
            return _datosMetaData;
        }
    }
}
