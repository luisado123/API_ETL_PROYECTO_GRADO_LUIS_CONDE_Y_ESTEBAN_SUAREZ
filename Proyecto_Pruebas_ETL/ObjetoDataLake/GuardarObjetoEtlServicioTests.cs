using Xunit;
using NSubstitute;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UTS.Etl.LuisConde.EstebanSuarez.Infraestructura.Adaptadores;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Puertos;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.servicios.ArchivoXlsx;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Constantes;
using NSubstitute.ExceptionExtensions;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Excepciones;
using FluentAssertions;
namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Tests
{
    public class GuardarObjetoEtlServicioTests
    {
        private readonly IDataLakeRepositorio _dataLakeRepositorioMock;

        public GuardarObjetoEtlServicioTests()
        {
            _dataLakeRepositorioMock = Substitute.For<IDataLakeRepositorio>();
        }

        [Fact]
        public async Task GuardarRawDataAsyncServicio_GuardaUnoEnDataLakeRepositorio()
        {
            // Arrange
            var objetoDataLake = new Entidades.ObjetoDataLake
            {
                Datos = new List<Dictionary<string, object>>(),
                DepartamentoOrigen = "Departamento"
            };

            var servicio = new GuardarObjetoEtlServicio(_dataLakeRepositorioMock);
            _dataLakeRepositorioMock.GuardarUno(Arg.Any<string>()).Returns(Task.FromResult<IActionResult>(new OkObjectResult(new { Message = MensajesExitosos.CargaEnDataWareHouseExitosa })));

            // Act
            var resultado =await servicio.GuardarRawDataAsync(objetoDataLake);

            // Assert
            await _dataLakeRepositorioMock.Received(1).GuardarUno(Arg.Any<string>());
            var okResult = resultado as OkObjectResult;
            okResult.Should().NotBeNull();
            var message = okResult.Value.GetType().GetProperty("Message")?.GetValue(okResult.Value)?.ToString();
            message.Should().Contain(MensajesExitosos.CargaEnDataWareHouseExitosa);

        }


        [Fact]
        public async Task GuardarUno_DeberiaLanzarExcepcionCuandoFallaLaInsercion()
        {
            // Arrange
            var objetoDataLake = new Entidades.ObjetoDataLake
            {
                Datos = new List<Dictionary<string, object>>(),
                DepartamentoOrigen = "Departamento"
            };

            var error = "Error interno";

            var servicio = new GuardarObjetoEtlServicio(_dataLakeRepositorioMock);
            _dataLakeRepositorioMock.GuardarUno(Arg.Any<string>()).Throws(new GuardadoEnDataLakeFallidoExcepcion($"{MensajesExcepciones.ErrorAlGuardar}: {error}"));

            // Act y Assert
            await Assert.ThrowsAsync<GuardadoEnDataLakeFallidoExcepcion>(() => servicio.GuardarRawDataAsync(objetoDataLake));

        }

    }

}
