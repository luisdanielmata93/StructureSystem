using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using StructureSystem.Model;
using static StructureSystem.Model.Enum;

namespace StructureSystem.Shared.Data
{
    internal static class Document
    {
        private static string DirectoryPath = ConfigurationManager.AppSettings["XMLpath"];
        private static string LocalPathXML = string.Empty;


        public static OperationResult Create(XMLData data)
        {
            return CreateXML(data);
        }

        public static OperationResult Import(string DocumentPath)
        {
            try
            {
                LocalPathXML = DocumentPath;
                IDictionary<string, object> generalData = new Dictionary<string, object>();
                
                XDocument doc = XDocument.Load(DocumentPath);
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

                return new OperationResult("0", "Archivo importado exitosamente", false, generalData);
            }
            catch (Exception ex)
            {
                return new OperationResult("-1", "Error al importar el documento.", true, ex);

            }
          

        }


        public static OperationResult SaveHorizontalWall(XMLData data)
        {
            return SaveHorizontalWallXML(data);
        }

        public static OperationResult SaveVerticalWall(XMLData data)
        {
            return SaveVerticalWallXML(data);
        }

        public static OperationResult Get(string tagName)
        {
            return GetNodeInfo(tagName);
        }

        public static OperationResult GetStoreysSpecification()
        {
            return GetStoreysSpecificationXML();
        }


        #region XML Interaction

        private static OperationResult GetStoreysSpecificationXML()
        {
            try
            {

                List<Storey> StoreysResult = new List<Storey>();

                string XMLPath = (LocalPathXML);

                XDocument doc = XDocument.Load(XMLPath);


                var storeys = doc.Root.Elements("HorizontalWalls")
                        .SelectMany(x => x.Elements("Storey"))
                        .ToList();


                foreach (var storey in storeys)
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

                        st.HorizontalWalls.Add(wa);
                    }

                    StoreysResult.Add(st);
                }


                var storeys2 = doc.Root.Elements("VerticalWalls")
                        .SelectMany(x => x.Elements("Storey"))
                        .ToList();


                foreach (var storey2 in storeys2)
                {


                    foreach (var wall in storey2.Elements("Wall"))
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

                        StoreysResult.Find(x => x.StoreyNumber == Convert.ToInt32((string)storey2.Attribute("level").Value)).
                            VerticalWalls.Add(wa);
                    }

                }

                return new OperationResult("0", "Obtención exitosa", false, StoreysResult);
            }
            catch (Exception ex)
            {
                return new OperationResult("-1", "Error al obtener información de los pisos", true, ex);
            }
        }


        private static OperationResult CreateXML(XMLData data)
        {
            try
            {
               
                LocalPathXML = string.Concat(DirectoryPath,data.ProjectName.ToString().Trim(), ".xml");
                

                if (!CreationValidate())
                    return new OperationResult("1", "Ya existe un proyecto registrado con este nombre, \n importe el proyecto o cree uno nuevo con diferente nombre", false);


                XDocument doc = new XDocument(new XElement("body",
                                                            new XElement("GeneralData",
                                                                new XElement("Client", data.ClientName),
                                                                new XElement("Project", data.ProjectName),
                                                                new XElement("Address", data.Address),
                                                                new XElement("Date", data.StartDate.ToString()),
                                                                new XElement("Storeys", data.Storeys.ToString()),
                                                                new XElement("Surface", data.Surface.ToString()),
                                                                new XElement("Regulation", data.Regulation.Name),
                                                                new XElement("Group", data.Group.Name),
                                                                new XElement("Usage", data.Usage.Name),
                                                                new XElement("Test", data.Test.Name)
                                                                        ),
                                                            new XElement("HorizontalWalls", ""),
                                                            new XElement("VerticalWalls", "")
                                                                    )
                                                        );

                System.IO.Directory.CreateDirectory(DirectoryPath);
                doc.Save(string.Concat(LocalPathXML));

                return new OperationResult("0", "Documento creado exitosamente", false);

            }
            catch (Exception ex)
            {
                return new OperationResult("-1", "Error al crear documento XML", true, ex);
            }

        }

        private static OperationResult SaveHorizontalWallXML(XMLData data)
        {
            try
            {
                XDocument mydoc = XDocument.Load(LocalPathXML);

                int StoreysInDoc = mydoc.Descendants("HorizontalWalls").Descendants("Storey").Count();

                if (StoreysInDoc < data.Storey)
                {
                    mydoc.Descendants("HorizontalWalls").LastOrDefault().Add(
                        new XElement("Storey", new XAttribute("level", data.Storey.ToString())));

                }
                mydoc.Descendants("HorizontalWalls").Descendants("Storey").LastOrDefault().Add( 
                    new XElement("Wall",
                                         new XElement("Number", data.CountH.ToString()),
                                         new XElement("Material", data.MaterialH.Name.ToString()),
                                         new XElement("Length", data.Length.ToString()),
                                         new XElement("TributaryArea", data.TributaryArea.ToString()),
                                         new XElement("Thickness", data.Thickness.ToString()),
                                         new XElement("Height", data.Height.ToString()),
                                         new XElement("PositionX", data.PositionX.ToString()),
                                         new XElement("PositionY", data.PositionY.ToString())));

                mydoc.Save(LocalPathXML);
                return new OperationResult("0", "Exito", false, data);
            }
            catch (Exception ex)
            {
                return new OperationResult("-1", string.Concat("Error al registrar muro horizontal"), true, ex);
            }
        }

        private static OperationResult SaveVerticalWallXML(XMLData data)
        {
            try
            {
                XDocument mydoc = XDocument.Load(LocalPathXML);

                int StoreysInDoc = mydoc.Descendants("VerticalWalls").Descendants("Storey").Count();

                if (StoreysInDoc < data.Storey)
                {
                    mydoc.Descendants("VerticalWalls").LastOrDefault().Add(
                        new XElement("Storey", new XAttribute("level", data.Storey.ToString())));

                }
                mydoc.Descendants("VerticalWalls").Descendants("Storey").LastOrDefault().Add(
                    new XElement("Wall",
                                         new XElement("Number", data.CountV.ToString()),
                                         new XElement("Material", data.MaterialV.Name.ToString()),
                                         new XElement("Length", data.Length.ToString()),
                                         new XElement("TributaryArea", data.TributaryArea.ToString()),
                                         new XElement("Thickness", data.Thickness.ToString()),
                                         new XElement("Height", data.Height.ToString()),
                                         new XElement("PositionX", data.PositionX.ToString()),
                                         new XElement("PositionY", data.PositionY.ToString())));

                mydoc.Save(LocalPathXML);

                return new OperationResult("0", "Exito", false, data);
            }
            catch (Exception ex)
            {
                return new OperationResult("-1", string.Concat("Error al registrar muro verticales"), true, ex);
            }
        }


        #endregion


        private static OperationResult GetNodeInfo(string tagName)
        {
            try
            {
                var mydoc = new XmlDocument();
                mydoc.Load(LocalPathXML);

                var Xmlnode = mydoc.GetElementsByTagName(tagName);

                var data = Xmlnode[0].InnerText;

                return new OperationResult("0", "Exito", false, data);
            }
            catch (Exception ex)
            {
                return new OperationResult("-1", string.Concat("Error al obtener nodo ", tagName, " en archivo XML"), true, ex);
            }
        }





        private static bool CreationValidate() { 
            if (!File.Exists(LocalPathXML))
                return true;
            else
                return false;
            
            
              
        }


    }//end of class
}//end of namespace
