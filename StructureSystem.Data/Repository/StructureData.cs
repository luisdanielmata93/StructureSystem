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
    public class StructureData : IStructureRepository
    {
        public bool Create(XMLStructureData entity)
        {
            throw new NotImplementedException();
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
                    wa.Side = Enums.SideType.Vertical;
                    StoreysResult.Find(x => x.StoreyNumber == Convert.ToInt32((string)storey.Attribute("level").Value)).
                        VerticalWalls.Add(wa);
                }

            }
            return StoreysResult;
        }


        public bool Update(XMLStructureData model)
        {
            return true;
            //try
            //{
            //    List<Storey> StoreysResult = new List<Storey>();

            //    XDocument doc = XDocument.Load(model.DocumentPath);


            //    var storeysH = doc.Root.Elements("HorizontalWalls")
            //            .SelectMany(x => x.Elements("Storey"))
            //            .ToList();

                
            //    foreach (var storey in storeysH)
            //    {
            //        Storey st = new Storey();

            //        st.StoreyNumber = Convert.ToInt32((string)storey.Attribute("level").Value);

                   
            //        foreach (var wall in storey.Elements("Wall"))
            //        {
            //            Wall wa = new Wall();
            //            wa.WallNumber = Convert.ToInt32((string)wall.Element("Number"));
            //            wa.Material = (string)wall.Element("Material");
            //            wa.Length = Convert.ToDouble((string)wall.Element("Length"));
            //            wa.TributaryArea = Convert.ToDouble((string)wall.Element("TributaryArea"));
            //            wa.Thickness = Convert.ToDouble((string)wall.Element("Thickness"));
            //            wa.Height = Convert.ToDouble((string)wall.Element("Height"));
            //            wa.PositionX = Convert.ToDouble((string)wall.Element("PositionX"));
            //            wa.PositionY = Convert.ToDouble((string)wall.Element("PositionY"));
            //            wa.Side = Enums.SideType.Horizontal;

            //            st.HorizontalWalls.Add(wa);
            //        }

            //        StoreysResult.Add(st);
            //    }


            //    var storeysV = doc.Root.Elements("VerticalWalls")
            //            .SelectMany(x => x.Elements("Storey"))
            //            .ToList();


            //    foreach (var storey in storeysV)
            //    {

            //        foreach (var wall in storey.Elements("Wall"))
            //        {
            //            Wall wa = new Wall();
            //            wa.WallNumber = Convert.ToInt32((string)wall.Element("Number"));
            //            wa.Material = (string)wall.Element("Material");
            //            wa.Length = Convert.ToDouble((string)wall.Element("Length"));
            //            wa.TributaryArea = Convert.ToDouble((string)wall.Element("TributaryArea"));
            //            wa.Thickness = Convert.ToDouble((string)wall.Element("Thickness"));
            //            wa.Height = Convert.ToDouble((string)wall.Element("Height"));
            //            wa.PositionX = Convert.ToDouble((string)wall.Element("PositionX"));
            //            wa.PositionY = Convert.ToDouble((string)wall.Element("PositionY"));
            //            wa.Side = Enums.SideType.Vertical;
            //            StoreysResult.Find(x => x.StoreyNumber == Convert.ToInt32((string)storey.Attribute("level").Value)).
            //                VerticalWalls.Add(wa);
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            
            //return StoreysResult;


        }
    }//end of class
}//end of namespace
