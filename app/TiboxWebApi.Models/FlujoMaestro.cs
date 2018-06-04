using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiboxWebApi.Models
{
    public class FlujoMaestro
    {
        public string nNroDoc { get; set; }
        public int nCodAge { get; set; }
        public int nProd { get; set; }
        public int nSubProd { get; set; }
        public string cNomform { get; set; }
        public int nCodCred { get; set; }
        public string cUsuReg { get; set; }
        public int nIdFlujo { get; set; }
        public int nCodPersReg { get; set; }
        public int nOrdenFlujo { get; set; }
        public string oScoringDatos { get; set; }
        public string oScoringVarDemo { get; set; }
        public string oScoringDetCuota { get; set; }
        public string oScoringDemo { get; set; }
        public string oScoringRCC { get; set; }
        public int nRechazado { get; set; }
        public string cClienteLenddo { get; set; }
        public int nIdFlujoMaestro { get; set; }
        public int nCodPers { get; set; }
        public double nTasa { get; set; }
        public double nCuotaDisp { get; set; }
        public double nPrestamoMax { get; set; }
        public double nPlazo { get; set; }
        public double nPrestamoMinimo { get; set; }
        public string dFechaSistema { get; set; }
        public double nSeguroDesgravamen { get; set; }
        public string cMovil { get; set; }
        public int nClientePEP { get; set; }
        public string cNombreProceso { get; set; }
        public string cClassEstilo { get; set; }
        public string cComentario { get; set; }
    }
}
