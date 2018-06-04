using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace TiboxWebApi.Models
{
    [Table("WebPersonaLenddo")]
    public class WebPersonaLenddo
    {
        [Key]
        public int nCodigo { get; set; }
        public string cClienteIDLenddo { get; set; }
        public string cDocumento { get; set; }
        public int nIdFlujo { get; set; }
        public DateTime dFecha { get; set; }
        public double nScore { get; set; }
    }
}
