using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GuardarRawDataAsync(List<string> datos)
        {
            if (datos.Count == 1)
            {
                return await _dataLakeRepositorio.GuardarUno(datos.First());
            }
            else
            {
                return await _dataLakeRepositorio.GuardarVarios(datos);
            }
        }

    }
}
