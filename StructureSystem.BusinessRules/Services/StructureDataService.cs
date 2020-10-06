using System;
using System.Collections.Generic;
using System.Linq;
using StructureSystem.Model;
using StructureSystem.Data;
using StructureSystem.BusinessRules.Configuration;
using System.IO;

namespace StructureSystem.BusinessRules.Services
{
    public class StructureDataService
    {
        public StructureDataService()
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
                    var info = data.Repositories.DocumentDataContext.StructureData.Get(document);
                    result.OperationSuccess(info, Enums.ActionType.Create);
                }
            }
            catch (Exception ex)
            {
                result.OperationError("Error al obtener pisos", Enums.ActionType.Get, ex);
            }
            return result;

        }

        public OperationResult SaveStoreys(List<Storey> Storeys)
        {
            OperationResult result = new OperationResult();
            try
            {
                dataXml = new XMLStructureData();
                string Document = GetDocumentPath();
                dataXml.Storeys = Storeys;


                using (var data = UnitOfWork.Create())
                {
                    data.Repositories.DocumentDataContext.StructureData.Update(dataXml);
                    result.OperationSuccess(result, Enums.ActionType.Update);
                }

            }
            catch (Exception ex)
            {
                result.OperationError("Error al actualizar pisos", Enums.ActionType.Update, ex);
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
        #endregion
    }//end of class
}//end of namespace
