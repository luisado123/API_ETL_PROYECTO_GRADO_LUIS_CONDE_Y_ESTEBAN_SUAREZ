
using Microsoft.AspNetCore.Mvc;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;

namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Puertos
{
    public interface IDataLakeRepositorio
    {
        Task<IActionResult> GuardarVarios(List<string> listadoRawData);
        Task<IActionResult> GuardarUno(string objetoRawData);

        Task<List<RespuestaConsultaPorDepartamento>> ObtenerPorCampo(string objetoRawData);

    }
}
