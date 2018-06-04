using Dapper;
using System.Data;
using System.Data.SqlClient;
using TiboxWebApi.Models;
using TiboxWebApi.Repository.Interfaces;
using System.Collections.Generic;
using System;

namespace TiboxWebApi.Repository.Repository
{
    public class PersonaRepository : BaseRepository<Persona>, IPersonaRepository
    {
        public readonly Utiles _utils = null;
        public PersonaRepository()
        {
            _utils = new Utiles();
        }

        public int LucasActPersona(Persona persona)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nNroDoc", persona.nNroDoc);
                parameters.Add("@cCodZona", persona.cCodZona);
                parameters.Add("@nTipoResidencia", persona.nTipoResidencia);
                parameters.Add("@nSexo", persona.nSexo);
                parameters.Add("@cTelefono", persona.cTelefono);
                parameters.Add("@dFechaNacimiento", persona.dFechaNacimiento);
                parameters.Add("@nEstadoCivil", persona.nEstadoCivil);
                parameters.Add("@nDirTipo1", persona.nDirTipo1);
                parameters.Add("@nDirTipo2", persona.nDirTipo2);
                parameters.Add("@nDirTipo3", persona.nDirTipo3);
                parameters.Add("@cDirValor1", persona.cDirValor1);
                parameters.Add("@cDirValor2", persona.cDirValor2);
                parameters.Add("@cDirValor3", persona.cDirValor3);
                parameters.Add("@nCodAge", persona.nCodAge);
                parameters.Add("@nCUUI", persona.nCUUI);
                parameters.Add("@nSitLab", persona.nSitLab);
                parameters.Add("@nProfes", persona.nProfes);
                parameters.Add("@nTipoEmp", persona.nTipoEmp);
                parameters.Add("@cDniConyuge", persona.cDniConyuge);
                parameters.Add("@cNomConyuge", persona.cNomConyuge);
                parameters.Add("@cApeConyuge", persona.cApeConyuge);
                parameters.Add("@cRuc", persona.cRuc);
                parameters.Add("@nIngresoDeclado", persona.nIngresoDeclado);
                parameters.Add("@cTelfEmpleo", persona.cTelfEmpleo);
                parameters.Add("@dFecIngrLab", persona.dFecIngrLab);
                parameters.Add("@bCargoPublico", persona.bCargoPublico);
                parameters.Add("@cNomEmpresa", persona.cNomEmpresa);
                parameters.Add("@cProfesionOtros", persona.cProfesionOtros);
                parameters.Add("@cCodZonaEmpleo", persona.cCodZonaEmpleo);
                parameters.Add("@nDirTipo1Empleo", persona.nDirTipo1Empleo);
                parameters.Add("@nDirTipo2Empleo", persona.nDirTipo2Empleo);
                parameters.Add("@nDirTipo3Empleo", persona.nDirTipo3Empleo);
                parameters.Add("@cDirValor1Empleo", persona.cDirValor1Empleo);
                parameters.Add("@cDirValor2Empleo", persona.cDirValor2Empleo);
                parameters.Add("@cDirValor3Empleo", persona.cDirValor3Empleo);
                parameters.Add("@nCodPers", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Query<int>("WebApi_LucasActPersona_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure, commandTimeout: 0);
                var nCodPers = parameters.Get<int>("@nCodPers");
                return nCodPers;

            }
        }

