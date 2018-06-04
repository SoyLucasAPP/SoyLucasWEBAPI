using System.Collections.Generic;
using TiboxWebApi.Models;

namespace TiboxWebApi.Repository.Interfaces
{
    public interface IFlujoRepository:IRepository<FlujoMaestro>
    {
        int LucasRegistraFlujo(FlujoMaestro flujo);
        int LucasRegistraMotor(FlujoMaestro flujo);
        IEnumerable<FlujoMaestro> LucasRecuperaFlujo(int nIdFlujoMaestro);
        IEnumerable<FlujoMaestro> LucasRecuperaSolicitud(int nIdFlujoMaestro);
        IEnumerable<FlujoMaestro> ObtieneWizard(int nIdFlujoMaestro);
        int EliminaFlujo(FlujoMaestro flujo);
    }
}
