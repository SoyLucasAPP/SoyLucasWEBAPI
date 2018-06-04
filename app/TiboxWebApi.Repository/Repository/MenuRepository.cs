using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using TiboxWebApi.Models;
using TiboxWebApi.Repository.Interfaces;
using System.Data;

namespace TiboxWebApi.Repository.Repository
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public IEnumerable<Menu> SelMenus(string cCodUsu)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@username", cCodUsu);

                return connection.Query<Menu>("WebApiADM_Menu_SP", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
