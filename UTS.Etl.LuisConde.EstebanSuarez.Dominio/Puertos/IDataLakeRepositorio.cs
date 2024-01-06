
namespace UTS.Etl.LuisConde.EstebanSuarez.Dominio.Puertos
{
    public interface IDataLakeRepositorio
    {
        Task<bool> GuardarVarios(IEnumerable<string> listaRawData);
        Task<bool> GuardarUno(string objetoRawData);

    }
}
