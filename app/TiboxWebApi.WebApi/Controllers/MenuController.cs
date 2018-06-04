using System.Web.Http;
using TiboxWebApi.UnitOfWork;

namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("Menu")]
    [Authorize]
    public class MenuController : BaseController
    {
        public MenuController(IUnitOfWork unit): base(unit)
        {
        }

        [Route("{cCodUsu}")]
        [HttpGet]
        public IHttpActionResult Menu(string cCodUsu)
        {
            return Ok(_unit.Menu.SelMenus(cCodUsu));
        }

    }
}
