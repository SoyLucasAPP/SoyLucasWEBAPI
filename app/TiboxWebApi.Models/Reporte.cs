using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiboxWebApi.Models
{
    public class Reporte
    {
        public int nCodCred { get; set; }
        public int nCodAge { get; set; }
        public int nPEP { get; set; }
        public string cEmail { get; set; }
        public string cNombres { get; set; }
        public double nPrestamo { get; set; }
        public bool bError { get; set; }
        public string cMensajeError { get; set; }
        public string oDocumento { get; set; }
    }
}
