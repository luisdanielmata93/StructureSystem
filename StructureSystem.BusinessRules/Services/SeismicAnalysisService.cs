using System;
using System.Collections.Generic;
using System.Linq;
using StructureSystem.Model;
using StructureSystem.Data;
using StructureSystem.BusinessRules.Configuration;
using System.IO;

namespace StructureSystem.BusinessRules.Services
{
    public class SeismicAnalysisService
    {

        public SeismicAnalysisService()
        {
            this.configData = new WebConfig();
        }
        public OperationResult GetStoreys()
        {
            OperationResult result = new OperationResult();

            try
            {
                var document = GetDocumentPath();

                using (var data = UnitOfWork.Create())
                {
                    var info = data.Repositories.DocumentDataContext.SeismicAnalysisData.Get(document);
                    result.OperationSuccess(info, Enums.ActionType.Create);
                }
            }
            catch (Exception ex)
            {
                result.OperationError("Error al obtener pisos", Enums.ActionType.Get, ex);
            }
            return result;

        }

        public void Test()
        {
            try
            {
                var materiales = configData.GetMaterialsCollection();

            }
            catch (Exception ex)
            {

                throw;
            }
        
            
        }
        private string GetDocumentPath()
        {
            return configData.GetElementByName("LastDocument");
        }

        #region Properties
        private WebConfig configData;
        private XMLStructureData dataXml;
        #endregion

    }//end of class
}//end of namespace
