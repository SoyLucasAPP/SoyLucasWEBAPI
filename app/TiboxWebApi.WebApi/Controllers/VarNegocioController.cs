using System.Web.Http;
using TiboxWebApi.UnitOfWork;

namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("VarNegocio")]
    [Authorize]
    public class VarNegocioController : BaseController
    {
        public VarNegocioController(IUnitOfWork unit): base(unit)
        {
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.VarNegocio.GetEntityById(id));
        }

    }
}
