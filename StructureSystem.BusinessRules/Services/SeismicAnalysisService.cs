using System;
using System.Collections.Generic;
using System.Linq;
using StructureSystem.Model;
using StructureSystem.Data;
using StructureSystem.BusinessRules.Configuration;
using System.IO;
using StructureSystem.BusinessRules.Functions;
using System.Threading.Tasks;
using System.Data;

namespace StructureSystem.BusinessRules.Services
{
    public class SeismicAnalysisService
    {
        #region Constructor

        public SeismicAnalysisService()
        {
            this.configData = new WebConfig();
            this.MaterialCollection = this.configData.GetMaterialsCollection();
        }

        #endregion


        #region Metodos iniciales
        public bool ValidateMethod()
        {
            bool result = false;

            using (var data = UnitOfWork.Create())
            {
                var info = data.Repositories.DocumentDataContext.GeneralData.Get(GetDocumentPath());

                if (info["Test"].ToString() == "Estático" || info["Test"].ToString() == "Estático y Dinámico")
                    result = true;
            }


            return result;
        }

        private Structure GetStructure()
        {
            Structure structure = new Structure();
            try
            {
                var document = GetDocumentPath();

                using (var data = UnitOfWork.Create())
                {
                    structure.Storeys = (List<Storey>)data.Repositories.DocumentDataContext.SeismicAnalysisData.Get(document);

                }
                GetInitialData(ref structure);
            }
            catch (Exception ex)
            {
            }
            return structure;

        }

