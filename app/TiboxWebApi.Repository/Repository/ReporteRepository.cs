using Dapper;
using System.Data;
using System.Data.SqlClient;
using TiboxWebApi.Models;
using TiboxWebApi.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TiboxWebApi.Repository.Repository
{
    public class ReporteRepository : BaseRepository<Reporte>, IReporteRepository
    {
        public int LucasInsCabeceraReporte(int nCodAcge, int nCodCred, string cAsunto, string cCuerpo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pnCodAge", nCodAcge);
                parameters.Add("@pnCodCred", nCodCred);
                parameters.Add("@pcAsunto", cAsunto);
                parameters.Add("@pcCuerpo", cCuerpo);

                connection.Query<int>("WebApi_ReporteInsertaCabecera_SP", parameters, commandType: CommandType.StoredProcedure);
                return 1;
            }
        }

        public int LucasInsDetalleReporte(int nCodAge, int nCodCred, int nTipo, byte[] oDoc)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pnCodAge", nCodAge);
                parameters.Add("@pnCodCred", nCodCred);
                parameters.Add("@pnTipoDcoumento", nTipo);
                parameters.Add("@poDocumento", oDoc);

                connection.Query<int>("WebApi_ReporteInsertaDetalle_SP", parameters, commandType: CommandType.StoredProcedure);
                return 1;
            }
        }

        public IEnumerable<Reporte> LucasSeleccionaReporte(int nCodAge, int nCodCred, int nTipo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nCodCred", nCodCred);
                parameters.Add("@nCodAge", nCodAge);
                parameters.Add("@nTipo", nTipo);

                return connection.Query<Reporte>("WebApi_ReporteSelecciona_SP", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
