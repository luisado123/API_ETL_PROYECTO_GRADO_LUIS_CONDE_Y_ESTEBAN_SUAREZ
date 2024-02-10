using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.servicios.ArchivoXlsx;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Servicios.ObjetoFinalDataSet;

namespace UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.Carga
{
    public class CargarObjetoDataSetManejador : IRequestHandler<CargarObjetoDataSetComando, IActionResult>
    {
        private GuardarOActualizarObjetoDataSetServicio _guardarOActualizarObjetoDataSetServicio;
        public CargarObjetoDataSetManejador(GuardarOActualizarObjetoDataSetServicio guardarOActualizarObjetoDataSetServicio)
        {
            _guardarOActualizarObjetoDataSetServicio = guardarOActualizarObjetoDataSetServicio;
        }
        public async Task<IActionResult> Handle(CargarObjetoDataSetComando request, CancellationToken cancellationToken)
        {
            return await _guardarOActualizarObjetoDataSetServicio.GuardarOActualizarDataSetAsync(request.objetoDataSet);
        }
    }
}
