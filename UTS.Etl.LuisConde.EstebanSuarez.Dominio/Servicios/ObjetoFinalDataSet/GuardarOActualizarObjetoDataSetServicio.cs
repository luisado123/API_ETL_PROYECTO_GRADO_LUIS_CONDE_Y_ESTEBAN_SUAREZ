using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Puertos;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.servicios;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Servicios.ObjetoFinalDataSet
{
    [DominioService]
    public class GuardarOActualizarObjetoDataSetServicio
    {
        IDataSetDataFinalRepositorio _dataSetDataFinalRepositorio;
        public GuardarOActualizarObjetoDataSetServicio(IDataSetDataFinalRepositorio dataSetDataFinalRepositorio)
        {
            _dataSetDataFinalRepositorio = dataSetDataFinalRepositorio;
        }


        public async Task<IActionResult> GuardarOActualizarDataSetAsync(Entidades.ObjetoDataLake objetoDataSet)
        {
            var datosExistentes = await _dataSetDataFinalRepositorio.ObtenerPorDepartamento(objetoDataSet.DepartamentoOrigen);
            string objetoDataSetJson = objetoDataSet.ConvertirAJson();
            if (datosExistentes == null)
            {
             return  await _dataSetDataFinalRepositorio.GuardarUno(objetoDataSetJson);
            }
            else
            {
                return await _dataSetDataFinalRepositorio.ActualizarPorDepartamento(objetoDataSet.DepartamentoOrigen, objetoDataSetJson);
            }
        }
    }
}
