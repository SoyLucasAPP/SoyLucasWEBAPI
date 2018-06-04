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
    public class DocumentoRepository : BaseRepository<Documento>, IDocumentoRepository
    {
        public IEnumerable<Documento> ListaDocumentos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Documento>("WebOnline_selListaDocumentosWeb_SP", commandType: CommandType.StoredProcedure);
            }
        }

        public int LucasInsDocumento(Documento documento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@iImagen", documento.iImagen);
                parameters.Add("@cNomArchivo", documento.cNomArchivo);
                parameters.Add("@cExtencion", documento.cExtencion);
                parameters.Add("@nIdFlujoMaestro", documento.nIdFlujoMaestro);
                parameters.Add("@cTipoArchivo", documento.cTipoArchivo);

                var resultado = documento.nIdFlujoMaestro;

                connection.Query<int>("WebApi_LucasDocumentosInserta_SP", parameters, commandType:CommandType.StoredProcedure);

                return resultado;
            }
        }
    }
}
