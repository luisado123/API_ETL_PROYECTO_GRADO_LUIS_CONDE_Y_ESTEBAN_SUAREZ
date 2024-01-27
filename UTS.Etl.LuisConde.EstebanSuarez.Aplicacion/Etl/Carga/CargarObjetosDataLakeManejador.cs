using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.servicios.ArchivoXlsx;

namespace UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.Carga
{
    public class CargarObjetosDataLakeManejador : IRequestHandler<CargarObjetosDataLakeComando, IActionResult>
    {
        private GuardarObjetoEtlServicio _guardarObjetoEtlServicio;
        public CargarObjetosDataLakeManejador(GuardarObjetoEtlServicio guardarObjetoEtlServicio)
        {
            _guardarObjetoEtlServicio = guardarObjetoEtlServicio;
        }
        public async Task<IActionResult> Handle(CargarObjetosDataLakeComando request, CancellationToken cancellationToken)
        {
            return await _guardarObjetoEtlServicio.GuardarRawDataAsync(request.objetoDataLake);
        }
    }
}
