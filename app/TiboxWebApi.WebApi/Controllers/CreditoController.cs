using System.Web.Http;
using TiboxWebApi.Models;
using TiboxWebApi.UnitOfWork;
using TiboxWebApi.WebApi.Utils;

namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("Credito")]
    [Authorize]
    public class CreditoController : BaseController
    {
        private Utiles _utils;
        public CreditoController(IUnitOfWork unit) : base(unit)
        {
            _utils = new Utiles();
        }

        [Route("Bandeja")]
        [HttpPost]
        public IHttpActionResult Bandeja(Credito credito)
        {
            if (credito == null) return BadRequest();
            if (credito.nCodAge == 0) return BadRequest();
            if (credito.nCodPers == 0) return BadRequest();
            return Ok(_unit.Credito.LucasBandeja(credito.nCodPers, credito.nPagina, credito.nTamanio, credito.nCodAge));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Credito(Credito credito)
        {
            if (credito == null) return BadRequest();
            var nCodCred = _unit.Credito.LucasInsCredito(credito);
            if (nCodCred == 0) return BadRequest();
            return Ok(new { nCodCred = nCodCred });
        }

        [Route("Modalidad")]
        [HttpPost]
        public IHttpActionResult Modalidad(Credito credito)
        {
            if (credito == null) return BadRequest();
            var nRetorno = _unit.Credito.LucasInsModalidad(credito);
            if (nRetorno == 0) return BadRequest();
            return Ok(new { bExito = nRetorno });
        }

        [Route("Firma")]
        [HttpPost]
        public IHttpActionResult Firma(Credito credito)
        {
            if (credito == null) return BadRequest();
            var nRetorno = _unit.Credito.LucasInsFirmaElectronica(credito);
            if (nRetorno == 0) return BadRequest();
            return Ok(new { bExito = nRetorno });
        }

        [Route("DatosPrestamo/{nCodAge}/{nCodCred}")]
        [HttpGet]
        public IHttpActionResult DatosPrestamo(int nCodAge, int nCodCred)
        {
            if (nCodAge == 0) return BadRequest();
            if (nCodCred == 0) return BadRequest();
            return Ok(_unit.Credito.LucasDatosPrestamo(nCodAge, nCodCred));
        }

        [Route("Calendario/Lista/{nCodAge}/{nCodCred}")]
        [HttpGet]
        public IHttpActionResult CalendarioLista(int nCodAge, int nCodCred)
        {
            if (nCodAge == 0) return BadRequest();
            if (nCodCred == 0) return BadRequest();
            return Ok(_unit.Credito.LucasCalendarioLista(nCodAge, nCodCred));
        }

        [Route("Kardex/Lista/{nCodAge}/{nCodCred}")]
        [HttpGet]
        public IHttpActionResult KardexLista(int nCodAge, int nCodCred)
        {
            if (nCodAge == 0) return BadRequest();
            if (nCodCred == 0) return BadRequest();
            return Ok(_unit.Credito.LucasKardexLista(nCodAge, nCodCred));
        }

        [Route("RechazadoPorDia/{cDocumento}")]
        [HttpGet]
        public IHttpActionResult RechazadoPorDia(string cDocumento)
        {
            return Ok(_unit.Credito.LucasRechazadoPorDia(cDocumento));
        }

        [Route("CreditoxFlujo/{cDocumento}")]
        [HttpGet]
        public IHttpActionResult CreditoxFlujo(string cDocumento)
        {
            return Ok(_unit.Credito.LucasCreditoEnFlujo(cDocumento));
        }

        [Route("AnulaxActualizacion/{cDocumento}")]
        [HttpGet]
        public IHttpActionResult AnulaxActualizacion(string cDocumento)
        {
            return Ok(_unit.Credito.LucasCreditoAnulaxActualizacion(cDocumento));
        }

        [Route("Calendario")]
        [HttpPost]
        public IHttpActionResult Calendario(Credito credito)
        {
            return Ok(_utils.GeneraCalendario(credito.nPrestamo, credito.nNroCuotas, credito.nPeriodo, credito.nTasa, credito.dFechaSistema, credito.nSeguro));
        }
    }
}
