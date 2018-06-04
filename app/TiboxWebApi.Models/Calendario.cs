using System;

namespace TiboxWebApi.Models
{
    public class Calendario
    {
        private DateTime dFechaPago;
        private int nCuota;
        private string nMontoCuota;
        private double nCapital;
        private double nInteres;
        private string dfechPago;
        private double nGastos;
        private double nSaldos;
        private double nSaldoCalendario;
        private double nDesgravamen;

        public string cFechaPago
        {
            get { return dfechPago; }
            set { dfechPago = value; }
        }

        public DateTime FechaPago
        {
            get { return dFechaPago; }
            set { dFechaPago = value; }
        }

        public double Seguro
        {
            get { return nDesgravamen; }
            set { nDesgravamen = value; }
        }

        public int Cuota
        {
            get { return nCuota; }
            set { nCuota = value; }
        }

        public string MontoCuota
        {
            get { return nMontoCuota; }
            set { nMontoCuota = value; }
        }

        public double Capital
        {
            get { return nCapital; }
            set { nCapital = value; }
        }

        public double Interes
        {
            get { return nInteres; }
            set { nInteres = value; }
        }

        public double Gastos
        {
            get { return nGastos; }
            set { nGastos = value; }
        }

        public double Saldos
        {
            get { return nSaldos; }
            set { nSaldos = value; }
        }

        public double SaldoCalendario
        {
            get { return nSaldoCalendario; }
            set { nSaldoCalendario = value; }
        }
    }
}
