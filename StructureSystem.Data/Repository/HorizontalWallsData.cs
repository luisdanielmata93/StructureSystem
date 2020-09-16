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
    public class HorizontalWallsData : IHorizontalWallRepository
    {
        public bool Create(XMLHorizontalWallData model)
        {
            try
            {
                XDocument mydoc = XDocument.Load(model.DocumentPath);

                int StoreysInDoc = mydoc.Descendants("HorizontalWalls").Descendants("Storey").Count();

                if (StoreysInDoc < model.Storey)
                {
                    mydoc.Descendants("HorizontalWalls").LastOrDefault().Add(
                        new XElement("Storey", new XAttribute("level", model.Storey.ToString())));

                }
                mydoc.Descendants("HorizontalWalls").Descendants("Storey").LastOrDefault().Add(
                    new XElement("Wall",
                                         new XElement("Number", model.Count.ToString()),
                                         new XElement("Material", model.MaterialH.Name.ToString()),
                                         new XElement("Length", model.Length.ToString()),
                                         new XElement("TributaryArea", model.TributaryArea.ToString()),
                                         new XElement("Thickness", model.Thickness.ToString()),
                                         new XElement("Height", model.Height.ToString()),
                                         new XElement("PositionX", model.PositionX.ToString()),
                                         new XElement("PositionY", model.PositionY.ToString())));

                mydoc.Save(model.DocumentPath);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IDictionary<string, object> Get(string documentPath)
        {
            throw new NotImplementedException();
        }

        public bool Update(XMLHorizontalWallData entity)
        {
            throw new NotImplementedException();
        }
    }//end of class
}//end of namespace
