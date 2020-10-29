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
    class VerticalWallsData : IVerticalWallRepository
    {
        public bool Create(XMLWallData model)
        {
            try
            {
                XDocument mydoc = XDocument.Load(model.DocumentPath);


                var storeys = mydoc.Root.Elements("VerticalWalls")
                    .SelectMany(x => x.Elements("Storey"))
                    .ToList();

                var st = storeys.Find(x => x.Attribute("level").Value == model.Storey.ToString());


                if (st != null)
                {
                    int registeredWalls = st.Descendants("Wall").Count() + 1;

                    st.Add(
                        new XElement("Wall",
                                         new XElement("Number", registeredWalls.ToString()),
                                         new XElement("Material", model.Material.Name.ToString()),
                                         new XElement("Length", model.Length.ToString()),
                                         new XElement("TributaryArea", model.TributaryArea.ToString()),
                                         new XElement("Thickness", model.Thickness.ToString()),
                                         new XElement("Height", model.Height.ToString()),
                                         new XElement("PositionX", model.PositionX.ToString()),
                                         new XElement("PositionY", model.PositionY.ToString()))
                        );
                    mydoc.Save(model.DocumentPath);
                    return true;
                }

                mydoc.Descendants("VerticalWalls").LastOrDefault().Add(
                        new XElement("Storey", new XAttribute("level", model.Storey.ToString()),
                             new XElement("Wall",
                                                  new XElement("Number", "1"),
                                                  new XElement("Material", model.Material.Name.ToString()),
                                                  new XElement("Length", model.Length.ToString()),
                                                  new XElement("TributaryArea", model.TributaryArea.ToString()),
                                                  new XElement("Thickness", model.Thickness.ToString()),
                                                  new XElement("Height", model.Height.ToString()),
                                                  new XElement("PositionX", model.PositionX.ToString()),
                                                  new XElement("PositionY", model.PositionY.ToString())
                        )));

                mydoc.Save(model.DocumentPath);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IDictionary<string, object> Get(string elementName)
        {
            throw new NotImplementedException();
        }
       
        public int Count(string documentPath, int storey)
        {
            try
            {
                XDocument mydoc = XDocument.Load(documentPath);

                int StoreysInDoc = mydoc.Descendants("VerticalWalls").Descendants("Storey").Count();
                if (StoreysInDoc == 0)
                    return 0;


                int registeredWalls = (from el in mydoc.Descendants("VerticalWalls").Descendants("Storey")
                                       where (string)el.Attribute("level") == storey.ToString()
                                       select el.Descendants("Wall").Count()).FirstOrDefault();


                return registeredWalls;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool Update(XMLWallData entity)
        {
            throw new NotImplementedException();
        }

    }//End of class
}//end of namespace
