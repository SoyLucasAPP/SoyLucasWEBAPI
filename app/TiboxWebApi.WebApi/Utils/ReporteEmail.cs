using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using TiboxWebApi.UnitOfWork;

namespace TiboxWebApi.WebApi.Utils
{
    public class ReporteEmail
    {
        private readonly IUnitOfWork _unit;

        public ReporteEmail(IUnitOfWork unit)
        {
            _unit = unit;
        }

        string SMTP = System.Configuration.ConfigurationManager.AppSettings["SMTP"].ToString();
        int PUERTO = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PUERTO"]);
        string CORREO = System.Configuration.ConfigurationManager.AppSettings["CORREO"].ToString();
        string CORREO_CREDENCIALES = System.Configuration.ConfigurationManager.AppSettings["CORREO_CREDENCIALES"].ToString();
        string CLAVE_CREDENCIALES = System.Configuration.ConfigurationManager.AppSettings["CLAVE_CREDENCIALES"].ToString();
        string NOMBRE = System.Configuration.ConfigurationManager.AppSettings["NOMBRE"].ToString();
        string USER_REPORTE = System.Configuration.ConfigurationManager.AppSettings["USER_REPORTE"].ToString();
        string PASS_REPORTE = System.Configuration.ConfigurationManager.AppSettings["PASS_REPORTE"].ToString();
        string DOMINIO = System.Configuration.ConfigurationManager.AppSettings["DOMINIO"].ToString();

        enum ReportFormat { PDF = 1, Word = 2, Excel = 3 }

        public string LastmimeType { get { return mimeType; } }

        private string mimeType;

        public Dictionary<int, string> listaDictionary()
        {
            //SI SE DESEA MAS REPORTES AGREGAR AQUÍ
            var ArrayString1 = new string[] {
                "/WEB/Hoja_Resumen_Informativa_Anexo_1_ONL",
                "/WEB/Hoja_Resumen_Informativa_Anexo_2_ONL",
                "/WEB/001_FichaDeSolicitudDePrestamo",
                "/WEB/002_ContratoPrestamoOnline",
                "/WEB/003_CertificadoDeSeguroDesgravamen",
                "/WEB/004_FormularioIdentificacionPersonas",
            };

            Dictionary<int, string> dListDictionary = new Dictionary<int, string>();
            for (int i = 0; i < ArrayString1.Length; i++)
            {
                dListDictionary.Add(i, ArrayString1[i]);
            }
            return dListDictionary;
        }

        private byte[] ReporteABytes(int nCodAge, int nCodCred, string cReporte)
        {
            byte[] renderedBytes;

            ReportViewer rs = new ReportViewer();

            try
            {
                ICredentials Credentials = null;
                Credentials = CredentialCache.DefaultCredentials;

                NetworkCredential credentiales = new NetworkCredential();
                credentiales.UserName = USER_REPORTE;
                credentiales.Password = PASS_REPORTE;
                credentiales.Domain = DOMINIO;

                rs.ShowCredentialPrompts = false;

                var oVarNegocio = _unit.VarNegocio.GetEntityById(2032);

                rs.ServerReport.ReportServerUrl = new Uri(oVarNegocio.cValorVar);
                rs.ServerReport.ReportPath = cReporte;
                rs.ServerReport.ReportServerCredentials.NetworkCredentials = credentiales;

                var listParam = new List<ReportParameter>();

                var param1 = new ReportParameter("nCodCred", nCodCred.ToString());
                listParam.Add(param1);
                var param2 = new ReportParameter("nCodAge", nCodAge.ToString());
                listParam.Add(param2);

                rs.ServerReport.SetParameters(listParam);
                rs.RefreshReport();

                var formatopdf = ReportFormat.PDF;
                string formato = formatopdf.ToString();
                string reportType = formatopdf.ToString();
                string encoding;
                string fileNameExtension;
                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>" + formatopdf.ToString() + "</OutputFormat>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;

                renderedBytes = rs.ServerReport.Render(formato,
                           deviceInfo,
                           out mimeType,
                           out encoding,
                           out fileNameExtension,
                           out streams,
                           out warnings);

            }
            catch (Exception)
            {
                renderedBytes = null;
                throw;
            }
            return renderedBytes;
        }

