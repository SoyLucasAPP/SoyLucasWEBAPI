using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using TiboxWebApi.Models;
using TiboxWebApi.Repository.Interfaces;
using System.Data;

namespace TiboxWebApi.Repository.Repository
{
    public class CatalogoCodigoRepository : BaseRepository<CatalogoCodigos>, ICatalogoCodigoRepository
    {
        public IEnumerable<CatalogoCodigos> selCatalogoCodigos(int nCodigo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nCodigo", nCodigo);

                return connection.Query<CatalogoCodigos>(
                    "dbo.WEBApi_selCatalogoCodigo_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<CatalogoCodigos> selTipovivienda()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<CatalogoCodigos>("WebApiLucas_TipoViviendaLista_SP", null, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
