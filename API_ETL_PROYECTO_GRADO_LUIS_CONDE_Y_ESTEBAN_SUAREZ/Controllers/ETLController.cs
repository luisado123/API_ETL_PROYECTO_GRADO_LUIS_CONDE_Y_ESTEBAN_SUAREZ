﻿using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Dto;
using API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Utils.HubSignalR;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.Analysis;
using UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.ArchivoXlsx.Comandos.Crear;
using UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.Carga;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;

namespace API_ETL_PROYECTO_GRADO_LUIS_CONDE_Y_ESTEBAN_SUAREZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ETLController : Controller
    {

        private readonly IHubContext<DataWarehouseHubClass> _hubContext;
        readonly IMediator _mediator;
        public ETLController(IHubContext<DataWarehouseHubClass> hubContext,IMediator mediator)
        {
            _hubContext = hubContext;
            _mediator = mediator;
        }

        [HttpPost("ProbarExtraccionExcel")]
        public async Task<RespuestaEtl> ExtraerYTransformarXlsx([FromForm] CrearObjetoDataLakeComando solicitud) => await _mediator.Send(solicitud);

        [HttpPost("CargarObjetos")]
        public async Task<IActionResult> GuardarObjetosEtl([FromForm] CargarObjetosDataLakeComando solicitud) => await _mediator.Send(solicitud);


        [HttpGet("Hola")]
        public IActionResult ObtenerSaludo()
        {
            return Ok("Hola");
        }


    }
}