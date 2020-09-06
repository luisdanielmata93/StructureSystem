using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.Model;


namespace StructureSystem.Shared.Configuration
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
                    materials.Add(new Material { Id = Convert.ToInt32(x.Id), Name = x.Name });
                });
            }
            catch (Exception ex) { throw; }

            return materials;
        }

    }//end of class
}//end of namespace
