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
            string objetoDataLakeJson = System.Text.Json.JsonSerializer.Serialize(objetoDataLake);
            return await _dataLakeRepositorio.GuardarUno(objetoDataLakeJson);        
        }

    }
}
