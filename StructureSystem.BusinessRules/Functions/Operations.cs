using StructureSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace StructureSystem.BusinessRules.Functions
{
    internal static class Operations
    {
        private const double PI = 3.14159265;

        public static double[] CalcularVectorPeriodosCirculares(double[,] M, double[,] K)
        {

            List<double> result = new List<double>();
            //Matrix<double> M = DenseMatrix.OfArray(m);
            try
            {
                //MathNet.Numerics.

              
                //var eigenvals = IntelMKL.genvals(M, K);
               //IntelMKL.genvals(M, K); 
               
                // Evd<double> Mw = M.Evd();
                //Evd<double> Kw = K.Evd();


                //Matrix<double> eigenvectorsK = Wk.EigenVectors;
                //Matrix<double> eigenvectorsM = Mw.EigenVectors;

                //  Vector<Complex> eigenvaluesM = Mw.EigenValues;
                // Vector<Complex> eigenvaluesK = Kw.EigenValues;

                //var sum = eigenvaluesM + eigenvaluesK;

                //foreach (var item in sum)
                //{
                //    result.Add(item.Real);
                //}
            }
            catch (Exception ex)
            {

                throw;
            }


            return result.ToArray<double>();
        }

        public static double[] CalcularVectorPeriodosNaturales(double[,] m, double[,] k)
        {
            List<double> result = new List<double>();
            //Matrix<double> K = DenseMatrix.OfArray(k);
            //Matrix<double> M = DenseMatrix.OfArray(m);
            //try
            //{


            //    Evd<double> Mw = M.Evd();
            //    Evd<double> Kw = K.Evd();

            //    //Matrix<double> eigenvectorsK = Wk.EigenVectors;
            //    //Matrix<double> eigenvectorsM = Mw.EigenVectors;

            //    Vector<Complex> eigenvaluesM = Mw.EigenValues;
            //    Vector<Complex> eigenvaluesK = Kw.EigenValues;

            //    var sum = eigenvaluesM + eigenvaluesK;

            //    foreach (var item in sum)
            //    {
            //        result.Add((2 * PI) / item.Real);
            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}


            return result.ToArray<double>();
        }

        public static double[] CalcularVectorFrecuencias(double[,] m, double[,] k)
        {
            List<double> result = new List<double>();
            //Matrix<double> K = DenseMatrix.OfArray(k);
            //Matrix<double> M = DenseMatrix.OfArray(m);
            //try
            //{


            //    Evd<double> Mw = M.Evd();
            //    Evd<double> Kw = K.Evd();

            //    //Matrix<double> eigenvectorsK = Wk.EigenVectors;
            //    //Matrix<double> eigenvectorsM = Mw.EigenVectors;

            //    Vector<Complex> eigenvaluesM = Mw.EigenValues;
            //    Vector<Complex> eigenvaluesK = Kw.EigenValues;

            //    var sum = eigenvaluesM + eigenvaluesK;

            //    foreach (var item in sum)
            //    {
            //        result.Add(1 / ((2 * PI) / item.Real));
            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

            return result.ToArray<double>();
        }

        public static double[,] CalcularMatrizEigenVectores(double[,] m, double[,] k)
        {
            return null;
            //Matrix<double> K = DenseMatrix.OfArray(k);
            //Matrix<double> M = DenseMatrix.OfArray(m);
            //try
            //{
            //    List<double> row = new List<double>();
            //    Evd<double> Mw = M.Evd();
            //    Evd<double> Kw = K.Evd();

            //    Matrix<double> eigenvectorsK = Kw.EigenVectors;
            //    Matrix<double> eigenvectorsM = Mw.EigenVectors;

            //    var result = eigenvectorsK + eigenvectorsM;

            //    return result.ToArray();
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
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
                        K[n-1, n-1] = Structure.Storeys[n-1].RigidezEntrepisoHorizontal;
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
                        K[n-1, n-1] = Structure.Storeys[n-1].RigidezEntrepisoVertical;
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
      
        public static double[,] CalcularMatrizEspectral(Structure Structure, Enums.SideType side)
        {
            int n = Structure.Storeys.Count;
            double[,] Mespectral = new double[n, n];


            return Mespectral;
        }


        public static double[,] CalcularMatrizMasasGeneralizada(Structure Structure, Enums.SideType side)
        {
            int n = Structure.Storeys.Count;
            double[,] Mg = new double[n, n];


            return Mg;
        }

        public static double[,] CalcularMatrizRigidecesGeneralizada(Structure Structure, Enums.SideType side)
        {
            int n = Structure.Storeys.Count;
            double[,] Mg = new double[n, n];


            return Mg;
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
                Pesom += X.Thickness/100*X.Height/100*X.Length/100 * Convert.ToDouble(materials.Single(Y=>Y.Name == X.Material).PV);
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


        public static double CalcularMomentoVolteoEntrepiso(Storey storey,Enums.SideType side)
        {
            double result = 3333.333;
            return result;
        }

        #endregion

    }//end of class
}//end of namespace
