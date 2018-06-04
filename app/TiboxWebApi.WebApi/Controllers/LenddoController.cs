using System.Web.Http;
using TiboxWebApi.Models;
using TiboxWebApi.UnitOfWork;

namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("Lenddo")]
    [Authorize]
    public class LenddoController : BaseController
    {
        public LenddoController(IUnitOfWork unit): base(unit)
        {
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.Lenddo.GetEntityById(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(WebPersonaLenddo lenddo)
        {
            var id = _unit.Lenddo.Insert(lenddo);
            return Ok(new { id = id });
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(WebPersonaLenddo lenndo)
        {
            var id = _unit.Lenddo.Update(lenndo);
            return Ok(new { status = true });
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var result = _unit.Lenddo.Delete(new WebPersonaLenddo { nCodigo = id });
            return Ok(new { detele = true });
        }

        [Route("List")]
        [HttpGet]
        public IHttpActionResult GetList()
        {
            return Ok(_unit.Lenddo.GetAll());
        }
    }
}