        private XMLLoadAnalysisData GetEntrepisoPorNivel(int Storey)
        {
            XMLLoadAnalysisData result = new XMLLoadAnalysisData();

            try
            {
                XMLLoadAnalysisData DocumentData = new XMLLoadAnalysisData();
                DocumentData.DocumentPath = GetDocumentPath();
                DocumentData.Storey = Storey;

                using (var data = UnitOfWork.Create())
                {
                    result = data.Repositories.DocumentDataContext.LoadAnalysisData.Get(DocumentData);
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        private void GetInitialData(ref Structure structure)
        {
            try
            {

                for (int i = 0; i < structure.Storeys.Count; i++)
                {
                    foreach (var wall in structure.Storeys[i].HorizontalWalls)
                    {
                        wall.Inercia = Functions.Operations.CalcularInercia(wall);
                        wall.AreaLongitudinal = Functions.Operations.CalcularAreaLongitudinal(wall);
                        wall.RigidezLateral = Functions.Operations.CalcularRigidezLateral(wall, this.MaterialCollection.Single(x => x.Name == wall.Material));
                    }
                    foreach (var wall in structure.Storeys[i].VerticalWalls)
                    {
                        wall.Inercia = Functions.Operations.CalcularInercia(wall);
                        wall.AreaLongitudinal = Functions.Operations.CalcularAreaLongitudinal(wall);
                        wall.RigidezLateral = Functions.Operations.CalcularRigidezLateral(wall, this.MaterialCollection.Single(x => x.Name == wall.Material));
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        #endregion



        #region Metodos


        public DataTable GetVectorRigidez(Enums.SideType side)
        {
            DataTable result = new DataTable();
            Structure Structure = this.GetStructure();
            try
            {

                if (side == Enums.SideType.Horizontal)
                {
                    foreach (var storey in Structure.Storeys)
                    {
                        storey.RigidezEntrepisoHorizontal = Functions.Operations.CalcularRigidezEntrepiso(storey, Enums.SideType.Horizontal);
                        storey.RigidezEntrepisoVertical = Functions.Operations.CalcularRigidezEntrepiso(storey, Enums.SideType.Vertical);
                    }

                }
                if (side == Enums.SideType.Vertical)
                {
                    foreach (var storey in Structure.Storeys)
                    {
                        storey.RigidezEntrepisoHorizontal = Functions.Operations.CalcularRigidezEntrepiso(storey, Enums.SideType.Horizontal);
                        storey.RigidezEntrepisoVertical = Functions.Operations.CalcularRigidezEntrepiso(storey, Enums.SideType.Vertical);
                    }
                }

                result.Columns.Add("Nivel");
                result.Columns.Add("Vector de Rigideces");

                if (side == Enums.SideType.Horizontal)
                    foreach (var storey in Structure.Storeys)
                        result.Rows.Add(storey.StoreyNumber, storey.RigidezEntrepisoHorizontal.ToString());

                if (side == Enums.SideType.Vertical)
                    foreach (var storey in Structure.Storeys)
                        result.Rows.Add(storey.StoreyNumber, storey.RigidezEntrepisoVertical.ToString());

                result.AcceptChanges();

            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public DataTable GetVectorMasas()
        {
            DataTable result = new DataTable();
            Structure Structure = this.GetStructure();
            try
            {
                Structure.MasaTotal = 0;
                foreach (var storey in Structure.Storeys)
                {
                    XMLLoadAnalysisData entrepiso = GetEntrepisoPorNivel(storey.StoreyNumber);
                    storey.MasasEntrepisos = Functions.Operations.CalcularMasasEntrepiso(MaterialCollection, storey, entrepiso);
                    Structure.MasaTotal += storey.MasasEntrepisos;
                }

                result.Columns.Add("Nivel");
                result.Columns.Add("Vector de Masas");

                foreach (var storey in Structure.Storeys)
                    result.Rows.Add(storey.StoreyNumber.ToString(), storey.MasasEntrepisos.ToString("N4"));

                result.AcceptChanges();
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public DataTable GetMatrizRigideces(Enums.SideType side)
        {
            Structure Structure = this.GetStructure();
            DataTable result = new DataTable();

            try
            {
                int n = Structure.Storeys.Count;

                if (side == Enums.SideType.Horizontal)
                {
                    foreach (var storey in Structure.Storeys)
                    {
                        storey.RigidezEntrepisoHorizontal = Functions.Operations.CalcularRigidezEntrepiso(storey, Enums.SideType.Horizontal);
                        storey.RigidezEntrepisoVertical = Functions.Operations.CalcularRigidezEntrepiso(storey, Enums.SideType.Vertical);
                    }

                }
                if (side == Enums.SideType.Vertical)
                {
                    foreach (var storey in Structure.Storeys)
                    {
                        storey.RigidezEntrepisoHorizontal = Functions.Operations.CalcularRigidezEntrepiso(storey, Enums.SideType.Horizontal);
                        storey.RigidezEntrepisoVertical = Functions.Operations.CalcularRigidezEntrepiso(storey, Enums.SideType.Vertical);
                    }
                }
                double[,] K = Operations.CalcularMatrizRigideces(Structure, side);

                //Llenamos columnas
                for (int i = 0; i < n; i++)
                {
                    result.Columns.Add(i.ToString());
                }

                //Llenamos filas
                for (int j = 0; j < n; j++)
                {
                    DataRow row = result.NewRow();
                    for (int k = 0; k < n; k++)
                    {
                        row[k] = K[j, k].ToString();
                    }
                    result.Rows.Add(row);
                }

                result.AcceptChanges();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public DataTable GetMatrizMasas(Enums.SideType side)
        {
            DataTable result = new DataTable();
            Structure Structure = this.GetStructure();

            try
            {
                int n = Structure.Storeys.Count;
                Structure.MasaTotal = 0;

                foreach (var storey in Structure.Storeys)
                {
                    XMLLoadAnalysisData entrepiso = GetEntrepisoPorNivel(storey.StoreyNumber);
                    storey.MasasEntrepisos = Functions.Operations.CalcularMasasEntrepiso(MaterialCollection,storey, entrepiso);
                    Structure.MasaTotal += storey.MasasEntrepisos;
                }

                double[,] M = Operations.CalcularMatrizMasas(Structure, side);

                for (int i = 0; i < n; i++)
                {
                    result.Columns.Add(i.ToString());
                }

                for (int j = 0; j < n; j++)
                {
                    DataRow row = result.NewRow();
                    for (int k = 0; k < n; k++)
                    {
                        row[k] = M[j, k].ToString("N4");
                    }
                    result.Rows.Add(row);
                }

                result.AcceptChanges();

            }
            catch (Exception ex)
            {
            }


            return result;
        }

        public DataTable GetVectorPeriodosCirculares(Enums.SideType side)
        {
            DataTable result = new DataTable();
            Structure Structure = this.GetStructure();

            double[,] M = Operations.CalcularMatrizMasas(Structure, side);
            double[,] K = Operations.CalcularMatrizRigideces(Structure, side);
            try
            {
                var data = Functions.Operations.CalcularVectorPeriodosCirculares(M, K);

                result.Columns.Add("Periodos Circulares");

                for (int i = 0; i < data.Length; i++)
                {
                    result.Rows.Add(data[i]);
                }

                result.AcceptChanges();
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public DataTable GetVectorPeriodosNaturales(Enums.SideType side)
        {
            DataTable result = new DataTable();
            Structure Structure = this.GetStructure();

            double[,] M = Operations.CalcularMatrizMasas(Structure, side);
            double[,] K = Operations.CalcularMatrizRigideces(Structure, side);
            try
            {
                var data = Functions.Operations.CalcularVectorPeriodosNaturales(M, K);

                result.Columns.Add("Periodos Naturales");

                for (int i = 0; i < data.Length; i++)
                {
                    result.Rows.Add(data[i]);
                }

                result.AcceptChanges();
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public DataTable GetVectorFrecuencias(Enums.SideType side)
        {
            DataTable result = new DataTable();
            Structure Structure = this.GetStructure();

            double[,] M = Operations.CalcularMatrizMasas(Structure, side);
            double[,] K = Operations.CalcularMatrizRigideces(Structure, side);
            try
            {
                var data = Functions.Operations.CalcularVectorFrecuencias(M, K);

                result.Columns.Add("Vector de Frecuencias");

                for (int i = 0; i < data.Length; i++)
                {
                    result.Rows.Add(data[i]);
                }

                result.AcceptChanges();
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public DataTable GetMatrizEigenVectoresNormalizados(Enums.SideType side)
        {
            DataTable result = new DataTable();
            Structure Structure = this.GetStructure();

            double[,] M = Operations.CalcularMatrizMasas(Structure, side);
            double[,] K = Operations.CalcularMatrizRigideces(Structure, side);
            try
            {
                var data = Functions.Operations.CalcularMatrizEigenVectores(M, K);

                for (int i = 0; i < data.GetLength(0); i++)
                {
                    result.Columns.Add(i.ToString());
                }

                for (int i = 0; i < data.GetLength(0); i++)
                {
                    DataRow row = result.NewRow();
                    for (int j = 0; j < data.GetLength(0); j++)
                    {
                        row[j] = data[i, j].ToString("N4");
                    }

                    result.Rows.Add(row);

                }

                result.AcceptChanges();

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public DataTable GetMatrizEspectral(Enums.SideType side)
        {
            Structure Structure = GetStructure();
            DataTable result = new DataTable();
            try
            {
                double[,] data = Operations.CalcularMatrizEspectral(Structure, side);


                for (int i = 0; i < data.GetLength(0); i++)
                {
                    result.Columns.Add(i.ToString());
                }

                for (int i = 0; i < data.GetLength(0); i++)
                {
                    DataRow row = result.NewRow();
                    for (int j = 0; j < data.GetLength(0); j++)
                    {
                        row[j] = data[i, j].ToString();
                    }

                    result.Rows.Add(row);

                }

                result.AcceptChanges();

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public DataTable GetMatrizMasasGeneralizada(Enums.SideType side)
        {
            Structure Structure = GetStructure();
            DataTable result = new DataTable();
            try
            {
                int n = Structure.Storeys.Count;
                Structure.MasaTotal = 0;

                foreach (var storey in Structure.Storeys)
                {
                    XMLLoadAnalysisData entrepiso = GetEntrepisoPorNivel(storey.StoreyNumber);
                    storey.MasasEntrepisos = Functions.Operations.CalcularMasasEntrepiso(MaterialCollection, storey, entrepiso);
                    Structure.MasaTotal += storey.MasasEntrepisos;
                }

                double[,] M = Operations.CalcularMatrizMasas(Structure, side);

                double[,] data = Operations.CalcularMatrizMasasGeneralizada(Structure, side);


                for (int i = 0; i < data.GetLength(0); i++)
                {
                    result.Columns.Add(i.ToString());
                }

                for (int i = 0; i < data.GetLength(0); i++)
                {
                    DataRow row = result.NewRow();
                    for (int j = 0; j < data.GetLength(0); j++)
                    {
                        row[j] = data[i, j].ToString();
                    }

                    result.Rows.Add(row);

                }

                result.AcceptChanges();


            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public DataTable GetMatrizRigidecesGeneralizada(Enums.SideType side)
        {
            Structure Structure = GetStructure();
            DataTable result = new DataTable();
            try
            {
                int n = Structure.Storeys.Count;
                Structure.MasaTotal = 0;

                double[,] data = Operations.CalcularMatrizRigidecesGeneralizada(Structure, side);

                for (int i = 0; i < data.GetLength(0); i++)
                {
                    result.Columns.Add(i.ToString());
                }

                for (int i = 0; i < data.GetLength(0); i++)
                {
                    DataRow row = result.NewRow();
                    for (int j = 0; j < data.GetLength(0); j++)
                    {
                        row[j] = data[i, j].ToString();
                    }

                    result.Rows.Add(row);

                }

                result.AcceptChanges();


            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public DataTable GetVectorUnos()
        {
            DataTable result = new DataTable();
            Structure Structure = GetStructure();
            try
            {
                result.Columns.Add("Vector de Unos");

                foreach (var storey in Structure.Storeys)
                    result.Rows.Add(1);

                result.AcceptChanges();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public DataTable GetVectorParticipacionModal(Enums.SideType side)
        {
            DataTable result = new DataTable();




            return result;
        }


        public DataTable GetMatrizModalNormalizada(Enums.SideType side)
        {
            DataTable result = new DataTable();




            return result;
        }


        public DataTable GetVectorMasasEfectivas(Enums.SideType side)
        {
            DataTable result = new DataTable();


            return result;
        }



        public DataTable GetMatrizFactParticipacionMasasModales(Enums.SideType side)
        {
            DataTable result = new DataTable();




            return result;
        }



        public DataTable GetVectorParticipacionMasas(Enums.SideType side)
        {
            DataTable result = new DataTable();




            return result;
        }



        public DataTable GetEspectroDisenio(Enums.SideType side)
        {
            DataTable result = new DataTable();



            return result;
        }



        public DataTable GetVectorAceleraciones(Enums.SideType side)
        {
            DataTable result = new DataTable();


            return result;
        }



        public DataTable GetVectorFuerzasFicticiasEquivalentes(Enums.SideType side)
        {
            DataTable result = new DataTable();




            return result;
        }




        public DataTable GetVectorFuerzasCortantesDisenio(Enums.SideType side)
        {
            DataTable result = new DataTable();



            return result;
        }




        public DataTable GetVectorDeterminacionFuerzasCortantes(Enums.SideType side)
        {
            DataTable result = new DataTable();



            return result;
        }


        public DataTable GetVectorDesplazamientosLaterales(Enums.SideType side)
        {
            DataTable result = new DataTable();



            return result;
        }

        public DataTable GetEstimacionPeriodoFundamentalEstructura(Enums.SideType side)
        {
            DataTable result = new DataTable();


            return result;
        }

        #endregion


        #region GetDocument

        private string GetDocumentPath()
        {
            return configData.GetElementByName("LastDocument");
        }

        #endregion

        #region Properties
        private WebConfig configData;
        private XMLStructureData dataXml;
        private List<Material> MaterialCollection;
        #endregion

    }//end of class
}//end of namespace
