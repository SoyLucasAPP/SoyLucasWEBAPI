using System.Collections.Generic;
using TiboxWebApi.Models;

namespace TiboxWebApi.Repository.Interfaces
{
    public interface ICatalogoCodigoRepository : IRepository<CatalogoCodigos>
    {
        IEnumerable<CatalogoCodigos> selCatalogoCodigos(int nCodigo);
        IEnumerable<CatalogoCodigos> selTipovivienda();
    }
}
