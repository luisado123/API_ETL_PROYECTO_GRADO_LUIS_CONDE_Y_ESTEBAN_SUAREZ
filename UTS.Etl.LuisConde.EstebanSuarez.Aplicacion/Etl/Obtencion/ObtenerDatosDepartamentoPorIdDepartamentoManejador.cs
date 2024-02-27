using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.Carga;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.servicios.ArchivoXlsx;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Servicios.ObjetoDataLake;

namespace UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.Obtencion
{
    public class ObtenerDatosDepartamentoPorIdDepartamentoManejador : IRequestHandler<ObtenerDatosDepartamentoPorIdDepartamentoConsulta, List<RespuestaConsultaPorDepartamento>>
    {
        private ObtenerDatosPorDepartamentoServicio _obtenerDatosPorDepartamentoServicio;
        public ObtenerDatosDepartamentoPorIdDepartamentoManejador(ObtenerDatosPorDepartamentoServicio obtenerDatosPorDepartamentoServicio)
        {
            _obtenerDatosPorDepartamentoServicio = obtenerDatosPorDepartamentoServicio;
        }
        public async Task<List<RespuestaConsultaPorDepartamento>> Handle(ObtenerDatosDepartamentoPorIdDepartamentoConsulta request, CancellationToken cancellationToken)
        {
            return await _obtenerDatosPorDepartamentoServicio.ObtenerPorDepartamentoAsync(request.codigoDepartamento);
        }
    }
}
