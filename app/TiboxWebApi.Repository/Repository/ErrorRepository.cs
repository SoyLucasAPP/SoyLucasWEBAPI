using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TiboxWebApi.Models;
using TiboxWebApi.Repository.Interfaces;
using Dapper;

namespace TiboxWebApi.Repository.Repository
{
    public class ErrorRepository : BaseRepository<Error>, IErrorRepository
    {
        public int InsertaError(string Controlador, string cError)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@cDescripcion", Controlador);
                parameters.Add("@cControlador", cError);

                connection.Query<int>("WebApi_InsertaError_SP", parameters, commandType: CommandType.StoredProcedure);
                return 1;
            }
            
        }
    }
}
