using System;
using TiboxWebApi.WebApi.wsCPD;
using TiboxWebApi.WebApi.wsIngresoPredecido;
using TiboxWebApi.WebApi.wsPreAprobacion;
using TiboxWebApi.WebApi.wsScoreBuro;
using TiboxWebApi.WebApi.wsScoreComportamiento;
using TiboxWebApi.WebApi.wsScoreDemografico;
using TiboxWebApi.WebApi.wsScoreLenddo;
using TiboxWebApi.WebApi.wsScoringBuro;
using TiboxWebApi.WebApi.wsScoringDemografico;
using TiboxWebApi.WebApi.wsScoringValidacionReglas;

namespace TiboxWebApi.WebApi.Utils
{
    public class EvaluacionMotor
    {
        private IwsScoreBuro oScoreBuro;
        private IwsScoreDemografico oScoreDemografo;
        private IScoringBuro oScoringBuro;
        private IwsScoringDemografico oScoringDemografico;
        private IngresoPredecido oScoringIngresoPredecido;
        private IwsScoreLenddo oScoreLenddo;
        private IwsCPD oScoreCPD;
        private IwsPreAprobacion oPreAprobacion;
        private IwsScoreComportamiento oComportamiento;
        private IwsScoringValidacionReglas oReglas;

        double nScoreLenddo = 0;
        double nIngDemo1 = 0;
        double nIngDemo2 = 0;
        double nIngDemo3 = 0;
        double nIngresoInfDemografico = 0;
        double nIngresoInfRCC = 0;
        double nIngRCC1 = 0;
        double nIngRCC2 = 0;
        double nIngRCC3 = 0;
        double nIngFinal1 = 0;
        double nIngFinal2 = 0;
        double nIngFinal3 = 0;
        double nMontoTotal = 0;
        double nCuotaUtilizada1 = 0;
        double nCuotaUtilizada2 = 0;
        double nScoreBuro = 0;
        double nCuotaMaxima = 0;
        double nCuotaDisp = 0;
        double nPrestamo1 = 0;
        double nPrestamo2 = 0;
        double nPrestamo3 = 0;
        double nPrestamo4 = 0;
        double nPrestamoMinimo = 0;
        double nTasa = 0;
        double nPlazo = 0;
        double nRCI = 0;
        double nPrestamoMax = 0;
        double nRMA = 0;
        double nPrestamoFinal = 0;
        double nMora = 0;
        double nSumatoria = 0;
        double nIngresoDeclarado = 0;
        double nPorGarantia = 0;
        double nScoreComportamiento = 0;
        double nScoreDemografico = 0;
        int nTipoSolicitud = 0;
        int nValorNecesario = 0;
        int nCodigoFlujo = 0;
        int nIdRechazado = 0;
        int nTipoBanca = 0;
        int nClientePEP = 0;

        string cDecisionReglas = "";
        string cMotivoRechazado = "";
        string cDecision = "";
        string cTipoBanca = "";
        string cClienteLenddo = "";

        public EvaluacionMotor()
        {
            oScoreBuro = new IwsScoreBuroClient();
            oScoreDemografo = new IwsScoreDemograficoClient();
            oScoringBuro = new ScoringBuroClient();
            oScoringDemografico = new IwsScoringDemograficoClient();
            oScoringIngresoPredecido = new IngresoPredecidoClient();
            oScoreLenddo = new IwsScoreLenddoClient();
            oScoreCPD = new IwsCPDClient(); 
            oPreAprobacion = new IwsPreAprobacionClient();
            oComportamiento = new IwsScoreComportamientoClient();
            oReglas = new IwsScoringValidacionReglasClient();
        }

