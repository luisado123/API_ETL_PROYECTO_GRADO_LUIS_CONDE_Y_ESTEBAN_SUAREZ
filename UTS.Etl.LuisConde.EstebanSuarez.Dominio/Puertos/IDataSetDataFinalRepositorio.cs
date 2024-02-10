using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Puertos
{
    public interface IDataSetDataFinalRepositorio
    {
        Task<IActionResult> GuardarUno(string objetoRawData);

        Task<IActionResult> ActualizarPorDepartamento(string objetoRawData,string objectRawData);

        Task <RespuestaConsultaPorDepartamento> ObtenerPorDepartamento(string objetoRawData);

    }
}
