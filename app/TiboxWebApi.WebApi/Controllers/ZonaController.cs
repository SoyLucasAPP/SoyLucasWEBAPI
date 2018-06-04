using System.Web.Http;
using TiboxWebApi.UnitOfWork;

namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("Zona")]
    [Authorize]
    public class ZonaController : BaseController
    {
        public ZonaController(IUnitOfWork unit) : base(unit)
        {
        }

        [Route("Departamento")]
        [HttpGet]
        public IHttpActionResult Departamento()
        {
            return Ok(_unit.Zona.selDepartamento());
        }

        [Route("Provincia/{cDep}")]
        [HttpGet]
        public IHttpActionResult Provincia(string cDep)
        {
            if (cDep == "" || cDep == null) return BadRequest();
            return Ok(_unit.Zona.selProvincia(cDep));
        }

        [Route("Distrito/{cDep}/{cPro}")]
        [HttpGet]
        public IHttpActionResult Distrito(string cDep, string cPro)
        {
            if (cDep == "" || cDep == null) return BadRequest();
            if (cPro == "" || cPro == null) return BadRequest();
            return Ok(_unit.Zona.selDistrito(cDep, cPro));
        }
    }
}
