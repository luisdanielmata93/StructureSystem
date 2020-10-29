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
    public class LoadAnalysisData : ILoadAnalysisRepository
    {
        public bool Create(XMLLoadAnalysisData model)
        {
            bool result = false;
            try
            {
                XDocument mydoc = XDocument.Load(model.DocumentPath);

                var storey = mydoc.Root.Elements("LoadAnalysis")
                   .SelectMany(x => x.Elements("Storey"))
                   .ToList();

                //var st = storey.Find(x => x.Attribute("level").Value == Level.ToString());

                //if (st != null)
                //{
                //    result = true;
                //}

                //mydoc.Descendants("LoadAnalysis").LastOrDefault().Add(
                //       new XElement("Storey", new XAttribute("level", Level.ToString()),new XAttribute(),
                //            new XElement("Wall",
                //                                 new XElement("Number", "1"),
                //                                 new XElement("Material", model.Material.Name.ToString()),
                //                                 new XElement("Length", model.Length.ToString()),
                //                                 new XElement("TributaryArea", model.TributaryArea.ToString()),
                //                                 new XElement("Thickness", model.Thickness.ToString()),
                //                                 new XElement("Height", model.Height.ToString()),
                //                                 new XElement("PositionX", model.PositionX.ToString()),
                //                                 new XElement("PositionY", model.PositionY.ToString())
                //       )));

                //mydoc.Save(model.DocumentPath);


            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public IDictionary<string, object> Get(string elementName)
        {
            throw new NotImplementedException();
        }

        public bool Update(XMLLoadAnalysisData entity)
        {
            throw new NotImplementedException();
        }



        public bool IsSaved(string documentPath, int Level)
        {
            bool result = false;
            try
            {
                XDocument mydoc = XDocument.Load(documentPath);
               
                var storey = mydoc.Root.Elements("LoadAnalysis")
                   .SelectMany(x => x.Elements("Storey"))
                   .ToList();

                var st = storey.Find(x => x.Attribute("level").Value == Level.ToString());

                if(st != null)
                {
                    result = true;
                }
   
            }
            catch (Exception ex)
            {
               result =  false;
            }

            return result;
        }

    }//end of class
}//end of namespace
