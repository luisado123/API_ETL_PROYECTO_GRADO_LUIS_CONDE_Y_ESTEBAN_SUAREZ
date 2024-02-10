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
    public class ObtenerDataSetPorIdDepartamentoServicio
    {
        private IDataSetDataFinalRepositorio _dataSetDataFinalRepositorio;
        public ObtenerDataSetPorIdDepartamentoServicio(IDataSetDataFinalRepositorio dataSetDataFinalRepositorio)
        {
            _dataSetDataFinalRepositorio = dataSetDataFinalRepositorio;
        }

        public async Task<RespuestaConsultaPorDepartamento> ObtenerPorDepartamentoAsync(string idDepartamento)
        {
            return await _dataSetDataFinalRepositorio.ObtenerPorDepartamento(idDepartamento);
        }
    }
}
