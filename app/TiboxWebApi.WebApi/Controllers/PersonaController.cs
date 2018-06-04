using FluentValidation;
using System.Net;
using System.Web.Http;
using TiboxWebApi.Models;
using TiboxWebApi.UnitOfWork;
using TiboxWebApi.WebApi.Utils;

namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("Persona")]
    [Authorize]
    public class PersonaController : BaseController
    {
        private readonly AbstractValidator<Persona> _validator;
        private Sms _sms;
        private Email _email;
        public PersonaController(IUnitOfWork unit, AbstractValidator<Persona> validator) : base(unit)
        {
            _validator = validator;
            _sms = new Sms();
            _email = new Email();
        }

        [Route("Datos")]
        [HttpPost]
        public IHttpActionResult Datos(Persona persona)
        {
            if (persona.nCodPers == 0) return BadRequest();
            if (persona.nNroDoc == "" || persona.nNroDoc == null) return BadRequest();
            if (persona.cEmail == "" || persona.cEmail == null) return BadRequest();
            return Ok(_unit.Persona.LucasDatosPersona(persona.nNroDoc, persona.cEmail, persona.nCodPers));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Persona persona)
        {
            var result = _validator.Validate(persona);
            if (!result.IsValid) return Content(HttpStatusCode.BadRequest, result.Errors);
            var nCodPers = _unit.Persona.LucasInsPersona(persona);
            if(nCodPers > 0)
            {
                _sms.enviarSMS(persona.cCelular, persona.cTextoSms);
                var cMensajeError = "";
                _email.envioEmail(persona.cEmail, persona.cTextoEmail, persona.cTituloEmail, ref cMensajeError);
            }
            return Ok(new { nCodPers = nCodPers });
        }

        [Route("Verifica/{cDocumento}")]
        [HttpGet]
        public IHttpActionResult Verifica(string cDocumento)
        {
            if (cDocumento == "" || cDocumento == null) return BadRequest();
            return Ok(_unit.Persona.LucasVerificaClienteExiste(cDocumento));
        }

        [Route("Put")]
        [HttpPost]
        public IHttpActionResult Put(Persona persona)
        {
            var result = _validator.Validate(persona);
            if (!result.IsValid) return Content(HttpStatusCode.BadRequest, result.Errors);
            var nCodPers = _unit.Persona.LucasActPersona(persona);
            return Ok(new { nCodPers = nCodPers});
        }

        [Route("Tratamiento")]
        [HttpPost]
        public IHttpActionResult Tratamiento(Tratamiento tratamiento)
        {
            if (tratamiento == null) return BadRequest();
            var nCodSolicitud = _unit.Persona.LucasTratamientoDatos(tratamiento);
            return Ok(new { nCodSolicitud = nCodSolicitud});
        }

        [Route("Celular/{cDocumento}/{cCelular}")]
        [HttpGet]
        public IHttpActionResult Celular(string cDocumento, string cCelular)
        {
            return Ok(_unit.Persona.LucasValidaPersonaCelular(cDocumento, cCelular));
        }
    }
}
