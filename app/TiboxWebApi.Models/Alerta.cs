using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiboxWebApi.Models
{
    public class Alerta
    {
        public string cMovil { get; set; }
        public string cTexto { get; set; }
        public string cEmail { get; set; }
        public string cTitulo { get; set; }
    }
}
