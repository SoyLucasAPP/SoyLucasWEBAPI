using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace TiboxWebApi.WebApi.Utils
{
    public class Email
    {
        public bool envioEmail(string cEmail, string cCuerpo, string cTitulo, ref string cMensajeError)
        {
            string SMTP = System.Configuration.ConfigurationManager.AppSettings["SMTP"].ToString();
            int PUERTO = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PUERTO"]);
            string CORREO = System.Configuration.ConfigurationManager.AppSettings["CORREO"].ToString();
            string CORREO_CREDENCIALES = System.Configuration.ConfigurationManager.AppSettings["CORREO_CREDENCIALES"].ToString();
            string CLAVE_CREDENCIALES = System.Configuration.ConfigurationManager.AppSettings["CLAVE_CREDENCIALES"].ToString();
            string NOMBRE = System.Configuration.ConfigurationManager.AppSettings["NOMBRE"].ToString();

            string Body = "";
            Body = cCuerpo;

            try
            {
                SmtpClient server = new SmtpClient(SMTP, PUERTO);
                server.Credentials = new System.Net.NetworkCredential(CORREO_CREDENCIALES, CLAVE_CREDENCIALES);
                server.EnableSsl = true;
                MailMessage mnsj = new MailMessage();
                mnsj.Subject = cTitulo;
                mnsj.To.Add(new MailAddress(cEmail));
                mnsj.From = new MailAddress(CORREO, NOMBRE);
                mnsj.IsBodyHtml = true;
                mnsj.Body = Body;
                server.Send(mnsj);
            }
            catch (Exception ex)
            {
                cMensajeError = ex.Message;
                return false;
            }
            return true;
        }
    }
}