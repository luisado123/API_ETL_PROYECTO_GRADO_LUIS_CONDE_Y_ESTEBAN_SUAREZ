using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Constantes;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Excepciones;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Puertos;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.servicios.ArchivoXlsx;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Servicios.ObjetoDataLake;
using Xunit;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Tests.ObjetoDataLake
{
    public class ObtenerDatosPorDepartamentoServicioTests
    {
        private readonly IDataLakeRepositorio _dataLakeRepositorioMock;
        public ObtenerDatosPorDepartamentoServicioTests()
        {
            _dataLakeRepositorioMock = Substitute.For<IDataLakeRepositorio>();
        }

        [Fact]
        public async Task ObtenerPorDepartamentoAsync_DeberiaDevolverListaDeRespuestaConsultaPorDepartamento()
        {
            // Arrange
            var idDepartamento = "1";
            var servicio = new ObtenerDatosPorDepartamentoServicio(_dataLakeRepositorioMock);
            var listaRespuestas = new List<RespuestaConsultaPorDepartamento>();

            _dataLakeRepositorioMock.ObtenerPorCampo(idDepartamento).Returns(Task.FromResult(listaRespuestas));

            // Act
            var resultado = await servicio.ObtenerPorDepartamentoAsync(idDepartamento);

            // Assert
            Assert.IsType<List<RespuestaConsultaPorDepartamento>>(resultado);
        }

        [Fact]
        public async Task ObtenerPorDepartamentoAsync_DeberiaLanzarExcepcionCuandoNoHayRegistros()
        {
            // Arrange
            var idDepartamento = "1";
            var servicio = new ObtenerDatosPorDepartamentoServicio(_dataLakeRepositorioMock);
            var listaRespuestas = new List<RespuestaConsultaPorDepartamento>();

            _dataLakeRepositorioMock.ObtenerPorCampo(Arg.Any<string>()).Throws(new ObtensionPorDepartamentoException(MensajesExcepciones.NoHayRegistros));

            // Act y Assert
            await Assert.ThrowsAsync<ObtensionPorDepartamentoException>(() => servicio.ObtenerPorDepartamentoAsync(idDepartamento));
        }



    }
}
