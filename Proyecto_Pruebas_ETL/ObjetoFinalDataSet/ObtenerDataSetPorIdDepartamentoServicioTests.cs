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
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Servicios.ObjetoDataLake;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Servicios.ObjetoFinalDataSet;
using Xunit;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Tests.ObjetoFinalDataSet
{
    public class ObtenerDataSetPorIdDepartamentoServicioTests
    {
        private readonly IDataSetDataFinalRepositorio _dataSetDataFinalRepositorioMock;
        public ObtenerDataSetPorIdDepartamentoServicioTests()
        {
            _dataSetDataFinalRepositorioMock = Substitute.For<IDataSetDataFinalRepositorio>();
        }

        [Fact]
        public async Task ObtenerPorDepartamentoAsync_DeberiaDevolverListaDeRespuestaConsultaPorDepartamento()
        {
            // Arrange
            var idDepartamento = "1";
            var servicio = new ObtenerDataSetPorIdDepartamentoServicio(_dataSetDataFinalRepositorioMock);

            var departamento = "test";
            var datos = new List<Dictionary<string, object>>
        {
          new Dictionary<string, object>
        {
        {"test", "test"}
        }
         };

            var listaRespuestas = new RespuestaConsultaPorDepartamento(departamento,datos);

            _dataSetDataFinalRepositorioMock.ObtenerPorDepartamento(idDepartamento).Returns(Task.FromResult(listaRespuestas));

            // Act
            var resultado = await servicio.ObtenerPorDepartamentoAsync(idDepartamento);

            // Assert
            Assert.IsType<RespuestaConsultaPorDepartamento>(resultado);
        }

        [Fact]
        public async Task ObtenerPorDepartamentoAsync_DeberiaLanzarExcepcionCuandoNoHayRegistros()
        {
            // Arrange
            var idDepartamento = "1";
            var servicio = new ObtenerDataSetPorIdDepartamentoServicio(_dataSetDataFinalRepositorioMock);
            var listaRespuestas = new List<RespuestaConsultaPorDepartamento>();

            _dataSetDataFinalRepositorioMock.ObtenerPorDepartamento(Arg.Any<string>()).Throws(new ObtensionPorDepartamentoException(MensajesExcepciones.NoHayRegistros));

            // Act y Assert
            await Assert.ThrowsAsync<ObtensionPorDepartamentoException>(() => servicio.ObtenerPorDepartamentoAsync(idDepartamento));
        }

    }
}
