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
                                         new XElement("Dfc", model.Dfc.ToString()),
                                         new XElement("AnchoDeApoyo", model.Thickness.ToString()),
                                         new XElement("ExcentricidadDeCarga", 0),
                                         new XElement("Tipo", ""),
                                         new XElement("RestringidoSuperiorInferior", "false"),
                                         new XElement("LongClaroIzquierdo", 0),
                                         new XElement("LongClaroDerecho", 0),
                                         new XElement("FE", 0),
                                         new XElement("FactorAlturaEfectiva", 0),
                                         new XElement("PR", 0),
                                         new XElement("Mo", 0),
                                         new XElement("P1X", 0),
                                         new XElement("P1Y", 0),
                                         new XElement("P2X", 0),
                                         new XElement("P2Y", 0),
                                         new XElement("P3X", 0),
                                         new XElement("P3Y", 0),
                                         new XElement("P4X", 0),
                                         new XElement("P4Y", 0),
                                         new XElement("P5X", 0),
                                         new XElement("P5Y", 0),
                                         new XElement("ResMamposteriaCortante", 0),
                                         new XElement("AceroDeRefuerzo", "false"),
                                         new XElement("Ash", 0),
                                         new XElement("Sh", 0),
                                         new XElement("EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal", 0),
                                         new XElement("ResAceroRefuerzoHorizontalCortante", 0),
                                         new XElement("ResistenciaTotalACortante", 0),
                                         new XElement("ResistenciaCortanteConcreto", 0),
                                         new XElement("Conclusion", ""),
                                         new XElement("As", 1),
                                         new XElement("bc", 5)
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
                                                 new XElement("Dfc", model.Dfc.ToString()),
                                                 new XElement("AnchoDeApoyo", model.Thickness.ToString()),
                                                 new XElement("ExcentricidadDeCarga", 0),
                                                 new XElement("Tipo", ""),
                                                 new XElement("RestringidoSuperiorInferior", "false"),
                                                 new XElement("LongClaroIzquierdo", 0),
                                                 new XElement("LongClaroDerecho", 0),
                                                 new XElement("FE", 0),
                                                 new XElement("FactorAlturaEfectiva", 0),
                                                 new XElement("PR", 0),
                                                 new XElement("Mo", 0),
                                                 new XElement("P1X", 0),
                                                 new XElement("P1Y", 0),
                                                 new XElement("P2X", 0),
                                                 new XElement("P2Y", 0),
                                                 new XElement("P3X", 0),
                                                 new XElement("P3Y", 0),
                                                 new XElement("P4X", 0),
                                                 new XElement("P4Y", 0),
                                                 new XElement("P5X", 0),
                                                 new XElement("P5Y", 0),
                                                 new XElement("ResMamposteriaCortante", 0),
                                                 new XElement("AceroDeRefuerzo", "false"),
                                                 new XElement("Ash", 0),
                                                 new XElement("Sh", 0),
                                                 new XElement("EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal", 0),
                                                 new XElement("ResAceroRefuerzoHorizontalCortante", 0),
                                                 new XElement("ResistenciaTotalACortante", 0),
                                                 new XElement("ResistenciaCortanteConcreto", 0),
                                                 new XElement("Conclusion", ""),
                                                 new XElement("As", 1),
                                                 new XElement("bc", 5)
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
