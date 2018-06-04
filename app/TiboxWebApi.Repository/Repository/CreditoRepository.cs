using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using TiboxWebApi.Models;
using TiboxWebApi.Repository.Interfaces;
using Dapper;

using System.Xml;
using System;
using System.Transactions;

namespace TiboxWebApi.Repository.Repository
{
    public class CreditoRepository : BaseRepository<Credito>, ICreditoRepository
    {
        public readonly Utiles _utils = null;
        public CreditoRepository()
        {
            _utils = new Utiles();
        }

        public IEnumerable<Credito> LucasBandeja(int nCodPers, int nPagina, int nTam, int nCodAge)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nCodPers", nCodPers);
                parameters.Add("@nNumPagina", nPagina);
                parameters.Add("@nTamPagina", nTam);
                parameters.Add("@nCodAge", nCodAge);

                return connection.Query<Credito>("WebApi_LucasBandeja_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Credito> LucasCalendarioLista(int nCodAge, int nCodCred)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nCodCred", nCodCred);
                parameters.Add("@nCodAge", nCodAge);

                return connection.Query<Credito>("WebApi_LucasCalendarioLista_SP", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public int LucasCreditoAnulaxActualizacion(string cDocumento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var resultado = 0;
                var parameters = new DynamicParameters();
                parameters.Add("@cDocumento", cDocumento);

                connection.Query<int>("WebApi_LucasCreditosAnularxActualizacion_SP", parameters, commandType: CommandType.StoredProcedure);

                resultado = 1;

                return resultado;
            }
        }

