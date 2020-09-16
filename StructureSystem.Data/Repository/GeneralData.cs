using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.Data.Model;
using System.Configuration;
using System.Xml.Linq;
using StructureSystem.Model;

namespace StructureSystem.Data
{
    public class GeneralData : IGeneralDataRepository
    {
      
        public bool Create(XMLGeneralData model)
        {
            try
            {
                string LocalPathXML = string.Concat(model.DocumentPath, model.ProjectName.ToString().Trim(), ".xml");

                XDocument doc = new XDocument(new XElement("body",
                                                            new XElement("GeneralData",
                                                                new XElement("Client", model.ClientName),
                                                                new XElement("Project", model.ProjectName),
                                                                new XElement("Address", model.Address),
                                                                new XElement("Date", model.StartDate.ToString()),
                                                                new XElement("Storeys", model.Storeys.ToString()),
                                                                new XElement("Surface", model.Surface.ToString()),
                                                                new XElement("Regulation", model.Regulation.Name),
                                                                new XElement("Group", model.Group.Name),
                                                                new XElement("Usage", model.Usage.Name),
                                                                new XElement("Test", model.Test.Name)
                                                                        ),
                                                            new XElement("HorizontalWalls", ""),
                                                            new XElement("VerticalWalls", "")
                                                                    )
                                                        );

                System.IO.Directory.CreateDirectory(model.DocumentPath);
                doc.Save(string.Concat(LocalPathXML));

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IDictionary<string, object> Get(string documentPath)
        {
            try
            {
                IDictionary<string, object> generalData = new Dictionary<string, object>();

                XDocument doc = XDocument.Load(documentPath);
                foreach (XElement item in doc.Root
                                      .Elements("GeneralData"))
                {
                    generalData.Add("Client", (string)item.Element("Client"));
                    generalData.Add("Project", (string)item.Element("Project"));
                    generalData.Add("Address", (string)item.Element("Address"));
                    generalData.Add("Date", (string)item.Element("Date"));
                    generalData.Add("Storeys", (string)item.Element("Storeys"));
                    generalData.Add("Surface", (string)item.Element("Surface"));
                    generalData.Add("Regulation", (string)item.Element("Regulation"));
                    generalData.Add("Group", (string)item.Element("Group"));
                    generalData.Add("Usage", (string)item.Element("Usage"));
                    generalData.Add("Test", (string)item.Element("Test"));

                }
                return generalData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public bool Update(XMLGeneralData model)
        {
            string LocalPathXML = string.Concat(model.DocumentPath, model.ProjectName.ToString().Trim(), ".xml");

            try
            {
                XDocument mydoc = XDocument.Load(LocalPathXML);
                var root = mydoc.Root.Element("GeneralData");
                root.SetElementValue("Client", model.ClientName.ToString());
                root.SetElementValue("Address", model.Address.ToString());
                root.SetElementValue("Date", model.StartDate.ToString());
                root.SetElementValue("Storeys", model.Storeys.ToString());
                root.SetElementValue("Surface", model.Surface.ToString());
                root.SetElementValue("Regulation", model.Regulation.Name.ToString());
                root.SetElementValue("Group", model.Group.Name.ToString());
                root.SetElementValue("Usage", model.Usage.Name.ToString());
                root.SetElementValue("Test", model.Test.Name.ToString());
                mydoc.Save(LocalPathXML);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }//end of class
}//end of namespace
