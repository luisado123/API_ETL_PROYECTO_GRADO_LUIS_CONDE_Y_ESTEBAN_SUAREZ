using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Excepciones;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Puertos;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Servicios.ObjetoFinalDataSet;
using Xunit;
using FluentAssertions;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;
using DocumentFormat.OpenXml.Bibliography;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Tests.ObjetoFinalDataSet
{
    public class GuardarOActualizarObjetoDataSetServicioTests
    {
        private readonly IDataSetDataFinalRepositorio _dataSetDataFinalRepositorioMock;

        public GuardarOActualizarObjetoDataSetServicioTests()
        {
            _dataSetDataFinalRepositorioMock = Substitute.For<IDataSetDataFinalRepositorio>();
        }

        [Fact]
        public async Task GuardarOActualizarDataSetAsync_DeberiaGuardarCuandoNoExistenDatos()
        {
            // Arrange
            var servicio = new GuardarOActualizarObjetoDataSetServicio(_dataSetDataFinalRepositorioMock);
            var objetoDataSet = new Entidades.ObjetoDataLake { DepartamentoOrigen = "Departamento" };

            _dataSetDataFinalRepositorioMock.ObtenerPorDepartamento(Arg.Any<string>()).Returns(Task.FromResult<RespuestaConsultaPorDepartamento>(null));

            // Act
            var resultado = await servicio.GuardarOActualizarDataSetAsync(objetoDataSet);

            // Assert
            await _dataSetDataFinalRepositorioMock.Received(1).GuardarUno(Arg.Any<string>());
        }

        [Fact]
        public async Task GuardarOActualizarDataSetAsync_DeberiaActualizarCuandoExistenDatos()
        {
            // Arrange
            var servicio = new GuardarOActualizarObjetoDataSetServicio(_dataSetDataFinalRepositorioMock);
            var departamento = "test";
            var datos = new List<Dictionary<string, object>>
        {
          new Dictionary<string, object>
        {
        {"test", "test"}
        }
         };
            var objetoDataSet = new Entidades.ObjetoDataLake { DepartamentoOrigen = "Departamento" };
            var respuestaConsulta = new RespuestaConsultaPorDepartamento(departamento, datos);

            _dataSetDataFinalRepositorioMock.ObtenerPorDepartamento(objetoDataSet.DepartamentoOrigen).Returns(respuestaConsulta);

            // Act
            var resultado = await servicio.GuardarOActualizarDataSetAsync(objetoDataSet);

            // Assert
            await _dataSetDataFinalRepositorioMock.Received(1).ActualizarPorDepartamento(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public async Task GuardarOActualizarDataSetAsync_DeberiaLanzarExcepcionAlGuardar()
        {
            // Arrange
            var servicio = new GuardarOActualizarObjetoDataSetServicio(_dataSetDataFinalRepositorioMock);
            var objetoDataSet = new Entidades.ObjetoDataLake { DepartamentoOrigen = "Departamento" };
            _dataSetDataFinalRepositorioMock.ObtenerPorDepartamento(Arg.Any<string>()).Returns(Task.FromResult<RespuestaConsultaPorDepartamento>(null));
            _dataSetDataFinalRepositorioMock.GuardarUno(Arg.Any<string>()).Throws(new GuardadoEnDataLakeFallidoExcepcion("Error al guardar"));

            // Act y Assert
            await Assert.ThrowsAsync<GuardadoEnDataLakeFallidoExcepcion>(() => servicio.GuardarOActualizarDataSetAsync(objetoDataSet));
        }

        [Fact]
        public async Task GuardarOActualizarDataSetAsync_DeberiaLanzarExcepcionAlActualizar()
        {
            // Arrange
            var servicio = new GuardarOActualizarObjetoDataSetServicio(_dataSetDataFinalRepositorioMock);
            var departamento = "test";
            var datos = new List<Dictionary<string, object>>
        {
          new Dictionary<string, object>
        {
        {"test", "test"}
        }
         };
            var objetoDataSet = new Entidades.ObjetoDataLake { DepartamentoOrigen = "Departamento" };
            var respuestaConsulta = new RespuestaConsultaPorDepartamento(departamento, datos);

            _dataSetDataFinalRepositorioMock.ObtenerPorDepartamento(objetoDataSet.DepartamentoOrigen).Returns(respuestaConsulta);
            _dataSetDataFinalRepositorioMock.ActualizarPorDepartamento(Arg.Any<string>(), Arg.Any<string>()).Throws(new ActualizacionFallidaException("Error al actualizar"));

            // Act y Assert
            await Assert.ThrowsAsync<ActualizacionFallidaException>(() => servicio.GuardarOActualizarDataSetAsync(objetoDataSet));
        }

    }
}
