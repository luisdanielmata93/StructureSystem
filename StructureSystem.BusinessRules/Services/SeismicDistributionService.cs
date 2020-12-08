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
   public class SeismicDistributionService
    {

        public SeismicDistributionService()
        {
            this.configData = new WebConfig();
            this.MaterialCollection = this.configData.GetMaterialsCollection();
            Structure = new Structure();
        }


        public Structure GetStructure()
        {
            try
            {
                var document = GetDocumentPath();

                using (var data = UnitOfWork.Create())
                {
                    Structure.Storeys = (List<Storey>)data.Repositories.DocumentDataContext.SeismicAnalysisData.Get(document);

                }
                CalculateInitialData();
            }
            catch (Exception ex)
            {
            }
            return Structure;

        }

        private void CalculateInitialData()
        {
            try
            {
                for (int i = 0; i < Structure.Storeys.Count; i++)
                {
                    foreach (var wall in Structure.Storeys[i].HorizontalWalls)
                    {
                        wall.Inercia = Functions.Operations.CalcularInercia(wall);
                        wall.AreaLongitudinal = Functions.Operations.CalcularAreaLongitudinal(wall);
                        wall.RigidezLateral = Functions.Operations.CalcularRigidezLateral(wall, this.MaterialCollection.Single(x => x.Name == wall.Material));
                    }
                    foreach (var wall in Structure.Storeys[i].VerticalWalls)
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


        private string GetDocumentPath()
        {
            return configData.GetElementByName("LastDocument");
        }



        #region Properties
        private WebConfig configData;
        private XMLStructureData dataXml;
        private static Structure Structure;
        private List<Material> MaterialCollection;
        #endregion

    }//end of class
}//end of namespace
