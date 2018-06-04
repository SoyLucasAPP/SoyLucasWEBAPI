using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace TiboxWebApi.WebApi.Utils
{
    public class ActiveDirectory
    {
        public Boolean Autenticado(string psUser, string psPass)
        {
            string cIPAD = System.Configuration.ConfigurationManager.AppSettings["IPAD"].ToString();
            bool Autentificado = false;
            DirectoryEntry deSystem = new DirectoryEntry(cIPAD);
            deSystem.AuthenticationType = AuthenticationTypes.Secure;
            deSystem.Username = psUser;
            deSystem.Password = psPass;

            DirectorySearcher deSearch = new DirectorySearcher();
            deSearch.SearchRoot = deSystem;
            deSearch.Filter = ("(anr=" + psUser + ")");

            try
            {
                SearchResultCollection results = deSearch.FindAll();

                if (results.Count == 0)
                {
                    Autentificado = false;
                }
                else
                {
                    Autentificado = true;
                }
                results = null;
                deSearch = null;
                deSystem = null;
            }
            catch (Exception ex)
            {
                Autentificado = false;
            }
            finally
            {
                deSystem = null;
            }
            return Autentificado;
        }
    }
}