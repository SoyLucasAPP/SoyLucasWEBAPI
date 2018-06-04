using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TiboxWebApi.Models;

namespace TiboxWebApi.Repository
{
    public class Utiles
    {
        public string Encriptar(string texto)
        {
            try
            {
                string key = "WebApiTiboxOnline"; //llave para encriptar datos
                byte[] keyArray;
                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);
                //Se utilizan las clases de encriptación MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
                //Algoritmo TripleDES
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
                tdes.Clear();
                texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return texto;
        }

        public List<Calendario> GeneraCalendario(double nPrestamo, int nCuotas, int nPeriodicidad, double nTasa, string dFechaSistema, double nSeguro)
        {
            var valor = false;

            double nGastoSegDeg = nSeguro;
            double nMonto = nPrestamo;
            double nMontoCuotaBK = 0.00;
            double nValorInc = 0.00;
            double nIGV = 0;
            int pnFinCuotaGracia = 0;
            int pnTipoGracia = 1;
            bool bOK = false;

            object MatFechas = new object();
            string[,] matDatos = new string[nCuotas + 1, 8];
            object[] MatFechas1 = new object[nCuotas];
            string[,] MatFechas2 = new string[nCuotas + 1, 2];

            DateTime fechaDesem = DateTime.Parse(dFechaSistema);
            List<Calendario> calendarioList = new List<Calendario>();

            DateTime dFechaPrueba = fechaDesem;

            if (!valor)
            {
                var nNumSub = 1;
                MatFechas = CalendarioFechasCuotaFija(nCuotas, dFechaPrueba, nPeriodicidad, nNumSub);
            }
            var cont = 0;
            var nInteres = 0.00;
            var nTasaCom = nTasa;
            var nMontoCuota = Math.Round(CuotaPeriodoFijo(nTasaCom, 0, nGastoSegDeg, nCuotas, nPrestamo, 30), 2);
            List<double> lstInteres = new List<double>();
            var dFecha = fechaDesem;
            double MontoCuotaReturn = 0.00;
            double nMontoNegativo = 0.00;
            double nMontoDiferenciaNeto = 0.00;
            double nPendIntComp = 0.00;
            double nPendComision = 0.00;
            double nMontoComisionCalculado = 0.00;
            double nMontoNetoICCOM = 0.00;
            double pnMontoComision = 0;
            //nCuotas = nCuotas + 1;
            //y,0 = fechapago;
            //y,1 = cuota;
            //y,2 = montocuota,;
            //y,3 = capital;
            //y,4 = interes;
            //y,5 = gastos;
            //y,6 = saldos;
            //y,7 = desgravamen

            do
            {
                // setear los valores
                nMonto = nPrestamo;
                nMontoCuotaBK = nMontoCuota;
                dFecha = dFechaPrueba;

                for (int k = 1; k <= nCuotas; k++)
                {
                    nMontoCuota = nMontoCuotaBK;

                    string[,] MatFe = (string[,])MatFechas;
                    var dFechaVenc = MatFe[k, 1];
                    matDatos[k, 0] = MatFe[k, 1];
                    var dias = diasrestantes(DateTime.Parse(dFecha.ToString()), DateTime.Parse(dFechaVenc.ToString()));
                    matDatos[k, 1] = Convert.ToString(k);
                    nInteres = Math.Pow((1 + nTasaCom / 100.00), (dias / 30.00)) - 1;
                    nInteres = Math.Round(Convert.ToDouble((nInteres * nMonto)), 2);
                    matDatos[k, 4] = Convert.ToDouble(nInteres).ToString();

                    nMontoComisionCalculado = 0;
                    if (pnTipoGracia != 4 || k > pnFinCuotaGracia)
                    {
                        if (pnMontoComision > 0)
                        {
                            nMontoComisionCalculado = Math.Round(nMonto * Math.Pow((1 + pnMontoComision / 100.00), (diasrestantes(DateTime.Parse(dFecha.ToString()), DateTime.Parse(MatFe[k, 1])) / 30.00)) - 1, 2);
                        }
                    }
                    else
                    {
                        //matDatos[k, 5] = "0.00";
                    }

                    nMontoNegativo = Convert.ToDouble(nMontoCuota - Convert.ToDouble(matDatos[k, 4]));

                    if (nMontoNegativo < 0)
                    {
                        nMontoDiferenciaNeto = Convert.ToDouble(Math.Round(Math.Abs(nMontoNegativo) / (1 + (nIGV + 100.00)), 2));


                        if (Convert.ToDouble(matDatos[k, 4]) > nMontoDiferenciaNeto)
                        {
                            nPendIntComp = Math.Round(Convert.ToDouble(nPendIntComp), 2) + nMontoDiferenciaNeto;
                            matDatos[k, 4] = Math.Round(Convert.ToDouble(matDatos[k, 4]) - nMontoDiferenciaNeto, 2).ToString();
                        }
                        else if (nMontoComisionCalculado > nMontoDiferenciaNeto)
                        {
                            nPendComision = nPendComision + nMontoDiferenciaNeto;
                            nMontoComisionCalculado = Math.Round(nMontoComisionCalculado - nMontoDiferenciaNeto, 2);
                        }
                        else
                        {
                            nPendIntComp = nPendIntComp + Convert.ToDouble(matDatos[k, 4]);
                            matDatos[k, 4] = "0.00";
                            nPendComision = nPendComision + (nMontoDiferenciaNeto - nPendIntComp);
                            nMontoComisionCalculado = Math.Round(nMontoComisionCalculado - (nMontoDiferenciaNeto - nPendIntComp), 2);
                        }
                    }
                    else
                    {
                        nMontoDiferenciaNeto = Convert.ToDouble(Math.Round(nMontoNegativo / (1 + (nIGV / 100.00)), 2));
                        if (nPendIntComp > 0)
                        {
                            if (nPendIntComp > nMontoDiferenciaNeto)
                            {
                                nMontoNetoICCOM = nMontoDiferenciaNeto;
                                matDatos[k, 4] = (Convert.ToDouble(matDatos[k, 4]) + nMontoNetoICCOM).ToString();
                                nPendIntComp = nPendIntComp - nMontoNetoICCOM;
                                nMontoNegativo = 0;
                            }
                            else
                            {
                                nMontoNetoICCOM = nPendIntComp;
                                nMontoNegativo = nMontoNegativo - nPendIntComp;
                                matDatos[k, 4] = (Convert.ToDouble(matDatos[k, 4]) + nMontoNetoICCOM).ToString();
                                nPendIntComp = 0;
                            }

                        }
                        if (nPendComision > 0)
                        {
                            if (nPendComision > nMontoDiferenciaNeto)
                            {
                                nMontoNetoICCOM = nMontoDiferenciaNeto;
                                nMontoComisionCalculado = nMontoComisionCalculado + nMontoNetoICCOM;
                                nPendComision = nPendComision - nMontoNetoICCOM;
                            }
                            else
                            {
                                nMontoNetoICCOM = nPendComision;
                                nMontoComisionCalculado = nMontoComisionCalculado + nMontoNetoICCOM;
                                nPendComision = 0;
                            }
                        }
                    }

                    if (k == 1)
                    {
                        matDatos[k, 7] = Math.Round(Convert.ToDouble(nPrestamo * nGastoSegDeg / 100.00), 2).ToString();
                    }
                    else
                    {
                        matDatos[k, 7] = Math.Round((Convert.ToDouble(matDatos[k - 1, 6]) * nGastoSegDeg / 100.00), 2).ToString();

                    }

                    if (pnTipoGracia != 4 || k > pnFinCuotaGracia)
                    {


                        if (k != nCuotas)
                        {
                            matDatos[k, 3] = Math.Round(nMontoCuota - (Convert.ToDouble(matDatos[k, 4]) + Convert.ToDouble(matDatos[k, 7])), 2).ToString();

                            if (Convert.ToDouble(matDatos[k, 3]) > 0 && Convert.ToDouble(matDatos[k, 3]) <= 0.05)
                            {
                                matDatos[k, 3] = "0.00";
                            }

                            if (Convert.ToDouble(matDatos[k, 3]) >= -0.05 && Convert.ToDouble(matDatos[k, 3]) < 0)
                            {
                                matDatos[k, 3] = "0.00";
                            }
                        }
                        else
                        {
                            if (k == 1)
                            {
                                matDatos[k, 3] = Math.Round(nMonto, 2).ToString();
                            }
                            else
                            {
                                matDatos[k, 3] = Math.Round(Convert.ToDouble(matDatos[k - 1, 6]), 2).ToString();
                            }
                            if (nPendComision > 0 || nPendIntComp > 0)
                            {
                                matDatos[k, 4] = Math.Round(Convert.ToDouble(matDatos[k, 4]) + nPendIntComp, 2).ToString();
                            }
                        }
                    }
                    else
                    {
                        matDatos[k, 3] = "0.00";
                    }

                    matDatos[k, 2] = Math.Round((Convert.ToDouble(matDatos[k, 3]) + Convert.ToDouble(matDatos[k, 4]) + Convert.ToDouble(matDatos[k, 7])), 2).ToString();

                    nMonto = Math.Round(nMonto - Convert.ToDouble(matDatos[k, 3]), 2);
                    matDatos[k, 6] = Math.Round(nMonto, 2).ToString(); // SALDO, 6

                    dFecha = Convert.ToDateTime(MatFe[k, 1]);
                }

                var ultimaFila = 0;
                for (int l = 0; l < matDatos.Rank; l++)
                {
                    ultimaFila = matDatos.GetUpperBound(l);
                    break;
                }

                if (int.Parse(ultimaFila.ToString()) > 1)
                {
                    if (Convert.ToDouble(matDatos[ultimaFila, 2]) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= -0.01 &&
                        Convert.ToDouble(matDatos[ultimaFila, 2]) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) <= 0.0)
                    {
                        MontoCuotaReturn = Convert.ToDouble(matDatos[ultimaFila - 1, 3]);
                        bOK = true;
                    }
                    else
                    {
                        if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 250)
                        {
                            nValorInc = 0.3;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 210)
                        {
                            nValorInc = 0.28;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 180)
                        {
                            nValorInc = 0.27;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 120)
                        {
                            nValorInc = 0.26;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 100)
                        {
                            nValorInc = 0.25;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 90)
                        {
                            nValorInc = 0.24;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 80)
                        {
                            nValorInc = 0.23;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 75)
                        {
                            nValorInc = 0.22;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 70)
                        {
                            nValorInc = 0.21;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 69)
                        {
                            nValorInc = 0.2;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 68)
                        {
                            nValorInc = 0.19;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 67)
                        {
                            nValorInc = 0.18;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 65)
                        {
                            nValorInc = 0.17;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 60)
                        {
                            nValorInc = 0.16;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 50)
                        {
                            nValorInc = 0.15;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 40)
                        {
                            nValorInc = 0.14;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 30)
                        {
                            nValorInc = 0.13;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 20)
                        {
                            nValorInc = 0.12;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 10)
                        {
                            nValorInc = 0.11;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 5)
                        {
                            nValorInc = 0.1;
                        }
                        else if (Math.Abs(Convert.ToDouble(matDatos[ultimaFila, 2])) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) >= 1)
                        {
                            nValorInc = 0.01;
                        }
                        else
                        {
                            nValorInc = 0.01;
                        }

                        if (Convert.ToDouble(matDatos[ultimaFila, 2]) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) <= -0.01)
                        {
                            nMontoCuota = Math.Round(Convert.ToDouble(nMontoCuota - nValorInc), 2);
                        }
                        else if (Convert.ToDouble(matDatos[ultimaFila, 2]) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) > 0.0)
                        {
                            nMontoCuota = Math.Round(Convert.ToDouble(nMontoCuota + nValorInc), 2);
                        }

