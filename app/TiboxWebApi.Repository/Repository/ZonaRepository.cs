using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiboxWebApi.Models;
using TiboxWebApi.Repository.Interfaces;

namespace TiboxWebApi.Repository.Repository
{
    public class ZonaRepository : BaseRepository<Zona>, IZonaRepository
    {
        public IEnumerable<Zona> selDepartamento()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Zona>(
                    "WebApi_selDepartamento_SP", 
                    null, 
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Zona> selDistrito(string cDepartamento, string cProvincia)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@cCodDepartamento", cDepartamento);
                parameters.Add("@cCodProvincia", cProvincia);
                return connection.Query<Zona>(
                    "WebApi_selDistrito_SP", 
                    parameters, 
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Zona> selProvincia(string cDepartamento)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@cCodDepartamento", cDepartamento);
                return connection.Query<Zona>(
                    "WebApi_selProvincia_SP", 
                    parameters, 
                    commandType:System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
