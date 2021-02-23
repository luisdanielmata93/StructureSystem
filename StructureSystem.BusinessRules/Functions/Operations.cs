using StructureSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using SeismicAnalysisNative;
using MathWorks.MATLAB.NET.Arrays;

namespace StructureSystem.BusinessRules.Functions
{
    internal static class Operations
    {

        private const double PI = 3.14159265;

        //private static SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();


        #region Calculos MathLab previos a calculos para muros
        public static double[] CalcularVectorPeriodosCirculares(double[,] M, double[,] K)
        {
            double[] result = new double[M.GetLength(0)];
            double[,] k = K;
            try
            {
                int n = M.GetLength(0);

                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;

                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();



                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var D = operations.EigenVals(eigsMatrix[1]);

                var wTF = operations.OmegaTauF(3, D, 1, PI);



                var w = (double[])((MWNumericArray)wTF[0]).ToVector(MWArrayComponent.Real);

                operations = null;

                result = w;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static double[] CalcularVectorPeriodosNaturales(double[,] M, double[,] K)
        {
            double[] result = new double[M.GetLength(0)];
            double[,] k = K;
            try
            {
                int n = M.GetLength(0);

                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;

                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var D = operations.EigenVals(eigsMatrix[1]);

                var wTF = operations.OmegaTauF(3, D, 1, PI);

                var T = (double[])((MWNumericArray)wTF[1]).ToVector(MWArrayComponent.Real);

                var TReverse = T.Reverse();
                result = TReverse.ToArray();

                operations = null;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static double[] CalcularVectorFrecuencias(double[,] M, double[,] K)
        {
            double[] result = new double[M.GetLength(0)];
            double[,] k = K;
            try
            {
                int n = M.GetLength(0);

                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;


                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var D = operations.EigenVals(eigsMatrix[1]);

                var wTF = operations.OmegaTauF(3, D, 1, PI);

                var F = (double[])((MWNumericArray)wTF[2]).ToVector(MWArrayComponent.Real);

                var FReverse = F.Reverse();
                result = FReverse.ToArray();

                operations = null;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static double[,] CalcularMatrizEigenVectores(double[,] M, double[,] K)
        {
            double[,] result = new double[M.GetLength(0), M.GetLength(0)];
            double[,] k = K;
            try
            {
                int n = M.GetLength(0);

                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;


                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                result = (double[,])((MWNumericArray)phi).ToArray(MWArrayComponent.Real);

                operations = null;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static double[,] CalcularMatrizRigideces(Structure Structure, Enums.SideType side)
        {
            int n = Structure.Storeys.Count;
            double[,] K = new double[n, n];

            if (side == Enums.SideType.Horizontal)
            {
                for (int i = 0; i <= n - 2; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j - 1)
                        {
                            K[j, i] = -1 * Structure.Storeys[i + 1].RigidezEntrepisoHorizontal;
                            K[i, j] = -1 * Structure.Storeys[i + 1].RigidezEntrepisoHorizontal;
                        }
                        K[i, i] = Structure.Storeys[i].RigidezEntrepisoHorizontal + Structure.Storeys[i + 1].RigidezEntrepisoHorizontal;
                        K[n - 1, n - 1] = Structure.Storeys[n - 1].RigidezEntrepisoHorizontal;
                    }
                }
            }
            if (side == Enums.SideType.Vertical)
            {
                for (int i = 0; i <= n - 2; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j - 1)
                        {
                            K[j, i] = -1 * Structure.Storeys[i + 1].RigidezEntrepisoVertical;
                            K[i, j] = -1 * Structure.Storeys[i + 1].RigidezEntrepisoVertical;
                        }
                        K[i, i] = Structure.Storeys[i].RigidezEntrepisoVertical + Structure.Storeys[i + 1].RigidezEntrepisoVertical;
                        K[n - 1, n - 1] = Structure.Storeys[n - 1].RigidezEntrepisoVertical;
                    }
                }
            }
            return K;
        }

        public static double[,] CalcularMatrizMasas(Structure Structure, Enums.SideType side)
        {
            int n = Structure.Storeys.Count;

            double[,] M = new double[n, n];
            if (side == Enums.SideType.Horizontal)
            {
                for (int i = 0; i < n; i++)
                    M[i, i] = Structure.Storeys[i].MasasEntrepisos;
            }
            if (side == Enums.SideType.Vertical)
            {
                for (int i = 0; i < n; i++)
                    M[i, i] = Structure.Storeys[i].MasasEntrepisos;
            }
            return M;
        }

        public static double[,] CalcularMatrizEspectral(double[,] M, double[,] K)
        {
            double[,] result = new double[M.GetLength(0), M.GetLength(0)];
            double[,] k = K;
            try
            {
                int n = M.GetLength(0);

                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;


                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);

                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var wTF = operations.OmegaTauF(3, eigenValues, 1, PI);

                var Omega = operations.OmegaU(eigenValues, wTF[0]);

                var resTemp = (double[,])((MWNumericArray)Omega).ToArray(MWArrayComponent.Real);

                double[,] Espectral = new double[resTemp.GetLength(0), resTemp.GetLength(1)];

                for (int i = 0; i < resTemp.GetLength(0); i++)
                {
                    for (int j = 0; j < resTemp.GetLength(1); j++)
                    {
                        Espectral[i, j] = resTemp[n - 1 - i, n - 1 - j];
                    }
                }

                result = Espectral;
                operations = null;
            }
            catch (Exception ex)
            {

            }

            return result;
        }


        public static double[,] CalcularMatrizMasasGeneralizada(double[,] M, double[,] K)
        {

            double[,] result = new double[M.GetLength(0), M.GetLength(0)];
            double[,] k = K;
            try
            {

                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;


                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var Mg = operations.Mg(eigenValues, Marr, phi);
                result = (double[,])((MWNumericArray)Mg).ToArray(MWArrayComponent.Real);

                operations = null;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static double[,] CalcularMatrizRigidecesGeneralizada(double[,] M, double[,] K)
        {
            double[,] result = new double[M.GetLength(0), M.GetLength(0)];
            double[,] k = K;
            try
            {

                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;


                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var Kg = operations.Mg(eigenValues, Karr, phi);
                result = (double[,])((MWNumericArray)Kg).ToArray(MWArrayComponent.Real);

                operations = null;
            }
            catch (Exception ex)
            {

            }

            return result;
        }


        public static double[] CalcularVectorParticipacionModal(double[,] M, double[,] K)
        {
            double[] result = new double[M.GetLength(0)];
            double[,] k = K;

            try
            {
                int n = M.GetLength(0);


                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;


                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var wTF = operations.OmegaTauF(3, eigenValues, 1, PI);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);
                var Mg = operations.Mg(eigenValues, Marr, phi);

                var Gamma = operations.GammaU(eigenValues, Marr, phi, Mg);

                result = (double[])((MWNumericArray)Gamma).ToVector(MWArrayComponent.Real);

                operations = null;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static double[,] CalcularMatrizModalNormalizada(double[,] M, double[,] K)
        {
            double[,] result = new double[M.GetLength(0), M.GetLength(0)];
            double[,] k = K;
            try
            {

                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;


                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var Mg = operations.Mg(eigenValues, Marr, phi);

                var phiN = operations.PhiN(eigenValues, phi, Mg);
                result = (double[,])((MWNumericArray)phiN).ToArray(MWArrayComponent.Real);

                operations = null;
            }
            catch (Exception ex)
            {

            }

            return result;

        }


        public static double[] CalcularVectorMasasEfectivas(double[,] M, double[,] K)
        {
            double[] result = new double[M.GetLength(0)];
            double[,] k = K;
            try
            {
                int n = M.GetLength(0);

                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;


                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var Me = operations.Me(eigenValues, Marr, phi);

                result = (double[])((MWNumericArray)Me).ToVector(MWArrayComponent.Real);

                operations = null;
            }
            catch (Exception ex)
            {


            }

            return result;
        }


        public static double[] CalcularMatrizFactoresParticipacionMasasModales(double[,] M, double[,] K)
        {
            double[] result = new double[M.GetLength(0)];
            double[,] k = K;
            try
            {
                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;

                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var Mg = operations.Mg(eigenValues, Marr, phi);

                var phiN = operations.PhiN(eigenValues, phi, Mg);

                var GammaN = operations.GammaN(eigenValues, Marr, phiN);

                result = (double[])((MWNumericArray)GammaN).ToVector(MWArrayComponent.Real);

                operations = null;
            }
            catch (Exception ex)
            {

            }

            return result;

        }

        public static double[] CalcularParticipacionDeMasas(double[,] M, double[,] K, double Mt)
        {
            double[] result = new double[M.GetLength(0)];
            double[,] k = K;
            try
            {
                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;

                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);

                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var Me = operations.Me(eigenValues, Marr, phi);

                var Pm = operations.Pm(eigenValues, 1, Me);

                result = (double[])((MWNumericArray)Pm).ToVector(MWArrayComponent.Real);

                for (int i = 0; i < result.Length; i++)
                    result[i] = result[i] / Mt;


                operations = null;
            }
            catch (Exception ex)
            {

            }

            return result;

        }


        public static double[] CalcularGamma(double[,] M, double[,] K)
        {
            double[] result = new double[M.GetLength(0)];
            double[,] k = K;
            try
            {
                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;

                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);

                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var phiAux = (double[,])((MWNumericArray)phi).ToArray(MWArrayComponent.Real);

                int n = M.GetLength(0);
                for (int i = 0; i < M.GetLength(0); i++)
                {
                    double Temp1 = 0, Temp2 = 0;
                    for (int j = 0; j < M.GetLength(1); j++)
                    {
                        Temp1 += M[n - 1 - i, j] * phiAux[i, j];
                        Temp2 += M[n - 1 - i, j] * Math.Pow(phiAux[i, j], 2);
                    }
                    result[i] = Temp1 / Temp2;
                }

                operations = null;
            }
            catch (Exception ex)
            {

            }

            return result;

        }


        public static double[] CalcularVectorDeAceleraciones(double[,] M, double[,] K, List<EspectroDisenio> datos)
        {
            double[] result = new double[M.GetLength(0)];
            double[,] k = K;
            try
            {
                for (int i = 0; i < k.GetLength(0); i++)
                    for (int j = 0; j < k.GetLength(1); j++)
                        k[i, j] = k[i, j] * 981;


                var dataArray = datos.ToArray();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(k);

                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var D = operations.EigenVals(eigsMatrix[1]);

                var wTF = operations.OmegaTauF(3, D, 1, PI);

                var T = (double[])((MWNumericArray)wTF[1]).ToVector(MWArrayComponent.Real);

                double SMayor = 0, SMenor = 0, TMayor = 0, TMenor = 0;

                for (int i = 0; i < T.Length; i++)
                {
                    for (int j = 0; j < datos.Count; j++)
                    {
                        if (T[i] <= datos[j].T)
                        {
                            TMayor = datos[j].T;
                            TMenor = datos[j - 1].T;
                            SMayor = datos[j].S;
                            SMenor = datos[j - 1].S;
                            break;
                        }
                        else if (T[i] > datos[j].T && j == datos.Count - 1)
                        {
                            TMayor = datos[j].T;
                            TMenor = datos[j].T;
                            SMayor = datos[j].S;
                            SMenor = datos[j].S;
                            break;
                        }
                    }

                    var resultado = (SMenor + (((SMayor - SMenor) / (TMayor - TMenor)) * (T[i] - TMenor))) * 981;

                    if (resultado > 0)
                        result[i] = resultado;
                    else
                        result[i] = SMayor * 981;
                }

            }
            catch (Exception ex)
            {

            }
            return result;
        }


        public static double[] CalcularVectorDeAceleracionPorModo(double[] va, double[] cm)
        {
            double[] result = new double[va.Length];
            try
            {
                for (int i = 0; i < va.Length; i++)
                {
                    result[i] = va[i] * cm[i];
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static double[,] CalcularVectoresDeAceleracionesPorModo(double[] a, double[,] phi)
        {
            double[,] result = new double[a.Length, a.Length];

            try
            {

                for (int i = 0; i < a.Length; i++)
                {

                    for (int j = 0; j < a.Length; j++)
                    {
                        result[i, j] = a[i] * phi[j, i];
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return result;
        }


        public static double[,] CalcularFuerzasFicticiasEquivalentesPorModo(double[,] A, double[] m)
        {
            int n = A.GetLength(0);
            double[,] result = new double[n, n];

            try
            {

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        result[i, j] = (A[i, j] * m[n - 1 - i]) / 1000;
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return result;
        }


        public static double[] CalcularVectorDeFuerzasFicticias(double[] VA, double[] m)
        {
            double[] result = new double[m.Length];
            try
            {
                for (int i = 0; i < m.Length; i++)
                {
                    result[i] = VA[i] * m[i];
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static double[,] CalcularVectoresDeFuerzasCortantesDeDisenio(double[,] Pn)
        {

            int n = Pn.GetLength(0);
            double[,] result = new double[n, n];

            try
            {

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (j == 0)
                            result[i, j] = Pn[i, j];
                        else
                            result[i, j] = Pn[i, j] + Pn[i, j - 1];
                    }
                }

            }
            catch (Exception ex)
            {

            }


            return result;
        }

        public static double[] CalcularDesplazamientoEnAzotea(double[,] A, double[] w, double Q)
        {
            int n = A.GetLength(0);
            double[] result = new double[n];

            try
            {

                for (int i = 0; i < n; i++)
                {
                    var a = w[i] / (2 * PI);
                    result[i] = (A[i, 0] / 10) * (Math.Pow(a, 2) * Q);
                }

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static double[,] CalcularDesplazamientoEntrepiso(double[] D, double[,] phi)
        {
            double[,] result = new double[D.Length, D.Length];

            try
            {

                for (int i = 0; i < D.Length; i++)
                {

                    for (int j = 0; j < D.Length; j++)
                    {
                        result[i, j] = D[i] * phi[j, i];
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static double[] CalcularVectorDeAceleracionCombinada(double[,] A)
        {
            int n = A.GetLength(0);
            double[] result = new double[n];

            try
            {

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        result[i] += Math.Pow(A[j, i], 2);
                    }
                    result[i] = Math.Sqrt(result[i]);
                }

            }
            catch (Exception ex)
            {

            }


            return result;
        }

        public static double[] CalcularVectorDeFuerzasFicticiasEquivalentesCombinada(double[,] P)
        {
            int n = P.GetLength(0);
            double[] result = new double[n];

            try
            {

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        result[i] += Math.Pow(P[j, i], 2);
                    }

                    result[i] = Math.Sqrt(result[i]);
                }

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static double[] CalcularVectorDeFuerzasCortantesCombinada(double[,] Vn)
        {
            int n = Vn.GetLength(0);
            double[] result = new double[n];

            try
            {

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        result[i] += Math.Pow(Vn[j, i], 2);
                    }

                    result[i] = Math.Sqrt(result[i]);
                }

            }
            catch (Exception ex)
            {

            }


            return result;
        }

        public static double[] CalcularVectorDeDesplazamientosCombinado(double[,] DEnt)
        {
            int n = DEnt.GetLength(0);
            double[] result = new double[n];

            try
            {

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        result[i] += Math.Pow(DEnt[j, i], 2);
                    }

                    result[i] = Math.Sqrt(result[i]);
                }

            }
            catch (Exception ex)
            {

            }


            return result;
        }

        public static double[] CalcularVectorDeFuerzasCortantesDeDisenioEstatico(double c, double Q, double R, Structure structure, Enums.SideType side)
        {
            double[] result = new double[structure.Storeys.Count];
            double[] m = new double[structure.Storeys.Count];
            double[] h = new double[structure.Storeys.Count];
            double SumWh = 0;
            try
            {
                for (int i = 0; i < structure.Storeys.Count; i++)
                    m[i] = structure.Storeys[i].MasasEntrepisos;

                if (side.Equals(Enums.SideType.Horizontal))
                {
                    for (int i = 0; i < structure.Storeys.Count && side.Equals(Enums.SideType.Horizontal); i++)
                    {
                        if (i == 0)
                            h[i] = structure.Storeys[i].HorizontalWalls.Max(x => x.Height);
                        else
                            h[i] = structure.Storeys[i - 1].HorizontalWalls.Max(x => x.Height) + structure.Storeys[i].HorizontalWalls.Max(x => x.Height); ;
                    }
                }

                if (side.Equals(Enums.SideType.Vertical))
                {
                    for (int i = 0; i < structure.Storeys.Count && side.Equals(Enums.SideType.Vertical); i++)
                    {
                        if (i == 0)
                            h[i] = structure.Storeys[i].VerticalWalls.Max(x => x.Height);
                        else
                            h[i] = structure.Storeys[i - 1].VerticalWalls.Max(x => x.Height) + structure.Storeys[i].VerticalWalls.Max(x => x.Height); ;
                    }
                }


                for (int i = 0; i < structure.Storeys.Count; i++)
                {
                    SumWh += m[i] * h[i];
                }

                for (int i = 0; i < structure.Storeys.Count; i++)
                {
                    result[i] = (c / (Q * R)) * m[i] * h[i] * (structure.MasaTotal / SumWh);
                }
            }
            catch (Exception)
            {

            }

            return result;
        }

        public static double[] CalcularDesplazamientosLaterales(double[] F, double[] K)
        {
            double[] result = new double[F.Length];
            try
            {

                for (int i = 0; i < F.Length; i++)
                    result[i] = F[i] / K[i];

            }
            catch (Exception)
            {

            }

            return result;
        }

        public static double CalcularPeriodoFundamental(double[] m, double[] X, double[] F)
        {
            double result = 0;
            try
            {
                double Aux1 = 0, Aux2 = 0;
                for (int i = 0; i < m.Length; i++)
                    Aux1 += (m[i] * Math.Pow(X[i], 2));

                for (int i = 0; i < m.Length; i++)
                    Aux2 += (F[i] * X[i]);

                Aux2 *= 981;

                result = 2 * PI * Math.Sqrt(Aux1 / Aux2);
            }
            catch (Exception)
            {

            }

            return result;
        }

        #endregion

        #region Operaciones para muros
        public static double CalcularInercia(Wall wall)
        {
            double result = 0;

            result = (wall.Thickness * Math.Pow(wall.Length, 3)) / 12;

            return result;
        }

        public static double CalcularAreaLongitudinal(Wall wall)
        {
            double result = 0;

            result = wall.Thickness * wall.Length;

            return result;
        }

        public static double CalcularRigidezLateral(Wall wall, Material material)
        {

            double result = 0;
            double H = wall.Height;
            double E = Convert.ToDouble(material.Em);
            double I = wall.Inercia;
            double G = Convert.ToDouble(material.Gm);
            double A = wall.AreaLongitudinal;

            result = Math.Pow(((4 * Math.Pow(H, 3)) / (12 * E * I)) + ((0.2 * H) / (G * A)), -1);

            return result;
        }

        public static double CalcularFuerzasCortantesDirectas(Wall wall, double Fc, double RlTotal)
        {
            double result = 0;

            result = (wall.RigidezLateral * Fc) / RlTotal;

            return Math.Round(result, 5);
        }

        public static string CalcularClasificacionMuro(double CM, double CT, Wall wall)
        {
            string result = "Clasificacion";

            if (wall.Side.Equals(Enums.SideType.Horizontal))
            {
                if (CM < CT)
                {
                    if (wall.PositionY <= CM)
                        result = "Flexible";
                    else
                        result = "Rigido";
                }
                else
                {
                    if (wall.PositionY <= CM)
                        result = "Rigido";
                    else
                        result = "Flexible";
                }
            }
            if (wall.Side.Equals(Enums.SideType.Vertical))
            {
                if (CM < CT)
                {
                    if (wall.PositionX <= CM)
                        result = "Flexible";
                    else
                        result = "Rigido";
                }
                else
                {
                    if (wall.PositionX <= CM)
                        result = "Rigido";
                    else
                        result = "Flexible";
                }
            }

            return result;
        }

        public static double CalcularExcentricidadesDisenio(double EE, double EA, string classific, Enums.SideType side, Enums.Coordenate coordenate)
        {
            double result = 0;


            if (side.Equals(Enums.SideType.Horizontal))
            {
                if (coordenate.Equals(Enums.Coordenate.X))
                    result = 1.5 * EE + EA;

                if (coordenate.Equals(Enums.Coordenate.Y))
                {
                    if (classific.Equals("Flexible"))
                        result = 1.5 * EE + EA;
                    else
                    {
                        if (Math.Abs(EE) > EA)
                            result = 0;
                        else
                            result = EA - Math.Abs(EE);
                    }
                }
            }
            if (side.Equals(Enums.SideType.Vertical))
            {
                if (coordenate.Equals(Enums.Coordenate.X))
                {
                    if (classific.Equals("Flexible"))
                        result = 1.5 * EE + EA;
                    else
                    {
                        if (Math.Abs(EE) > EA)
                            result = 0;
                        else
                            result = EA - Math.Abs(EE);
                    }
                }
                if (coordenate.Equals(Enums.Coordenate.Y))
                    result = 1.5 * EE + EA;
            }

            return Math.Round(result, 5);
        }


        public static double CalcularCortanteTorsion(Wall wall, double CTX, double CTY, double EEX, double EEY, double EAX, double EAY, double RTE, double Fcx, double Fcy, Enums.Coordenate coordenate)
        {
            //s - Estatica a - Accidental
            double result = 0;
            if (wall.Side.Equals(Enums.SideType.Horizontal))
            {
                if (wall.Clasificacion.Equals("Flexible"))
                {
                    if (coordenate.Equals(Enums.Coordenate.X))
                    {
                        double dif = wall.PositionY - CTY;
                        result = wall.RigidezLateral * Math.Abs(dif) / RTE * (1.5 * Fcx * EEY);
                    }
                    if (coordenate.Equals(Enums.Coordenate.Y))
                    {
                        double dif = wall.PositionY - CTY;
                        double a = EAY * Fcx;
                        double b = 0.3 * EAX * Fcy;
                        result = wall.RigidezLateral * Math.Abs(dif) / RTE * (0.3 * 1.5 * Fcy * EEX + Math.Max(a, b));
                    }
                }
                else if (wall.Clasificacion.Equals("Rigido"))
                {
                    if (coordenate.Equals(Enums.Coordenate.X))
                    {
                        double dif = wall.PositionY - CTY;
                        result = wall.RigidezLateral * Math.Abs(dif) / RTE * (0.3 * 1.5 * Fcy * EEX);
                    }
                    if (coordenate.Equals(Enums.Coordenate.Y))
                    {
                        double dif = wall.PositionY - CTY;
                        double a = 0.3 * EAX * Fcy;
                        double b = 0;
                        if (EEY < EAY)
                            b = (EAY - EEY) * Fcx;
                        result = wall.RigidezLateral * Math.Abs(dif) / RTE * Math.Max(a, b);
                    }
                }
            }

            if (wall.Side.Equals(Enums.SideType.Vertical))
            {
                if (wall.Clasificacion.Equals("Flexible"))
                {
                    if (coordenate.Equals(Enums.Coordenate.X))
                    {
                        double dif = wall.PositionX - CTX;
                        double a = EAX * Fcy;
                        double b = 0.3 * EAY * Fcx;
                        result = wall.RigidezLateral * Math.Abs(dif) / RTE * (0.3 * 1.5 * Fcx * EEY + Math.Max(a, b));
                    }
                    if (coordenate.Equals(Enums.Coordenate.Y))
                    {
                        double dif = wall.PositionX - CTX;
                        result = wall.RigidezLateral * Math.Abs(dif) / RTE * (1.5 * Fcy * EEX);
                    }
                }
                else if (wall.Clasificacion.Equals("Rigido"))
                {
                    if (coordenate.Equals(Enums.Coordenate.X))
                    {
                        double dif = wall.PositionX - CTX;
                        double a = 0;
                        if (EEX < EAX)
                            a = (EAX - EEX) * Fcy;
                        double b = 0.3 * EAY * Fcx;
                        result = wall.RigidezLateral * Math.Abs(dif) / RTE * Math.Max(a, b);
                    }
                    if (coordenate.Equals(Enums.Coordenate.Y))
                    {
                        double dif = wall.PositionX - CTX;
                        result = wall.RigidezLateral * Math.Abs(dif) / RTE * (0.3 * 1.5 * Fcx * EEY);
                    }
                }
            }
            return Math.Round(result, 5);
        }

        public static double CalcularCortantesTotales(Wall wall)
        {
            double result = wall.CortanteDirecto + wall.CortantePorTorsionX + wall.CortantePorTorsionY;

            return Math.Round(result, 5);
        }

        public static double CalcularMomentoVolteo(Wall wall, double RE, double MV)
        {
            double result = 0;

            result = MV * wall.RigidezLateral / RE;

            return Math.Round(result, 5);
        }


        public static double CalcularCargaAxialUltima(Wall wall, XMLLoadAnalysisData entrepiso, int nivel, double CAU, Structure structure)
        {
            double result = 0, CM = 0, CV = 0, P = 0, A = 0;
          
            #region Entrepisos
            if (entrepiso.LosaMacizaAzoteaP != null)
            {
                CM = entrepiso.LosaMacizaAzoteaP.CargaMuerta;
                CV = entrepiso.LosaMacizaAzoteaP.CargaVivaMaxima;
            }
            if (entrepiso.LosaMacizaEntrepisoP != null)
            {
                CM = entrepiso.LosaMacizaEntrepisoP.CargaMuerta;
                CV = entrepiso.LosaMacizaEntrepisoP.CargaVivaMaxima;
            }
            if (entrepiso.LosaNervadaAzoteaP != null)
            {
                CM = entrepiso.LosaNervadaAzoteaP.CargaMuerta;
                CV = entrepiso.LosaNervadaAzoteaP.CargaVivaMaxima;
            }
            if (entrepiso.LosaNervadaEntrepisoP != null)
            {
                CM = entrepiso.LosaNervadaEntrepisoP.CargaMuerta;
                CV = entrepiso.LosaNervadaEntrepisoP.CargaVivaMaxima;
            }
            if (entrepiso.LosaViguetaBovedillaAzoteaP != null)
            {
                CM = entrepiso.LosaViguetaBovedillaAzoteaP.CargaMuerta;
                CV = entrepiso.LosaViguetaBovedillaAzoteaP.CargaVivaMaxima;
            }
            if (entrepiso.LosaViguetaBovedillaEntrepisoP != null)
            {
                CM = entrepiso.LosaViguetaBovedillaEntrepisoP.CargaMuerta;
                CV = entrepiso.LosaViguetaBovedillaEntrepisoP.CargaVivaMaxima;
            }
            if (entrepiso.LosaOtroTipoP != null)
            {
                CM = entrepiso.LosaOtroTipoP.CargaMuerta;
                CV = entrepiso.LosaOtroTipoP.CargaVivaMaxima;
            }
            #endregion

            double X = wall.PositionX;
            double Y = wall.PositionY;
            int n = structure.Storeys.Count - 1;
            if (wall.Side.Equals(Enums.SideType.Horizontal))
            {
                if (nivel == n + 1)
                {
                    result = wall.Peso + (1.3 * CM + 1.5 * CV) * wall.TributaryArea;
                }
                else
                {
                    result = wall.Peso + (1.3 * CM + 1.5 * CV) * wall.TributaryArea + CAU;
                }
            }
            if (wall.Side.Equals(Enums.SideType.Vertical))
            {
                if (nivel == n + 1)
                {
                    result = wall.Peso + (1.3 * CM + 1.5 * CV) * wall.TributaryArea;
                }
                else
                {
                    result = wall.Peso + (1.3 * CM + 1.5 * CV) * wall.TributaryArea + CAU;
                }
            }

            return Math.Round(result, 5);
        }


        public static double CalcularCargaAxialDeSismo(Wall wall, XMLLoadAnalysisData entrepiso, int nivel, double CAU, Structure structure)
        {
            double result = 0, CM = 0, CV = 0, P = 0, A = 0;
            
            #region entrepiso
            if (entrepiso.LosaMacizaAzoteaP != null)
            {
                CM = entrepiso.LosaMacizaAzoteaP.CargaMuerta;
                CV = entrepiso.LosaMacizaAzoteaP.CargaVivaInstantanea;
            }
            if (entrepiso.LosaMacizaEntrepisoP != null)
            {
                CM = entrepiso.LosaMacizaEntrepisoP.CargaMuerta;
                CV = entrepiso.LosaMacizaEntrepisoP.CargaVivaInstantanea;
            }
            if (entrepiso.LosaNervadaAzoteaP != null)
            {
                CM = entrepiso.LosaNervadaAzoteaP.CargaMuerta;
                CV = entrepiso.LosaNervadaAzoteaP.CargaVivaInstantanea;
            }
            if (entrepiso.LosaNervadaEntrepisoP != null)
            {
                CM = entrepiso.LosaNervadaEntrepisoP.CargaMuerta;
                CV = entrepiso.LosaNervadaEntrepisoP.CargaVivaInstantanea;
            }
            if (entrepiso.LosaViguetaBovedillaAzoteaP != null)
            {
                CM = entrepiso.LosaViguetaBovedillaAzoteaP.CargaMuerta;
                CV = entrepiso.LosaViguetaBovedillaAzoteaP.CargaVivaInstantanea;
            }
            if (entrepiso.LosaViguetaBovedillaEntrepisoP != null)
            {
                CM = entrepiso.LosaViguetaBovedillaEntrepisoP.CargaMuerta;
                CV = entrepiso.LosaViguetaBovedillaEntrepisoP.CargaVivaInstantanea;
            }
            if (entrepiso.LosaOtroTipoP != null)
            {
                CM = entrepiso.LosaOtroTipoP.CargaMuerta;
                CV = entrepiso.LosaOtroTipoP.CargaVivaInstantanea;
            }
            #endregion

            double X = wall.PositionX;
            double Y = wall.PositionY;
            int n = structure.Storeys.Count - 1;
            if (wall.Side.Equals(Enums.SideType.Horizontal))
            {
                if (nivel == n + 1)
                {
                    result = wall.Peso + (CM + CV) * wall.TributaryArea;
                }
                else
                {
                    result = wall.Peso + (CM + CV) * wall.TributaryArea + CAU;
                }
            }
            if (wall.Side.Equals(Enums.SideType.Vertical))
            {
                if (nivel == n + 1)
                {
                    result = wall.Peso + (CM + CV) * wall.TributaryArea;
                }
                else
                {
                    result = wall.Peso + (CM + CV) * wall.TributaryArea+ CAU;
                }
            }

            return Math.Round(result, 5);
        }

        public static double CalcularExcentricidadDeCarga(Wall wall)
        {

            double result = (wall.Thickness / 2) - (wall.AnchoDeApoyo / 3);
            return Math.Round(result, 5);
        }

        public static double CalcularFactorDeAlturaEfectiva(Wall wall)
        {
            double result = 0;

            if (wall.Tipo.Equals("Interior"))
                result = 0.8;
            else
            {
                if (!wall.RestringidoSuperiorInferior)
                    result = 2;
                else
                    result = 1;
            }

            return Math.Round(result, 5);
        }

        public static double CalcularFactorReduccionExcentricidadEsbeltez(Wall wall, XMLLoadAnalysisData entrepiso)
        {
            double result = 0, CV = 0, CM = 0;
            if (entrepiso.LosaMacizaAzoteaP != null)
            {
                CM = entrepiso.LosaMacizaAzoteaP.CargaMuerta;
                CV = entrepiso.LosaMacizaAzoteaP.CargaVivaInstantanea;
            }
            if (entrepiso.LosaMacizaEntrepisoP != null)
            {
                CM = entrepiso.LosaMacizaEntrepisoP.CargaMuerta;
                CV = entrepiso.LosaMacizaEntrepisoP.CargaVivaInstantanea;
            }
            if (entrepiso.LosaNervadaAzoteaP != null)
            {
                CM = entrepiso.LosaNervadaAzoteaP.CargaMuerta;
                CV = entrepiso.LosaNervadaAzoteaP.CargaVivaInstantanea;
            }
            if (entrepiso.LosaNervadaEntrepisoP != null)
            {
                CM = entrepiso.LosaNervadaEntrepisoP.CargaMuerta;
                CV = entrepiso.LosaNervadaEntrepisoP.CargaVivaInstantanea;
            }
            if (entrepiso.LosaViguetaBovedillaAzoteaP != null)
            {
                CM = entrepiso.LosaViguetaBovedillaAzoteaP.CargaMuerta;
                CV = entrepiso.LosaViguetaBovedillaAzoteaP.CargaVivaInstantanea;
            }
            if (entrepiso.LosaViguetaBovedillaEntrepisoP != null)
            {
                CM = entrepiso.LosaViguetaBovedillaEntrepisoP.CargaMuerta;
                CV = entrepiso.LosaViguetaBovedillaEntrepisoP.CargaVivaInstantanea;
            }
            if (entrepiso.LosaOtroTipoP != null)
            {
                CM = entrepiso.LosaOtroTipoP.CargaMuerta;
                CV = entrepiso.LosaOtroTipoP.CargaVivaInstantanea;
            }

            if (wall.RestringidoSuperiorInferior)
            {
                if (wall.ExcentricidadDeCarga <= (wall.Thickness / 6))
                {
                    if ((wall.Height / wall.Thickness) <= 20)
                    {
                        if (wall.Tipo.Equals("Interior"))
                        {
                            if ((Math.Min(wall.LongClaroIzquierdo, wall.LongClaroDerecho) / Math.Max(wall.LongClaroIzquierdo, wall.LongClaroDerecho)) > 0.5)
                            {
                                result = 0.7;
                            }
                        }
                        else if (wall.Tipo.Equals("Extremo"))
                        {
                            if ((Math.Min(wall.LongClaroIzquierdo, wall.LongClaroDerecho) / Math.Max(wall.LongClaroIzquierdo, wall.LongClaroDerecho)) < 0.5 || (CV / CM) > 1)
                            {
                                result = 0.6;
                            }
                        }
                    }
                }
            }
            else if ((wall.ExcentricidadDeCarga > (wall.Thickness / 6)) || ((wall.Height / wall.Thickness) > 20))
            {
                double e_ = wall.ExcentricidadDeCarga + (wall.Thickness / 24);
                double a = 1 - (2 * e_) / wall.Thickness;
                double b = (wall.FactorAlturaEfectiva * wall.Height) / (30 * wall.Thickness);
                double c = 1 - Math.Pow(b, 2);
                result = a * c;
            }
            return Math.Round(result, 5);
        }

        public static double CalcularResistenciaCompresionPura(Wall wall, Material material)
        {

            double result = 0.6 * wall.FE * (Convert.ToDouble(material.Dfm) * wall.Length * wall.Thickness + (wall.As * 2) * wall.Fy);

            return result;
        }


        public static double CalcularResistenciaFlexionPura(Wall wall)
        {

            double Dd = wall.Length - wall.bc;
            double result = wall.As * wall.Fy * Dd;

            return result;
        }

        public static double CalcularP1X(Wall wall)
        {
            double result = 0;

            return result;
        }

        public static double CalcularP1Y(Wall wall)
        {

            double result = -0.8 * wall.FE * wall.As * wall.Fy;

            return result;
        }


        public static double CalcularP2X(Wall wall)
        {
            double result = wall.Mo * 0.8;

            return result;
        }


        public static double CalcularP2Y(Wall wall)
        {
            double result = 0;

            return result;
        }


        public static double CalcularP3X(Wall wall)
        {
            double d = wall.Length - (wall.bc / 2);
            double result = 0.8 * wall.Mo + 0.3 * (wall.PR / 3) * d;
            return result;
        }

        public static double CalcularP3Y(Wall wall)
        {

            double result = wall.PR / 3;
            return result;
        }


        public static double CalcularP4X(Wall wall)
        {
            double d = wall.Length - (wall.bc / 2);
            double result = (1.5 * 0.6 * wall.Mo + 0.15 * wall.PR * d) * 0.66666666;
            return Math.Round(result, 3);
        }

        public static double CalcularP4Y(Wall wall)
        {
            double result = wall.PR / 3;
            return result;
        }

        public static double CalcularP5X(Wall wall)
        {
            double result = 0;
            return result;
        }

        public static double CalcularP5Y(Wall wall)
        {
            double result = wall.PR;
            return result;
        }

        public static double CalcularTP1P2(Wall wall)
        {
            double result = 0;

            return result;
        }

        public static double CalcularTP2P3(Wall wall)
        {
            double result = 0/*wall.P1X - (wall.P1X/wall.ResistenciaFlexionPura) * */;

            return result;
        }

        public static double CalcularTP3P4(Wall wall)
        {
            double result = 0;

            return result;
        }

        public static double CalcularTP4P5(Wall wall)
        {
            double result = 0;

            return result;
        }

        public static double CalcularResistenciaMamposteriaCortante(Wall wall, Material material)
        {

            double f = 0;

            if (wall.Height / wall.Length < 0.2)
                f = 1.5;
            else if (wall.Height / wall.Length >= 1)
                f = 1;
            else
                f = 1.625 - 0.625 * (wall.Height / wall.Length);


            double a = 0.7 * ((0.5 * Convert.ToDouble(material.Dvm) * wall.AreaLongitudinal + 0.3 * wall.CargaAxialSismo) * f);

            double b = 1.5 * 0.7 * Convert.ToDouble(material.Dvm) * wall.AreaLongitudinal * f;

            double result = Math.Max(a, b);


            return Math.Round(result,5);
        }

        public static double CalcularResistenciaAceroRefuerzoHorizontalCortante(Wall wall, Material material)
        {
            if (!wall.AceroDeRefuerzo)
                return 0;

            double n = 0, ph = 0, fyh = 0, k0 = 0, k1 = 0, fan = 0, ns = 0, ns1 = 0, Dfm = 0, Ab = 0, An = 0;

            fyh = wall.EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal;
            Dfm = Convert.ToDouble(material.Dfm);
            An = Convert.ToDouble(material.An);
            Ab = Convert.ToDouble(material.Ab);

            ph = wall.Ash / (wall.Sh * wall.Thickness);

            if (wall.Height / wall.Length <= 1)
                k0 = 1.3;
            else if (wall.Height / wall.Length >= 1.5)
                k0 = 1;
            else
                k0 = 1.9 - 0.6 * (wall.Height / wall.Length);

            fan = An / Ab;

            k1 = Math.Max((1 - 0.045 * ph * fyh), (1 - 0.1 * fan * Dfm * 0.045));

            if (Dfm >= 90)
                ns1 = 0.75;
            else if (Dfm >= 60)
                ns1 = 0.55;
            else
                ns1 = 0.15 + 0.00666666666666666666666666666667 * Dfm;


            if ((ph * fyh) > (0.1 * fan * Dfm))
                ns = ns1;
            else
                ns = ns1 * ((0.1 * fan * Dfm) / ph * fyh);

            n = (wall.ResMamposteriaCortante / (0.7 * ph * fyh * wall.AreaLongitudinal)) * (k0 * k1 - 1) + ns;

            double result = 0.7 * n * ph * fyh * wall.AreaLongitudinal;


            return Math.Round(result,5);
        }

        public static double CalcularResistenciaTotalACortante(Wall wall)
        {
            double result = wall.ResMamposteriaCortante + wall.ResAceroRefuerzoHorizontalCortante;


            return Math.Round(result,5);
        }

        public static double CalcularResistenciaCortanteConcreto(Wall wall)
        {
            double result = 0;

            return result;
        }

        public static string CalcularConclusionPorCortante(Wall wall)
        {
            string result = string.Empty;
            if (wall.CortanteTotales < wall.ResistenciaTotalACortante)
                result = "SI PASA";
            else
                result = "NO PASA";

            return result;
        }

        public static bool ValidarGrafica(Wall wall)
        {
            bool result = false;
            int IsGraphValidCondition = 0;
            try
            {
                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Xa;
                MWNumericArray Ya;
                MWNumericArray Pa;

                    double[] X = { wall.P1X, wall.P2X, wall.P3X, wall.P4X, wall.P5X };
                    double[] Y = { wall.P1Y, wall.P2Y, wall.P3Y, wall.P4Y, wall.P5Y };
                    double[,] P = { { wall.MomentoVolteo }, { wall.CargaAxialMaxima } };
                    Xa = new MWNumericArray(X);
                    Ya = new MWNumericArray(Y);
                    Pa = new MWNumericArray(P);
                
                string titulo = wall.WallNumber.ToString();

                var IsValid = operations.isValidToPlot(Xa, Ya, Pa);
                IsGraphValidCondition = Convert.ToInt32(IsValid.ToString());

                if (IsGraphValidCondition == 1)
                    result = true;


            }
            catch (Exception ex)
            {

            }

            return result;

        }


        public static void Graficar(Wall wall)
        {
            try
            {
                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                double[] X = { wall.P1X, wall.P2X, wall.P3X, wall.P4X, wall.P5X };
                double[] Y = { wall.P1Y, wall.P2Y, wall.P3Y, wall.P4Y, wall.P5Y };
                double[,] P = { { wall.MomentoVolteo }, { wall.CargaAxialMaxima } };

                string titulo = wall.WallNumber.ToString();

                MWNumericArray Xa = new MWNumericArray(X);
                MWNumericArray Ya = new MWNumericArray(Y);
                MWNumericArray Pa = new MWNumericArray(P);
                operations.plotter(Xa, Ya, Pa, titulo);


            }
            catch (Exception ex)
            {

            }
        }


        public static bool CalcularBc(Wall wall)
        {
            bool result = false;

            double AsMax = (0.85 * wall.Dfc / wall.Fy) * (0.85 * 0.9 * 6000 / (wall.Fy + 6000)) * wall.bc * wall.Thickness;
            if (wall.As  > AsMax)
                result = true;

            return result;
        }
        #endregion



        #region Operaciones para niveles

        public static double CalcularRigidezEntrepiso(Storey storey, Enums.SideType side)
        {
            double result = 0;

            if (side.Equals(Enums.SideType.Horizontal))
            {
                foreach (var wall in storey.HorizontalWalls)
                {
                    result += wall.RigidezLateral;
                }
            }


            if (side.Equals(Enums.SideType.Vertical))
            {
                foreach (var wall in storey.VerticalWalls)
                {
                    result += wall.RigidezLateral;
                }
            }

            return result;
        }

        public static double CalcularMasasEntrepiso(List<Material> materials, Storey storey, XMLLoadAnalysisData entrepiso)
        {
            double result = 0;
            double At = 0;
            double Pesom = 0;

            storey.HorizontalWalls.ToList().ForEach(X =>
            {
                At += X.TributaryArea;
                Pesom += X.Thickness / 100 * X.Height / 100 * X.Length / 100 * Convert.ToDouble(materials.Single(Y => Y.Name == X.Material).PV);
            });
            storey.VerticalWalls.ToList().ForEach(X =>
            {
                At += X.TributaryArea;
                Pesom += X.Thickness / 100 * X.Height / 100 * X.Length / 100 * Convert.ToDouble(materials.Single(Y => Y.Name == X.Material).PV);
            });

            if (entrepiso.LosaMacizaEntrepisoP != null)
                result = ((entrepiso.LosaMacizaEntrepisoP.CargaMuerta + entrepiso.LosaMacizaEntrepisoP.CargaVivaInstantanea) * At + (Pesom / 2));

            if (entrepiso.LosaMacizaAzoteaP != null)
                result = ((entrepiso.LosaMacizaAzoteaP.CargaMuerta + entrepiso.LosaMacizaAzoteaP.CargaVivaInstantanea) * At + (Pesom / 2));

            if (entrepiso.LosaNervadaEntrepisoP != null)
                result = ((entrepiso.LosaNervadaEntrepisoP.CargaMuerta + entrepiso.LosaNervadaEntrepisoP.CargaVivaInstantanea) * At + (Pesom / 2));

            if (entrepiso.LosaNervadaAzoteaP != null)
                result = ((entrepiso.LosaNervadaAzoteaP.CargaMuerta + entrepiso.LosaNervadaAzoteaP.CargaVivaInstantanea) * At + (Pesom / 2));

            if (entrepiso.LosaViguetaBovedillaEntrepisoP != null)
                result = ((entrepiso.LosaViguetaBovedillaEntrepisoP.CargaMuerta + entrepiso.LosaViguetaBovedillaEntrepisoP.CargaVivaInstantanea) * At + (Pesom / 2));

            if (entrepiso.LosaViguetaBovedillaAzoteaP != null)
                result = ((entrepiso.LosaViguetaBovedillaAzoteaP.CargaMuerta + entrepiso.LosaViguetaBovedillaAzoteaP.CargaVivaInstantanea) * At + (Pesom / 2));

            if (entrepiso.LosaOtroTipoP != null)
                result = ((entrepiso.LosaOtroTipoP.CargaMuerta + entrepiso.LosaOtroTipoP.CargaVivaInstantanea) * At + (Pesom / 2));

            return result;
        }

        public static double CalcularCentroDeMasas(Storey piso, Enums.Coordenate coordenate)
        {
            double result = 0, a = 0, b = 0;
            try
            {
                if (coordenate.Equals(Enums.Coordenate.X))
                {
                    foreach (var muro in piso.HorizontalWalls)
                    {
                        a += muro.PositionY * muro.WTotal;
                        b += muro.WTotal;
                    }

                }
                if (coordenate.Equals(Enums.Coordenate.Y))
                {
                    foreach (var muro in piso.VerticalWalls)
                    {
                        a += muro.PositionX * muro.WTotal;
                        b += muro.WTotal;
                    }
                }

                result = a / b;

            }
            catch (Exception ex)
            {

            }
            return Math.Round(result, 5);
        }

        public static double CalcularCentroCortante(double V, double Fc, double CM, Enums.Coordenate coordenate, bool isLast, double CMant = 0, double Fca = 0)
        {

            double result = 0, a = 0, b = 0;
            try
            {
                if (isLast)
                {
                    result += (Fc * CM) / V;
                }
                else
                {
                    result += ((Fc * CM) + (Fca * CMant)) / V;
                }
            }
            catch (Exception ex)
            {

            }

            return Math.Round(result, 5);
        }

        public static double CalcularCentroTorsion(Storey storey, Enums.Coordenate coordenate)
        {
            double result = 0;

            if (coordenate.Equals(Enums.Coordenate.X))
            {
                result = storey.HorizontalWalls.Sum(x => x.RigidezLateral * x.PositionY) / storey.HorizontalWalls.Sum(x => x.RigidezLateral);
            }
            if (coordenate.Equals(Enums.Coordenate.Y))
            {
                result = storey.VerticalWalls.Sum(x => x.RigidezLateral * x.PositionX) / storey.VerticalWalls.Sum(x => x.RigidezLateral);
            }
            return Math.Round(result, 5);
        }


        public static double CalcularExcentricidadesEstaticas(double CM, double CT)
        {
            double a = Math.Round(CM, 5) - Math.Round(CT, 5);
            double result = Math.Abs(a);
            return Math.Round(result, 5);
        }

        public static double CalcularExcentricidadesAccidentales(int Niveles, Storey storey, Enums.Coordenate coordenate)
        {
            double result = 0;

            if (coordenate.Equals(Enums.Coordenate.X))
            {

                double a = storey.VerticalWalls.Max(x => x.PositionX);
                double b = storey.HorizontalWalls.Max(x => x.PositionX);
                double Max = 0;
                if (a > b)
                    Max = a;
                else
                    Max = b;

                result = (0.05 + ((storey.StoreyNumber - 1) / (Niveles - 1)) * 0.05) * Max;

            }
            if (coordenate.Equals(Enums.Coordenate.Y))
            {
                double a = storey.VerticalWalls.Max(x => x.PositionY);
                double b = storey.HorizontalWalls.Max(x => x.PositionY);
                double Max = 0;
                if (a > b)
                    Max = a;
                else
                    Max = b;

                result = (0.05 + ((storey.StoreyNumber - 1) / (Niveles - 1)) * 0.05) * Max;
            }
            return Math.Round(result, 5);
        }


        public static double CalcularExcentricidadDisenioEntrepiso(double EEstatica, double EAccidental)
        {
            double result = EEstatica + EAccidental;
            return Math.Round(result, 0);
        }

        public static double CalcularMomentosTorsionantes(double ED, double Fs)
        {
            double result = ED * Fs;

            return Math.Round(result, 5);
        }

        public static double CalcularRigidezTorsionalEntrepiso(Storey storey, double CTx, double CTy)
        {
            double result = 0, a = 0, b = 0;

            foreach (var wall in storey.HorizontalWalls)
            {
                a += wall.RigidezLateral * Math.Pow(wall.PositionY - CTy, 2);
            }

            foreach (var wall in storey.VerticalWalls)
            {
                b += wall.RigidezLateral * Math.Pow((wall.PositionX - CTx), 2);
            }

            result = a + b;

            return Math.Round(result, 5);
        }

        public static double CalcularMomentoVolteoEntrepiso(Storey storey, double CE)
        {
            double result = 0;

            double Mh = storey.HorizontalWalls.Max(x => x.Height);
            double Mv = storey.VerticalWalls.Max(x => x.Height);
            double Max = Math.Max(Mh, Mv);

            result = CE * Max;

            return Math.Round(result, 5);
        }


        public static string CalcularConclusionPorCortanteDeEntrepiso(double V, double VRT)
        {
            string result = string.Empty;


            if (V < VRT)
                result = "SI PASA";
            else
                result = "NO PASA";

            return result;
        }


        public static double CalcularEsfuerzoNormalPromedioDeEntrepiso(Storey storey, Material material)
        {
            double P = storey.HorizontalWalls.Count > 0 ? storey.HorizontalWalls.Average(x => x.CargaAxialMaxima / x.AreaLongitudinal) : 0.0;
            double Dvm = Convert.ToDouble(material.Dvm);
            double result = Math.Max(P, 3.33 * Dvm);

            return Math.Round(result, 5);
        }

        public static double CalcularCortanteResistenteEntrepisoMamposteria(Storey storey, Material material)
        {
            double Dvm = Convert.ToDouble(material.Dvm);
            double nMin = 0, phmin = 0;
            double n = 0, ph = 0, fyh = 0, k0 = 0, k1 = 0, fan = 0, ns = 0, ns1 = 0, Dfm = 0, Ab = 0, An = 0;

            for (int i = 0; i < storey.HorizontalWalls.Count; i++)
            {
                Wall wall = storey.HorizontalWalls[i];
                fyh = wall.EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal;
                Dfm = Convert.ToDouble(material.Dfm);
                An = Convert.ToDouble(material.An);
                Ab = Convert.ToDouble(material.Ab);

                ph = wall.Ash / (wall.Sh * wall.Thickness);

                if (i == 0 || ph < phmin)
                    phmin = ph;

                if (wall.Height / wall.Length <= 1)
                    k0 = 1.3;
                else if (wall.Height / wall.Length >= 1.5)
                    k0 = 1;
                else
                    k0 = 1.9 - 0.6 * (wall.Height / wall.Length);

                fan = An / Ab;

                k1 = Math.Max((1 - 0.045 * ph * fyh), (1 - 0.1 * fan * Dfm * 0.045));

                if (Dfm >= 90)
                    ns1 = 0.75;
                else if (Dfm >= 60)
                    ns1 = 0.55;
                else
                    ns1 = 0.15 + 0.00666666666666666666666666666667 * Dfm;


                if ((ph * fyh) > (0.1 * fan * Dfm))
                    ns = ns1;
                else
                    ns = ns1 * ((0.1 * fan * Dfm) / ph * fyh);

                n = (wall.ResMamposteriaCortante / (0.7 * ph * fyh * wall.AreaLongitudinal)) * (k0 * k1 - 1) + ns;

                if (i == 0 || n < nMin)
                    nMin = n;
            }

            double SumAT = storey.HorizontalWalls.Sum(x => x.AreaLongitudinal);

            double result = (0.7 * 0.5 * Dvm + 0.3 * storey.EsfuerzoNormalPromedio) * SumAT + nMin * phmin * fyh * SumAT;



            return Math.Round(result, 5);
        }

        public static double CalcularCortanteResistenteEntrepisoConcreto(Storey storey)
        {
            double result = storey.HorizontalWalls.Sum(x => x.ResistenciaCortanteConcreto);


            return Math.Round(result, 5);
        }

        public static double CalcularCortanteEntrepisoTotal(Storey storey)
        {
            double result = storey.CortanteResistenteEntrepisoMamposteria + storey.CortanteResistenteEntrepisoConcreto;

            return Math.Round(result, 5);
        }

        #endregion

    }//end of class
}//end of namespace
