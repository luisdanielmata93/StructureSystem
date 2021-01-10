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
    public class SeismicAnalysisData : ISeismicAnalysisRepository
    {
        public bool Create(XMLSeismicAnalysisData document)
        {
            bool result = false;
           
            XDocument doc = XDocument.Load(document.DocumentPath);
            var root = doc.Root.Elements("DisenioEspectral").LastOrDefault();

          
            var Rows = doc.Root.Elements("DisenioEspectral")
                     .SelectMany(x => x.Elements("Row"))
                     .ToList();

            if (Rows.Count > 0)
                doc.Root.Elements("DisenioEspectral").SelectMany(x => x.Elements("Row")).Remove();

            foreach (var item in document.DisenioEspectral)
            {
                root.Add(
                     new XElement("Row", new XAttribute("T",item.T ), new XAttribute("S",item.S))
                     );
            }

            doc.Save(document.DocumentPath);

            return result;
        }

        public IList<EspectroDisenio> Get(string XMLPath)
        {
            List<EspectroDisenio> result = new List<EspectroDisenio>();

            XDocument doc = XDocument.Load(XMLPath);

            var Rows = doc.Root.Elements("DisenioEspectral")
                    .SelectMany(x => x.Elements("Row"))
                    .ToList();


            foreach (var row in Rows)
            {
                EspectroDisenio st = new EspectroDisenio();

                    st.T = Convert.ToDouble((string)row.Attribute("T"));
                    st.S = Convert.ToDouble((string)row.Attribute("S"));                

                result.Add(st);
            }


            
            return result;
        }

      

        public bool Update(XMLSeismicAnalysisData entity)
        {
            throw new NotImplementedException();
        }
    }//end of class
}//end of namespace
