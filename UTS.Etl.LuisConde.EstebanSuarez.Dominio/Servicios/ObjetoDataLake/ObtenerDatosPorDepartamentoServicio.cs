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

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Servicios.ObjetoDataLake
{
    [DominioService]
    public class ObtenerDatosPorDepartamentoServicio
    {
        private IDataLakeRepositorio _dataLakeRepositorio;
        public ObtenerDatosPorDepartamentoServicio(IDataLakeRepositorio dataLakeRepositorio)
        {
            _dataLakeRepositorio = dataLakeRepositorio;
        }

        public async Task<List<RespuestaConsultaPorDepartamento>> ObtenerPorDepartamentoAsync(string idDepartamento)
        {

            return await _dataLakeRepositorio.ObtenerPorCampo(idDepartamento);
        }
    }
}
