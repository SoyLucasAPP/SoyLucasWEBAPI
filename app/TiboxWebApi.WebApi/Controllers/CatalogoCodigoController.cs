using System.Web.Http;
using TiboxWebApi.UnitOfWork;

namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("CatalogoCodigo")]
    [Authorize]
    public class CatalogoCodigoController : BaseController
    {
        public CatalogoCodigoController(IUnitOfWork unit) : base(unit)
        {            
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.CatalogoCodigo.selCatalogoCodigos(id));
        }

        [Route("TipoVivienda")]
        [HttpGet]
        public IHttpActionResult Tipovivienda()
        {
            return Ok(_unit.CatalogoCodigo.selTipovivienda());
        }
    }
}
