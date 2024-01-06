using Microsoft.AspNetCore.Http;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS.Etl.LuisConde.EstebanSuarez.Dominio.Entidades;

namespace UTS.Etl.LuisConde.EstebanSuarez.Aplicacion.Etl.ArchivoXlsx.Comandos.Crear
{
    public record CrearObjetoDataLakeComando(
        [Required] IFormFile Archivo,
        string Departamento
    ) : IRequest<RespuestaEtl>;

}
