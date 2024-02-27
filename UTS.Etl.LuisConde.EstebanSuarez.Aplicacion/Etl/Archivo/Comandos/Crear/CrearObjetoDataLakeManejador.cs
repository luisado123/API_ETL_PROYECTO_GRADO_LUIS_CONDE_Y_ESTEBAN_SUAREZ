using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Puertos;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.servicios.ArchivoXlsx;

namespace UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.ArchivoXlsx.Comandos.Crear
{
    public class CrearObjetoDataLakeManejador : IRequestHandler<CrearObjetoDataLakeComando, RespuestaEtl>
    {
        private IProcesarArchivoServicio _procesarArchivoServicio;

        public CrearObjetoDataLakeManejador(IProcesarArchivoServicio procesarArchivoServicio)
        {
            _procesarArchivoServicio = procesarArchivoServicio;
        }

        public async Task<RespuestaEtl> Handle(CrearObjetoDataLakeComando request, CancellationToken cancellationToken)
        {
            var datosExcel = await _procesarArchivoServicio.ProcesarAsync(request.Archivo);
            var metadata = new ObjetoDataLake(datosExcel, request.Departamento);
            var objetoSerializado = metadata.ConvertirAJson();
            return metadata.CrearRespuestaEtl(metadata.DepartamentoOrigen, objetoSerializado);
        }
    }
}
