using Dapper;
using System.Data;
using System.Data.SqlClient;
using TiboxWebApi.Models;
using TiboxWebApi.Repository.Interfaces;
using System.Collections.Generic;
using System;

namespace TiboxWebApi.Repository.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly Utiles _util = null;
        public UserRepository()
        {
            _util = new Utiles();
        }

        public int LucasCambiaPass(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var Encriptado = _util.Encriptar(password);
                var parametrs = new DynamicParameters();
                parametrs.Add("@cEmail", email);
                parametrs.Add("@cPass", password);
                parametrs.Add("@cPassEncriptado", Encriptado);
                parametrs.Add("@nCodPers", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Query<int>("WebApi_insCambioPass_SP", parametrs, commandType: CommandType.StoredProcedure);
                var nCodPers = parametrs.Get<int>("@nCodPers");
                return nCodPers;
            }
        }

        public IEnumerable<Persona> LucasDatosLogin(string cEmail)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@cEmail", cEmail);
                return connection.Query<Persona>("WebApi_LucasDatosLogin_SP", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<User> LucasVerificaEmail(string cEmail)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var paramaters = new DynamicParameters();
                paramaters.Add("@cEmail", cEmail);
                return connection.Query<User>("WebApi_LucasValidaCorreoExiste_SP", 
                    paramaters, 
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int selCambioPass(int nCodPers)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nCodPers", nCodPers);
                parameters.Add("@nTotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Query<int>("WebApi_selCambioPass_SP", parameters, commandType: CommandType.StoredProcedure);
                var total = parameters.Get<int>("@nTotal");
                return total;
            }
        }

        public User ValidateUser(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string pass = _util.Encriptar(password);
                var parameters = new DynamicParameters();
                parameters.Add("@email", email);
                parameters.Add("@password", pass);
                return connection.QueryFirstOrDefault<User>("WebApi_ValidateUser_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public User validateUserAD(string userName, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@cCodUsu", userName);

                return connection.QueryFirstOrDefault<User>("WebApi_DatosWinUsuario_SP", 
                    parameters, commandType: 
                    CommandType.StoredProcedure);
            }
        }
    }
}
