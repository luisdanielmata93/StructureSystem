using StructureSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Data;
using MathNet.Numerics.LinearAlgebra.Factorization;
using System.Numerics;

namespace StructureSystem.BusinessRules.Functions
{
    internal static class Operations
    {
        private const double PI = 3.14159265;

        public static double[] CalcularVectorPeriodosCirculares(double[,] m, double[,] k)
        {
            List<double> result = new List<double>();
            Matrix<double> K = DenseMatrix.OfArray(k);
            Matrix<double> M = DenseMatrix.OfArray(m);
            try
            {

                Evd<double> Mw = M.Evd();
                Evd<double> Kw = K.Evd();

                //Matrix<double> eigenvectorsK = Wk.EigenVectors;
                //Matrix<double> eigenvectorsM = Mw.EigenVectors;

                Vector<Complex> eigenvaluesM = Mw.EigenValues;
                Vector<Complex> eigenvaluesK = Kw.EigenValues;

                var sum = eigenvaluesM + eigenvaluesK;

                foreach (var item in sum)
                {
                    result.Add(item.Real);
                }
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
            Matrix<double> K = DenseMatrix.OfArray(k);
            Matrix<double> M = DenseMatrix.OfArray(m);
            try
            {


                Evd<double> Mw = M.Evd();
                Evd<double> Kw = K.Evd();

                //Matrix<double> eigenvectorsK = Wk.EigenVectors;
                //Matrix<double> eigenvectorsM = Mw.EigenVectors;

                Vector<Complex> eigenvaluesM = Mw.EigenValues;
                Vector<Complex> eigenvaluesK = Kw.EigenValues;

                var sum = eigenvaluesM + eigenvaluesK;

                foreach (var item in sum)
                {
                    result.Add((2*PI) / item.Real);
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return result.ToArray<double>();
        }

        public static double[] CalcularVectorFrecuencias(double[,] m, double[,] k)
        {
            List<double> result = new List<double>();
            Matrix<double> K = DenseMatrix.OfArray(k);
            Matrix<double> M = DenseMatrix.OfArray(m);
            try
            {


                Evd<double> Mw = M.Evd();
                Evd<double> Kw = K.Evd();

                //Matrix<double> eigenvectorsK = Wk.EigenVectors;
                //Matrix<double> eigenvectorsM = Mw.EigenVectors;

                Vector<Complex> eigenvaluesM = Mw.EigenValues;
                Vector<Complex> eigenvaluesK = Kw.EigenValues;

                var sum = eigenvaluesM + eigenvaluesK;

                foreach (var item in sum)
                {
                    result.Add( 1 /((2 * PI) / item.Real));
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return result.ToArray<double>();
        }

        public static double[,] CalcularMatrizEigenVectores(double[,]m , double[,] k)
        {

            Matrix<double> K = DenseMatrix.OfArray(k);
            Matrix<double> M = DenseMatrix.OfArray(m);
            try
            {
                List<double> row = new List<double>();
                Evd<double> Mw = M.Evd();
                Evd<double> Kw = K.Evd();

                Matrix<double> eigenvectorsK = Kw.EigenVectors;
                Matrix<double> eigenvectorsM = Mw.EigenVectors;

                var result = eigenvectorsK + eigenvectorsM;

                return result.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
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
            double H = wall.Length;
            double E = Convert.ToDouble(material.Em);
            double I = wall.Inercia;
            double G = Convert.ToDouble(material.Gm);
            double A = wall.AreaLongitudinal;

            result = Math.Pow(((4 * Math.Pow(H, 3)) / (12 * E * I)) + ((0.2 * H) / (G * A)), -1);

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

        public static double CalcularMasasEntrepiso(Storey storey, XMLLoadAnalysisData entrepiso)
        {
            double result = 0;
            double At = 0;
            double Pesom = 0;

            storey.HorizontalWalls.ForEach(X=> {
                At += X.TributaryArea;
                Pesom += X.Height;
            });
            storey.VerticalWalls.ForEach(X =>
            {
                At += X.TributaryArea;
                Pesom += X.Height;
            });

            if (entrepiso.LosaMacizaEntrepisoP != null)
                result = ((entrepiso.LosaMacizaEntrepisoP.CargaMuerta + entrepiso.LosaMacizaEntrepisoP.CargaVivaInstantanea) * At  + (Pesom / 2));
            
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
        
            if(entrepiso.LosaOtroTipoP != null)
                result = ((entrepiso.LosaOtroTipoP.CargaMuerta + entrepiso.LosaOtroTipoP.CargaVivaInstantanea) * At + (Pesom / 2));

            return result;
        }
        
    
        #endregion

    }//end of class
}//end of namespace
