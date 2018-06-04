using System;
using System.Web.Http;
using TiboxWebApi.Models;
using TiboxWebApi.UnitOfWork;
using TiboxWebApi.WebApi.Utils;

namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("Evaluacion")]
    [Authorize]
    public class EvaluacionController : BaseController
    {
        EvaluacionMotor _evaluacion = null;
        public EvaluacionController(IUnitOfWork unit) : base(unit)
        {
            _evaluacion = new EvaluacionMotor();
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Evaluacion(Persona persona)
        {
            var FlujoRespuesta = 0;
            int nRechazado = 0;
            int nPEP = 0;
            var cMensajeTry = "";
            try
            {
                if (persona == null) return BadRequest("Faltan un dato");
                string cXmlScoringDatos = "";
                string cXmlScoringCuota = "";
                string cXmlDeudas = "";
                string cXMLPuntajeIPDItems = "";
                string cXmlScoringDemo = "";
                string cMensajeError = "";

                var splited = persona.dFechaNacimiento.Split('/');
                DateTime fechaNacimiento = Convert.ToDateTime(splited[2] + '-' + splited[1] + '-' + splited[0]);
                int nEdad = DateTime.Today.Year - fechaNacimiento.Year;
                

                if (fechaNacimiento.Month > DateTime.Today.Month)
                {
                    nEdad = nEdad - 1;
                }
                else if (fechaNacimiento.Month == DateTime.Today.Month && fechaNacimiento.Day > DateTime.Today.Day)
                {
                    nEdad = nEdad - 1;
                }

                bool bResultado = _evaluacion.Evaluacion(persona.nNroDoc,
                    persona.cDistrito,
                    persona.cProvincia,
                    persona.cDepartamento,
                    nEdad,
                    int.Parse(persona.nSexo),
                    int.Parse(persona.nEstadoCivil),
                    int.Parse(persona.nCUUI),
                    persona.nProducto,
                    persona.nModalidad,
                    int.Parse(persona.nSitLab),
                    int.Parse(persona.nTipoResidencia),
                    0,
                    1,
                    1,
                    persona.cDniConyuge,
                    int.Parse(persona.nTipoEmp),
                    persona.nCodPers,
                    persona.nIngresoDeclado,
                    persona.nSitLab,
                    persona.nTipoEmp,
                    ref cXmlScoringDatos,
                    ref cXmlScoringCuota,
                    ref cXmlDeudas,
                    ref cXMLPuntajeIPDItems,
                    ref cXmlScoringDemo,
                    ref cMensajeError,
                    ref nRechazado,
                    ref nPEP);

                if (!bResultado) return BadRequest(cMensajeError);

                var flujo = new FlujoMaestro();
                flujo.nNroDoc = persona.nNroDoc;
                flujo.nCodAge = persona.nCodAge;
                flujo.nProd = persona.nProd;
                flujo.nSubProd = persona.nSubProd;
                flujo.cNomform = "/StateClienteNuevo";
                flujo.nCodCred = 0;
                flujo.cUsuReg = "USU-LUCAS";
                flujo.nIdFlujo = 0;
                flujo.nCodPersReg = persona.nCodPers;
                flujo.nOrdenFlujo = 0;
                flujo.oScoringDatos = cXmlScoringDatos;
                flujo.oScoringVarDemo = cXMLPuntajeIPDItems;
                flujo.oScoringDetCuota = cXmlScoringCuota;
                flujo.oScoringDemo = cXmlScoringDemo;
                flujo.oScoringRCC = cXmlDeudas;
                flujo.nRechazado = nRechazado;
                flujo.cClienteLenddo = persona.cLenddo;

                var FlujoMaestro = _unit.FlujoMaestro.LucasRegistraMotor(flujo);

                if (FlujoMaestro == 0) return BadRequest("Error de evaluación.");

                FlujoRespuesta = FlujoMaestro;
            }
            catch (Exception ex)
            {
                cMensajeTry = ex.Message;
                _unit.Error.InsertaError("EVALUACION - Evaluacion", ex.Message);
            }
            return Ok(new { nIdFlujoMaestro = FlujoRespuesta, nRechazado = nRechazado, cMensajeTry = cMensajeTry, nPEP = nPEP });
        }

        [Route("PreEvaluacion")]
        [HttpPost]
        public IHttpActionResult PreEvaluacion(Persona persona)
        {
            if (persona == null) return BadRequest("Falta un dato");
            if (persona.nNroDoc == "" || persona.nNroDoc == null) return BadRequest("Falta un dato.");
            if (persona.nProducto == 0) return BadRequest("Falta un dato.");
            
            string cRespuesta = "";
            string cMensajeError = "";
            bool bResultado = _evaluacion.preEvaluacion(persona.nNroDoc, persona.nProducto, persona.nModalidad, ref cRespuesta, ref cMensajeError);
            if (!bResultado) return BadRequest(cMensajeError);
            return Ok(new { cResultado = cRespuesta });
        }
    }
}
