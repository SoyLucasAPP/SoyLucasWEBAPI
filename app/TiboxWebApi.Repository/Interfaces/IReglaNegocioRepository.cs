using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiboxWebApi.Models;

namespace TiboxWebApi.Repository.Interfaces
{
    public interface IReglaNegocioRepository: IRepository<ReglaNegocio>
    {
        IEnumerable<ReglaNegocio> ListaRegla(string cNomForm);
    }
}
