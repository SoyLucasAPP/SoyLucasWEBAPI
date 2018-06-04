using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiboxWebApi.Models;
using TiboxWebApi.Repository.Interfaces;
using Dapper;

namespace TiboxWebApi.Repository.Repository
{
    public class ReglaNegocioRepository : BaseRepository<ReglaNegocio>, IReglaNegocioRepository
    {
        public IEnumerable<ReglaNegocio> ListaRegla(string cNomForm)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pnIdForm", cNomForm);

                return connection.Query<ReglaNegocio>("WebApi_ReglaNegocioSelecciona_SP", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