        public IEnumerable<Persona> LucasDatosPersona(string cDocumento, string cEmail, int nCodPers)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@cNroDoc", cDocumento);
                parameters.Add("@cEmail", cEmail);
                parameters.Add("@nCodPers", nCodPers);
                return connection.Query<Persona>("WebApi_LucasDatosPersona_SP", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public int LucasInsPersona(Persona persona)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string Pass = _utils.Encriptar(persona.nNroDoc);
                var parameters = new DynamicParameters();
                parameters.Add("@cNombres", persona.cNombres);
                parameters.Add("@cApePat", persona.cApePat);
                parameters.Add("@cApeMat", persona.cApeMat);
                parameters.Add("@nTipoDoc", persona.nTipoDoc);
                parameters.Add("@nNroDoc", persona.nNroDoc);
                parameters.Add("@cCelular", persona.cCelular);
                parameters.Add("@cEmail", persona.cEmail);
                parameters.Add("@cCodZona", persona.cCodZona);
                parameters.Add("@nTipoResidencia", persona.nTipoResidencia);
                parameters.Add("@nSexo", persona.nSexo);
                parameters.Add("@cTelefono", persona.cTelefono);
                parameters.Add("@dFechaNacimiento", persona.dFechaNacimiento);
                parameters.Add("@nEstadoCivil", persona.nEstadoCivil);
                parameters.Add("@nDirTipo1", persona.nDirTipo1);
                parameters.Add("@nDirTipo2", persona.nDirTipo2);
                parameters.Add("@nDirTipo3", persona.nDirTipo3);
                parameters.Add("@cDirValor1", persona.cDirValor1);
                parameters.Add("@cDirValor2", persona.cDirValor2);
                parameters.Add("@cDirValor3", persona.cDirValor3);
                parameters.Add("@nCodAge", persona.nCodAge);
                parameters.Add("@nCUUI", persona.nCUUI);
                parameters.Add("@nSitLab", persona.nSitLab);
                parameters.Add("@nProfes", persona.nProfes);
                parameters.Add("@nTipoEmp", persona.nTipoEmp);
                parameters.Add("@cDniConyuge", persona.cDniConyuge);
                parameters.Add("@cNomConyuge", persona.cNomConyuge);
                parameters.Add("@cApeConyuge", persona.cApeConyuge);
                parameters.Add("@cRuc", persona.cRuc);
                parameters.Add("@nIngresoDeclado", persona.nIngresoDeclado);
                parameters.Add("@cTelfEmpleo", persona.cTelfEmpleo);
                parameters.Add("@dFecIngrLab", persona.dFecIngrLab);
                parameters.Add("@bCargoPublico", persona.bCargoPublico);
                parameters.Add("@cNomEmpresa", persona.cNomEmpresa);
                parameters.Add("@cProfesionOtros", persona.cProfesionOtros);
                parameters.Add("@cPassEncripta", Pass);
                parameters.Add("@nCodigoVerificador", persona.nCodigoVerificador);
                parameters.Add("@cCodZonaEmpleo", persona.cCodZonaEmpleo);
                parameters.Add("@nDirTipo1Empleo", persona.nDirTipo1Empleo);
                parameters.Add("@nDirTipo2Empleo", persona.nDirTipo2Empleo);
                parameters.Add("@nDirTipo3Empleo", persona.nDirTipo3Empleo);
                parameters.Add("@cDirValor1Empleo", persona.cDirValor1Empleo);
                parameters.Add("@cDirValor2Empleo", persona.cDirValor2Empleo);
                parameters.Add("@cDirValor3Empleo", persona.cDirValor3Empleo);
                parameters.Add("@nCodPers", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Query<int>("WebApi_LucasInsPersona_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure, commandTimeout: 0);
                var nCodPers = parameters.Get<int>("@nCodPers");
                return nCodPers;
            }
        }

        public int LucasTratamientoDatos(Tratamiento tratamiento)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nCodPers", tratamiento.nCodPers);
                parameters.Add("@cDocumento", tratamiento.cDocumento);
                parameters.Add("@cUsuario", tratamiento.cUsuario);
                parameters.Add("@cApePat", tratamiento.cApePat);
                parameters.Add("@cApeMat", tratamiento.cApeMat);
                parameters.Add("@cNombres", tratamiento.cNombres);
                parameters.Add("@nCodAge", tratamiento.nCodAge);
                parameters.Add("@nTipoSolicitud", tratamiento.nTipoSolicitud);
                parameters.Add("@nModoRegistro", tratamiento.nModoRegistro);
                parameters.Add("@nTipoResp", tratamiento.nTipoResp);
                parameters.Add("@cPedido", tratamiento.cPedido);
                parameters.Add("@cComentario", tratamiento.cComentario);
                parameters.Add("@nCodPersTit", tratamiento.nCodPersTit);
                parameters.Add("@cApePatTit", tratamiento.cApePatTit);
                parameters.Add("@cApeMatTit", tratamiento.cApeMatTit);
                parameters.Add("@cNomTit", tratamiento.cNomTit);
                parameters.Add("@cDocumentoTit", tratamiento.cDocumentoTit);
                parameters.Add("@nCodSolicitud", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Query<int>("WebApi_LucasTratamientoDatosInserta_SP", parameters, commandType: CommandType.StoredProcedure);
                var nCodSolcitud = parameters.Get<int>("@nCodSolicitud");
                return nCodSolcitud;
            }
        }

        public int LucasValidaPersonaCelular(string cDocumento, string cTelefono)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@cCelular", cTelefono);
                parameters.Add("@cDocumento", cDocumento);
                parameters.Add("@nRes", dbType: DbType.Int32, direction: ParameterDirection.Output);
                
                connection.Query<Persona>("WebApi_PersonaValidaCelular_SP", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@nRes");
            }
        }

        public IEnumerable<User> LucasVerificaClienteExiste(string cDocumento)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@cDocumento", cDocumento);

                return connection.Query<User>("WebApi_LucasVerificaClienteExiste_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure, commandTimeout: 0);
            }
        }
    }
}
