﻿using System;
using System.Collections.Generic;
using System.Linq;
using StructureSystem.Model;
using StructureSystem.Data;
using StructureSystem.BusinessRules.Configuration;
using System.IO;

namespace StructureSystem.BusinessRules.Services
{
    public class VerticalWallsService
    {
        #region Constructor
        public VerticalWallsService()
        {
            this.configData = new WebConfig();
        }
        #endregion


        public OperationResult CreateWall(Object Data)
        {
            OperationResult result = new OperationResult();
            try
            {
                dataXml = new XMLVerticalWallData();
                dataXml.DocumentPath = GetDocumentPath();
                Object dataRef = dataXml;
                foreach (var prop in Data.GetType().GetProperties())
                {
                    Helper.SetPropertyValue(ref dataRef, prop.Name, prop.GetValue(Data, null));
                }
                dataXml = (XMLVerticalWallData)dataRef;


                using (var data = UnitOfWork.Create())
                {
                    data.Repositories.DocumentDataContext.VerticalWallsData.Create(dataXml);
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


        private string GetDocumentPath()
        {
            return configData.GetElementByName("LastDocument");
        }




        #region Properties
        private WebConfig configData;
        private XMLVerticalWallData dataXml;
        #endregion
    }//end of class
}//end of namespace
