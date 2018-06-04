using System;
using System.IO;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using TiboxWebApi.Models;
using TiboxWebApi.UnitOfWork;

namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("Documento")]
    [Authorize]
    public class DocumentoController : BaseController
    {
        public DocumentoController(IUnitOfWork unit) : base(unit)
        {
        }

        [Route("Tipo")]
        [HttpGet]
        public IHttpActionResult Tipo()
        {
            return Ok(_unit.Documento.ListaDocumentos());
        }

        [Route("Subir")]
        [HttpPost]
        public IHttpActionResult Subir(Documento documento)
        {
            var valores = new Documento();
            valores.iImagen = Convert.FromBase64String(documento.oDocumento);
            valores.cNomArchivo = documento.cNomArchivo;
            valores.cExtencion = documento.cExtencion;
            valores.nIdFlujoMaestro = documento.nIdFlujoMaestro;
            valores.cTipoArchivo = documento.cTipoArchivo;

            var resultado = _unit.Documento.LucasInsDocumento(valores);

            return Ok(new { bRespuesta = resultado });

        }

    }
}
