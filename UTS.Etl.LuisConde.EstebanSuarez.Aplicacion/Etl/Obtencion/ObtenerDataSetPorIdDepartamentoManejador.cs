using MediatR;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Servicios.ObjetoFinalDataSet;

namespace UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.Obtencion
{
    public class ObtenerDataSetPorIdDepartamentoManejador : IRequestHandler<ObtenerDataSetPorIdDepartamentoConsulta, RespuestaConsultaPorDepartamento>
    {
        private  ObtenerDataSetPorIdDepartamentoServicio _obtenerDataSetPorIdDepartamentoServicio;
        public ObtenerDataSetPorIdDepartamentoManejador(ObtenerDataSetPorIdDepartamentoServicio obtenerDataSetPorIdDepartamentoServicio)
        {
            _obtenerDataSetPorIdDepartamentoServicio = obtenerDataSetPorIdDepartamentoServicio ;
        }
        public async Task<RespuestaConsultaPorDepartamento> Handle(ObtenerDataSetPorIdDepartamentoConsulta request, CancellationToken cancellationToken)
        {
            return await _obtenerDataSetPorIdDepartamentoServicio.ObtenerPorDepartamentoAsync(request.codigoDepartamento);
        }
    }
}