        public int LucasCreditoEnFlujo(string cDocumento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@cDocumento", cDocumento);
                parameters.Add("@nRes", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Query<int>("WebApi_LucasCreditoPorFlujo_SP", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@nRes");
            }
        }

        public IEnumerable<Credito> LucasDatosPrestamo(int nCodAge, int nCodCred)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nCodAge", nCodAge);
                parameters.Add("@nCodCred", nCodCred);

                return connection.Query<Credito>("WebApi_LucasDatosCredito_SP", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public int LucasInsCredito(Credito credito)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var lstCalendario = new List<Calendario>();
                        lstCalendario = _utils.GeneraCalendario(credito.nPrestamo, credito.nNroCuotas, credito.nPeriodo, credito.nTasa, credito.dFechaSistema, credito.nSeguro);

                        string oCreditoXml = "";
                        var xml = new XmlDocument();

                        XmlElement root = xml.CreateElement("Credito");
                        xml.AppendChild(root);
                        foreach (var cust in lstCalendario)
                        {
                            XmlElement child = xml.CreateElement("Credito");
                            child.SetAttribute("FechaPago", cust.FechaPago.ToString());
                            child.SetAttribute("cFechaPago", cust.cFechaPago.ToString());
                            child.SetAttribute("Cuota", cust.Cuota.ToString());
                            child.SetAttribute("MontoCuota", cust.MontoCuota);
                            child.SetAttribute("Capital", cust.Capital.ToString());
                            child.SetAttribute("Interes", cust.Interes.ToString());
                            child.SetAttribute("Gastos", cust.Gastos.ToString());
                            child.SetAttribute("Saldos", cust.Saldos.ToString());
                            child.SetAttribute("SaldoCalendario", cust.SaldoCalendario.ToString());
                            root.AppendChild(child);
                        }

                        oCreditoXml = xml.OuterXml;

                        var parameters = new DynamicParameters();
                        parameters.Add("@nCodPers", credito.nCodPers);
                        parameters.Add("@nCodAge", credito.nCodAge);
                        parameters.Add("@nProd", credito.nProd);
                        parameters.Add("@nSubProd", credito.nSubProd);
                        parameters.Add("@nNroCuotas", credito.nNroCuotas);
                        parameters.Add("@nMontoCuota", credito.nMontoCuota);
                        parameters.Add("@nTasa", credito.nTasa);
                        parameters.Add("@nPeriodo", credito.nPeriodo);
                        parameters.Add("@nMontoSol", credito.nPrestamo);
                        parameters.Add("@nCodUsu", credito.nCodUsu);
                        parameters.Add("@oCredito", oCreditoXml);
                        parameters.Add("@nIdFlujoMaestro", credito.nIdFlujoMaestro);
                        parameters.Add("@cNomForm", credito.cFormulario);
                        parameters.Add("@cUsuReg", credito.cUsuReg);
                        parameters.Add("@nCodPersReg", credito.nCodPersReg);
                        parameters.Add("@nIdFlujo", credito.nIdFlujo);
                        parameters.Add("@nOrdenFlujo", credito.nOrdenFlujo);
                        parameters.Add("@nCodCred", credito.nCodCred, dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Query<int>("WEBApi_LucasinsCredito_SP", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                        var nCodCred = parameters.Get<int>("@nCodCred");

                        transaction.Commit();

                        return nCodCred;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public int LucasInsFirmaElectronica(Credito credito)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@nCodCred", credito.nCodCred);
                        parameters.Add("@nCodAge", credito.nCodAge);
                        parameters.Add("@cNumCelular", credito.cMovil);
                        parameters.Add("@cCodElectronica", credito.nFirma);
                        parameters.Add("@nIdFlujoMaestro", credito.nIdFlujoMaestro);
                        parameters.Add("@nProd", credito.nProd);
                        parameters.Add("@nSubProd", credito.nSubProd);
                        parameters.Add("@cNomForm", credito.cFormulario);
                        parameters.Add("@cUsuReg", credito.cUsuReg);
                        parameters.Add("@nCodPersReg", credito.nCodPersReg);
                        parameters.Add("@nIdFlujo", credito.nIdFlujo);
                        parameters.Add("@nCodPers", credito.nCodPers);
                        parameters.Add("@nOrdenFlujo", credito.nOrdenFlujo);
                        parameters.Add("@nRetorno", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Query<int>("WebApi_LucasFirmaElectronica_SP", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                        var nRetorno = parameters.Get<int>("@nRetorno");

                        transaction.Commit();

                        return nRetorno;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public int LucasInsModalidad(Credito credito)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@nCodCred", credito.nCodCred);
                        parameters.Add("@nCodAge", credito.nCodAge);
                        parameters.Add("@nTipoDesembolso", credito.nTipoDesembolso);
                        parameters.Add("@nBanco", credito.nBanco);
                        parameters.Add("@cCuentaBancaria", credito.cNroCuenta);
                        parameters.Add("@nIdFlujoMaestro", credito.nIdFlujoMaestro);
                        parameters.Add("@nProd", credito.nProd);
                        parameters.Add("@nSubProd", credito.nSubProd);
                        parameters.Add("@cNomForm", credito.cFormulario);
                        parameters.Add("@cUsuReg", credito.cUsuReg);
                        parameters.Add("@nCodPersReg", credito.nCodPersReg);
                        parameters.Add("@nIdFlujo", credito.nIdFlujo);
                        parameters.Add("@nCodPers", credito.nCodPers);
                        parameters.Add("@nOrdenFlujo", credito.nOrdenFlujo);
                        parameters.Add("@nRetorno", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Query<int>("WEBApi_LucasInsModalidad_SP", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                        var nRetorno = parameters.Get<int>("@nRetorno");

                        transaction.Commit();

                        return nRetorno;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IEnumerable<Credito> LucasKardexLista(int nCodAge, int nCodCred)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nCodCred", nCodCred);
                parameters.Add("@nCodAge", nCodAge);

                return connection.Query<Credito>("WebApi_LucasKardexLista_SP", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public int LucasRechazadoPorDia(string cDocumento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@cDocumento", cDocumento);
                parameters.Add("@nRes", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Query<int>("WebApi_LucasRechazadoPorDia_SP", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@nRes");
            }
        }
    }
}
