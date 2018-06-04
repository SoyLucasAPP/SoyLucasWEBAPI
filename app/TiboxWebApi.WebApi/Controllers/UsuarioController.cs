using System.Web.Http;
using TiboxWebApi.WebApi.Utils;
using TiboxWebApi.UnitOfWork;
using TiboxWebApi.Models;

namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("Usuario")]
    [Authorize]
    public class UsuarioController : BaseController
    {
        private readonly Utiles _utils;
        public UsuarioController(IUnitOfWork unit) : base(unit)
        {
            _utils = new Utiles();
        }

        [Route("Encriptar/{cTexto}")]
        [HttpGet]
        public IHttpActionResult Encripta(string cTexto)
        {
            if (cTexto == "" || cTexto == null) return BadRequest();
            var Resultado = _utils.Encriptar(cTexto);
            return Ok(new { cTexto = Resultado });
        }

        [Route("Desencriptar")]
        [HttpPost]
        public IHttpActionResult Desencriptar(User user)
        {
            if (user.Password == "" || user.Password == null) return BadRequest();
            var Resultado = _utils.Desencriptar(user.Password);
            return Ok(new { cTexto = Resultado });
        }

        [Route("Verificar")]
        [HttpPost]
        public IHttpActionResult VerificaEmail(User user)
        {
            if (user.Email == "" || user.Email == null) return BadRequest();
            return Ok(_unit.Users.LucasVerificaEmail(user.Email));
        }

        [Route("DatosLogin")]
        [HttpPost]
        public IHttpActionResult DatosLogin(User user)
        {
            if (user.Email == "" || user.Email == null) return BadRequest();
            return Ok(_unit.Users.LucasDatosLogin(user.Email));
        }

        [Route("CambioPass")]
        [HttpPost]
        public IHttpActionResult CambioPass(User user)
        {
            if (user.Email == "" || user.Email == null) return BadRequest();
            return Ok(new { nCodPers = _unit.Users.LucasCambiaPass(user.Email, user.Password) });
        }

        [Route("DatosADM/{cCodUsu}")]
        [HttpGet]
        public IHttpActionResult DatosADM(string cCodUsu)
        {
            return Ok(_unit.Users.validateUserAD(cCodUsu, ""));
        }

        [Route("ConsultaCambio/{nCodPers}")]
        [HttpGet]
        public IHttpActionResult ConsultaCambio(int nCodPers)
        {
            return Ok(_unit.Users.selCambioPass(nCodPers));
        }
    }
}
