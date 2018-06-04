using System.Web.Http;
using TiboxWebApi.Models;
using TiboxWebApi.UnitOfWork;
using TiboxWebApi.WebApi.Utils;

namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("Alerta")]
    [Authorize]
    public class AlertaController : BaseController
    {
        private Sms _sms = null;
        private Email _email = null;
        public AlertaController(IUnitOfWork unit) : base(unit)
        {
            _sms = new Sms();
            _email = new Email();
        }

        [Route("SMS")]
        [HttpPost]
        public IHttpActionResult SMS(Alerta alerta)
        {
            if (alerta == null) return BadRequest("Faltan datos");
            var bResultado = _sms.enviarSMS(alerta.cMovil, alerta.cTexto);
            //if (!bResultado) return BadRequest();
            return Ok(new { cRed = true });
        }

        [Route("Email")]
        [HttpPost]
        public IHttpActionResult Email(Alerta alerta)
        {
            string cMensajeError = "";
            if (alerta == null) return BadRequest("Faltan datos");
            var bResultado = _email.envioEmail(alerta.cEmail, alerta.cTexto, alerta.cTitulo, ref cMensajeError);
            //if (!bResultado) return BadRequest();
            return Ok(new { cRed = true });
        }
    }
}
