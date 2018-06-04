using TiboxWebApi.WebApi.wsSms;

namespace TiboxWebApi.WebApi.Utils
{
    public class Sms
    {
        private wsSMSSoap _sms;
        public Sms()
        {
            _sms = new wsSMSSoapClient();
        }

        public bool enviarSMS(string cMovil, string cMensaje)
        {
            EnviarMensajeRequestBody smsCuerpo = new EnviarMensajeRequestBody();
            smsCuerpo.Telefono = cMovil;
            smsCuerpo.Msg = cMensaje;

            var smsEnvia = new EnviarMensajeRequest();

            smsEnvia.Body = smsCuerpo;

            var result = _sms.EnviarMensaje(smsEnvia);

            if (!result.Body.EnviarMensajeResult) return false;

            return true;
        }
    }
}