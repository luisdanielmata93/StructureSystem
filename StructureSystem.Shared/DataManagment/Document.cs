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
using Notifications.Wpf;

namespace StructureSystem.Shared.Data
{
    //internal static class Document
    //{
  

    //    private static OperationResult GetStoreysSpecificationXML()
    //    {
    //        try
    //        {

    //            List<Storey> StoreysResult = new List<Storey>();

    //            string XMLPath = (LocalPathXML);

    //            XDocument doc = XDocument.Load(XMLPath);


    //            var storeys = doc.Root.Elements("HorizontalWalls")
    //                    .SelectMany(x => x.Elements("Storey"))
    //                    .ToList();


    //            foreach (var storey in storeys)
    //            {
    //                Storey st = new Storey();

    //                st.StoreyNumber = Convert.ToInt32((string)storey.Attribute("level").Value);

    //                foreach (var wall in storey.Elements("Wall"))
    //                {
    //                    Wall wa = new Wall();
    //                    wa.WallNumber = Convert.ToInt32((string)wall.Element("Number"));
    //                    wa.Material = (string)wall.Element("Material");
    //                    wa.Length = Convert.ToDouble((string)wall.Element("Length"));
    //                    wa.TributaryArea = Convert.ToDouble((string)wall.Element("TributaryArea"));
    //                    wa.Thickness = Convert.ToDouble((string)wall.Element("Thickness"));
    //                    wa.Height = Convert.ToDouble((string)wall.Element("Height"));
    //                    wa.PositionX = Convert.ToDouble((string)wall.Element("PositionX"));
    //                    wa.PositionY = Convert.ToDouble((string)wall.Element("PositionY"));

    //                    st.HorizontalWalls.Add(wa);
    //                }

    //                StoreysResult.Add(st);
    //            }


    //            var storeys2 = doc.Root.Elements("VerticalWalls")
    //                    .SelectMany(x => x.Elements("Storey"))
    //                    .ToList();


    //            foreach (var storey2 in storeys2)
    //            {


    //                foreach (var wall in storey2.Elements("Wall"))
    //                {
    //                    Wall wa = new Wall();
    //                    wa.WallNumber = Convert.ToInt32((string)wall.Element("Number"));
    //                    wa.Material = (string)wall.Element("Material");
    //                    wa.Length = Convert.ToDouble((string)wall.Element("Length"));
    //                    wa.TributaryArea = Convert.ToDouble((string)wall.Element("TributaryArea"));
    //                    wa.Thickness = Convert.ToDouble((string)wall.Element("Thickness"));
    //                    wa.Height = Convert.ToDouble((string)wall.Element("Height"));
    //                    wa.PositionX = Convert.ToDouble((string)wall.Element("PositionX"));
    //                    wa.PositionY = Convert.ToDouble((string)wall.Element("PositionY"));

    //                    StoreysResult.Find(x => x.StoreyNumber == Convert.ToInt32((string)storey2.Attribute("level").Value)).
    //                        VerticalWalls.Add(wa);
    //                }

    //            }

    //            return new OperationResult(0, "Obtención exitosa", StoreysResult, NotificationType.Success, false);
    //        }
    //        catch (Exception ex)
    //        {
    //            return new OperationResult(-1, "Error al obtener información de los pisos", NotificationType.Error, true, ex);
    //        }
    //    }


  





    //}//end of class
}//end of namespace
