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
        public bool Create(XMLWallData model)
        {
            try
            {
                XDocument mydoc = XDocument.Load(model.DocumentPath);


                var storeys = mydoc.Root.Elements("HorizontalWalls")
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
                                         new XElement("PositionY", model.PositionY.ToString()),
                                         new XElement("Dfc", model.Dfc.ToString())
                        ));

                    mydoc.Save(model.DocumentPath);
                    return true;
                }

                mydoc.Descendants("HorizontalWalls").LastOrDefault().Add(
                        new XElement("Storey", new XAttribute("level", model.Storey.ToString()),
                             new XElement("Wall",
                                                  new XElement("Number", "1"),
                                                  new XElement("Material", model.Material.Name.ToString()),
                                                  new XElement("Length", model.Length.ToString()),
                                                  new XElement("TributaryArea", model.TributaryArea.ToString()),
                                                  new XElement("Thickness", model.Thickness.ToString()),
                                                  new XElement("Height", model.Height.ToString()),
                                                  new XElement("PositionX", model.PositionX.ToString()),
                                                  new XElement("PositionY", model.PositionY.ToString()),
                                                  new XElement("Dfc", model.Dfc.ToString())
                        )));

                mydoc.Save(model.DocumentPath);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(XMLWallData model)
        {
            try
            {
                XDocument mydoc = XDocument.Load(model.DocumentPath);

                mydoc.Element("HorizontalWalls")
                 .Elements("Storey").Where(x => Convert.ToInt32((string)x.Attribute("level")) == model.Storey)
                 .Elements("Wall")
                 .Where(x => (string)x.Element("Number") == model.CountH.ToString() &&
                             (string)x.Element("Material") == model.Material.ToString() &&
                             (string)x.Element("Length") == model.Length.ToString() &&
                             (string)x.Element("TributaryArea") == model.TributaryArea.ToString() &&
                             (string)x.Element("Thickness") == model.Thickness.ToString() &&
                             (string)x.Element("Height") == model.Height.ToString() &&
                             (string)x.Element("PositionX") == model.PositionX.ToString() &&
                             (string)x.Element("PositionY") == model.PositionY.ToString() &&
                             (string)x.Element("Dfc") == model.Dfc.ToString())
                 .Remove();

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

        public int Count(string documentPath, int storey)
        {
            try
            {
                XDocument mydoc = XDocument.Load(documentPath);

                int StoreysInDoc = mydoc.Descendants("HorizontalWalls").Descendants("Storey").Count();
                if (StoreysInDoc == 0)
                    return 0;


                int registeredWalls = (from el in mydoc.Descendants("HorizontalWalls").Descendants("Storey")
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
    }//end of class
}//end of namespace
