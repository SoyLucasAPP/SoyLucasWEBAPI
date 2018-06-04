using System.Web.Http;
using TiboxWebApi.UnitOfWork;


namespace TiboxWebApi.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        protected readonly IUnitOfWork _unit;
        public BaseController(IUnitOfWork unit)
        {
            _unit = unit;
        }
    }
}
