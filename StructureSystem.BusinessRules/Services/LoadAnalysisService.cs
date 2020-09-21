using System;
using System.Collections.Generic;
using System.Linq;
using StructureSystem.Model;
using StructureSystem.Data;
using StructureSystem.BusinessRules.Configuration;
using System.IO;

namespace StructureSystem.BusinessRules.Services
{
    public class LoadAnalysisService
    {
        #region Constructor
        public LoadAnalysisService()
        {
            this.configData = new WebConfig();

        }
        #endregion




        #region Methods

        public OperationResult GetStoreys()
        {
            OperationResult result = new OperationResult();
            try
            {
                using (var data = UnitOfWork.Create())
                {
                    string document = GetDocumentPath();
                    var documentInfo = data.Repositories.DocumentDataContext.GeneralData.Get(document);

                    result.OperationSuccess(documentInfo["Storeys"], Enums.ActionType.Get);
                }
            }
            catch (Exception ex)
            {
                result.OperationError("Error al importar documento", Enums.ActionType.Get, ex);
            }

            return result;
        }

        public OperationResult GetFlooringMaterials()
        {
            OperationResult result = new OperationResult();
            try
            {
                using (var data = UnitOfWork.Create())
                {
                    var flooringData = this.configData.GetFlooringMaterialsCollection();
                    result.OperationSuccess(flooringData, Enums.ActionType.Get);
                }
            }
            catch (Exception ex)
            {
                result.OperationError("Error al obtener los tipos de pisos", Enums.ActionType.Get, ex);
            }

            return result;
        }


        private string GetDocumentPath()
        {
            return configData.GetElementByName("LastDocument");
        }
        #endregion
        #region Properties
        private WebConfig configData;
        private XMLGeneralData dataXml;
        #endregion

    }//end of class
}//end of namespace
