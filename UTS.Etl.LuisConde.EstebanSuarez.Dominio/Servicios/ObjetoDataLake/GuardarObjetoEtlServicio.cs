using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Puertos;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.servicios.ArchivoXlsx
{
    [DominioService]
    public class GuardarObjetoEtlServicio
    {
        private IDataLakeRepositorio _dataLakeRepositorio;
        public GuardarObjetoEtlServicio(IDataLakeRepositorio dataLakeRepositorio)
        {
            _dataLakeRepositorio = dataLakeRepositorio;
        }
        public async Task<IActionResult> GuardarRawDataAsync(ObjetoDataLake objetoDataLake)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                Converters = { new Newtonsoft.Json.Converters.StringEnumConverter() }, // Esto es opcional y solo si necesitas serializar enumeraciones como cadenas
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, // Ignorar valores predeterminados
                NullValueHandling = NullValueHandling.Ignore // Ignorar valores nulos
            };

            string objetoDataLakeJson = System.Text.Json.JsonSerializer.Serialize(objetoDataLake);
            // Utiliza objetoDataLakeJson como parámetro donde sea necesario.

            return await _dataLakeRepositorio.GuardarUno(objetoDataLakeJson);

          
        }

    }
}