        private string generaCuerpoEmail(string cNombre, double nPrestamo)
        {
            var cuerpo = "<table border='0' style='width: 100%; background: #f1f1f1; font-family: verdana'>" +
                                "<tr>" +
                                    "<td>" +
                                        "<table cellspacing='0' cellpadding='0' border='0' style='margin: 0 auto; width: 85%'>" +
                                            "<tr style='background: #FD293F'>" +
                                                "<td style='padding: 25px'>" +
                                                    "<h1 style='text-transform: uppercase; text-align: center; color: white; margin: 0 auto'>¡FELICIDADES!</h1>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='background: white'>" +
                                                "<td style='padding: 15px;'>" +
                                                    "<h2 style='background: white; text-transform: uppercase; text-align: center'>HOLA " + cNombre.ToUpper() + "</h2>" +
                                                    "<h4 style='font-weight: 500; text-transform: uppercase; text-align: center'><i>¡Solicitud generada!</i></h4>" +
                                                    "<p style='text-align: justify; padding: 15px'>Tu solicitud de pr&eacute;stamo por S/ " + string.Format("{0:#,#.00}", nPrestamo) + " ha sido generada, Se adjuntan los documentos contractuales.</p>" +
                                                    "<p>Gracias por confiar en nosotros!</p>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='background: #454544; text-transform: uppercase; text-align: center'>" +
                                                "<td style='padding: 25px;'>" +
                                                    "<button style='text-align: center; color: #fff; font-size: 14px; font-weight: 500;  border:none; padding: 10px 20px; background-color: #FFB700; font-size: 25px'>" +
                                                    "<span style='color: #454544; text-transform: uppercase; font-weight: bold; font-size: 25px'>Al&oacute; Lucas:</span> 01 615-7030</button>" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</td>" +
                                "</tr>" +
                            "</table>";

            return cuerpo;
        }

        public bool generaReportes(int nCodCred, int nCodAge, int nPEP, ref bool bError, ref string cMensajeError)
        {
            bool bRespuesta = true;
            MemoryStream datoFile = null;
            List<Attachment> lstFiles = new List<Attachment>();
            List<string> lstArchivos = new List<string>();
            List<object> lstArchivos1 = new List<object>();
            List<string> lstArchivos2 = new List<string>();
            List<DocumentosReporte> lstDocumentos = new List<DocumentosReporte>();
            Attachment anexo = null;
            Dictionary<int, string> lstDiccionario = null;
            DocumentosReporte documentos = new DocumentosReporte();

            lstDiccionario = listaDictionary();
            string cNombreDocumento = "";

            try
            {
                foreach (var item in lstDiccionario)
                {
                    if (item.Value == "/WEB/004_FormularioIdentificacionPersonas" && nPEP == 0)
                    {
                        //nada
                    }
                    else
                    {
                        var renderedbytes = ReporteABytes(nCodAge, nCodCred, item.Value);
                        datoFile = new MemoryStream(renderedbytes);
                        datoFile.Seek(0, SeekOrigin.Begin);
                        var splited = item.Value.Split('/');
                        cNombreDocumento = splited[2] + ".PDF";
                        anexo = new Attachment(datoFile, cNombreDocumento);
                        lstFiles.Add(anexo);
                        documentos = new DocumentosReporte();
                        documentos.nombre = cNombreDocumento;
                        documentos.doc = renderedbytes;
                        lstDocumentos.Add(documentos);
                    }

                }


                _unit.Reporte.LucasInsCabeceraReporte(nCodAge, nCodCred, "¡Felicitaciones!, tu préstamo ha sido aprobado.", "");

                var nTipoDoc = 0;
                for (int i = 0; i < lstDocumentos.Count; i++)
                {
                    if (lstDocumentos[i].nombre == "Hoja_Resumen_Informativa_Anexo_1_ONL.PDF") { nTipoDoc = 1; }
                    if (lstDocumentos[i].nombre == "Hoja_Resumen_Informativa_Anexo_2_ONL.PDF") { nTipoDoc = 2; }
                    if (lstDocumentos[i].nombre == "001_FichaDeSolicitudDePrestamo.PDF") { nTipoDoc = 6; }
                    if (lstDocumentos[i].nombre == "002_ContratoPrestamoOnline.PDF") { nTipoDoc = 5; }
                    if (lstDocumentos[i].nombre == "003_CertificadoDeSeguroDesgravamen.PDF") { nTipoDoc = 7; }
                    if (lstDocumentos[i].nombre == "004_FormularioIdentificacionPersonas.PDF") { nTipoDoc = 8; }

                    _unit.Reporte.LucasInsDetalleReporte(nCodAge, nCodCred, nTipoDoc, lstDocumentos[i].doc);
                }

            }
            catch (Exception ex)
            {
                bError = true;
                cMensajeError = "Error: " + ex.Message;
                bRespuesta = false;
                _unit.Error.InsertaError("Reporte Controller - generaReportes", ex.Message);
            }

            return bRespuesta;
        }

