﻿using StructureSystem.Model;
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

        public static double[] CalcularVectorPeriodosCirculares(double[,] M, double[,] K)
        {
            double[] result = new double[M.GetLength(0)];
            try
            {
                int n = M.GetLength(0);
                //M = new double[,]
                //    {
                //        { 1.8* Math.Pow(10,5), 0, 0, 0, 0, 0},
                //        { 0, 1.85 * Math.Pow(10,5), 0, 0, 0, 0},
                //        { 0, 0, 1.9 * Math.Pow(10,5) ,0 ,0, 0},
                //        { 0, 0, 0, 2 * Math.Pow(10,5), 0 ,0},
                //        { 0, 0, 0,0, 2 * Math.Pow(10,5), 0},
                //        { 0 ,0, 0 ,0, 0, 1.9 * Math.Pow(10,5)} };

                //K = new double[,] {
                //{9.585 * Math.Pow(10,7), - 5.099* Math.Pow(10,7), 0, 0, 0, 0},
                //{ -5.099* Math.Pow(10,7) ,9.993 * Math.Pow(10,7),- 4.895 * Math.Pow(10,7),0, 0, 0},
                //{ 0, - 4.895 * Math.Pow(10,7),9.687* Math.Pow(10,7), - 4.793* Math.Pow(10,7), 0, 0},
                //{ 0 ,0, - 4.793* Math.Pow(10,7) ,9.279* Math.Pow(10,7), - 4.487* Math.Pow(10,7), 0},
                //{ 0 ,0 ,0 ,- 4.487* Math.Pow(10,7) ,9.585 * Math.Pow(10,7),- 5.099* Math.Pow(10,7)},
                //{ 0, 0, 0, 0, - 5.099* Math.Pow(10,7) ,5.099 * Math.Pow(10,7)}
                //};


                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var D = operations.EigenVals(eigsMatrix[1]);

                var wTF = operations.OmegaTauF(3, D, 3.13, PI);



                var w = (double[])((MWNumericArray)wTF[0]).ToVector(MWArrayComponent.Real);

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
            try
            {
                int n = M.GetLength(0);

                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var D = operations.EigenVals(eigsMatrix[1]);

                var wTF = operations.OmegaTauF(3, D, 3.13, PI);

                var T = (double[])((MWNumericArray)wTF[1]).ToVector(MWArrayComponent.Real);

                result = T;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static double[] CalcularVectorFrecuencias(double[,] M, double[,] K)
        {
            double[] result = new double[M.GetLength(0)];
            try
            {
                int n = M.GetLength(0);
               
                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var D = operations.EigenVals(eigsMatrix[1]);

                var wTF = operations.OmegaTauF(3, D, 3.13, PI);

                var F = (double[])((MWNumericArray)wTF[2]).ToVector(MWArrayComponent.Real);

                result = F;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static double[,] CalcularMatrizEigenVectores(double[,] M, double[,] K)
        {
            double[,] result = new double[M.GetLength(0), M.GetLength(0)];

            try
            {
                int n = M.GetLength(0);

                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                result = (double[,])((MWNumericArray)phi).ToArray(MWArrayComponent.Real);
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
            try
            {
                int n = M.GetLength(0);

                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);

                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var wTF = operations.OmegaTauF(3, eigenValues, 3.13, PI);

                var Omega = operations.OmegaU(eigenValues, wTF[0]);

                result = (double[,])((MWNumericArray)Omega).ToArray(MWArrayComponent.Real);

            }
            catch (Exception ex)
            {

            }

            return result;
        }


        public static double[,] CalcularMatrizMasasGeneralizada(double[,] M, double[,] K)
        {

            double[,] result = new double[M.GetLength(0), M.GetLength(0)];

            try
            {
           
                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var Mg = operations.Mg(eigenValues, Marr, phi);
                result = (double[,])((MWNumericArray)Mg).ToArray(MWArrayComponent.Real);
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static double[,] CalcularMatrizRigidecesGeneralizada(double[,] M, double[,] K)
        {
            double[,] result = new double[M.GetLength(0), M.GetLength(0)];

            try
            {
            
                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var Kg = operations.Mg(eigenValues, Karr, phi);
                result = (double[,])((MWNumericArray)Kg).ToArray(MWArrayComponent.Real);
            }
            catch (Exception ex)
            {

            }

            return result;
        }


        public static double[] CalcularVectorParticipacionModal(double[,] M, double[,] K)
        {
            double[] result = new double[M.GetLength(0)];
            try
            {
                int n = M.GetLength(0);
             

                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var wTF = operations.OmegaTauF(3, eigenValues, 3.13, PI);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);
                var Mg = operations.Mg(eigenValues, Marr, phi);

                var Gamma = operations.GammaU(eigenValues, Marr, phi, Mg);

                result = (double[])((MWNumericArray)Gamma).ToVector(MWArrayComponent.Real);

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static double[,] CalcularMatrizModalNormalizada(double[,] M, double[,] K)
        {
            double[,] result = new double[M.GetLength(0), M.GetLength(0)];

            try
            {
              
                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var Mg = operations.Mg(eigenValues, Marr, phi);

                var phiN = operations.PhiN(eigenValues, phi, Mg);
                result = (double[,])((MWNumericArray)phiN).ToArray(MWArrayComponent.Real);
            }
            catch (Exception ex)
            {

            }

            return result;

        }


        public static double[] CalcularVectorMasasEfectivas(double[,] M, double[,] K)
        {
            double[] result = new double[M.GetLength(0)];
            try
            {
                int n = M.GetLength(0);

                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var Me = operations.Me(eigenValues, Marr, phi);

                result = (double[])((MWNumericArray)Me).ToVector(MWArrayComponent.Real);

            }
            catch (Exception ex)
            {


            }

            return result;
        }


        public static double[] CalcularMatrizFactoresParticipacionMasasModales(double[,] M, double[,] K)
        {
            double[] result = new double[M.GetLength(0)];

            try
            {

                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var Mg = operations.Mg(eigenValues, Marr, phi);

                var phiN = operations.PhiN(eigenValues, phi, Mg);

                var GammaN = operations.GammaN(eigenValues, Marr, phiN);

                result = (double[])((MWNumericArray)GammaN).ToVector(MWArrayComponent.Real);
            }
            catch (Exception ex)
            {

            }

            return result;

        }

        public static double[] CalcularParticipacionDeMasas(double[,] M, double[,] K, double Mt)
        {
            double[] result = new double[M.GetLength(0)];

            try
            {

                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);

                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var Me = operations.Me(eigenValues, Marr, phi);

                var Pm = operations.Pm(eigenValues, 1.1453 * Math.Pow(10, 6), Me);

                result = (double[])((MWNumericArray)Pm).ToVector(MWArrayComponent.Real);
            }
            catch (Exception ex)
            {

            }

            return result;

        }


        public static double[] CalcularGamma(double[,] M, double[,] K)
        {
            double[] result = new double[M.GetLength(0)];

            try
            {
            
                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);

                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var eigenValues = operations.EigenVals(eigsMatrix[1]);

                var phi = operations.Phi(eigenValues, eigsMatrix[0]);

                var Gamma = operations.Gamma(eigenValues, Marr, phi, (18 * Math.Pow(10, 4)));

                result = (double[])((MWNumericArray)Gamma).ToVector(MWArrayComponent.Real);
            }
            catch (Exception ex)
            {

            }

            return result;

        }


        public static double[] CalcularVectorDeAceleraciones(double[,] M, double[,] K, List<EspectroDisenio> datos)
        {
            double[] result = new double[datos.Count];
            try
            {
                var dataArray = datos.ToArray();

                MWNumericArray Marr = new MWNumericArray(M);
                MWNumericArray Karr = new MWNumericArray(K);
                SeismicAnalysis.SeismicAnalysisOperations operations = new SeismicAnalysis.SeismicAnalysisOperations();
                var eigsMatrix = operations.EigsMatrix(2, Karr, Marr, M.GetLength(0));

                var D = operations.EigenVals(eigsMatrix[1]);

                var wTF = operations.OmegaTauF(3, D, 3.13, PI);

                var T = (double[])((MWNumericArray)wTF[1]).ToVector(MWArrayComponent.Real);

                for (int i = 1; i < result.Length; i++)
                {
                    result[i - 1] = dataArray[i - 1].S + ((dataArray[i].S - dataArray[i - 1].S) / (dataArray[i].T - dataArray[i - 1].T)) * (T[i - 1] - dataArray[i - 1].T);
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

        public static double[] CalcularVectorDeFuerzasCortantesDeDisenio(double[] Pn)
        {
            double[] result = new double[Pn.Length];
            try
            {
                for (int i = 0; i < Pn.Length; i++)
                    result[i] += Pn[i];

            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static double[] CalcularVectorDeFuerzasCortantesDeDisenioEstatico(double c, double Q, double R, Structure structure)
        {
            double[] result = new double[structure.Storeys.Count];
            double[] m = new double[structure.Storeys.Count];
            //double[] SumWh = new double[structure.Storeys.Count];
            double SumWh = 0;
            try
            {

                for (int i = 0; i < structure.Storeys.Count; i++)
                    m[i] = structure.Storeys[i].MasasEntrepisos;

                int h = 1; //Sustituir por un vector con la altura(acumulada) del i-esimo entrepiso (¿Como se obtiene?)

                for (int i = 0; i < structure.Storeys.Count; i++)
                {
                    SumWh += m[i] * h;
                }

                for (int i = 0; i < structure.Storeys.Count; i++)
                {
                    result[i] = (c / (Q * R)) * m[i] * h * (structure.MasaTotal / SumWh);
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
                double Aux1 = 0, Aux2=0;
                for (int i = 0; i < m.Length; i++)
                    Aux1 += m[i] + Math.Pow(X[i], 2);

                for (int i = 0; i < m.Length; i++)
                    Aux2 += F[i] * X[i];

                Aux2 *= 9.81;

                result = 2 * PI * Math.Sqrt(Aux1 / Aux2);
            }
            catch (Exception)
            {

            }

            return result;
        }


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
        public static double CalcularFuerzasCortantesDirectas(Enums.SideType side)
        {
            double result = 2222.32234234;



            return 0;
        }

        public static string CalcularClasificacionMuro(Enums.SideType side)
        {
            string result = "Clasificacion";

            return result;
        }

        public static double CalcularExcentricidadesDisenio(Enums.Coordenate coordenate, Enums.SideType side)
        {
            double result = 2222.32234234;
            return result;
        }

        public static double CalcularCortanteTorsion(Enums.SideType side)
        {
            double result = 2222.32234234;
            return result;

        }


        public static double CalcularCortantesTotales(Enums.SideType side)
        {
            double result = 2222.32234234;



            return result;
        }


        public static double CalcularMomentoVolteo(Enums.SideType side)
        {
            double result = 2222.32234234;
            return result;
        }




        public static double CalcularCargaAxialUltima(Enums.SideType side)
        {
            double result = 2222.32234234;
            return result;
        }


        public static double CalcularCargaAxialDeSismo(Enums.SideType side)
        {
            double result = 2222.32234234;
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

            storey.HorizontalWalls.ForEach(X =>
            {
                At += X.TributaryArea;
                Pesom += X.Thickness / 100 * X.Height / 100 * X.Length / 100 * Convert.ToDouble(materials.Single(Y => Y.Name == X.Material).PV);
            });
            storey.VerticalWalls.ForEach(X =>
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

        public static double CalcularCentroDeMasas(Storey storey, Enums.Coordenate coordenate)
        {
            double result = 3333.333;
            return result;
        }
        public static double CalcularCentroCortante(Storey storey, Enums.Coordenate coordenate)
        {
            double result = 4444.444;


            return result;
        }

        public static double CalcularCentroTorsion(Storey storey, Enums.Coordenate coordenate)
        {
            double result = 3333.333;



            return result;
        }


        public static double CalcularExcentricidadesEstaticas(Storey storey, Enums.Coordenate coordenate)
        {
            double result = 4444.444;
            return result;
        }

        public static double CalcularExcentricidadesAccidentales(Storey storey, Enums.Coordenate coordenate)
        {
            double result = 3333.333;
            return result;
        }


        public static double CalcularExcentricidadDisenioEntrepiso(Storey storey, Enums.Coordenate coordenate)
        {
            double result = 3333.333;
            return result;
        }

        public static double CalcularMomentosTorsionantes(Storey storey, Enums.Coordenate coordenate)
        {
            double result = 3333.333;
            return result;
        }

        public static double CalcularRigidezTorsionalEntrepiso(Storey storey)
        {
            double result = 3333.333;
            return result;
        }


        public static double CalcularMomentoVolteoEntrepiso(Storey storey, Enums.SideType side)
        {
            double result = 3333.333;
            return result;
        }

        #endregion

    }//end of class
}//end of namespace
