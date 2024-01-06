using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;

namespace UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Puertos
{
    public interface IProcesarArchivoServicio
    {
        public Task<List<Dictionary<string, object>>> ProcesarAsync(IFormFile archivo);
    }
}
