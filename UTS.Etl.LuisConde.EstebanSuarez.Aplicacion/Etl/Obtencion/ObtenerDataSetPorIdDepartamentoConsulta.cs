using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;

namespace UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.Obtencion
{
    public record ObtenerDataSetPorIdDepartamentoConsulta(
    [Required] string codigoDepartamento
) : IRequest<RespuestaConsultaPorDepartamento>;

}