        public bool EnviarReportePorEmail(int nCodCred, int nCodAge, string cEmail, string cnombres, double nPrestamo, int nPEP, ref bool bError, ref string cMensajeError)
        {
            bool bRespuesta = true;
            MemoryStream datoFile = null;
            List<Attachment> lstFiles = new List<Attachment>();
            List<string> lstArchivos = new List<string>();
            List<object> lstArchivos1 = new List<object>();
            List<string> lstArchivos2 = new List<string>();
            List<DocumentosReporte> lstDocumentos = new List<DocumentosReporte>();
            Attachment anexo = null;
            Dictionary<int, string> lstDiccionario = null;
            DocumentosReporte documentos = new DocumentosReporte();

            lstDiccionario = listaDictionary();
            string cNombreDocumento = "";

            string cBodyCliente = generaCuerpoEmail(cnombres, nPrestamo);
            try
            {
                foreach (var item in lstDiccionario)
                {
                    if (item.Value == "/WEB/004_FormularioIdentificacionPersonas" && nPEP == 0)
                    {
                        //nada
                    }
                    else
                    {
                        var renderedbytes = ReporteABytes(nCodAge, nCodCred, item.Value);
                        datoFile = new MemoryStream(renderedbytes);
                        datoFile.Seek(0, SeekOrigin.Begin);
                        var splited = item.Value.Split('/');
                        cNombreDocumento = splited[2] + ".PDF";
                        anexo = new Attachment(datoFile, cNombreDocumento);
                        lstFiles.Add(anexo);
                        documentos = new DocumentosReporte();
                        documentos.nombre = cNombreDocumento;
                        documentos.doc = renderedbytes;
                        lstDocumentos.Add(documentos);
                    }

                }

                using (var smtpCliente = new SmtpClient(SMTP, PUERTO))
                {
                    smtpCliente.Credentials = new NetworkCredential(CORREO_CREDENCIALES, CLAVE_CREDENCIALES);
                    smtpCliente.EnableSsl = true;

                    MailMessage mail = new MailMessage();
                    mail.Body = cBodyCliente;
                    mail.IsBodyHtml = true;
                    mail.To.Add(new MailAddress(cEmail));
                    mail.From = new MailAddress(CORREO, NOMBRE);
                    mail.Subject = "¡Felicitaciones!, tu préstamo ha sido aprobado.";
                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.Priority = MailPriority.Normal;
                    mail.IsBodyHtml = true;

                    foreach (var item in lstFiles)
                    {
                        mail.Attachments.Add(item);
                    }

                    smtpCliente.Send(mail);
                    bRespuesta = true;
                }

                if (bRespuesta)
                {
                    _unit.Reporte.LucasInsCabeceraReporte(nCodAge, nCodCred, "¡Felicitaciones!, tu préstamo ha sido aprobado.", cBodyCliente);

                    var nTipoDoc = 0;
                    for (int i = 0; i < lstDocumentos.Count; i++)
                    {
                        if (lstDocumentos[i].nombre == "Hoja_Resumen_Informativa_Anexo_1_ONL.PDF") { nTipoDoc = 1; }
                        if (lstDocumentos[i].nombre == "Hoja_Resumen_Informativa_Anexo_2_ONL.PDF") { nTipoDoc = 2; }
                        if (lstDocumentos[i].nombre == "001_FichaDeSolicitudDePrestamo.PDF") { nTipoDoc = 6; }
                        if (lstDocumentos[i].nombre == "002_ContratoPrestamoOnline.PDF") { nTipoDoc = 5; }
                        if (lstDocumentos[i].nombre == "003_CertificadoDeSeguroDesgravamen.PDF") { nTipoDoc = 7; }
                        if (lstDocumentos[i].nombre == "004_FormularioIdentificacionPersonas.PDF") { nTipoDoc = 8; }

                        _unit.Reporte.LucasInsDetalleReporte(nCodAge, nCodCred, nTipoDoc, lstDocumentos[i].doc);
                    }
                }
            }
            catch (Exception ex)
            {
                bError = true;
                cMensajeError = "Error: " + ex.Message;
                bRespuesta = false;
                _unit.Error.InsertaError("Reporte Controller - EnviarReportePorEmail", ex.Message);
            }

            return bRespuesta;
        }
    }
}