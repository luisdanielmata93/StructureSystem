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
            try
            {

                XDocument doc = XDocument.Load(model.DocumentPath);

                var storeysH = doc.Root.Elements("HorizontalWalls")
                        .SelectMany(x => x.Elements("Storey"))
                        .ToList();

                foreach (var storey in storeysH)
                {
                    int StoreyNumberAux = Convert.ToInt32((string)storey.Attribute("level").Value);

                    var StryTmp = model.Storeys.Find(x => x.StoreyNumber == StoreyNumberAux);
                    var wallCount = storey.Elements("Wall").Count();

                    if (StryTmp.HorizontalWalls.Count != wallCount)
                    {
                        storey.Elements("Wall").Remove();
                        for (int i = 0; i < StryTmp.HorizontalWalls.Count; i++)
                        {
                            storey.Add(
                            new XElement("Wall",
                                        new XElement("Number", StryTmp.HorizontalWalls[i].WallNumber.ToString()),
                                        new XElement("Material", StryTmp.HorizontalWalls[i].Material.ToString()),
                                        new XElement("Length", StryTmp.HorizontalWalls[i].Length.ToString()),
                                        new XElement("TributaryArea", StryTmp.HorizontalWalls[i].TributaryArea.ToString()),
                                        new XElement("Thickness", StryTmp.HorizontalWalls[i].Thickness.ToString()),
                                        new XElement("Height", StryTmp.HorizontalWalls[i].Height.ToString()),
                                        new XElement("PositionX", StryTmp.HorizontalWalls[i].PositionX.ToString()),
                                        new XElement("PositionY", StryTmp.HorizontalWalls[i].PositionY.ToString())));
                        }
                    }
                    else
                    {
                        foreach (var wall in storey.Elements("Wall"))
                        {
                            int wallNum = Convert.ToInt32((string)wall.Element("Number"));

                            var wallTmp = StryTmp.HorizontalWalls.ToList().Find(x => x.WallNumber == wallNum);

                            wall.SetElementValue("Material", wallTmp.Material);
                            wall.SetElementValue("Length", wallTmp.Length);
                            wall.SetElementValue("TributaryArea", wallTmp.TributaryArea);
                            wall.SetElementValue("Thickness", wallTmp.Thickness);
                            wall.SetElementValue("Height", wallTmp.Height);
                            wall.SetElementValue("PositionX", wallTmp.PositionX);
                            wall.SetElementValue("PositionY", wallTmp.PositionY);

                        }
                    }
                }


                var storeysV = doc.Root.Elements("VerticalWalls")
                        .SelectMany(x => x.Elements("Storey"))
                        .ToList();


                foreach (var storey in storeysV)
                {
                    int StoreyNumberAux = Convert.ToInt32((string)storey.Attribute("level").Value);

                    var StryTmp = model.Storeys.Find(x => x.StoreyNumber == StoreyNumberAux);
                    var wallCount = storey.Elements("Wall").Count();

                    if (StryTmp.VerticalWalls.Count != wallCount)
                    {
                        storey.Elements("Wall").Remove();
                        for (int i = 0; i < StryTmp.VerticalWalls.Count; i++)
                        {
                            storey.Add(
                            new XElement("Wall",
                                        new XElement("Number", StryTmp.VerticalWalls[i].WallNumber.ToString()),
                                        new XElement("Material", StryTmp.VerticalWalls[i].Material.ToString()),
                                        new XElement("Length", StryTmp.VerticalWalls[i].Length.ToString()),
                                        new XElement("TributaryArea", StryTmp.VerticalWalls[i].TributaryArea.ToString()),
                                        new XElement("Thickness", StryTmp.VerticalWalls[i].Thickness.ToString()),
                                        new XElement("Height", StryTmp.VerticalWalls[i].Height.ToString()),
                                        new XElement("PositionX", StryTmp.VerticalWalls[i].PositionX.ToString()),
                                        new XElement("PositionY", StryTmp.VerticalWalls[i].PositionY.ToString())));
                        }
                    }
                    else
                    {
                        foreach (var wall in storey.Elements("Wall"))
                        {
                            int wallNum = Convert.ToInt32((string)wall.Element("Number"));

                            var wallTmp = StryTmp.VerticalWalls.ToList().Find(x => x.WallNumber == wallNum);

                            wall.SetElementValue("Material", wallTmp.Material);
                            wall.SetElementValue("Length", wallTmp.Length);
                            wall.SetElementValue("TributaryArea", wallTmp.TributaryArea);
                            wall.SetElementValue("Thickness", wallTmp.Thickness);
                            wall.SetElementValue("Height", wallTmp.Height);
                            wall.SetElementValue("PositionX", wallTmp.PositionX);
                            wall.SetElementValue("PositionY", wallTmp.PositionY);
                        }
                    }
                }

                doc.Save(model.DocumentPath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }//end of class
}//end of namespace
