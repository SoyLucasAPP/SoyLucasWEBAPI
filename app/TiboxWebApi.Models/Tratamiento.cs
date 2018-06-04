using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiboxWebApi.Models
{
    public class Tratamiento
    {
        public int nCodPers { get; set; }
        public string cDocumento { get; set; }
        public string cUsuario { get; set; }
        public string cApePat { get; set; }
        public string cApeMat { get; set; }
        public string cNombres { get; set; }
        public int nCodAge { get; set; }
        public int nTipoSolicitud { get; set; }
        public int nModoRegistro { get; set; }
        public int nTipoResp { get; set; }
        public string cPedido { get; set; }
        public string cComentario { get; set; }
        public int nCodPersTit { get; set; }
        public string cApePatTit { get; set; }
        public string cApeMatTit { get; set; }
        public string cNomTit { get; set; }
        public string cDocumentoTit { get; set; }
        public int nCodSolicitud { get; set; }
    }
}
