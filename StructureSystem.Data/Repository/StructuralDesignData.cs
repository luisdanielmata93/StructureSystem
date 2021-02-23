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
    public class StructuralDesignData : IStructuralDesignRepository
    {
        public bool Create(XMLStructuralDesignData model)
        {
            bool result = true;
            try
            {
                XDocument doc = XDocument.Load(model.DocumentPath);
                List<XElement> storeysH = new List<XElement>();
                List<XElement> storeysV = new List<XElement>();

                storeysH = doc.Root.Elements("HorizontalWalls")
                     .SelectMany(x => x.Elements("Storey"))
                     .ToList();

                storeysV = doc.Root.Elements("VerticalWalls")
                 .SelectMany(x => x.Elements("Storey"))
                 .ToList();


                if (storeysH.Count <= 0)
                    return false;
                if (storeysV.Count <= 0)
                    return false;


                foreach (var storey in storeysH)
                {
                    var stTemp = model.Storeys.Single(x => x.StoreyNumber == Convert.ToInt32((string)storey.Attribute("level").Value));
                    storey.SetAttributeValue("EsfuerzoNormalPromedio", stTemp.EsfuerzoNormalPromedio);
                    storey.SetAttributeValue("CortanteResistenteEntrepisoMamposteria", stTemp.CortanteResistenteEntrepisoMamposteria);
                    storey.SetAttributeValue("CortanteResistenteEntrepisoConcreto", stTemp.CortanteResistenteEntrepisoConcreto);
                    storey.SetAttributeValue("CortanteEntrepisoTotal", stTemp.CortanteEntrepisoTotal);
                    storey.SetAttributeValue("ConclusionX", stTemp.ConclusionX);
                    storey.SetAttributeValue("ConclusionY", stTemp.ConclusionY);

                    foreach (var wall in storey.Elements("Wall"))
                    {

                        var wallTemp = stTemp.HorizontalWalls.Single(x => x.WallNumber == Convert.ToInt32((string)wall.Element("Number")));

                        wall.SetElementValue("AnchoDeApoyo", wallTemp.AnchoDeApoyo.ToString());
                        wall.SetElementValue("ExcentricidadDeCarga", wallTemp.ExcentricidadDeCarga.ToString());
                        wall.SetElementValue("Tipo", wallTemp.Tipo);
                        wall.SetElementValue("RestringidoSuperiorInferior", wallTemp.RestringidoSuperiorInferior.ToString());
                        wall.SetElementValue("LongClaroIzquierdo", wallTemp.LongClaroIzquierdo.ToString());
                        wall.SetElementValue("LongClaroDerecho", wallTemp.LongClaroDerecho.ToString());
                        wall.SetElementValue("FE", wallTemp.FE.ToString());
                        wall.SetElementValue("FactorAlturaEfectiva", wallTemp.FactorAlturaEfectiva.ToString());
                        wall.SetElementValue("PR", wallTemp.PR.ToString());
                        wall.SetElementValue("Mo", wallTemp.Mo.ToString());
                        wall.SetElementValue("P1X", wallTemp.P1X.ToString());
                        wall.SetElementValue("P1Y", wallTemp.P1Y.ToString());
                        wall.SetElementValue("P2X", wallTemp.P2X.ToString());
                        wall.SetElementValue("P2Y", wallTemp.P2Y.ToString());
                        wall.SetElementValue("P3X", wallTemp.P3X.ToString());
                        wall.SetElementValue("P3Y", wallTemp.P3Y.ToString());
                        wall.SetElementValue("P4X", wallTemp.P4X.ToString());
                        wall.SetElementValue("P4Y", wallTemp.P4Y.ToString());
                        wall.SetElementValue("P5X", wallTemp.P5X.ToString());
                        wall.SetElementValue("P5Y", wallTemp.P5Y.ToString());
                        wall.SetElementValue("ResMamposteriaCortante", wallTemp.ResMamposteriaCortante.ToString());
                        wall.SetElementValue("AceroDeRefuerzo", wallTemp.AceroDeRefuerzo.ToString());
                        wall.SetElementValue("Ash", wallTemp.Ash.ToString());
                        wall.SetElementValue("Sh", wallTemp.Sh.ToString());
                        wall.SetElementValue("EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal", wallTemp.EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal > 0 ? wallTemp.EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal.ToString() : "6000");
                        wall.SetElementValue("ResAceroRefuerzoHorizontalCortante", wallTemp.ResAceroRefuerzoHorizontalCortante.ToString());
                        wall.SetElementValue("ResistenciaTotalACortante", wallTemp.ResistenciaTotalACortante.ToString());
                        wall.SetElementValue("ResistenciaCortanteConcreto", wallTemp.ResistenciaCortanteConcreto.ToString());
                        wall.SetElementValue("Conclusion", wallTemp.Conclusion.ToString());
                        wall.SetElementValue("As", wallTemp.As.ToString());
                        wall.SetElementValue("bc", wallTemp.bc.ToString());

                    }

                }

                foreach (var storey in storeysV)
                {
                    var stTemp = model.Storeys.Single(x => x.StoreyNumber == Convert.ToInt32((string)storey.Attribute("level").Value));
                    storey.SetAttributeValue("EsfuerzoNormalPromedio", stTemp.EsfuerzoNormalPromedio);
                    storey.SetAttributeValue("CortanteResistenteEntrepisoMamposteria", stTemp.CortanteResistenteEntrepisoMamposteria);
                    storey.SetAttributeValue("CortanteResistenteEntrepisoConcreto", stTemp.CortanteResistenteEntrepisoConcreto);
                    storey.SetAttributeValue("CortanteEntrepisoTotal", stTemp.CortanteEntrepisoTotal);
                    storey.SetAttributeValue("ConclusionX", stTemp.ConclusionX);
                    storey.SetAttributeValue("ConclusionY", stTemp.ConclusionY);

                    foreach (var wall in storey.Elements("Wall"))
                    {
                        var wallTemp = stTemp.VerticalWalls.Single(x => x.WallNumber == Convert.ToInt32((string)wall.Element("Number")));
                        wall.SetElementValue("AnchoDeApoyo", wallTemp.AnchoDeApoyo.ToString());
                        wall.SetElementValue("ExcentricidadDeCarga", wallTemp.ExcentricidadDeCarga.ToString());
                        wall.SetElementValue("Tipo", wallTemp.Tipo);
                        wall.SetElementValue("RestringidoSuperiorInferior", wallTemp.RestringidoSuperiorInferior.ToString());
                        wall.SetElementValue("LongClaroIzquierdo", wallTemp.LongClaroIzquierdo.ToString());
                        wall.SetElementValue("LongClaroDerecho", wallTemp.LongClaroDerecho.ToString());
                        wall.SetElementValue("FE", wallTemp.FE.ToString());
                        wall.SetElementValue("FactorAlturaEfectiva", wallTemp.FactorAlturaEfectiva.ToString());
                        wall.SetElementValue("PR", wallTemp.PR.ToString());
                        wall.SetElementValue("Mo", wallTemp.Mo.ToString());
                        wall.SetElementValue("P1X", wallTemp.P1X.ToString());
                        wall.SetElementValue("P1Y", wallTemp.P1Y.ToString());
                        wall.SetElementValue("P2X", wallTemp.P2X.ToString());
                        wall.SetElementValue("P2Y", wallTemp.P2Y.ToString());
                        wall.SetElementValue("P3X", wallTemp.P3X.ToString());
                        wall.SetElementValue("P3Y", wallTemp.P3Y.ToString());
                        wall.SetElementValue("P4X", wallTemp.P4X.ToString());
                        wall.SetElementValue("P4Y", wallTemp.P4Y.ToString());
                        wall.SetElementValue("P5X", wallTemp.P5X.ToString());
                        wall.SetElementValue("P5Y", wallTemp.P5Y.ToString());
                        wall.SetElementValue("ResMamposteriaCortante", wallTemp.ResMamposteriaCortante.ToString());
                        wall.SetElementValue("AceroDeRefuerzo", wallTemp.AceroDeRefuerzo.ToString());
                        wall.SetElementValue("Ash", wallTemp.Ash.ToString());
                        wall.SetElementValue("Sh", wallTemp.Sh.ToString());
                        wall.SetElementValue("EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal", wallTemp.EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal > 0 ? wallTemp.EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal.ToString() : "6000");
                        wall.SetElementValue("ResAceroRefuerzoHorizontalCortante", wallTemp.ResAceroRefuerzoHorizontalCortante.ToString());
                        wall.SetElementValue("ResistenciaTotalACortante", wallTemp.ResistenciaTotalACortante.ToString());
                        wall.SetElementValue("ResistenciaCortanteConcreto", wallTemp.ResistenciaCortanteConcreto.ToString());
                        wall.SetElementValue("Conclusion", wallTemp.Conclusion.ToString());
                        wall.SetElementValue("As", wallTemp.As.ToString());
                        wall.SetElementValue("bc", wallTemp.bc.ToString());
                    }

                }

                doc.Save(model.DocumentPath);

            }
            catch (Exception ex)
            {
                result = false;
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
                    wa.Side = Enums.SideType.Horizontal;
                    wa.AnchoDeApoyo = Convert.ToDouble((string)wall.Element("AnchoDeApoyo"));
                    wa.ExcentricidadDeCarga = Convert.ToDouble((string)wall.Element("ExcentricidadDeCarga"));
                    wa.Tipo = (string)wall.Element("Tipo");
                    wa.RestringidoSuperiorInferior = Convert.ToBoolean((string)wall.Element("RestringidoSuperiorInferior"));
                    wa.LongClaroIzquierdo = Convert.ToDouble((string)wall.Element("LongClaroIzquierdo"));
                    wa.LongClaroDerecho = Convert.ToDouble((string)wall.Element("LongClaroDerecho"));
                    wa.FE = Convert.ToDouble((string)wall.Element("FE"));
                    wa.FactorAlturaEfectiva = Convert.ToDouble((string)wall.Element("FactorAlturaEfectiva"));
                    wa.PR = Convert.ToDouble((string)wall.Element("PR"));
                    wa.Mo = Convert.ToDouble((string)wall.Element("Mo"));
                    wa.P1X = Convert.ToDouble((string)wall.Element("P1X"));
                    wa.P1Y = Convert.ToDouble((string)wall.Element("P1Y"));
                    wa.P2X = Convert.ToDouble((string)wall.Element("P2X"));
                    wa.P2Y = Convert.ToDouble((string)wall.Element("P2Y"));
                    wa.P3X = Convert.ToDouble((string)wall.Element("P3X"));
                    wa.P3Y = Convert.ToDouble((string)wall.Element("P3Y"));
                    wa.P4X = Convert.ToDouble((string)wall.Element("P4X"));
                    wa.P4Y = Convert.ToDouble((string)wall.Element("P4Y"));
                    wa.P5X = Convert.ToDouble((string)wall.Element("P5X"));
                    wa.P5Y = Convert.ToDouble((string)wall.Element("P5Y"));
                    wa.ResMamposteriaCortante = Convert.ToDouble((string)wall.Element("ResMamposteriaCortante"));
                    wa.AceroDeRefuerzo = Convert.ToBoolean((string)wall.Element("AceroDeRefuerzo"));
                    wa.Ash = Convert.ToDouble((string)wall.Element("Ash"));
                    wa.Sh = Convert.ToDouble((string)wall.Element("Sh"));
                    wa.EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal = Convert.ToDouble((string)wall.Element("EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal"));
                    wa.ResAceroRefuerzoHorizontalCortante = Convert.ToDouble((string)wall.Element("ResAceroRefuerzoHorizontalCortante"));
                    wa.ResistenciaTotalACortante = Convert.ToDouble((string)wall.Element("ResistenciaTotalACortante"));
                    wa.ResistenciaCortanteConcreto = Convert.ToDouble((string)wall.Element("ResistenciaCortanteConcreto"));
                    wa.Conclusion = (string)wall.Element("Conclusion");
                    wa.As = Convert.ToDouble((string)wall.Element("As"));
                    wa.bc = Convert.ToDouble((string)wall.Element("bc"));
                    wa.Dfc = Convert.ToDouble((string)wall.Element("Dfc"));


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
                    wa.Side = Enums.SideType.Horizontal;
                    wa.AnchoDeApoyo = Convert.ToDouble((string)wall.Element("AnchoDeApoyo"));
                    wa.ExcentricidadDeCarga = Convert.ToDouble((string)wall.Element("ExcentricidadDeCarga"));
                    wa.Tipo = (string)wall.Element("Tipo");
                    wa.RestringidoSuperiorInferior = Convert.ToBoolean((string)wall.Element("RestringidoSuperiorInferior"));
                    wa.LongClaroIzquierdo = Convert.ToDouble((string)wall.Element("LongClaroIzquierdo"));
                    wa.LongClaroDerecho = Convert.ToDouble((string)wall.Element("LongClaroDerecho"));
                    wa.FE = Convert.ToDouble((string)wall.Element("FE"));
                    wa.FactorAlturaEfectiva = Convert.ToDouble((string)wall.Element("FactorAlturaEfectiva"));
                    wa.PR = Convert.ToDouble((string)wall.Element("PR"));
                    wa.Mo = Convert.ToDouble((string)wall.Element("Mo"));
                    wa.P1X = Convert.ToDouble((string)wall.Element("P1X"));
                    wa.P1Y = Convert.ToDouble((string)wall.Element("P1Y"));
                    wa.P2X = Convert.ToDouble((string)wall.Element("P2X"));
                    wa.P2Y = Convert.ToDouble((string)wall.Element("P2Y"));
                    wa.P3X = Convert.ToDouble((string)wall.Element("P3X"));
                    wa.P3Y = Convert.ToDouble((string)wall.Element("P3Y"));
                    wa.P4X = Convert.ToDouble((string)wall.Element("P4X"));
                    wa.P4Y = Convert.ToDouble((string)wall.Element("P4Y"));
                    wa.P5X = Convert.ToDouble((string)wall.Element("P5X"));
                    wa.P5Y = Convert.ToDouble((string)wall.Element("P5Y"));
                    wa.ResMamposteriaCortante = Convert.ToDouble((string)wall.Element("ResMamposteriaCortante"));
                    wa.AceroDeRefuerzo = Convert.ToBoolean((string)wall.Element("AceroDeRefuerzo"));
                    wa.Ash = Convert.ToDouble((string)wall.Element("Ash"));
                    wa.Sh = Convert.ToDouble((string)wall.Element("Sh"));
                    wa.EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal = Convert.ToDouble((string)wall.Element("EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal"));
                    wa.ResAceroRefuerzoHorizontalCortante = Convert.ToDouble((string)wall.Element("ResAceroRefuerzoHorizontalCortante"));
                    wa.ResistenciaTotalACortante = Convert.ToDouble((string)wall.Element("ResistenciaTotalACortante"));
                    wa.ResistenciaCortanteConcreto = Convert.ToDouble((string)wall.Element("ResistenciaCortanteConcreto"));
                    wa.Conclusion = (string)wall.Element("Conclusion");
                    wa.As = Convert.ToDouble((string)wall.Element("As"));
                    wa.bc = Convert.ToDouble((string)wall.Element("bc"));
                    wa.Dfc = Convert.ToDouble((string)wall.Element("Dfc"));



                    StoreysResult.Find(x => x.StoreyNumber == Convert.ToInt32((string)storey.Attribute("level").Value)).
                        VerticalWalls.Add(wa);
                }

            }

            return StoreysResult;
        }

        public bool Update(XMLStructuralDesignData model)
        {
            bool result = true;
            try
            {
                XDocument doc = XDocument.Load(model.DocumentPath);
                List<XElement> storeysH = new List<XElement>();
                List<XElement> storeysV = new List<XElement>();

                storeysH = doc.Root.Elements("HorizontalWalls")
                     .SelectMany(x => x.Elements("Storey"))
                     .ToList();

                storeysV = doc.Root.Elements("VerticalWalls")
                 .SelectMany(x => x.Elements("Storey"))
                 .ToList();


                if (storeysH.Count <= 0)
                    return false;
                if (storeysV.Count <= 0)
                    return false;


                foreach (var storey in storeysH)
                {
                    var stTemp = model.Storeys.Single(x => x.StoreyNumber == Convert.ToInt32((string)storey.Attribute("level").Value));

                    storey.SetAttributeValue("EsfuerzoNormalPromedio", stTemp.EsfuerzoNormalPromedio);
                    storey.SetAttributeValue("CortanteResistenteEntrepisoMamposteria", stTemp.CortanteResistenteEntrepisoMamposteria);
                    storey.SetAttributeValue("CortanteResistenteEntrepisoConcreto", stTemp.CortanteResistenteEntrepisoConcreto);
                    storey.SetAttributeValue("CortanteEntrepisoTotal", stTemp.CortanteEntrepisoTotal);
                    storey.SetAttributeValue("ConclusionX", stTemp.ConclusionX);
                    storey.SetAttributeValue("ConclusionY", stTemp.ConclusionY);

                    foreach (var wall in storey.Elements("Wall"))
                    {

                        var wallTemp = stTemp.HorizontalWalls.Single(x => x.WallNumber == Convert.ToInt32((string)wall.Element("Number")));

                        wall.SetElementValue("AnchoDeApoyo", wallTemp.AnchoDeApoyo.ToString());
                        wall.SetElementValue("ExcentricidadDeCarga", wallTemp.ExcentricidadDeCarga.ToString());
                        wall.SetElementValue("Tipo", wallTemp.Tipo);
                        wall.SetElementValue("RestringidoSuperiorInferior", wallTemp.RestringidoSuperiorInferior.ToString());
                        wall.SetElementValue("LongClaroIzquierdo", wallTemp.LongClaroIzquierdo.ToString());
                        wall.SetElementValue("LongClaroDerecho", wallTemp.LongClaroDerecho.ToString());
                        wall.SetElementValue("FE", wallTemp.FE.ToString());
                        wall.SetElementValue("FactorAlturaEfectiva", wallTemp.FactorAlturaEfectiva.ToString());
                        wall.SetElementValue("PR", wallTemp.PR.ToString());
                        wall.SetElementValue("Mo", wallTemp.Mo.ToString());
                        wall.SetElementValue("P1X", wallTemp.P1X.ToString());
                        wall.SetElementValue("P1Y", wallTemp.P1Y.ToString());
                        wall.SetElementValue("P2X", wallTemp.P2X.ToString());
                        wall.SetElementValue("P2Y", wallTemp.P2Y.ToString());
                        wall.SetElementValue("P3X", wallTemp.P3X.ToString());
                        wall.SetElementValue("P3Y", wallTemp.P3Y.ToString());
                        wall.SetElementValue("P4X", wallTemp.P4X.ToString());
                        wall.SetElementValue("P4Y", wallTemp.P4Y.ToString());
                        wall.SetElementValue("P5X", wallTemp.P5X.ToString());
                        wall.SetElementValue("P5Y", wallTemp.P5Y.ToString());
                        wall.SetElementValue("ResMamposteriaCortante", wallTemp.ResMamposteriaCortante.ToString());
                        wall.SetElementValue("AceroDeRefuerzo", wallTemp.AceroDeRefuerzo.ToString());
                        wall.SetElementValue("Ash", wallTemp.Ash.ToString());
                        wall.SetElementValue("Sh", wallTemp.Sh.ToString());
                        wall.SetElementValue("EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal", wallTemp.EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal > 0 ? wallTemp.EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal.ToString() : "6000");
                        wall.SetElementValue("ResAceroRefuerzoHorizontalCortante", wallTemp.ResAceroRefuerzoHorizontalCortante.ToString());
                        wall.SetElementValue("ResistenciaTotalACortante", wallTemp.ResistenciaTotalACortante.ToString());
                        wall.SetElementValue("ResistenciaCortanteConcreto", wallTemp.ResistenciaCortanteConcreto.ToString());
                        wall.SetElementValue("Conclusion", wallTemp.Conclusion.ToString());
                        wall.SetElementValue("As", wallTemp.As.ToString());
                        wall.SetElementValue("bc", wallTemp.bc.ToString());

                    }

                }

                foreach (var storey in storeysV)
                {
                    var stTemp = model.Storeys.Single(x => x.StoreyNumber == Convert.ToInt32((string)storey.Attribute("level").Value));

                    storey.SetAttributeValue("EsfuerzoNormalPromedio", stTemp.EsfuerzoNormalPromedio);
                    storey.SetAttributeValue("CortanteResistenteEntrepisoMamposteria", stTemp.CortanteResistenteEntrepisoMamposteria);
                    storey.SetAttributeValue("CortanteResistenteEntrepisoConcreto", stTemp.CortanteResistenteEntrepisoConcreto);
                    storey.SetAttributeValue("CortanteEntrepisoTotal", stTemp.CortanteEntrepisoTotal);
                    storey.SetAttributeValue("ConclusionX", stTemp.ConclusionX);
                    storey.SetAttributeValue("ConclusionY", stTemp.ConclusionY);

                    foreach (var wall in storey.Elements("Wall"))
                    {
                        var wallTemp = stTemp.VerticalWalls.Single(x => x.WallNumber == Convert.ToInt32((string)wall.Element("Number")));
                        wall.SetElementValue("AnchoDeApoyo", wallTemp.AnchoDeApoyo.ToString());
                        wall.SetElementValue("ExcentricidadDeCarga", wallTemp.ExcentricidadDeCarga.ToString());
                        wall.SetElementValue("Tipo", wallTemp.Tipo);
                        wall.SetElementValue("RestringidoSuperiorInferior", wallTemp.RestringidoSuperiorInferior.ToString());
                        wall.SetElementValue("LongClaroIzquierdo", wallTemp.LongClaroIzquierdo.ToString());
                        wall.SetElementValue("LongClaroDerecho", wallTemp.LongClaroDerecho.ToString());
                        wall.SetElementValue("FE", wallTemp.FE.ToString());
                        wall.SetElementValue("FactorAlturaEfectiva", wallTemp.FactorAlturaEfectiva.ToString());
                        wall.SetElementValue("PR", wallTemp.PR.ToString());
                        wall.SetElementValue("Mo", wallTemp.Mo.ToString());
                        wall.SetElementValue("P1X", wallTemp.P1X.ToString());
                        wall.SetElementValue("P1Y", wallTemp.P1Y.ToString());
                        wall.SetElementValue("P2X", wallTemp.P2X.ToString());
                        wall.SetElementValue("P2Y", wallTemp.P2Y.ToString());
                        wall.SetElementValue("P3X", wallTemp.P3X.ToString());
                        wall.SetElementValue("P3Y", wallTemp.P3Y.ToString());
                        wall.SetElementValue("P4X", wallTemp.P4X.ToString());
                        wall.SetElementValue("P4Y", wallTemp.P4Y.ToString());
                        wall.SetElementValue("P5X", wallTemp.P5X.ToString());
                        wall.SetElementValue("P5Y", wallTemp.P5Y.ToString());
                        wall.SetElementValue("ResMamposteriaCortante", wallTemp.ResMamposteriaCortante.ToString());
                        wall.SetElementValue("AceroDeRefuerzo", wallTemp.AceroDeRefuerzo.ToString());
                        wall.SetElementValue("Ash", wallTemp.Ash.ToString());
                        wall.SetElementValue("Sh", wallTemp.Sh.ToString());
                        wall.SetElementValue("EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal", wallTemp.EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal > 0 ? wallTemp.EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal.ToString() : "6000");
                        wall.SetElementValue("ResAceroRefuerzoHorizontalCortante", wallTemp.ResAceroRefuerzoHorizontalCortante.ToString());
                        wall.SetElementValue("ResistenciaTotalACortante", wallTemp.ResistenciaTotalACortante.ToString());
                        wall.SetElementValue("ResistenciaCortanteConcreto", wallTemp.ResistenciaCortanteConcreto.ToString());
                        wall.SetElementValue("Conclusion", wallTemp.Conclusion.ToString());
                        wall.SetElementValue("As", wallTemp.As.ToString());
                        wall.SetElementValue("bc", wallTemp.bc.ToString());

                    }

                }

                doc.Save(model.DocumentPath);
            }
            catch (Exception ex)
            {
                result = false;
            }




            return result;
        }


    }//end of class
}//end of namespace
