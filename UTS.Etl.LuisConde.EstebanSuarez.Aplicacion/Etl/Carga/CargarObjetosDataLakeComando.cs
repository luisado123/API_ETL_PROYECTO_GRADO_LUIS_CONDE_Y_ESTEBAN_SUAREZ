﻿using MediatR;
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
    public record CargarObjetosDataLakeComando : IRequest<IActionResult>
    {
        [FromBody, Required]
        public List<string>? ListaJsons { get; init; }

    }

}
