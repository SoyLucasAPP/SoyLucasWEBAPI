using System.Collections;
using System.Collections.Generic;
using TiboxWebApi.Models;

namespace TiboxWebApi.Repository.Interfaces
{
    public interface IReporteRepository:IRepository<Reporte>
    {
        int LucasInsCabeceraReporte(int nCodAcge, int nCodCred, string cAsunto, string cCuerpo);
        int LucasInsDetalleReporte(int nCodAge, int nCodCred, int nTipo, byte[] oDoc);
        IEnumerable<Reporte> LucasSeleccionaReporte(int nCodAge, int nCodCred, int nTipo);
    }
}
