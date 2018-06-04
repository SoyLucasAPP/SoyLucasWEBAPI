using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TiboxWebApi.Models
{
    public class Documento
    {
        public int nCodigo { get; set; }
        public string cNombreDoc { get; set; }
        public int nCodAge { get; set; }
        public int nCodCred { get; set; }
        public byte[] iImagen { get; set; }
        public string cNomArchivo { get; set; }
        public string cExtencion { get; set; }
        public int nIdFlujoMaestro { get; set; }
        public string cTipoArchivo { get; set; }
        public string oDocumento { get; set; }
    }
}
