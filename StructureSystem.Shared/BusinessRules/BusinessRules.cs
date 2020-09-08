using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.Shared.Data;
using StructureSystem.Model;
using System.Reflection;
using StructureSystem.Shared.Configuration;
using System.Diagnostics;

namespace StructureSystem.Shared.BusinessRules
{
    public class BusinessRules
    {
        #region Properties
        WebConfig configData;
        XMLData dataXml;

        #endregion

        #region Constructor
        public BusinessRules()
        {
            dataXml = new XMLData();
            configData = new WebConfig();
        }

        #endregion


        #region Get Data
        public IDictionary<string, IList<object>> GetDefinitionData()
        {
            IDictionary<string, IList<object>> result = new Dictionary<string, IList<object>>();

            var groups = configData.GetGroupsCollection().ToList<object>();
            var regulations = configData.GetRegulationsCollection().ToList<object>();
            var usages = configData.GetUsagesCollection().ToList<object>();
            var tests = configData.GetTestsCollection().ToList<object>();

            result.Add("groups", groups);
            result.Add("regulations", regulations);
            result.Add("usages", usages);
            result.Add("tests", tests);

            return result;
        }

        public IDictionary<string, IList<object>> GetDWallData()
        {
            IDictionary<string, IList<object>> result = new Dictionary<string, IList<object>>();

            var materials = configData.GetMaterialsCollection().ToList<object>();

            result.Add("materials", materials);
            return result;
        }

        public OperationResult GetStoreysSpecification()
        {
            return Document.GetStoreysSpecification();
        }

        public OperationResult GetGeneralDataNode(string node)
        {
            return Document.Get(node);
        }

        #endregion



        public OperationResult SaveHorizontalWalls(Object Data)
        {
            foreach (var prop in Data.GetType().GetProperties())
            {
                SetPropertyValue(ref dataXml, prop.Name, prop.GetValue(Data, null));
            }
            return Document.SaveHorizontalWall(dataXml);
        }

        public OperationResult SaveVerticalWalls(Object Data)
        {
            foreach (var prop in Data.GetType().GetProperties())
            {
                SetPropertyValue(ref dataXml, prop.Name, prop.GetValue(Data, null));
            }
            return Document.SaveVerticalWall(dataXml);
        }




        public OperationResult CreateDocument(Object Data)
        {
            foreach (var prop in Data.GetType().GetProperties())
            {
                SetPropertyValue(ref dataXml, prop.Name, prop.GetValue(Data, null));
            }

            return Document.Create(dataXml);
        }


        public OperationResult UpdateDocument(Object Data)
        {
            foreach (var prop in Data.GetType().GetProperties())
            {
                SetPropertyValue(ref dataXml, prop.Name, prop.GetValue(Data, null));
            }

            return Document.Create(dataXml);
        }

        public OperationResult ImportDocument(string Path)
        {
            return Document.Import(Path);
        }



        #region Auxiliary Methods
        private static void SetPropertyValue(ref XMLData recipientObj, string propertyName, object propertyValue)
        {
            try
            {
                PropertyInfo prop = recipientObj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
                if (prop != null)
                {
                    prop.SetValue(recipientObj, propertyValue);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }//end of class
}//end of namespace
