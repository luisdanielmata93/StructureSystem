using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using StructureSystem.Model;

namespace StructureSystem.BusinessRules.Configuration
{
    public class WebConfig
    {
        private WebConfiguration config = null;


        public WebConfig()
        {
            try
            {
                config = (WebConfiguration)ConfigurationManager.GetSection("BaseConfigurationData");
            }
            catch (Exception ex) { throw ex; }
        }

        public string GetElementByName(string element)
        {
            string result;
            try
            {
                result = ConfigurationManager.AppSettings[element];
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public bool SetLastDocument(string xmlPath)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("LastDocument");
                config.AppSettings.Settings.Add("LastDocument", xmlPath);
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

   
        public List<Regulation> GetRegulationsCollection()
        {
            var regulations = new List<Regulation>();

            try
            {
                config.Regulations.Cast<ConfigurationObject>().ToList().ForEach(x =>
                {
                    regulations.Add(new Regulation { Id = Convert.ToInt32(x.Id), Name = x.Name });
                });
            }
            catch (Exception ex) { throw; }

            return regulations;
        }

        public List<Group> GetGroupsCollection()
        {
            var groups = new List<Group>();

            try
            {
                config.Groups.Cast<ConfigurationObject>().ToList().ForEach(x =>
                {
                    groups.Add(new Group { Id = Convert.ToInt32(x.Id), Name = x.Name });
                });
            }
            catch (Exception ex) { throw; }

            return groups;
        }

        public List<Usage> GetUsagesCollection()
        {
            var usages = new List<Usage>();

            try
            {
                config.Usages.Cast<ConfigurationObject>().ToList().ForEach(x =>
                {
                    usages.Add(new Usage { Id = Convert.ToInt32(x.Id), Name = x.Name });
                });
            }
            catch (Exception ex) { throw; }

            return usages;
        }

        public List<Test> GetTestsCollection()
        {
            var tests = new List<Test>();

            try
            {
                config.Tests.Cast<ConfigurationObject>().ToList().ForEach(x =>
                {
                    tests.Add(new Test { Id = Convert.ToInt32(x.Id), Name = x.Name });
                });
            }
            catch (Exception ex) { throw; }

            return tests;
        }

        public List<Material> GetMaterialsCollection()
        {
            var materials = new List<Material>();

            try
            {

                config.Materials.Cast<ConfigurationObject>().ToList().ForEach(x =>
                {
                    materials.Add(new Material { Id = Convert.ToInt32(x.Id), Name = x.Name ,Dfm = x.Dfm, Dvm = x.Dvm, Em = x.Em, Gm = x.Gm, vm=x.vm, PV=x.PV });
                });
            }
            catch (Exception ex) { throw; }

            return materials;
        }

        public List<FlooringMaterial> GetFlooringMaterialsCollection()
        {
            var flooringMaterials = new List<FlooringMaterial>();

            try
            {
                config.FlooringMaterials.Cast<ConfigurationObject>().ToList().ForEach(x =>
                {
                    flooringMaterials.Add(new FlooringMaterial { Id = Convert.ToInt32(x.Id), Name = x.Name });
                });
            }
            catch (Exception ex) { throw; }

            return flooringMaterials;
        }
    }//end of class
}//end of namespace