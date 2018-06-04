using System.Web.Http;
using TiboxWebApi.Models;
using TiboxWebApi.UnitOfWork;
using TiboxWebApi.WebApi.Utils;

namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("Reporte")]
    [Authorize]
    public class ReporteController : BaseController
    {
        private ReporteEmail _reporte = null;
        public ReporteController(IUnitOfWork unit) : base(unit)
        {
            _reporte = new ReporteEmail(unit);
        }

        [Route("Envio")]
        [HttpPost]
        public IHttpActionResult Envio(Reporte reporte)
        {
            if (reporte == null) return BadRequest();
            bool bError = false;
            string cMendajeError = "";
            var resultado = _reporte.EnviarReportePorEmail(reporte.nCodCred, reporte.nCodAge, reporte.cEmail, reporte.cNombres, reporte.nPrestamo, reporte.nPEP, ref bError, ref cMendajeError);
            return Ok(new {
                bresultado = resultado,
                bError = bError,
                cMensaje = cMendajeError
            });
        }

        [Route("Generar/{nCodCred}/{nCodAge}/{nPEP}")]
        [HttpGet]
        public IHttpActionResult GenerarReportes(int nCodCred, int nCodAge, int nPEP)
        {
            bool bError = false;
            string cMendajeError = "";
            var resultado = _reporte.generaReportes(nCodCred, nCodAge, nPEP, ref bError, ref cMendajeError);
            return Ok(new
            {
                bresultado = resultado,
                bError = bError,
                cMensaje = cMendajeError
            });
        }

        [Route("{nCodAge}/{nCodCred}/{nTipo}")]
        [HttpGet]
        public IHttpActionResult Get(int nCodAge, int nCodCred, int nTipo)
        {
            if (nCodAge == 0) return BadRequest();
            if (nCodCred == 0) return BadRequest();
            if (nTipo == 0) return BadRequest();
            return Ok(_unit.Reporte.LucasSeleccionaReporte(nCodAge, nCodCred, nTipo));
        }
    }
}
