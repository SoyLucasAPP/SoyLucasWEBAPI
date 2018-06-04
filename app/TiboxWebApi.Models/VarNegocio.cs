using Dapper.Contrib.Extensions;

namespace TiboxWebApi.Models
{
    [Table("VarNegocio")]
    public class VarNegocio
    {
        [Key]
        public int nCodVar { get; set; }
        public string cNomVar { get; set; }
        public string cValorVar { get; set; }
        public int nTipoVar { get; set; }
    }
}
