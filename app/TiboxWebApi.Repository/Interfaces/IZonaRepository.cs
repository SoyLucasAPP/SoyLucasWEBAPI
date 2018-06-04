using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiboxWebApi.Models;

namespace TiboxWebApi.Repository.Interfaces
{
    public interface IZonaRepository: IRepository<Zona>
    {
        IEnumerable<Zona> selDepartamento();
        IEnumerable<Zona> selProvincia(string cDepartamento);
        IEnumerable<Zona> selDistrito(string cDepartamento, string cProvincia);
    }
}
