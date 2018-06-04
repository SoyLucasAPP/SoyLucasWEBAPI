using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiboxWebApi.Models
{
    
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int nTipo { get; set; }
        public int nCodPers { get; set; }
        public string cMovil { get; set; }
        //Datos WinUsuario
        public int nCodAge { get; set; }
        public string cUserName { get; set; }
        public int nCodUsu { get; set; }
        public string cDNIUsu { get; set; }
        public int nIdRol { get; set; }
        public string cRol { get; set; }
        public string dFechaSistema { get; set; }
        public string cNomAge { get; set; }
        public string cNomUsu { get; set; }
        public int changePass { get; set; }
    }
}
