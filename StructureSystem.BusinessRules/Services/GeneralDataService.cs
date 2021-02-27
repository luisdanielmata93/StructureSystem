using System;
using System.Collections.Generic;
using System.Linq;
using StructureSystem.Model;
using StructureSystem.Data;
using StructureSystem.BusinessRules.Configuration;
using System.IO;

namespace StructureSystem.BusinessRules.Services
{
    public class GeneralDataService
    {
     
        #region Constructor

        public GeneralDataService()
        {
            this.configData = new WebConfig();
        }

        #endregion


        #region Methods
        public OperationResult CreateDocument(Object Data)
        {
            OperationResult result = new OperationResult();
            try
            {
                dataXml = new XMLGeneralData();
                dataXml.DocumentPath = this.GetDocumentPath();
                Object dataRef = dataXml;

                foreach (var prop in Data.GetType().GetProperties())
                {
                    Helper.SetPropertyValue(ref dataRef, prop.Name, prop.GetValue(Data, null));
                }
                dataXml = (XMLGeneralData)dataRef;

                if (ExistDocument(string.Concat(dataXml.DocumentPath, dataXml.ProjectName, ".xml")))
                {
                    result.OperationError("Ya existe un proyecto con el mismo nombre.", Enums.ActionType.Update, null);
                    return result;
                }

                using (var data = UnitOfWork.Create())
                {
                    data.Repositories.DocumentDataContext.GeneralData.Create(dataXml);
                    result.OperationSuccess(result, Enums.ActionType.Create);
                }

                configData.SetLastDocument(string.Concat(dataXml.DocumentPath, dataXml.ProjectName, ".xml"));
            }
            catch (Exception ex)
            {
                result.OperationError("Error al crear documento", Enums.ActionType.Create, ex);
            }
            return result;
        }

        public OperationResult ImportDocument(string documentPath)
        {
            OperationResult result = new OperationResult();
            try
            {
                using (var data = UnitOfWork.Create())
                {
                    var documentInfo = data.Repositories.DocumentDataContext.GeneralData.Get(documentPath);

                    result.OperationSuccess(documentInfo, Enums.ActionType.Get);
                }
            }
            catch (Exception ex)
            {
                result.OperationError("Error al importar documento", Enums.ActionType.Get, ex);
            }

            configData.SetLastDocument(documentPath);

            return result;
        }

        public OperationResult UpdateDocument(Object Data)
        {
            OperationResult result = new OperationResult();
            try
            {
                dataXml = new XMLGeneralData();
                dataXml.DocumentPath = this.GetDocumentPath();
                Object dataRef = dataXml;

                foreach (var prop in Data.GetType().GetProperties())
                {
                    Helper.SetPropertyValue(ref dataRef, prop.Name, prop.GetValue(Data, null));
                }
                dataXml = (XMLGeneralData)dataRef;

                using (var data = UnitOfWork.Create())
                {
                    data.Repositories.DocumentDataContext.GeneralData.Update(dataXml);
                    result.OperationSuccess(result, Enums.ActionType.Update);
                }

            }
            catch (Exception ex)
            {
                result.OperationError("Error al actualizar el documento", Enums.ActionType.Update, ex);
            }

            return result;
        }

        public OperationResult GetConfigElements()
        {
            OperationResult result = new OperationResult();
            try
            {
                IDictionary<string, IList<object>> resultInfo = new Dictionary<string, IList<object>>();

                var groups = configData.GetGroupsCollection().ToList<object>();
                var regulations = configData.GetRegulationsCollection().ToList<object>();
                var usages = configData.GetUsagesCollection().ToList<object>();
                var tests = configData.GetTestsCollection().ToList<object>();

                resultInfo.Add("groups", groups);
                resultInfo.Add("regulations", regulations);
                resultInfo.Add("usages", usages);
                resultInfo.Add("tests", tests);

                result.OperationSuccess(resultInfo, Enums.ActionType.Get);

            }
            catch (Exception ex)
            {
                result.OperationError("Error al importar documento", Enums.ActionType.Get, ex);

            }
            return result;
        }

        public OperationResult ExportDocument()
        {
            OperationResult result = new OperationResult();
            try
            {
                ExportDocument doc = new ExportDocument();
                doc.Export();

                result.OperationSuccess("Exportación exitosa", Enums.ActionType.Get);

            }
            catch (Exception ex)
            {
                result.OperationError("Error al exportar documento", Enums.ActionType.Get, ex);

            }
            return result;
        }

        public string GetDocumentPath()
        {
            return configData.GetElementByName("XMLpath");
        }

        #endregion


        #region Private methods

        private bool ExistDocument(string documentPath)
        {
            if (File.Exists(documentPath))
                return true;
            else
                return false;
        }

        #endregion


        #region Properties
        private WebConfig configData;
        private XMLGeneralData dataXml;
        #endregion

    }//end of class
}//end of namespace