                        cont += 1;
                        if (cont > 2500)
                        {
                            if (Convert.ToDouble(matDatos[ultimaFila, 2]) - Convert.ToDouble(matDatos[ultimaFila - 1, 2]) < 0 || cont == 2502) //1502
                            {
                                MontoCuotaReturn = Convert.ToDouble(matDatos[ultimaFila - 1, 3]);
                                bOK = true;
                            }
                        }
                    }
                }
                else
                {
                    MontoCuotaReturn = Convert.ToDouble(matDatos[1, 2]);
                    bOK = true;
                }

                var cantidad1 = matDatos.Length - 1;
            } while (!bOK);


            double nMontoRef = 0;
            for (int m = 0; m < nCuotas * 1; m++)
            {
                nMontoRef += Math.Round(Convert.ToDouble(matDatos[m, 2]), 2);
            }


            for (int y = 1; y <= nCuotas; y++)
            {
                Calendario obj = new Calendario();
                obj.cFechaPago = Convert.ToString(DateTime.Parse(matDatos[y, 0]).ToShortDateString());
                obj.FechaPago = DateTime.Parse(matDatos[y, 0]);
                obj.Cuota = int.Parse(matDatos[y, 1].ToString());
                obj.MontoCuota = matDatos[y, 2].ToString();

                var datoCapital = matDatos[y, 3] == null || matDatos[y, 3] == "null" ? 0.00 : double.Parse(matDatos[y, 3].ToString());

                obj.Capital = Math.Round(datoCapital, 2);
                obj.Interes = double.Parse(matDatos[y, 4].ToString());
                obj.Saldos = double.Parse(matDatos[y, 6].ToString());
                calendarioList.Add(obj);
            }

            return calendarioList;
        }

        public int diasrestantes(DateTime dFecInicio, DateTime dFechaVenc)
        {
            TimeSpan span = dFechaVenc.Subtract(dFecInicio);
            return (int)span.TotalDays;
        }

        private object CalendarioFechasCuotaFija(int nCuotas, DateTime fechaDesem, int nPeriodicidad, int nNumSub)
        {
            DateTime pdFecha = default(DateTime);
            //nCuotas = nCuotas + 1;
            string[,] MatFechas = new string[nCuotas + 1, 2];
            string[,] MatFechasEsp = new string[nCuotas * nNumSub + 1, 2];
            DateTime dFechaDesembTempo = default(DateTime);

            dFechaDesembTempo = fechaDesem;

            MatFechas = new string[nCuotas + 1, 3];

            pdFecha = dFechaDesembTempo;

            for (int i = 1; i <= nCuotas; i++)
            {
                pdFecha = pdFecha.AddDays(nPeriodicidad);

                if (pdFecha.DayOfWeek == DayOfWeek.Sunday) pdFecha = pdFecha.AddDays(1);

                if (pdFecha.DayOfWeek == DayOfWeek.Saturday) pdFecha = pdFecha.AddDays(2);
                                
                MatFechas[i, 1] = pdFecha.ToString();
            }
            return MatFechas;
        }

        public static int Weekday(DateTime dt, DayOfWeek startOfWeek)
        {
            return (dt.DayOfWeek - startOfWeek + 7) % 7;
        }

        public double CuotaPeriodoFijo(double pTasaComp, double pTasaComision, double pnTasaSeguro, double pnCuotas, double vMonto, double pnPlazo)
        {
            double CuotaPeriodoFijo = 0;
            double Pot1 = 0, nTasaTmp = 0, nTasaComiTmp = 0;
            nTasaTmp = ((Math.Pow((1 + (pTasaComp / 100)), (pnPlazo / 30))) - 1);
            nTasaComiTmp = ((Math.Pow((1 + (pTasaComision / 100)), (pnPlazo / 30))) - 1);
            nTasaComiTmp = double.Parse(string.Format("{0:0.##}", nTasaComiTmp));
            nTasaTmp = nTasaTmp + nTasaComiTmp;
            nTasaTmp = (nTasaTmp * (1 + (pnTasaSeguro / 100)));
            Pot1 = Math.Pow((1 + nTasaTmp), pnCuotas);
            CuotaPeriodoFijo = ((Pot1 * nTasaTmp) / (Pot1 - 1)) * vMonto;
            CuotaPeriodoFijo = Math.Round(CuotaPeriodoFijo, 2);
            return CuotaPeriodoFijo;

        }
    }
}
