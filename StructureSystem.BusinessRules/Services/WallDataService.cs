using System;
using System.Collections.Generic;
using System.Linq;
using StructureSystem.Model;
using StructureSystem.Data;
using StructureSystem.BusinessRules.Configuration;
using System.IO;

namespace StructureSystem.BusinessRules.Services
{
   public class WallDataService
    {

        #region Constructor
        public WallDataService()
        {
            this.configData = new WebConfig();
        }
        #endregion

        public OperationResult CreateVerticalWall(Object Data)
        {
            OperationResult result = new OperationResult();
            try
            {
                dataXml = new XMLWallData();
                dataXml.DocumentPath = GetDocumentPath();
                dataXml.Side = Enums.SideType.Vertical;
                Object dataRef = dataXml;
                foreach (var prop in Data.GetType().GetProperties())
                {
                    Helper.SetPropertyValue(ref dataRef, prop.Name, prop.GetValue(Data, null));
                }
                dataXml = (XMLWallData)dataRef;


                using (var data = UnitOfWork.Create())
                {
                    data.Repositories.DocumentDataContext.VerticalWallsData.Create(dataXml);
                    result.OperationSuccess(result, Enums.ActionType.Create);
                }

            }
            catch (Exception ex)
            {
                result.OperationError("Error al crear muros verticales", Enums.ActionType.Create, ex);
            }
            return result;
        }

        public OperationResult GetVerticalWalls(int storey)
        {
            OperationResult result = new OperationResult();
            try
            {
                using (var data = UnitOfWork.Create())
                {
                    string documentPath = GetDocumentPath();

                    int countV = data.Repositories.DocumentDataContext.VerticalWallsData.Count(documentPath, storey);
                    result.OperationSuccess(countV, Enums.ActionType.Create);
                }

            }
            catch (Exception ex)
            {
                result.OperationError("Error al crear muros verticales", Enums.ActionType.Create, ex);
            }
            return result;
        }

        public OperationResult CreateHorizontalWall(Object Data)
        {
            OperationResult result = new OperationResult();
            try
            {
                dataXml = new XMLWallData();
                dataXml.DocumentPath = GetDocumentPath();
                dataXml.Side = Enums.SideType.Horizontal;
                Object dataRef = dataXml;
                foreach (var prop in Data.GetType().GetProperties())
                {
                    Helper.SetPropertyValue(ref dataRef, prop.Name, prop.GetValue(Data, null));
                }
                dataXml = (XMLWallData)dataRef;


                using (var data = UnitOfWork.Create())
                {
                    data.Repositories.DocumentDataContext.HorizontalWallsData.Create(dataXml);
                    result.OperationSuccess(result, Enums.ActionType.Create);
                }

            }
            catch (Exception ex)
            {
                result.OperationError("Error al crear muros horizontales", Enums.ActionType.Create, ex);
            }
            return result;
        }

        public OperationResult GetConfigElements()
        {
            OperationResult result = new OperationResult();
            try
            {
                IDictionary<string, IList<object>> resultInfo = new Dictionary<string, IList<object>>();

                var materials = configData.GetMaterialsCollection().ToList<object>();

                resultInfo.Add("materials", materials);
                result.OperationSuccess(resultInfo, Enums.ActionType.Get);

            }
            catch (Exception ex)
            {
                result.OperationError("Error al importar información", Enums.ActionType.Get, ex);

            }
            return result;
        }

        public OperationResult GetHorizontalWalls(int storey)
        {
            OperationResult result = new OperationResult();
            try
            {
                using (var data = UnitOfWork.Create())
                {
                    string documentPath = GetDocumentPath();

                    int countH = data.Repositories.DocumentDataContext.HorizontalWallsData.Count(documentPath, storey);
                    result.OperationSuccess(countH, Enums.ActionType.Create);
                }

            }
            catch (Exception ex)
            {
                result.OperationError("Error al obtener muros horizontales", Enums.ActionType.Create, ex);
            }
            return result;
        }
       
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

        #region Private methods
        private string GetDocumentPath()
        {
            return configData.GetElementByName("LastDocument");
        }


        #endregion
       
        #region Properties
        private WebConfig configData;
        private XMLWallData dataXml;
        #endregion

    }//end of class
}//end of service
