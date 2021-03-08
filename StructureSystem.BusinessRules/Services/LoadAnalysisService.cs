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

        public OperationResult Save(Object model, StructureSystem.Model.Enums.MaterialType type, int Nivel)
        {
            OperationResult result = new OperationResult();
            try
            {
                dataXml = new XMLLoadAnalysisData();
                dataXml.Storey = Nivel;
                switch (type)
                {
                    case Enums.MaterialType.MacisaEntrepiso:
                        dataXml.LosaMacizaEntrepisoP = (LosaMacizaEntrepiso)model;
                        break;
                    case Enums.MaterialType.MacisaAzotea:
                        dataXml.LosaMacizaAzoteaP = (LosaMacizaAzotea)model;
                        break;
                    case Enums.MaterialType.NervadaEntrepiso:
                        dataXml.LosaNervadaEntrepisoP = (LosaNervadaEntrepiso)model;
                        break;
                    case Enums.MaterialType.NervadaAzotea:
                        dataXml.LosaNervadaAzoteaP = (LosaNervadaAzotea)model;
                        break;
                    case Enums.MaterialType.ViguetaBovedillaEntrepiso:
                        dataXml.LosaViguetaBovedillaEntrepisoP = (LosaViguetaBovedillaEntrepiso)model;
                        break;
                    case Enums.MaterialType.ViguetaBovedillaAzotea:
                        dataXml.LosaViguetaBovedillaAzoteaP = (LosaViguetaBovedillaAzotea)model;
                        break;
                    case Enums.MaterialType.Otra:
                        dataXml.LosaOtroTipoP = (LosaOtroTipo)model;
                        break;
                    default:
                        break;
                }

                using (var data = UnitOfWork.Create())
                {
                   dataXml.DocumentPath = GetDocumentPath();
                    var documentInfo = data.Repositories.DocumentDataContext.LoadAnalysisData.Create(dataXml);

                    result.OperationSuccess(true, Enums.ActionType.Create);
                }
            }
            catch (Exception ex)
            {
                result.OperationError("Error al insertar tipo de losa", Enums.ActionType.Create, ex);
            }

            return result;
        }

        public OperationResult IsSaved(int Level)
        {
            OperationResult result = new OperationResult();
            try
            {
                using (var data = UnitOfWork.Create())
                {
                    string document = GetDocumentPath();
                    var documentInfo = data.Repositories.DocumentDataContext.LoadAnalysisData.IsSaved(document, Level);

                    result.OperationSuccess(documentInfo, Enums.ActionType.Get);
                }
            }
            catch (Exception ex)
            {
                result.OperationError("Error al validar piso guardado", Enums.ActionType.Get, ex);
            }

            return result;
        }


        public OperationResult GetStoreyList()
        {
            OperationResult result = new OperationResult();
            try
            {
                using (var data = UnitOfWork.Create())
                {
                    string document = GetDocumentPath();
                    var documentInfo = data.Repositories.DocumentDataContext.GeneralData.Get(document);

                    List<int> storeysList = new List<int>();

                    int temp = Convert.ToInt32(documentInfo["Storeys"]);
                    for (int i = 0; i < temp; i++)
                    {
                        storeysList.Add(i + 1);
                    }

                    result.OperationSuccess(storeysList, Enums.ActionType.Get);
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

        public OperationResult GetSavedData(int Storey)
        {
            OperationResult result = new OperationResult();
            try
            {
                XMLLoadAnalysisData model = new XMLLoadAnalysisData();
                model.DocumentPath = this.GetDocumentPath();
                model.Storey = Storey;
                using(var data = UnitOfWork.Create())
                {
                    var info = data.Repositories.DocumentDataContext.LoadAnalysisData.Get(model);
                    result.OperationSuccess(info, Enums.ActionType.Get);
                }
            }
            catch (Exception ex)
            {
                result.OperationError("Error al obtener los entrepisos almacenados", Enums.ActionType.Get, ex);
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
        private XMLLoadAnalysisData dataXml;
        #endregion

    }//end of class
}//end of namespace
