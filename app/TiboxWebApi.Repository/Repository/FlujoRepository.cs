using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using TiboxWebApi.Models;
using TiboxWebApi.Repository.Interfaces;

namespace TiboxWebApi.Repository.Repository
{
    public class FlujoRepository : BaseRepository<FlujoMaestro>, IFlujoRepository
    {
        public int EliminaFlujo(FlujoMaestro flujo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nIdFlujoMaestro", flujo.nIdFlujoMaestro);
                parameters.Add("@cComentario", flujo.cComentario);
                parameters.Add("@cUser", flujo.cUsuReg);
                parameters.Add("@nRes", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Query<int>("WebOnline_insAnularCreditoFlujo_SP", parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<int>("@nRes");
            }
        }

        public IEnumerable<FlujoMaestro> LucasRecuperaFlujo(int nIdFlujoMaestro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nIdFlujoMaestro", nIdFlujoMaestro);
                return connection.Query<FlujoMaestro>("WebApi_ObtenerFLujoSiguiente_SP", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<FlujoMaestro> LucasRecuperaSolicitud(int nIdFlujoMaestro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nIdFlujoMaestro", nIdFlujoMaestro);
                return connection.Query<FlujoMaestro>("WebApi_LucasDatosSolicitud_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int LucasRegistraFlujo(FlujoMaestro flujo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nProd", flujo.nProd);
                parameters.Add("@nSubProd", flujo.nSubProd);
                parameters.Add("@cNombFlujo", flujo.cNomform);
                parameters.Add("@nCodPers", flujo.nCodPers);
                parameters.Add("@cDoc", flujo.nNroDoc);
                parameters.Add("@cUsuReg", flujo.cUsuReg);
                parameters.Add("@nIdFlujo", flujo.nIdFlujo);
                parameters.Add("@nCodCred", flujo.nCodCred);
                parameters.Add("@nCodAge", flujo.nCodAge);
                parameters.Add("@nCodPersReg", flujo.nCodPersReg);
                parameters.Add("@nOrdenFlujo", flujo.nOrdenFlujo);
                parameters.Add("@nValorNecesario", 1);
                parameters.Add("@cTipoBanca", "");
                parameters.Add("@nRechazado", 0);
                parameters.Add("@nIdFlujoMaestro", flujo.nIdFlujoMaestro);
                parameters.Add("@nResultado", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Query<int>("WebApi_FlujoRegistra_SP", parameters, commandType: CommandType.StoredProcedure);

                var res = parameters.Get<int>("@nResultado");
                return res;
            }
        }

        public int LucasRegistraMotor(FlujoMaestro flujo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var nIdFlujoMaestro = 0;

                        var parameters = new DynamicParameters();
                        parameters.Add("@nNroDoc", flujo.nNroDoc);
                        parameters.Add("@nCodAge", flujo.nCodAge);
                        parameters.Add("@nProd", flujo.nProd);
                        parameters.Add("@nSubProd", flujo.nSubProd);
                        parameters.Add("@cNomForm", flujo.cNomform);
                        parameters.Add("@nCodCred", flujo.nCodCred);
                        parameters.Add("@cUsuReg", flujo.cUsuReg);
                        parameters.Add("@nIdFlujo", flujo.nIdFlujo);
                        parameters.Add("@nCodPersReg", flujo.nCodPersReg);
                        parameters.Add("@nOrdenFlujo", flujo.nOrdenFlujo);
                        parameters.Add("@oScoringDatos", flujo.oScoringDatos, dbType: DbType.Xml);
                        parameters.Add("@oScoringVarDemo", flujo.oScoringVarDemo, dbType: DbType.Xml);
                        parameters.Add("@oScoringDetCuota", flujo.oScoringDetCuota, dbType: DbType.Xml);
                        parameters.Add("@oScoringDemo", flujo.oScoringDemo, dbType: DbType.Xml);
                        parameters.Add("@oScoringRCC", flujo.oScoringRCC, dbType: DbType.Xml);
                        parameters.Add("@nRechazado", flujo.nRechazado);
                        parameters.Add("@cClienteLenddo", flujo.cClienteLenddo);
                        parameters.Add("@nIdFlujoMaestro", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Query<int>("WebApi_LucasIniciaFlujo_SP",
                            parameters,
                            commandType: CommandType.StoredProcedure, transaction: transaction);
                        nIdFlujoMaestro = parameters.Get<int>("@nIdFlujoMaestro");

                        transaction.Commit();

                        return nIdFlujoMaestro;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IEnumerable<FlujoMaestro> ObtieneWizard(int nIdFlujoMaestro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nIdFlujoMaestro", nIdFlujoMaestro);

                return connection.Query<FlujoMaestro>("WebApi_ObtieneWizard_SP", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