        string devuelveXMLDatos(string cDocumento)
        {
            string cXmlScoringDatosArma = "<GENESYS><DATA cDocumento = '" + cDocumento + "' " +
                "nIngDemo1 = '" + nIngDemo1 + "' " + "nIngDemo2 = '" + nIngDemo2 + "' " + "nIngDemo3 = '" + nIngDemo3 + "' " + "nIngresoInfDemografico = '" + nIngresoInfDemografico + "' " +
                "nIngresoInfRCC = '" + nIngresoInfRCC + "' " + "nIngRCC1 = '" + nIngRCC1 + "' " + "nIngRCC2 = '" + nIngRCC2 + "' " + "nIngRCC3 = '" + nIngRCC3 + "' " +
                "nIngFinal1 = '" + nIngFinal1 + "' " + "nIngFinal2 = '" + nIngFinal2 + "' " + "nIngFinal3 = '" + nIngFinal3 + "' " + "nMontoTotal = '" + nMontoTotal + "' " +
                "nCuotaUtilizada1 = '" + nCuotaUtilizada1 + "' " + "nCuotaUtilizada2 = '" + nCuotaUtilizada2 + "' " + "nScoreBuro = '" + nScoreBuro + "' " + "nCuotaMaxima = '" + nCuotaMaxima + "' " +
                "nCuotaDisp = '" + nCuotaDisp + "' " + "nPrestamo1 = '" + nPrestamo1 + "' " + "nPrestamo2 = '" + nPrestamo2 + "' " + "nPrestamo3 = '" + nPrestamo3 + "' " +
                "nPrestamo4 = '" + nPrestamo4 + "' " + "nTasa = '" + nTasa + "' " + "nPlazo = '" + nPlazo + "' " + "nRCI = '" + nRCI + "' " +
                "nPrestamoMax = '" + nPrestamoMax + "' " + "nRMA = '" + nRMA + "' " + "nPrestamoFinal = '" + nPrestamoFinal + "' " + "nMora = '" + nMora + "' " +
                "nSumatoria = '" + nSumatoria + "' " + "cDecision = '" + cDecision + "' " + "cTipoBanca = '" + cTipoBanca + "' " + " nScoreLenddo = '" + nScoreLenddo + "' " +
                "nIngresoDeclarado = '" + nIngresoDeclarado + "' nPorGarantia = '" + nPorGarantia + "' nTipoSolicitud = '" + nTipoSolicitud + "' " +
                "cClienteLenddo = '" + cClienteLenddo + "' nValorNecesario = '" + nValorNecesario + "' nCodigoFlujo = '" + nCodigoFlujo + "' nPrestamoMinimo = '" + nPrestamoMinimo + "' " +
                "nScoreComportamiento = '" + nScoreComportamiento + "' nScoreDemografico = '" + nScoreDemografico + "' cMotivoRechazado = '" + cMotivoRechazado + "' " +
                "nIdRechazado = '" + nIdRechazado + "' cDecisionReglas = '" + cDecisionReglas + "' nClientePEP = '" + nClientePEP + "'></DATA></GENESYS>";
            return cXmlScoringDatosArma;
        }

