using System.Web.Http;
using TiboxWebApi.UnitOfWork;

namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("ReglaNegocio")]
    [Authorize]
    public class ReglaNegocioController : BaseController
    {
        public ReglaNegocioController(IUnitOfWork unit): base(unit)
        {
        }

        [Route("{cForm}")]
        [HttpGet]
        public IHttpActionResult Get(string cForm)
        {
            if (cForm == "" || cForm == null) return BadRequest("falta parametro");
            return Ok(_unit.ReglaNegocio.ListaRegla(cForm));
        }
    }
}
