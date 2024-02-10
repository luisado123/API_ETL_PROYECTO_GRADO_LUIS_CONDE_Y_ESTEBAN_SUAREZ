using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;

namespace UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.Carga
{
    public record CargarObjetoDataSetComando : IRequest<IActionResult>
    {
        [FromBody, Required]
        public ObjetoDataLake? objetoDataSet { get; init; }

    }
}