        public bool Evaluacion(string cDocumento, 
                                    string cDistrito, 
                                    string cProvincia, 
                                    string cDepartamento, 
                                    int nEdad, 
                                    int nGenero,
                                    int nEstadoCivil, 
                                    int nCIIU, 
                                    int nProducto, 
                                    int nModalidad, 
                                    int nCondicion, 
                                    int nVivienda, 
                                    int nScoreLendo, 
                                    int nTipoDocumento,
                                    int nTipoDocumentoConyuge, 
                                    string cDocumentoConyuge, 
                                    int nCondicionSituacionLaboral, 
                                    int nCodPers, 
                                    double IngresoDeclarado,
                                    string nTipoDependiente,
                                    string nTipoFormal,
                                    ref string cXmlScoringDatos,
                                    ref string cXmlScoringCuota,
                                    ref string cXmlDeudas,
                                    ref string cXMLPuntajeIPDItems,
                                    ref string cXmlScoringDemo,
                                    ref string cMensajeError,
                                    ref int nRechazado, 
                                    ref int nPEP)
        {
            cXmlScoringDatos = "<GENESYS></GENESYS>";
            cXmlScoringCuota = "<GENESYS></GENESYS>";
            cXmlDeudas = "<GENESYS></GENESYS>";
            cXMLPuntajeIPDItems = "<GENESYS></GENESYS>";
            cXmlScoringDemo = "<GENESYS></GENESYS>";

            string cDep, cProv, cDis;
            string cUbigueo = "";
            string cZona = "";

            cDep = cDepartamento;
            cProv = cProvincia;
            cDis = cDistrito;
            cDepartamento = cDep + "0000000000";
            cProvincia = cDep + cProv + "00000000";
            cDistrito = cDep + cProv + cDis + "000000";
            cZona = cDistrito;

            if (cZona.Substring(0, 2) == "15") cUbigueo = cZona.Substring(0, 6);
            else if (cZona.Substring(0, 2) == "15" && cZona.Substring(2, 2) != "01") cUbigueo.Substring(0, 6);
            else
            {
                cUbigueo = cZona.Substring(0, 6);
            }
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ELIngresoPredecidoDemograficoDatos oIP = new ELIngresoPredecidoDemograficoDatos();
            ELIngresoPredecidoDemograficoResultado oIPReturn = new ELIngresoPredecidoDemograficoResultado();

            try
            {
                oIP.nGenero = nGenero.ToString();
                oIP.nEstadoCivil = nEstadoCivil.ToString();
                oIP.nEdad = nEdad.ToString();
                oIP.sUbiGeo = cUbigueo;
                oIP.sCIIU = nCIIU.ToString();

                oIPReturn = oScoringIngresoPredecido.IngresoPredecidoDemografico(oIP);

                if (oIPReturn.bError == true)
                {
                    cMensajeError = "IngresoPredecidoDemografico Error: " + oIPReturn.sMensajeError;
                    return false;
                }

                cXMLPuntajeIPDItems = "<GENESYS>";
                if (oIPReturn.PuntajeIPDItems != null)
                {
                    for (var h = 0; h <= oIPReturn.PuntajeIPDItems.Length - 1; h++)
                    {
                        cXMLPuntajeIPDItems = cXMLPuntajeIPDItems + "<DATA cVariable = '" + oIPReturn.PuntajeIPDItems[h].sVariable + "' cVariableDescripcion = '" + oIPReturn.PuntajeIPDItems[h].sVariableDescripcion + "' " +
                                                                "nVariablePuntaje = '" + oIPReturn.PuntajeIPDItems[h].nVariablePuntaje + "' ></DATA>";
                    }
                }
                cXMLPuntajeIPDItems = cXMLPuntajeIPDItems + "</GENESYS>";

                nIngDemo1 = oIPReturn.nIngresoPredecidoDemografico1;
                nIngDemo2 = oIPReturn.nIngresoPredecidoDemografico2;
                nIngDemo3 = oIPReturn.nIngresoPredecidoDemografico3;
                nIngresoInfDemografico = oIPReturn.nIngresoPredecidoDemografico3;
            }
            catch (Exception ex)
            {
                cMensajeError = "IngresoPredecidoDemografico Error: " + ex.Message;
                return false;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ELScoreBuroDatos oIPScoreBuro = new ELScoreBuroDatos();
            ELScoreBuroResultado oIPScoreBuroReturn = new ELScoreBuroResultado();

            try
            {
                oIPScoreBuro.sNroDoc = cDocumento;

                oIPScoreBuroReturn = oScoreBuro.ScoreBuro(oIPScoreBuro);

                if (oIPScoreBuroReturn.bError == true)
                {
                    cMensajeError = "ScoreBuro Error: " + oIPScoreBuroReturn.sMensajeError;
                    return false;
                }

                nScoreBuro =  oIPScoreBuroReturn.nScoreBuro;
            }
            catch (Exception ex)
            {
                cMensajeError = "ScoreBuro Error: " + ex.Message;
                return false;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ELIngresoPredecidoRCCDatos oIPRCC = new ELIngresoPredecidoRCCDatos();
            ELIngresoPredecidoRCCResultado oIPRCCReturn = new ELIngresoPredecidoRCCResultado();

            try
            {

                oIPRCC.nEstadoCivil = nEstadoCivil.ToString();

                wsIngresoPredecido.ELDocumento oDocumento = new wsIngresoPredecido.ELDocumento();

                oDocumento = new wsIngresoPredecido.ELDocumento();
                oDocumento.nTipoDoc = nTipoDocumento.ToString();
                oDocumento.sNroDoc = cDocumento;

                oIPRCC.oDocumentoTitular = oDocumento;

                if (oIPRCC.nEstadoCivil == "2")
                {
                    oDocumento = new wsIngresoPredecido.ELDocumento();
                    oDocumento.nTipoDoc = nTipoDocumentoConyuge.ToString();
                    oDocumento.sNroDoc = cDocumentoConyuge;
                    oIPRCC.oDocumentoConyuge = oDocumento;
                }

                oIPRCCReturn = oScoringIngresoPredecido.IngresoPredecidoRCC(oIPRCC);

                if (oIPRCCReturn.bError == true)
                {
                    cMensajeError = "IngresoPredecidoRCC Error: " + oIPRCCReturn.sMensajeError;
                    return false;
                }

                cXmlDeudas = "<GENESYS>";
                int nContarError = 0;
                if (oIPRCCReturn.Deudas != null)
                {
                    for (var i = 0; i <= oIPRCCReturn.Deudas.Length - 1; i++)
                    {
                        cXmlDeudas = cXmlDeudas + "<DATA cTipoDescripcion = '" + oIPRCCReturn.Deudas[i].sTipoDescripcion + "' cCaracteristica = '" + oIPRCCReturn.Deudas[i].sCaracteristica + "' " +
                                                        "nMonto = '" + oIPRCCReturn.Deudas[i].nMonto + "' nFactor = '" + oIPRCCReturn.Deudas[i].nFactor + "' nInferencia = '" + oIPRCCReturn.Deudas[i].nInferencia + "' ></DATA>";
                        nContarError = nContarError + 1;
                    }
                }

                cXmlDeudas = cXmlDeudas + "</GENESYS>";

                nIngRCC1 = oIPRCCReturn.nIngresoPredecidoRCC1;
                nIngRCC2 = oIPRCCReturn.nIngresoPredecidoRCC2;
                nIngRCC3 = oIPRCCReturn.nIngresoPredecidoRCC3;
                nIngresoInfRCC = oIPRCCReturn.nIngresoPredecidoRCC3;
            }
            catch (Exception ex)
            {
                cMensajeError = "IngresoPredecidoRCC Error: " + ex.Message;
                return false;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ELIngresoPredecidoDatos oIPFinal = new ELIngresoPredecidoDatos();
            ELIngresoPredecidoResultado oIPFinalReturn = new ELIngresoPredecidoResultado();

            try
            {
                wsIngresoPredecido.ELDocumento oDocumento = new wsIngresoPredecido.ELDocumento();

                oDocumento = new wsIngresoPredecido.ELDocumento();
                oDocumento.nTipoDoc = nTipoDocumento.ToString();
                oDocumento.sNroDoc = cDocumento;

                oIPFinal.oDocumentoTitular = oDocumento;

                oIPFinal.nIngresoPredecidoDemografico = nIngresoInfDemografico.ToString();
                oIPFinal.nIngresoPredecidoRCC = nIngresoInfRCC.ToString();

                oIPFinalReturn = oScoringIngresoPredecido.IngresoPredecido(oIPFinal);

                if (oIPFinalReturn.bError == true)
                {
                    cMensajeError = "IngresoPredecido Error: " + oIPFinalReturn.sMensajeError;
                    return false;
                }

                nIngFinal1 = oIPFinalReturn.nIngresoPredecidoFinal1;
                nIngFinal2 = oIPFinalReturn.nIngresoPredecidoFinal2;
                nPrestamo1 = oIPFinalReturn.nIngresoPredecidoFinal2;

                ELIngresoCliente OElIngresoCliente = new ELIngresoCliente();
                ELIngresoCliente OElIngresoClienteResultado = new ELIngresoCliente();

                OElIngresoCliente.nIngresoPredecido = nIngFinal2.ToString();
                OElIngresoCliente.nIngresoDeclarado = IngresoDeclarado.ToString();
                OElIngresoCliente.nIngresoFinal = "0";

                OElIngresoClienteResultado = oScoringIngresoPredecido.IngresoPredecidoVsIngresoDeclarado(OElIngresoCliente);

                if (OElIngresoClienteResultado.oError.bError == true)
                {
                    cMensajeError = "IngresoPredecidoVsIngresoDeclarado Error: " + OElIngresoClienteResultado.oError.sMensajeError;
                    return false;
                }

                nIngFinal3 = Convert.ToDouble(OElIngresoClienteResultado.nIngresoFinal);
            }
            catch (Exception ex)
            {
                cMensajeError = "Error: " + ex.Message;
                return false;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ELScoringBuroCuotaUtilizadaDatos oSBCuotaUtilizadaDatos = new ELScoringBuroCuotaUtilizadaDatos();
            ELScoringBuroCuotaUtilizadaResultado oSBCuotaUtilizadaResultado = new ELScoringBuroCuotaUtilizadaResultado();

            try
            {
                oSBCuotaUtilizadaDatos.nEstadoCivil = nEstadoCivil.ToString();

                wsScoringBuro.ELDocumento oDocumento = new wsScoringBuro.ELDocumento();

                oDocumento = new wsScoringBuro.ELDocumento();
                oDocumento.nTipoDoc = nTipoDocumento.ToString();
                oDocumento.sNroDoc = cDocumento;

                oSBCuotaUtilizadaDatos.oDocumentoTitular = oDocumento;

                if (oSBCuotaUtilizadaDatos.nEstadoCivil == "2")
                {
                    oDocumento = new wsScoringBuro.ELDocumento();
                    oDocumento.nTipoDoc = nTipoDocumentoConyuge.ToString();
                    oDocumento.sNroDoc = cDocumentoConyuge;
                    oSBCuotaUtilizadaDatos.oDocumentoConyuge = oDocumento;
                }

                oSBCuotaUtilizadaResultado = oScoringBuro.ScoringBuroCuotaUtilizada(oSBCuotaUtilizadaDatos);

                if (oSBCuotaUtilizadaResultado.bError == true)
                {
                    cMensajeError = "ScoringBuroCuotaUtilizada Error: " + oSBCuotaUtilizadaResultado.sMensajeError;
                    return false;
                }

                var dgvDatos = oSBCuotaUtilizadaResultado.DatosCuotaUtilizada;

                cXmlScoringCuota = "<GENESYS>";
                if (dgvDatos != null)
                {
                    for (var i = 0; i <= dgvDatos.Length - 1; i++)
                    {
                        cXmlScoringCuota = cXmlScoringCuota + "<DATA cTipoDescripcion = '" + dgvDatos[i].sTipoDescripcion + "' nMonto = '" + dgvDatos[i].nMonto + "' " +
                                                            "nPlazo = '" + dgvDatos[i].nPlazo + "' nTasa = '" + dgvDatos[i].nTasa + "' " +
                                                            "nFactorUtilizacion = '" + dgvDatos[i].nFactorUtilizacion + "' nCuota = '" + dgvDatos[i].nCuota + "' " +
                                                            "nTipoId = '" + dgvDatos[i].nTipoId + "' ></DATA>";
                        nMontoTotal = nMontoTotal + Convert.ToDouble(dgvDatos[i].nMonto);
                    }
                }

                cXmlScoringCuota = cXmlScoringCuota + "</GENESYS>";

                nCuotaUtilizada1 = oSBCuotaUtilizadaResultado.nCuotaUtilizada;
                nCuotaUtilizada2 = oSBCuotaUtilizadaResultado.nCuotaUtilizada;
            }
            catch (Exception ex)
            {
                cMensajeError = "ScoringBuroCuotaUtilizada Error: " + ex.Message;
                return false;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (nScoreBuro > 0) //BANCARIZADO
            {
                try
                {
                    ELScoringBuroDatos oBuroIP = new ELScoringBuroDatos();
                    ELScoringBuroResultado oBuroIPReturn = new ELScoringBuroResultado();

                    ELCPDMatrizDatos oMatriz = new ELCPDMatrizDatos();
                    ELCPDMatrizResultado oMatrizResultado = new ELCPDMatrizResultado();

                    cTipoBanca = "BANCARIZADO";

                    nTipoBanca = 1;

                    oMatriz.nTipoScore = nTipoBanca.ToString();

                    oMatriz.nSitucionLaboral = nCondicion.ToString();
                    oMatriz.nCodicionSituacionlaboral = nCondicionSituacionLaboral.ToString();

                    oMatriz.oProducto = new wsCPD.ELProducto();
                    oMatriz.oProducto.nProducto = nProducto.ToString();
                    oMatriz.oProducto.nModalidad = nModalidad.ToString();

                    oMatrizResultado = oScoreCPD.DevuelveRequiereCPD(oMatriz);

                    if (oMatrizResultado.bError == true)
                    {
                        cMensajeError = "DevuelveRequiereCPD Error: " + oMatrizResultado.sMensajeError;
                        return false;
                    }

                    nValorNecesario = oMatrizResultado.bReqCPD ? 1 : 0;

                    wsScoringBuro.ELDocumento oDocumento = new wsScoringBuro.ELDocumento();
                    oDocumento.nTipoDoc = nTipoDocumento.ToString();
                    oDocumento.sNroDoc = cDocumento;
                    oBuroIP.oDocumentoTitual = oDocumento;

                    ELScoreDemograficoResultado oScoreDemoRes = new ELScoreDemograficoResultado();
                    wsScoreDemografico.ELDocumento oMCDocumento = new wsScoreDemografico.ELDocumento();

                    oMCDocumento.nTipoDoc = nTipoDocumento.ToString();
                    oMCDocumento.sNroDoc = cDocumento;

                    nMora = oScoreDemografo.DevuelveMoraComercial(oMCDocumento);

                    oBuroIP.nGarantia = "0";
                    oBuroIP.nMoraComercial = nMora.ToString();
                    oBuroIP.nScore = nScoreBuro.ToString();
                    oBuroIP.nCuotaUtilizada = oSBCuotaUtilizadaResultado.nCuotaUtilizada.ToString();
                    oBuroIP.nIngresoPredecido = nIngFinal3.ToString();
                    oBuroIP.nProducto = nProducto.ToString();
                    oBuroIP.nModalidad = nModalidad.ToString();
                    oBuroIP.nScoreOtros = nScoreLendo.ToString();
                    oBuroIP.nModalidadLaboral = nTipoDependiente;
                    oBuroIP.nTipoFormalidad = nTipoFormal;

                    oBuroIPReturn = oScoringBuro.ScoringBuro(oBuroIP);

                    if (oBuroIPReturn.bError == true)
                    {
                        cMensajeError = "ScoringBuro Error: " + oBuroIPReturn.sMensajeError;
                        return false;
                    }

                    nCuotaMaxima = oBuroIPReturn.nCuotaMaxima;
                    nCuotaDisp = oBuroIPReturn.nCuotaDisponible;
                    nPrestamo1 = oBuroIPReturn.nPrestamo1;
                    nPrestamo2 = oBuroIPReturn.nPrestamo2;
                    nPrestamo3 = oBuroIPReturn.nPrestamo3;
                    nPrestamo4 = oBuroIPReturn.nPrestamo4;
                    nPrestamoMinimo = oBuroIPReturn.nPrestamoMinimo;
                    nTasa = oBuroIPReturn.nTasa;
                    nPlazo = oBuroIPReturn.nPlazo;
                    nRCI = oBuroIPReturn.nRCI;
                    nPrestamoMax = oBuroIPReturn.nPrestamoMaximo;
                    nRMA = oBuroIPReturn.nRMA;
                    nPorGarantia = oBuroIPReturn.nPorcGarantiaAvaluo;
                    nPrestamoFinal = oBuroIPReturn.nPrestamo4;
                    cMotivoRechazado = oBuroIPReturn.sDescripcionRechazo;

                    ELScoreDemograficoDatos oDemoIP = new ELScoreDemograficoDatos();
                    ELScoreDemograficoResultado oDemoIPReturn = new ELScoreDemograficoResultado();

                    oDemoIP.nCondicionLaboral = nCondicion.ToString();
                    oDemoIP.nGenero = nGenero.ToString();

                    if (string.IsNullOrEmpty(cDistrito)) oDemoIP.nDepartamento = cDepartamento;
                    else
                    {
                        if (cDistrito.Substring(0, 2) == "15") oDemoIP.nDepartamento = cDistrito;
                        else
                        {
                            oDemoIP.nDepartamento = cDepartamento;
                        }
                    }

                    wsScoreDemografico.ELDocumento oDocumentoScoDemo = new wsScoreDemografico.ELDocumento();

                    oDocumentoScoDemo.nTipoDoc = nTipoDocumento.ToString();
                    oDocumentoScoDemo.sNroDoc = cDocumento;
                    oDemoIP.oDocumento = oDocumentoScoDemo;

                    nMora = oScoreDemografo.DevuelveMoraComercial(oDocumentoScoDemo);

                    oDemoIP.nIngresoSalarial = nIngFinal2.ToString();
                    oDemoIP.nEdad = nEdad.ToString();
                    oDemoIP.nMoraComercial = nMora.ToString();
                    oDemoIP.nEstadoCivil = nEstadoCivil.ToString();
                    oDemoIP.nVivienda = nVivienda.ToString();
                    oDemoIP.nSituacionLaboral = nCondicionSituacionLaboral.ToString();
                    oDemoIP.nCondicionLaboral = nCondicion.ToString();
                    oDemoIP.nGenero = nGenero.ToString();
                    oDemoIP.nDepartamento = cDistrito.Substring(0, 6);

                    oDemoIPReturn = oScoreDemografo.ScoreDemografico(oDemoIP);

                    if (oDemoIPReturn.bError == true)
                    {
                        cMensajeError = "ScoreDemografico Error: " + oDemoIPReturn.sMensajeError;
                        return false;
                    }

                    nSumatoria = Convert.ToDouble(oDemoIPReturn.nScoreDemografico);
                    nScoreDemografico = nSumatoria;                   

                    if (oBuroIPReturn.nDecicion == 1) cDecision = "APROBADO";
                    else if (oBuroIPReturn.nDecicion == 2) cDecision = "RECHAZADO";
                    else
                    {
                        cDecision = "INDECISO";
                    }
                }
                catch (Exception ex)
                {
                    cMensajeError = "Error: " + ex.Message;
                    return false;
                }
            }
            else // NO BANCARIZADO
            {
                try
                {
                    ELScoreDemograficoDatos oDemoIP = new ELScoreDemograficoDatos();
                    ELScoreDemograficoResultado oDemoIPReturn = new ELScoreDemograficoResultado();

                    cTipoBanca = "NO BANCARIZADO";

                    oDemoIP.nCondicionLaboral = nCondicion.ToString();
                    oDemoIP.nGenero = nGenero.ToString();

                    if (string.IsNullOrEmpty(cDistrito)) oDemoIP.nDepartamento = cDepartamento;
                    else
                    {
                        if (cDistrito.Substring(0, 2) == "15") oDemoIP.nDepartamento = cDistrito;
                        else
                        {
                            oDemoIP.nDepartamento = cDepartamento;
                        }
                    }
                    
                    nTipoBanca = 2;
                    
                    wsScoreDemografico.ELDocumento oDocumento = new wsScoreDemografico.ELDocumento();

                    oDocumento.nTipoDoc = nTipoDocumento.ToString();
                    oDocumento.sNroDoc = cDocumento;
                    oDemoIP.oDocumento = oDocumento;

                    nMora = oScoreDemografo.DevuelveMoraComercial(oDocumento);

                    oDemoIP.nIngresoSalarial = nIngFinal2.ToString();
                    oDemoIP.nEdad = nEdad.ToString();
                    oDemoIP.nMoraComercial = nMora.ToString();
                    oDemoIP.nEstadoCivil = nEstadoCivil.ToString();
                    oDemoIP.nVivienda = nVivienda.ToString();
                    oDemoIP.nSituacionLaboral = nCondicionSituacionLaboral.ToString();
                    oDemoIP.nCondicionLaboral = nCondicion.ToString();
                    oDemoIP.nGenero = nGenero.ToString();
                    oDemoIP.nDepartamento = cDistrito.Substring(0, 6);

                    oDemoIPReturn = oScoreDemografo.ScoreDemografico(oDemoIP);

                    if (oDemoIPReturn.bError == true)
                    {
                        cMensajeError = "ScoreDemografico Error: " + oDemoIPReturn.sMensajeError;
                        return false;
                    }

                    cXmlScoringDemo = "<GENESYS>";
                    if (oDemoIPReturn.oScoreItems != null)
                    {
                        for (var k = 0; k <= oDemoIPReturn.oScoreItems.Length - 1; k++)
                        {
                            cXmlScoringDemo = cXmlScoringDemo + "<DATA nScoreID = '" + oDemoIPReturn.oScoreItems[k].nScoreId + "' cScore = '" + oDemoIPReturn.oScoreItems[k].sScore + "' " +
                                                            "cScoreDescripcion = '" + oDemoIPReturn.oScoreItems[k].sScoreDescripcion + "' nScorePuntaje = '" + oDemoIPReturn.oScoreItems[k].nScorePuntaje + "' " +
                                                            "></DATA>";
                        }
                    }
                    cXmlScoringDemo = cXmlScoringDemo + "</GENESYS>";

                    nSumatoria = Convert.ToDouble(oDemoIPReturn.nScoreDemografico);
                    nScoreDemografico = nSumatoria;
                }
                catch (Exception ex)
                {
                    cMensajeError = "ScoreDemografico Error: " + ex.Message;
                    return false;
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ELScoringDemograficoDatos oScoDemoIP = new ELScoringDemograficoDatos();
                ELScoringDemograficoResultado oScoDemoIPReturn = new ELScoringDemograficoResultado();

                try
                {
                    ELCPDMatrizDatos oMatriz = new ELCPDMatrizDatos();
                    ELCPDMatrizResultado oMatrizResultado = new ELCPDMatrizResultado();

                    oMatriz.nTipoScore = nTipoBanca.ToString();
                    oMatriz.nSitucionLaboral = nCondicion.ToString();
                    oMatriz.nCodicionSituacionlaboral = nCondicionSituacionLaboral.ToString();
                    oMatriz.oProducto = new wsCPD.ELProducto();
                    oMatriz.oProducto.nProducto = nProducto.ToString();
                    oMatriz.oProducto.nModalidad = nModalidad.ToString();
                    oMatrizResultado = oScoreCPD.DevuelveRequiereCPD(oMatriz);

                    if(oMatrizResultado.bError == true)
                    {
                        cMensajeError = "DevuelveRequiereCPD Error: " + oMatrizResultado.sMensajeError;
                        return false;
                    }

                    nValorNecesario = oMatrizResultado.bReqCPD ? 1 : 0;

                    wsScoringDemografico.ELDocumento oDocumento = new wsScoringDemografico.ELDocumento();
                    oDocumento.nTipoDoc = nTipoDocumento.ToString();
                    oDocumento.sNroDoc = cDocumento;
                    oScoDemoIP.oDocumentoTitual = oDocumento;

                    oScoDemoIP.nGarantia = "0";
                    oScoDemoIP.nMoraComercial = nMora.ToString();
                    oScoDemoIP.nScore = nSumatoria.ToString();
                    oScoDemoIP.nIngresoPredecido = nIngFinal3.ToString();
                    oScoDemoIP.nProducto = nProducto.ToString();
                    oScoDemoIP.nModalidad = nModalidad.ToString();
                    oScoDemoIP.nScoreOtros = nScoreLendo.ToString();
                    oScoDemoIP.nModalidadLaboral = nTipoDependiente;
                    oScoDemoIP.nTipoFormalidad = nTipoFormal;

                    oScoDemoIPReturn = oScoringDemografico.ScoringDemografico(oScoDemoIP);

                    if (oScoDemoIPReturn.bError == true)
                    {
                        cMensajeError = "ScoringDemografico Error: " + oScoDemoIPReturn.sMensajeError;
                        return false;
                    }

                    nCuotaMaxima = oScoDemoIPReturn.nCuotaMaxima;
                    nCuotaDisp = oScoDemoIPReturn.nCuotaDisponible;
                    nPrestamo1 = oScoDemoIPReturn.nPrestamo1;
                    nPrestamo2 = oScoDemoIPReturn.nPrestamo2;
                    nPrestamo3 = oScoDemoIPReturn.nPrestamo3;
                    nPrestamoFinal = oScoDemoIPReturn.nPrestamo4;
                    nPrestamoMinimo = oScoDemoIPReturn.nPrestamoMinimo;
                    nTasa = oScoDemoIPReturn.nTasa;
                    nPlazo = oScoDemoIPReturn.nPlazo;
                    nPrestamo4 = oScoDemoIPReturn.nPrestamo4;
                    nRCI = oScoDemoIPReturn.nRCI;
                    nPrestamoMax = oScoDemoIPReturn.nPrestamoMaximo;
                    nPorGarantia = oScoDemoIPReturn.nPorcGarantiaAvaluo;
                    nRMA = oScoDemoIPReturn.nRMA;
                    cMotivoRechazado = oScoDemoIPReturn.sDescripcionRechazo;

                    if (oScoDemoIPReturn.nDecicion == 1) cDecision = "APROBADO";
                    else if (oScoDemoIPReturn.nDecicion == 2) cDecision = "RECHAZADO";
                    else
                    {
                        cDecision = "INDECISO";
                    }
                }
                catch (Exception ex)
                {
                    cMensajeError = "Error: " + ex.Message;
                }
            }

            if (nCodPers == 0)
            {
                nScoreComportamiento = 0;
            }
            else
            {
                nScoreComportamiento = 0;
                ELScoreComportamientoDatos oScoreComportamiento = new ELScoreComportamientoDatos();
                ELScoreComportamientoResultado oScoreComportamientoResultado = new ELScoreComportamientoResultado();
                try
                {
                    oScoreComportamiento.nCodPers = nCodPers.ToString();
                    oScoreComportamiento.nProducto = nProducto.ToString();

                    oScoreComportamientoResultado = oComportamiento.DevuelveScoreComportamiento(oScoreComportamiento);

                    if (oScoreComportamientoResultado.bError == true)
                    {
                        cMensajeError = "DevuelveScoreComportamiento Error: " + oScoreComportamientoResultado.sMensajeError;
                        return false;
                    }

                    nScoreComportamiento = oScoreComportamientoResultado.nScoreComportamiento;
                }
                catch (Exception ex)
                {
                    cMensajeError = "DevuelveScoreComportamiento Error: " + ex.Message;
                    return false;
                }
            }

            ELScoringValidacionReglasDatos oSReglas = new ELScoringValidacionReglasDatos();
            ELScoringValidacionReglasResultado oSReglasResultado = new ELScoringValidacionReglasResultado();
            ELClientePEPResultado oSReglaPEP = new ELClientePEPResultado();
            try
            {
                wsScoringValidacionReglas.ELDocumento oDocumentoValReglas = new wsScoringValidacionReglas.ELDocumento();
                wsScoringValidacionReglas.ELProducto oProductoValReglas = new wsScoringValidacionReglas.ELProducto();

                oDocumentoValReglas.nTipoDoc = "1";
                oDocumentoValReglas.sNroDoc = cDocumento;
                oSReglas.oDocumento = oDocumentoValReglas;

                oSReglaPEP = oReglas.ValidaClientePEP(oDocumentoValReglas);

                if(oSReglaPEP.oError.bError == true)
                {
                    cMensajeError = "ValidaClientePEP Error: " + oSReglaPEP.oError.sMensajeError;
                    return false;
                }

                nClientePEP = oSReglaPEP.bEsPEP == true ? 1 : 0;
                nPEP = nClientePEP;

                oProductoValReglas.nModalidad = nModalidad.ToString();
                oProductoValReglas.nProducto = nProducto.ToString();
                oSReglas.oProducto = oProductoValReglas;
                oSReglas.nTipoScoring = (cTipoBanca.ToUpper().Equals("NO BANCARIZADO") ? 2 : 1).ToString();

                oSReglasResultado = oReglas.ScoringValidacionReglasRechazo(oSReglas);

                if (oSReglasResultado.bError == true)
                {
                    cMensajeError = "ScoringValidacionReglasRechazo Error: " + oSReglasResultado.sMensajeError;
                    return false;
                }

                nIdRechazado = oSReglasResultado.nIdRechazo;
                cDecisionReglas = oSReglasResultado.sDescRechazo;

                if (oSReglasResultado.nIdRechazo != 0) cDecision = "RECHAZADO";
                else
                {
                    if (cDecision == "INDECISO") cDecision = "INDECISO";
                    else
                    {
                        if (cDecision != "APROBADO") cDecision = "RECHAZADO";
                    }
                }

            }
            catch (Exception ex)
            {
                cMensajeError = "ScoringValidacionReglasRechazo Error: " + ex.Message;
                return false;
            }

            if (cDecision == "RECHAZADO") nRechazado = 1;
            cXmlScoringDatos = devuelveXMLDatos(cDocumento);

            return true;
        }

        public bool preEvaluacion(string cDocumento, int nProducto, int nModalidad, ref string cResultado, ref string cMensajeError)
        {
            try
            {
                ELPreAprobacionResultado oPreAprobacionResultado = new ELPreAprobacionResultado();

                wsPreAprobacion.ELDocumento oPreAprobacionDocumento = new wsPreAprobacion.ELDocumento();
                oPreAprobacionDocumento.sNroDoc = cDocumento;
                oPreAprobacionDocumento.nTipoDoc = "1";

                wsPreAprobacion.ELProducto oPreAprobacionProducto = new wsPreAprobacion.ELProducto();
                oPreAprobacionProducto.nProducto = nProducto.ToString();
                oPreAprobacionProducto.nModalidad = nModalidad.ToString();

                oPreAprobacionResultado = oPreAprobacion.DevuelvePreAprobacion(oPreAprobacionDocumento, oPreAprobacionProducto);


                if (oPreAprobacionResultado.bError == true)
                {
                    cMensajeError = "DevuelvePreAprobacion Error: " + oPreAprobacionResultado.sMensajeError;
                    return false;
                }
                else
                {
                    cResultado = oPreAprobacionResultado.cDecision + "|" + oPreAprobacionResultado.cTipoScoring + "|" + oPreAprobacionResultado.nPrestamoMaximo + "|" + oPreAprobacionResultado.nPrestamoMinimo;
                }
            }
            catch (Exception ex)
            {
                cMensajeError = "DevuelvePreAprobacion Error: " + ex.Message;
                return false;
            }
            return true;
        }
    }
}