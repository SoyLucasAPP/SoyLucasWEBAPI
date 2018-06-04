using System.Collections;
using System.Collections.Generic;
using TiboxWebApi.Models;

namespace TiboxWebApi.Repository.Interfaces
{
    public interface IPersonaRepository:IRepository<Persona>
    {
        int LucasInsPersona(Persona persona);
        IEnumerable<User> LucasVerificaClienteExiste(string cDocumento);
        IEnumerable<Persona> LucasDatosPersona(string cDocumento, string cEmail, int nCodPers);
        int LucasActPersona(Persona persona);
        int LucasTratamientoDatos(Tratamiento tratamiento);
        int LucasValidaPersonaCelular(string cDocumento, string cTelefono);
    }
}
