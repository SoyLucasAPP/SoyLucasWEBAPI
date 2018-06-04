using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiboxWebApi.Models;

namespace TiboxWebApi.Repository.Interfaces
{
    public interface IDocumentoRepository: IRepository<Documento>
    {
        IEnumerable<Documento> ListaDocumentos();
        int LucasInsDocumento(Documento documento);
    }
}
