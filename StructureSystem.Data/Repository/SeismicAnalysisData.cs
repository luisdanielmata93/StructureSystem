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
                     new XElement("Row", new XAttribute("T", item.T), new XAttribute("S", item.S))
                     );
            }

            doc.Save(document.DocumentPath);

            return result;
        }

        public IList<EspectroDisenio> GetEspectroDisenio(string XMLPath)
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

        public IList<Storey> Get(string XMLPath)
        {
            List<Storey> StoreysResult = new List<Storey>();

            XDocument doc = XDocument.Load(XMLPath);

            var storeysH = doc.Root.Elements("HorizontalWalls")
                    .SelectMany(x => x.Elements("Storey"))
                    .ToList();


            foreach (var storey in storeysH)
            {
                Storey st = new Storey();

                st.StoreyNumber = Convert.ToInt32((string)storey.Attribute("level").Value);

                foreach (var wall in storey.Elements("Wall"))
                {
                    Wall wa = new Wall();
                    wa.WallNumber = Convert.ToInt32((string)wall.Element("Number"));
                    wa.Material = (string)wall.Element("Material");
                    wa.Length = Convert.ToDouble((string)wall.Element("Length"));
                    wa.TributaryArea = Convert.ToDouble((string)wall.Element("TributaryArea"));
                    wa.Thickness = Convert.ToDouble((string)wall.Element("Thickness"));
                    wa.Height = Convert.ToDouble((string)wall.Element("Height"));
                    wa.PositionX = Convert.ToDouble((string)wall.Element("PositionX"));
                    wa.PositionY = Convert.ToDouble((string)wall.Element("PositionY"));
                    wa.AnchoDeApoyo = wa.Thickness;
                    wa.Side = Enums.SideType.Horizontal;

                    st.HorizontalWalls.Add(wa);
                }

                StoreysResult.Add(st);
            }


            var storeysV = doc.Root.Elements("VerticalWalls")
                    .SelectMany(x => x.Elements("Storey"))
                    .ToList();


            foreach (var storey in storeysV)
            {

                foreach (var wall in storey.Elements("Wall"))
                {
                    Wall wa = new Wall();
                    wa.WallNumber = Convert.ToInt32((string)wall.Element("Number"));
                    wa.Material = (string)wall.Element("Material");
                    wa.Length = Convert.ToDouble((string)wall.Element("Length"));
                    wa.TributaryArea = Convert.ToDouble((string)wall.Element("TributaryArea"));
                    wa.Thickness = Convert.ToDouble((string)wall.Element("Thickness"));
                    wa.Height = Convert.ToDouble((string)wall.Element("Height"));
                    wa.PositionX = Convert.ToDouble((string)wall.Element("PositionX"));
                    wa.PositionY = Convert.ToDouble((string)wall.Element("PositionY"));
                    wa.AnchoDeApoyo = wa.Thickness;
                    wa.Side = Enums.SideType.Vertical;
                    StoreysResult.Find(x => x.StoreyNumber == Convert.ToInt32((string)storey.Attribute("level").Value)).
                        VerticalWalls.Add(wa);
                }

            }
            return StoreysResult;
        }


        public bool Update(XMLSeismicAnalysisData entity)
        {
            throw new NotImplementedException();
        }

        public bool GuardarProps(string document, double c, double Q, double R)
        {
            bool result = false;
            try
            {
                XDocument doc = XDocument.Load(document);
                var root = doc.Root.Elements("GeneralData").LastOrDefault();

                bool existsObj = root.Attribute("c") != null || root.Attribute("Q") != null || root.Attribute("R") != null ? true : false;


                if (!existsObj)
                    root.Add(new XAttribute("c", c), new XAttribute("Q", Q), new XAttribute("R", R));
                else
                {
                    root.Attribute("c").Value = c.ToString();
                    root.Attribute("Q").Value = Q.ToString();
                    root.Attribute("R").Value = R.ToString();
                }

                doc.Save(document);
            }
            catch (Exception ex)
            {

            }
           
            return result;
        }
    }//end of class
}//end of namespace
