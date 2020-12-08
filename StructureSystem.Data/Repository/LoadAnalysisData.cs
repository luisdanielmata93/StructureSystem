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
            bool result = true;
            try
            {
                CreateMacizaEntrepiso(model);
                CreateMacizaAzotea(model);
                CreateNervadaEntrepiso(model);
                CreateNervadaAzotea(model);
                CreateViguetaBovedillaEntrepiso(model);
                CreateViguetaBovedillaAzotea(model);
                CreateOtroTipo(model);

            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public XMLLoadAnalysisData Get(XMLLoadAnalysisData model)
        {
            XMLLoadAnalysisData result = new XMLLoadAnalysisData();


            XDocument mydoc = XDocument.Load(model.DocumentPath);

            var storey = mydoc.Root.Elements("LoadAnalysis")
               .SelectMany(x => x.Elements("Storey"))
               .ToList();

            var st = storey.Find(x => x.Attribute("level").Value == model.Storey.ToString());

            if (st == null)
                return result;


            if (st.Attribute("TipoLosa").ToString().Contains("LosaMacizaAzotea"))
                GetLosaMacizaAzotea(ref result, st);
            
            if (st.Attribute("TipoLosa").ToString().Contains("LosaMacizaEntrepiso"))
                GetLosaMacizaEntrepiso(ref result, st);
            
            if (st.Attribute("TipoLosa").ToString().Contains("LosaNervadaAzotea"))
                GetLosaNervadaAzotea(ref result, st);
            
            if (st.Attribute("TipoLosa").ToString().Contains("LosaNervadaEntrepiso"))
                GetLosaNervadaEntrepiso(ref result, st);
            
            if (st.Attribute("TipoLosa").ToString().Contains("LosaViguetaBovedillaAzotea"))
                GetLosaViguetaBovedillaAzotea(ref result, st);
            
            if (st.Attribute("TipoLosa").ToString().Contains("LosaViguetaBovedillaEntrepiso"))
                GetLosaViguetaBovedillaEntrepiso(ref result, st);
          
            if (st.Attribute("TipoLosa").ToString().Contains("LosaOtroTipo"))
                GetLosaOtroTipo(ref result, st);
          
            return result;
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

                if (st != null)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        #region Obtener valores de losa por nivel
        private void GetLosaMacizaAzotea(ref XMLLoadAnalysisData data, XElement storey)
        {
            data.LosaMacizaAzoteaP = new LosaMacizaAzotea();
            data.LosaMacizaAzoteaP.Impermeabilizante = Convert.ToDouble((string)storey.Element("Impermeabilizante").Element("Peso"));
            data.LosaMacizaAzoteaP.MorteroPV = Convert.ToDouble((string)storey.Element("Mortero").Element("PesoVolumetrico"));
            data.LosaMacizaAzoteaP.MorteroES = Convert.ToDouble((string)storey.Element("Mortero").Element("Espesor"));
            data.LosaMacizaAzoteaP.MorteroPE = Convert.ToDouble((string)storey.Element("Mortero").Element("Peso"));
            data.LosaMacizaAzoteaP.RellenoPV = Convert.ToDouble((string)storey.Element("Relleno").Element("PesoVolumetrico"));
            data.LosaMacizaAzoteaP.RellenoES = Convert.ToDouble((string)storey.Element("Relleno").Element("Espesor"));
            data.LosaMacizaAzoteaP.RellenoPE = Convert.ToDouble((string)storey.Element("Relleno").Element("Peso"));
            data.LosaMacizaAzoteaP.LosaPV = Convert.ToDouble((string)storey.Element("Losa").Element("PesoVolumetrico"));
            data.LosaMacizaAzoteaP.LosaES = Convert.ToDouble((string)storey.Element("Losa").Element("Espesor"));
            data.LosaMacizaAzoteaP.LosaPE = Convert.ToDouble((string)storey.Element("Losa").Element("Peso"));
            data.LosaMacizaAzoteaP.AplanadoPV = Convert.ToDouble((string)storey.Element("Aplanado").Element("PesoVolumetrico"));
            data.LosaMacizaAzoteaP.AplanadoES = Convert.ToDouble((string)storey.Element("Aplanado").Element("Espesor"));
            data.LosaMacizaAzoteaP.AplanadoPE = Convert.ToDouble((string)storey.Element("Aplanado").Element("Peso"));
            data.LosaMacizaAzoteaP.Instalaciones = Convert.ToDouble((string)storey.Element("Instalaciones").Element("Peso"));
            data.LosaMacizaAzoteaP.CargaAdicionalPorColado = Convert.ToDouble((string)storey.Element("CargaAdicionalPorColado").Element("Peso"));
            data.LosaMacizaAzoteaP.CargaAdicionalPorMortero = Convert.ToDouble((string)storey.Element("CargaAdicionalPorMortero").Element("Peso"));
            data.LosaMacizaAzoteaP.CargaMuerta = Convert.ToDouble((string)storey.Element("CargaMuertaTotal").Element("CargaMuertaTotal"));
            data.LosaMacizaAzoteaP.CargaVivaMaxima = Convert.ToDouble((string)storey.Element("CargaVivaMaxima").Element("CargaVivaMaxima"));
            data.LosaMacizaAzoteaP.CargaVivaInstantanea = Convert.ToDouble((string)storey.Element("CargaVivaInstantanea").Element("CargaVivaInstantanea"));
        }

        private void GetLosaMacizaEntrepiso(ref XMLLoadAnalysisData data, XElement storey)
        {
            data.LosaMacizaEntrepisoP = new LosaMacizaEntrepiso();
            data.LosaMacizaEntrepisoP.RellenoAdicionalPV = Convert.ToDouble((string)storey.Element("RellenoAdicional").Element("PesoVolumetrico"));
            data.LosaMacizaEntrepisoP.RellenoAdicionalES = Convert.ToDouble((string)storey.Element("RellenoAdicional").Element("Espesor"));
            data.LosaMacizaEntrepisoP.RellenoAdicionalPE = Convert.ToDouble((string)storey.Element("RellenoAdicional").Element("Peso"));
            data.LosaMacizaEntrepisoP.PisoPV = Convert.ToDouble((string)storey.Element("Piso").Element("PesoVolumetrico"));
            data.LosaMacizaEntrepisoP.PisoES = Convert.ToDouble((string)storey.Element("Piso").Element("Espesor"));
            data.LosaMacizaEntrepisoP.PisoPE = Convert.ToDouble((string)storey.Element("Piso").Element("Peso"));
            data.LosaMacizaEntrepisoP.MorteroPV = Convert.ToDouble((string)storey.Element("Mortero").Element("PesoVolumetrico"));
            data.LosaMacizaEntrepisoP.MorteroES = Convert.ToDouble((string)storey.Element("Mortero").Element("Espesor"));
            data.LosaMacizaEntrepisoP.MorteroPE = Convert.ToDouble((string)storey.Element("Mortero").Element("Peso"));
            data.LosaMacizaEntrepisoP.FirmePV = Convert.ToDouble((string)storey.Element("Firme").Element("PesoVolumetrico"));
            data.LosaMacizaEntrepisoP.FirmeES = Convert.ToDouble((string)storey.Element("Firme").Element("Espesor"));
            data.LosaMacizaEntrepisoP.FirmePE = Convert.ToDouble((string)storey.Element("Firme").Element("Peso"));
            data.LosaMacizaEntrepisoP.LosaPV = Convert.ToDouble((string)storey.Element("Losa").Element("PesoVolumetrico"));
            data.LosaMacizaEntrepisoP.LosaES = Convert.ToDouble((string)storey.Element("Losa").Element("Espesor"));
            data.LosaMacizaEntrepisoP.LosaPE = Convert.ToDouble((string)storey.Element("Losa").Element("Peso"));
            data.LosaMacizaEntrepisoP.AplanadoPV = Convert.ToDouble((string)storey.Element("Aplanado").Element("PesoVolumetrico"));
            data.LosaMacizaEntrepisoP.AplanadoES = Convert.ToDouble((string)storey.Element("Aplanado").Element("Espesor"));
            data.LosaMacizaEntrepisoP.AplanadoPE = Convert.ToDouble((string)storey.Element("Aplanado").Element("Peso"));
            data.LosaMacizaEntrepisoP.Instalaciones = Convert.ToDouble((string)storey.Element("Instalaciones").Element("Peso"));
            data.LosaMacizaEntrepisoP.CargaAdicionalPorColado = Convert.ToDouble((string)storey.Element("CargaAdicionalPorColado").Element("Peso"));
            data.LosaMacizaEntrepisoP.CargaAdicionalPorMortero = Convert.ToDouble((string)storey.Element("CargaAdicionalPorMortero").Element("Peso"));
            data.LosaMacizaEntrepisoP.CargaMuerta = Convert.ToDouble((string)storey.Element("CargaMuertaTotal").Element("CargaMuertaTotal"));
            data.LosaMacizaEntrepisoP.CargaVivaMaxima = Convert.ToDouble((string)storey.Element("CargaVivaMaxima").Element("CargaVivaMaxima"));
            data.LosaMacizaEntrepisoP.CargaVivaInstantanea = Convert.ToDouble((string)storey.Element("CargaVivaInstantanea").Element("CargaVivaInstantanea"));
        }

        private void GetLosaNervadaAzotea(ref XMLLoadAnalysisData data, XElement storey)
        {
            data.LosaNervadaAzoteaP = new LosaNervadaAzotea();
            data.LosaNervadaAzoteaP.EspesorCapaCompresion = Convert.ToDouble((string)storey.Element("EspesorCapaCompresion").Element("Medida"));
            data.LosaNervadaAzoteaP.PeralteTotalDeLosa = Convert.ToDouble((string)storey.Element("PeralteTotalDeLosa").Element("Medida"));
            data.LosaNervadaAzoteaP.AnchoNervadura = Convert.ToDouble((string)storey.Element("AnchoNervadura").Element("Medida"));
            data.LosaNervadaAzoteaP.AnchoCaseton = Convert.ToDouble((string)storey.Element("AnchoCaseton").Element("Medida"));
            data.LosaNervadaAzoteaP.LongitudModulo = Convert.ToDouble((string)storey.Element("LongitudModulo").Element("Medida"));
            data.LosaNervadaAzoteaP.AreaConcreto = Convert.ToDouble((string)storey.Element("AreaConcreto").Element("Medida"));
            data.LosaNervadaAzoteaP.AreaCaseton = Convert.ToDouble((string)storey.Element("AreaCaseton").Element("Medida"));
            data.LosaNervadaAzoteaP.Impermeabilizante = Convert.ToDouble((string)storey.Element("Impermeabilizante").Element("Peso"));
            data.LosaNervadaAzoteaP.MorteroPV = Convert.ToDouble((string)storey.Element("Mortero").Element("PesoVolumetrico"));
            data.LosaNervadaAzoteaP.MorteroES = Convert.ToDouble((string)storey.Element("Mortero").Element("Espesor"));
            data.LosaNervadaAzoteaP.MorteroPE = Convert.ToDouble((string)storey.Element("Mortero").Element("Peso"));
            data.LosaNervadaAzoteaP.RellenoPV = Convert.ToDouble((string)storey.Element("Relleno").Element("PesoVolumetrico"));
            data.LosaNervadaAzoteaP.RellenoES = Convert.ToDouble((string)storey.Element("Relleno").Element("Espesor"));
            data.LosaNervadaAzoteaP.RellenoPE = Convert.ToDouble((string)storey.Element("Relleno").Element("Peso"));
            data.LosaNervadaAzoteaP.LosaPV = Convert.ToDouble((string)storey.Element("Losa").Element("PesoVolumetrico"));
            data.LosaNervadaAzoteaP.LosaPE = Convert.ToDouble((string)storey.Element("Losa").Element("Peso"));
            data.LosaNervadaAzoteaP.CasetonesPV = Convert.ToDouble((string)storey.Element("Casetones").Element("PesoVolumetrico"));
            data.LosaNervadaAzoteaP.CasetonesPE = Convert.ToDouble((string)storey.Element("Casetones").Element("Peso"));
            data.LosaNervadaAzoteaP.AplanadoPV = Convert.ToDouble((string)storey.Element("Aplanado").Element("PesoVolumetrico"));
            data.LosaNervadaAzoteaP.AplanadoES = Convert.ToDouble((string)storey.Element("Aplanado").Element("Espesor"));
            data.LosaNervadaAzoteaP.AplanadoPE = Convert.ToDouble((string)storey.Element("Aplanado").Element("Peso"));
            data.LosaNervadaAzoteaP.Instalaciones = Convert.ToDouble((string)storey.Element("Instalaciones").Element("Peso"));
            data.LosaNervadaAzoteaP.CargaAdicionalPorColado = Convert.ToDouble((string)storey.Element("CargaAdicionalPorColado").Element("Peso"));
            data.LosaNervadaAzoteaP.CargaAdicionalPorMortero = Convert.ToDouble((string)storey.Element("CargaAdicionalPorMortero").Element("Peso"));
            data.LosaNervadaAzoteaP.CargaMuerta = Convert.ToDouble((string)storey.Element("CargaMuertaTotal").Element("CargaMuertaTotal"));
            data.LosaNervadaAzoteaP.CargaVivaMaxima = Convert.ToDouble((string)storey.Element("CargaVivaMaxima").Element("CargaVivaMaxima"));
            data.LosaNervadaAzoteaP.CargaVivaInstantanea = Convert.ToDouble((string)storey.Element("CargaVivaInstantanea").Element("CargaVivaInstantanea"));
        }

        private void GetLosaNervadaEntrepiso(ref XMLLoadAnalysisData data, XElement storey)
        {
            data.LosaNervadaEntrepisoP = new LosaNervadaEntrepiso();
            data.LosaNervadaEntrepisoP.EspesorCapaCompresion = Convert.ToDouble((string)storey.Element("EspesorCapaCompresion").Element("Medida"));
            data.LosaNervadaEntrepisoP.PeralteTotalDeLosa = Convert.ToDouble((string)storey.Element("PeralteTotalDeLosa").Element("Medida"));
            data.LosaNervadaEntrepisoP.AnchoNervadura = Convert.ToDouble((string)storey.Element("AnchoNervadura").Element("Medida"));
            data.LosaNervadaEntrepisoP.AnchoCaseton = Convert.ToDouble((string)storey.Element("AnchoCaseton").Element("Medida"));
            data.LosaNervadaEntrepisoP.LongitudModulo = Convert.ToDouble((string)storey.Element("LongitudModulo").Element("Medida"));
            data.LosaNervadaEntrepisoP.AreaConcreto = Convert.ToDouble((string)storey.Element("AreaConcreto").Element("Medida"));
            data.LosaNervadaEntrepisoP.AreaCaseton = Convert.ToDouble((string)storey.Element("AreaCaseton").Element("Medida"));
            data.LosaNervadaEntrepisoP.RellenoAdicionalPV = Convert.ToDouble((string)storey.Element("RellenoAdicional").Element("PesoVolumetrico"));
            data.LosaNervadaEntrepisoP.RellenoAdicionalES = Convert.ToDouble((string)storey.Element("RellenoAdicional").Element("Espesor"));
            data.LosaNervadaEntrepisoP.RellenoAdicionalPE = Convert.ToDouble((string)storey.Element("RellenoAdicional").Element("Peso"));
            data.LosaNervadaEntrepisoP.PisoPV = Convert.ToDouble((string)storey.Element("Piso").Element("PesoVolumetrico"));
            data.LosaNervadaEntrepisoP.PisoES = Convert.ToDouble((string)storey.Element("Piso").Element("Espesor"));
            data.LosaNervadaEntrepisoP.PisoPE = Convert.ToDouble((string)storey.Element("Piso").Element("Peso"));
            data.LosaNervadaEntrepisoP.MorteroPV = Convert.ToDouble((string)storey.Element("Mortero").Element("PesoVolumetrico"));
            data.LosaNervadaEntrepisoP.MorteroES = Convert.ToDouble((string)storey.Element("Mortero").Element("Espesor"));
            data.LosaNervadaEntrepisoP.MorteroPE = Convert.ToDouble((string)storey.Element("Mortero").Element("Peso"));
            data.LosaNervadaEntrepisoP.FirmePV = Convert.ToDouble((string)storey.Element("Firme").Element("PesoVolumetrico"));
            data.LosaNervadaEntrepisoP.FirmeES = Convert.ToDouble((string)storey.Element("Firme").Element("Espesor"));
            data.LosaNervadaEntrepisoP.FirmePE = Convert.ToDouble((string)storey.Element("Firme").Element("Peso"));
            data.LosaNervadaEntrepisoP.LosaPV = Convert.ToDouble((string)storey.Element("Losa").Element("PesoVolumetrico"));
            data.LosaNervadaEntrepisoP.LosaPE = Convert.ToDouble((string)storey.Element("Losa").Element("Peso"));
            data.LosaNervadaEntrepisoP.CasetonesPV = Convert.ToDouble((string)storey.Element("Casetones").Element("PesoVolumetrico"));
            data.LosaNervadaEntrepisoP.CasetonesPE = Convert.ToDouble((string)storey.Element("Casetones").Element("Peso"));
            data.LosaNervadaEntrepisoP.AplanadoPV = Convert.ToDouble((string)storey.Element("Aplanado").Element("PesoVolumetrico"));
            data.LosaNervadaEntrepisoP.AplanadoES = Convert.ToDouble((string)storey.Element("Aplanado").Element("Espesor"));
            data.LosaNervadaEntrepisoP.AplanadoPE = Convert.ToDouble((string)storey.Element("Aplanado").Element("Peso"));
            data.LosaNervadaEntrepisoP.Instalaciones = Convert.ToDouble((string)storey.Element("Instalaciones").Element("Peso"));
            data.LosaNervadaEntrepisoP.CargaAdicionalPorColado = Convert.ToDouble((string)storey.Element("CargaAdicionalPorColado").Element("Peso"));
            data.LosaNervadaEntrepisoP.CargaAdicionalPorMortero = Convert.ToDouble((string)storey.Element("CargaAdicionalPorMortero").Element("Peso"));
            data.LosaNervadaEntrepisoP.CargaMuerta = Convert.ToDouble((string)storey.Element("CargaMuertaTotal").Element("CargaMuertaTotal"));
            data.LosaNervadaEntrepisoP.CargaVivaMaxima = Convert.ToDouble((string)storey.Element("CargaVivaMaxima").Element("CargaVivaMaxima"));
            data.LosaNervadaEntrepisoP.CargaVivaInstantanea = Convert.ToDouble((string)storey.Element("CargaVivaInstantanea").Element("CargaVivaInstantanea"));
        }

        private void GetLosaViguetaBovedillaAzotea(ref XMLLoadAnalysisData data, XElement storey)
        {
            data.LosaViguetaBovedillaAzoteaP = new LosaViguetaBovedillaAzotea();
            data.LosaViguetaBovedillaAzoteaP.EspesorCapaCompresion = Convert.ToDouble((string)storey.Element("EspesorCapaCompresion").Element("Medida"));
            data.LosaViguetaBovedillaAzoteaP.PeralteTotalDeLosa = Convert.ToDouble((string)storey.Element("PeralteTotalDeLosa").Element("Medida"));
            data.LosaViguetaBovedillaAzoteaP.AnchoPatin = Convert.ToDouble((string)storey.Element("AnchoPatin").Element("Medida"));
            data.LosaViguetaBovedillaAzoteaP.PeraltePatin = Convert.ToDouble((string)storey.Element("PeraltePatin").Element("Medida"));
            data.LosaViguetaBovedillaAzoteaP.AnchoBovedilla = Convert.ToDouble((string)storey.Element("AnchoBovedilla").Element("Medida"));
            data.LosaViguetaBovedillaAzoteaP.AnchoVigueta = Convert.ToDouble((string)storey.Element("AnchoVigueta").Element("Medida"));
            data.LosaViguetaBovedillaAzoteaP.LongitudModulo = Convert.ToDouble((string)storey.Element("LongitudModulo").Element("Medida"));
            data.LosaViguetaBovedillaAzoteaP.AreaConcreto = Convert.ToDouble((string)storey.Element("AreaConcreto").Element("Medida"));
            data.LosaViguetaBovedillaAzoteaP.AreaBovedilla = Convert.ToDouble((string)storey.Element("AreaBovedilla").Element("Medida"));
            data.LosaViguetaBovedillaAzoteaP.Impermeabilizante = Convert.ToDouble((string)storey.Element("Impermeabilizante").Element("Peso"));
            data.LosaViguetaBovedillaAzoteaP.MorteroPV = Convert.ToDouble((string)storey.Element("Mortero").Element("PesoVolumetrico"));
            data.LosaViguetaBovedillaAzoteaP.MorteroES = Convert.ToDouble((string)storey.Element("Mortero").Element("Espesor"));
            data.LosaViguetaBovedillaAzoteaP.MorteroPE = Convert.ToDouble((string)storey.Element("Mortero").Element("Peso"));
            data.LosaViguetaBovedillaAzoteaP.FirmePV = Convert.ToDouble((string)storey.Element("Firme").Element("PesoVolumetrico"));
            data.LosaViguetaBovedillaAzoteaP.FirmeES = Convert.ToDouble((string)storey.Element("Firme").Element("Espesor"));
            data.LosaViguetaBovedillaAzoteaP.FirmePE = Convert.ToDouble((string)storey.Element("Firme").Element("Peso"));
            data.LosaViguetaBovedillaAzoteaP.LosaPV = Convert.ToDouble((string)storey.Element("Losa").Element("PesoVolumetrico"));
            data.LosaViguetaBovedillaAzoteaP.LosaPE = Convert.ToDouble((string)storey.Element("Losa").Element("Peso"));
            data.LosaViguetaBovedillaAzoteaP.BovedillasPV = Convert.ToDouble((string)storey.Element("Bovedillas").Element("PesoVolumetrico"));
            data.LosaViguetaBovedillaAzoteaP.BovedillasPE = Convert.ToDouble((string)storey.Element("Bovedillas").Element("Peso"));
            data.LosaViguetaBovedillaAzoteaP.AplanadoPV = Convert.ToDouble((string)storey.Element("Aplanado").Element("PesoVolumetrico"));
            data.LosaViguetaBovedillaAzoteaP.AplanadoES = Convert.ToDouble((string)storey.Element("Aplanado").Element("Espesor"));
            data.LosaViguetaBovedillaAzoteaP.AplanadoPE = Convert.ToDouble((string)storey.Element("Aplanado").Element("Peso"));
            data.LosaViguetaBovedillaAzoteaP.Instalaciones = Convert.ToDouble((string)storey.Element("Instalaciones").Element("Peso"));
            data.LosaViguetaBovedillaAzoteaP.CargaAdicionalPorColado = Convert.ToDouble((string)storey.Element("CargaAdicionalPorColado").Element("Peso"));
            data.LosaViguetaBovedillaAzoteaP.CargaAdicionalPorMortero = Convert.ToDouble((string)storey.Element("CargaAdicionalPorMortero").Element("Peso"));
            data.LosaViguetaBovedillaAzoteaP.CargaMuerta = Convert.ToDouble((string)storey.Element("CargaMuertaTotal").Element("CargaMuertaTotal"));
            data.LosaViguetaBovedillaAzoteaP.CargaVivaMaxima = Convert.ToDouble((string)storey.Element("CargaVivaMaxima").Element("CargaVivaMaxima"));
            data.LosaViguetaBovedillaAzoteaP.CargaVivaInstantanea = Convert.ToDouble((string)storey.Element("CargaVivaInstantanea").Element("CargaVivaInstantanea"));
        }

        private void GetLosaViguetaBovedillaEntrepiso(ref XMLLoadAnalysisData data, XElement storey)
        {
            data.LosaViguetaBovedillaEntrepisoP = new LosaViguetaBovedillaEntrepiso();
            data.LosaViguetaBovedillaEntrepisoP.EspesorCapaCompresion = Convert.ToDouble((string)storey.Element("EspesorCapaCompresion").Element("Medida"));
            data.LosaViguetaBovedillaEntrepisoP.PeralteTotalDeLosa = Convert.ToDouble((string)storey.Element("PeralteTotalDeLosa").Element("Medida"));
            data.LosaViguetaBovedillaEntrepisoP.AnchoPatin = Convert.ToDouble((string)storey.Element("AnchoPatin").Element("Medida"));
            data.LosaViguetaBovedillaEntrepisoP.PeraltePatin = Convert.ToDouble((string)storey.Element("PeraltePatin").Element("Medida"));
            data.LosaViguetaBovedillaEntrepisoP.AnchoBovedilla = Convert.ToDouble((string)storey.Element("AnchoBovedilla").Element("Medida"));
            data.LosaViguetaBovedillaEntrepisoP.AnchoVigueta = Convert.ToDouble((string)storey.Element("AnchoVigueta").Element("Medida"));
            data.LosaViguetaBovedillaEntrepisoP.LongitudModulo = Convert.ToDouble((string)storey.Element("LongitudModulo").Element("Medida"));
            data.LosaViguetaBovedillaEntrepisoP.AreaConcreto = Convert.ToDouble((string)storey.Element("AreaConcreto").Element("Medida"));
            data.LosaViguetaBovedillaEntrepisoP.AreaBovedilla = Convert.ToDouble((string)storey.Element("AreaBovedilla").Element("Medida"));
            data.LosaViguetaBovedillaEntrepisoP.RellenoAdicionalPV = Convert.ToDouble((string)storey.Element("RellenoAdicional").Element("PesoVolumetrico"));
            data.LosaViguetaBovedillaEntrepisoP.RellenoAdicionalES = Convert.ToDouble((string)storey.Element("RellenoAdicional").Element("Espesor"));
            data.LosaViguetaBovedillaEntrepisoP.RellenoAdicionalPE = Convert.ToDouble((string)storey.Element("RellenoAdicional").Element("Peso"));
            data.LosaViguetaBovedillaEntrepisoP.PisoPV = Convert.ToDouble((string)storey.Element("Piso").Element("PesoVolumetrico"));
            data.LosaViguetaBovedillaEntrepisoP.PisoES = Convert.ToDouble((string)storey.Element("Piso").Element("Espesor"));
            data.LosaViguetaBovedillaEntrepisoP.PisoPE = Convert.ToDouble((string)storey.Element("Piso").Element("Peso"));
            data.LosaViguetaBovedillaEntrepisoP.MorteroPV = Convert.ToDouble((string)storey.Element("Mortero").Element("PesoVolumetrico"));
            data.LosaViguetaBovedillaEntrepisoP.MorteroES = Convert.ToDouble((string)storey.Element("Mortero").Element("Espesor"));
            data.LosaViguetaBovedillaEntrepisoP.MorteroPE = Convert.ToDouble((string)storey.Element("Mortero").Element("Peso"));
            data.LosaViguetaBovedillaEntrepisoP.FirmePV = Convert.ToDouble((string)storey.Element("Firme").Element("PesoVolumetrico"));
            data.LosaViguetaBovedillaEntrepisoP.FirmeES = Convert.ToDouble((string)storey.Element("Firme").Element("Espesor"));
            data.LosaViguetaBovedillaEntrepisoP.FirmePE = Convert.ToDouble((string)storey.Element("Firme").Element("Peso"));
            data.LosaViguetaBovedillaEntrepisoP.LosaPV = Convert.ToDouble((string)storey.Element("Losa").Element("PesoVolumetrico"));
            data.LosaViguetaBovedillaEntrepisoP.LosaPE = Convert.ToDouble((string)storey.Element("Losa").Element("Peso"));
            data.LosaViguetaBovedillaEntrepisoP.BovedillasPV = Convert.ToDouble((string)storey.Element("Bovedillas").Element("PesoVolumetrico"));
            data.LosaViguetaBovedillaEntrepisoP.BovedillasPE = Convert.ToDouble((string)storey.Element("Bovedillas").Element("Peso"));
            data.LosaViguetaBovedillaEntrepisoP.AplanadoPV = Convert.ToDouble((string)storey.Element("Aplanado").Element("PesoVolumetrico"));
            data.LosaViguetaBovedillaEntrepisoP.AplanadoES = Convert.ToDouble((string)storey.Element("Aplanado").Element("Espesor"));
            data.LosaViguetaBovedillaEntrepisoP.AplanadoPE = Convert.ToDouble((string)storey.Element("Aplanado").Element("Peso"));
            data.LosaViguetaBovedillaEntrepisoP.Instalaciones = Convert.ToDouble((string)storey.Element("Instalaciones").Element("Peso"));
            data.LosaViguetaBovedillaEntrepisoP.CargaAdicionalPorColado = Convert.ToDouble((string)storey.Element("CargaAdicionalPorColado").Element("Peso"));
            data.LosaViguetaBovedillaEntrepisoP.CargaAdicionalPorMortero = Convert.ToDouble((string)storey.Element("CargaAdicionalPorMortero").Element("Peso"));
            data.LosaViguetaBovedillaEntrepisoP.CargaMuerta = Convert.ToDouble((string)storey.Element("CargaMuertaTotal").Element("CargaMuertaTotal"));
            data.LosaViguetaBovedillaEntrepisoP.CargaVivaMaxima = Convert.ToDouble((string)storey.Element("CargaVivaMaxima").Element("CargaVivaMaxima"));
            data.LosaViguetaBovedillaEntrepisoP.CargaVivaInstantanea = Convert.ToDouble((string)storey.Element("CargaVivaInstantanea").Element("CargaVivaInstantanea"));

        }

        private void GetLosaOtroTipo(ref XMLLoadAnalysisData data, XElement storey)
        {
            data.LosaOtroTipoP = new LosaOtroTipo();
            data.LosaOtroTipoP.CargaMuerta = Convert.ToDouble((string)storey.Element("CargaMuertaTotal").Element("CargaMuertaTotal"));
            data.LosaOtroTipoP.CargaVivaMaxima = Convert.ToDouble((string)storey.Element("CargaVivaMaxima").Element("CargaVivaMaxima"));
            data.LosaOtroTipoP.CargaVivaInstantanea = Convert.ToDouble((string)storey.Element("CargaVivaInstantanea").Element("CargaVivaInstantanea"));
        }


        #endregion

        #region Creacion de entrepisos
        private void CreateMacizaEntrepiso(XMLLoadAnalysisData model)
        {

            if (model.LosaMacizaEntrepisoP == null)
                return;

            XDocument mydoc = XDocument.Load(model.DocumentPath);


            var storey = mydoc.Root.Elements("LoadAnalysis")
               .SelectMany(x => x.Elements("Storey"))
               .ToList();

            var st = storey.Find(x => x.Attribute("level").Value == model.Storey.ToString());

            if (st != null)
            {
                mydoc.Root.Elements("LoadAnalysis")
                 .SelectMany(x => x.Elements("Storey"))
                 .ToList().Find(x => x.Attribute("level").Value == model.Storey.ToString()).Remove();
            }


            mydoc.Descendants("LoadAnalysis").LastOrDefault().Add(
                   new XElement("Storey", new XAttribute("level", model.Storey.ToString()), new XAttribute("TipoLosa", model.LosaMacizaEntrepisoP.GetType().ToString()),
                                             new XElement("RellenoAdicional",
                                                            new XElement("PesoVolumetrico", model.LosaMacizaEntrepisoP.RellenoAdicionalPV.ToString()),
                                                            new XElement("Espesor", model.LosaMacizaEntrepisoP.RellenoAdicionalES.ToString()),
                                                            new XElement("Peso", model.LosaMacizaEntrepisoP.RellenoAdicionalPE.ToString())),
                                             new XElement("Piso",
                                                            new XElement("PesoVolumetrico", model.LosaMacizaEntrepisoP.PisoPV.ToString()),
                                                            new XElement("Espesor", model.LosaMacizaEntrepisoP.PisoES.ToString()),
                                                            new XElement("Peso", model.LosaMacizaEntrepisoP.PisoPE.ToString())),
                                              new XElement("Mortero",
                                                            new XElement("PesoVolumetrico", model.LosaMacizaEntrepisoP.MorteroPV.ToString()),
                                                            new XElement("Espesor", model.LosaMacizaEntrepisoP.MorteroES.ToString()),
                                                            new XElement("Peso", model.LosaMacizaEntrepisoP.MorteroPE.ToString())),
                                               new XElement("Firme",
                                                            new XElement("PesoVolumetrico", model.LosaMacizaEntrepisoP.FirmePV.ToString()),
                                                            new XElement("Espesor", model.LosaMacizaEntrepisoP.FirmeES.ToString()),
                                                            new XElement("Peso", model.LosaMacizaEntrepisoP.FirmePE.ToString())),
                                                new XElement("Losa",
                                                            new XElement("PesoVolumetrico", model.LosaMacizaEntrepisoP.LosaPV.ToString()),
                                                            new XElement("Espesor", model.LosaMacizaEntrepisoP.LosaES.ToString()),
                                                            new XElement("Peso", model.LosaMacizaEntrepisoP.LosaPE.ToString())),
                                                 new XElement("Aplanado",
                                                            new XElement("PesoVolumetrico", model.LosaMacizaEntrepisoP.AplanadoPV.ToString()),
                                                            new XElement("Espesor", model.LosaMacizaEntrepisoP.AplanadoES.ToString()),
                                                            new XElement("Peso", model.LosaMacizaEntrepisoP.AplanadoPE.ToString())),
                                                  new XElement("Instalaciones",
                                                            new XElement("Peso", model.LosaMacizaEntrepisoP.Instalaciones.ToString())),
                                                   new XElement("CargaAdicionalPorColado",
                                                            new XElement("Peso", model.LosaMacizaEntrepisoP.CargaAdicionalPorColado.ToString())),
                                                    new XElement("CargaAdicionalPorMortero",
                                                            new XElement("Peso", model.LosaMacizaEntrepisoP.CargaAdicionalPorMortero.ToString())),
                                                     new XElement("CargaMuertaTotal",
                                                            new XElement("CargaMuertaTotal", model.LosaMacizaEntrepisoP.CargaMuerta.ToString())),
                                                      new XElement("CargaVivaMaxima",
                                                            new XElement("CargaVivaMaxima", model.LosaMacizaEntrepisoP.CargaVivaMaxima.ToString())),
                                                         new XElement("CargaVivaInstantanea",
                                                            new XElement("CargaVivaInstantanea", model.LosaMacizaEntrepisoP.CargaVivaInstantanea.ToString()))
                                                    )

                   );



            mydoc.Save(model.DocumentPath);


        }

        private void CreateMacizaAzotea(XMLLoadAnalysisData model)
        {
            if (model.LosaMacizaAzoteaP == null)
                return;

            XDocument mydoc = XDocument.Load(model.DocumentPath);


            var storey = mydoc.Root.Elements("LoadAnalysis")
               .SelectMany(x => x.Elements("Storey"))
               .ToList();

            var st = storey.Find(x => x.Attribute("level").Value == model.Storey.ToString());

            if (st != null)
            {
                mydoc.Root.Elements("LoadAnalysis")
                 .SelectMany(x => x.Elements("Storey"))
                 .ToList().Find(x => x.Attribute("level").Value == model.Storey.ToString()).Remove();
            }


            mydoc.Descendants("LoadAnalysis").LastOrDefault().Add(
             new XElement("Storey", new XAttribute("level", model.Storey.ToString()), new XAttribute("TipoLosa", model.LosaMacizaAzoteaP.GetType().ToString()),
                                       new XElement("Impermeabilizante",
                                                      new XElement("Peso", model.LosaMacizaAzoteaP.Impermeabilizante.ToString())),
                                        new XElement("Mortero",
                                                      new XElement("PesoVolumetrico", model.LosaMacizaAzoteaP.MorteroPV.ToString()),
                                                      new XElement("Espesor", model.LosaMacizaAzoteaP.MorteroES.ToString()),
                                                      new XElement("Peso", model.LosaMacizaAzoteaP.MorteroPE.ToString())),
                                         new XElement("Relleno",
                                                      new XElement("PesoVolumetrico", model.LosaMacizaAzoteaP.RellenoPV.ToString()),
                                                      new XElement("Espesor", model.LosaMacizaAzoteaP.RellenoES.ToString()),
                                                      new XElement("Peso", model.LosaMacizaAzoteaP.RellenoPE.ToString())),
                                          new XElement("Losa",
                                                      new XElement("PesoVolumetrico", model.LosaMacizaAzoteaP.LosaPV.ToString()),
                                                      new XElement("Espesor", model.LosaMacizaAzoteaP.LosaES.ToString()),
                                                      new XElement("Peso", model.LosaMacizaAzoteaP.LosaPE.ToString())),
                                           new XElement("Aplanado",
                                                      new XElement("PesoVolumetrico", model.LosaMacizaAzoteaP.AplanadoPV.ToString()),
                                                      new XElement("Espesor", model.LosaMacizaAzoteaP.AplanadoES.ToString()),
                                                      new XElement("Peso", model.LosaMacizaAzoteaP.AplanadoPE.ToString())),
                                            new XElement("Instalaciones",
                                                      new XElement("Peso", model.LosaMacizaAzoteaP.Instalaciones.ToString())),
                                             new XElement("CargaAdicionalPorColado",
                                                      new XElement("Peso", model.LosaMacizaAzoteaP.CargaAdicionalPorColado.ToString())),
                                              new XElement("CargaAdicionalPorMortero",
                                                      new XElement("Peso", model.LosaMacizaAzoteaP.CargaAdicionalPorMortero.ToString())),
                                               new XElement("CargaMuertaTotal",
                                                      new XElement("CargaMuertaTotal", model.LosaMacizaAzoteaP.CargaMuerta.ToString())),
                                                new XElement("CargaVivaMaxima",
                                                      new XElement("CargaVivaMaxima", model.LosaMacizaAzoteaP.CargaVivaMaxima.ToString())),
                                                   new XElement("CargaVivaInstantanea",
                                                      new XElement("CargaVivaInstantanea", model.LosaMacizaAzoteaP.CargaVivaInstantanea.ToString()))
                                              )
             );




            mydoc.Save(model.DocumentPath);

        }

        private void CreateNervadaEntrepiso(XMLLoadAnalysisData model)
        {
            if (model.LosaNervadaEntrepisoP == null)
                return;

            XDocument mydoc = XDocument.Load(model.DocumentPath);


            var storey = mydoc.Root.Elements("LoadAnalysis")
               .SelectMany(x => x.Elements("Storey"))
               .ToList();

            var st = storey.Find(x => x.Attribute("level").Value == model.Storey.ToString());

            if (st != null)
            {
                mydoc.Root.Elements("LoadAnalysis")
                 .SelectMany(x => x.Elements("Storey"))
                 .ToList().Find(x => x.Attribute("level").Value == model.Storey.ToString()).Remove();
            }


            mydoc.Descendants("LoadAnalysis").LastOrDefault().Add(
                   new XElement("Storey", new XAttribute("level", model.Storey.ToString()), new XAttribute("TipoLosa", model.LosaNervadaEntrepisoP.GetType().ToString()),
                                            new XElement("EspesorCapaCompresion",
                                                            new XElement("Medida", model.LosaNervadaEntrepisoP.EspesorCapaCompresion.ToString())),
                                             new XElement("PeralteTotalDeLosa",
                                                            new XElement("Medida", model.LosaNervadaEntrepisoP.PeralteTotalDeLosa.ToString())),
                                             new XElement("AnchoNervadura",
                                                            new XElement("Medida", model.LosaNervadaEntrepisoP.AnchoNervadura.ToString())),
                                             new XElement("AnchoCaseton",
                                                            new XElement("Medida", model.LosaNervadaEntrepisoP.AnchoCaseton.ToString())),
                                             new XElement("LongitudModulo",
                                                            new XElement("Medida", model.LosaNervadaEntrepisoP.LongitudModulo.ToString())),
                                             new XElement("AreaConcreto",
                                                            new XElement("Medida", model.LosaNervadaEntrepisoP.AreaConcreto.ToString())),
                                             new XElement("AreaCaseton",
                                                            new XElement("Medida", model.LosaNervadaEntrepisoP.AreaCaseton.ToString())),
                                             new XElement("RellenoAdicional",
                                                            new XElement("PesoVolumetrico", model.LosaNervadaEntrepisoP.RellenoAdicionalPV.ToString()),
                                                            new XElement("Espesor", model.LosaNervadaEntrepisoP.RellenoAdicionalES.ToString()),
                                                            new XElement("Peso", model.LosaNervadaEntrepisoP.RellenoAdicionalPE.ToString())),
                                             new XElement("Piso",
                                                            new XElement("PesoVolumetrico", model.LosaNervadaEntrepisoP.PisoPV.ToString()),
                                                            new XElement("Espesor", model.LosaNervadaEntrepisoP.PisoES.ToString()),
                                                            new XElement("Peso", model.LosaNervadaEntrepisoP.PisoPE.ToString())),
                                              new XElement("Mortero",
                                                            new XElement("PesoVolumetrico", model.LosaNervadaEntrepisoP.MorteroPV.ToString()),
                                                            new XElement("Espesor", model.LosaNervadaEntrepisoP.MorteroES.ToString()),
                                                            new XElement("Peso", model.LosaNervadaEntrepisoP.MorteroPE.ToString())),
                                               new XElement("Firme",
                                                            new XElement("PesoVolumetrico", model.LosaNervadaEntrepisoP.FirmePV.ToString()),
                                                            new XElement("Espesor", model.LosaNervadaEntrepisoP.FirmeES.ToString()),
                                                            new XElement("Peso", model.LosaNervadaEntrepisoP.FirmePE.ToString())),
                                                new XElement("Losa",
                                                            new XElement("PesoVolumetrico", model.LosaNervadaEntrepisoP.LosaPV.ToString()),
                                                            new XElement("Peso", model.LosaNervadaEntrepisoP.LosaPE.ToString())),
                                                new XElement("Casetones",
                                                            new XElement("PesoVolumetrico", model.LosaNervadaEntrepisoP.CasetonesPV.ToString()),
                                                            new XElement("Peso", model.LosaNervadaEntrepisoP.CasetonesPE.ToString())),
                                                 new XElement("Aplanado",
                                                            new XElement("PesoVolumetrico", model.LosaNervadaEntrepisoP.AplanadoPV.ToString()),
                                                            new XElement("Espesor", model.LosaNervadaEntrepisoP.AplanadoES.ToString()),
                                                            new XElement("Peso", model.LosaNervadaEntrepisoP.AplanadoPE.ToString())),
                                                  new XElement("Instalaciones",
                                                            new XElement("Peso", model.LosaNervadaEntrepisoP.Instalaciones.ToString())),
                                                   new XElement("CargaAdicionalPorColado",
                                                            new XElement("Peso", model.LosaNervadaEntrepisoP.CargaAdicionalPorColado.ToString())),
                                                    new XElement("CargaAdicionalPorMortero",
                                                            new XElement("Peso", model.LosaNervadaEntrepisoP.CargaAdicionalPorMortero.ToString())),
                                                     new XElement("CargaMuertaTotal",
                                                            new XElement("CargaMuertaTotal", model.LosaNervadaEntrepisoP.CargaMuerta.ToString())),
                                                      new XElement("CargaVivaMaxima",
                                                            new XElement("CargaVivaMaxima", model.LosaNervadaEntrepisoP.CargaVivaMaxima.ToString())),
                                                         new XElement("CargaVivaInstantanea",
                                                            new XElement("CargaVivaInstantanea", model.LosaNervadaEntrepisoP.CargaVivaInstantanea.ToString()))
                                                    )

                   );



            mydoc.Save(model.DocumentPath);

        }

        private void CreateNervadaAzotea(XMLLoadAnalysisData model)
        {
            if (model.LosaNervadaAzoteaP == null)
                return;

            XDocument mydoc = XDocument.Load(model.DocumentPath);


            var storey = mydoc.Root.Elements("LoadAnalysis")
               .SelectMany(x => x.Elements("Storey"))
               .ToList();

            var st = storey.Find(x => x.Attribute("level").Value == model.Storey.ToString());

            if (st != null)
            {
                mydoc.Root.Elements("LoadAnalysis")
                 .SelectMany(x => x.Elements("Storey"))
                 .ToList().Find(x => x.Attribute("level").Value == model.Storey.ToString()).Remove();
            }



            mydoc.Descendants("LoadAnalysis").LastOrDefault().Add(
                   new XElement("Storey", new XAttribute("level", model.Storey.ToString()), new XAttribute("TipoLosa", model.LosaNervadaAzoteaP.GetType().ToString()),
                                            new XElement("EspesorCapaCompresion",
                                                            new XElement("Medida", model.LosaNervadaAzoteaP.EspesorCapaCompresion.ToString())),
                                             new XElement("PeralteTotalDeLosa",
                                                            new XElement("Medida", model.LosaNervadaAzoteaP.PeralteTotalDeLosa.ToString())),
                                             new XElement("AnchoNervadura",
                                                            new XElement("Medida", model.LosaNervadaAzoteaP.AnchoNervadura.ToString())),
                                             new XElement("AnchoCaseton",
                                                            new XElement("Medida", model.LosaNervadaAzoteaP.AnchoCaseton.ToString())),
                                             new XElement("LongitudModulo",
                                                            new XElement("Medida", model.LosaNervadaAzoteaP.LongitudModulo.ToString())),
                                             new XElement("AreaConcreto",
                                                            new XElement("Medida", model.LosaNervadaAzoteaP.AreaConcreto.ToString())),
                                             new XElement("AreaCaseton",
                                                            new XElement("Medida", model.LosaNervadaAzoteaP.AreaCaseton.ToString())),
                                             new XElement("Impermeabilizante",
                                                            new XElement("Peso", model.LosaNervadaAzoteaP.Impermeabilizante.ToString())),
                                              new XElement("Mortero",
                                                            new XElement("PesoVolumetrico", model.LosaNervadaAzoteaP.MorteroPV.ToString()),
                                                            new XElement("Espesor", model.LosaNervadaAzoteaP.MorteroES.ToString()),
                                                            new XElement("Peso", model.LosaNervadaAzoteaP.MorteroPE.ToString())),
                                               new XElement("Relleno",
                                                            new XElement("PesoVolumetrico", model.LosaNervadaAzoteaP.RellenoPV.ToString()),
                                                            new XElement("Espesor", model.LosaNervadaAzoteaP.RellenoES.ToString()),
                                                            new XElement("Peso", model.LosaNervadaAzoteaP.RellenoPE.ToString())),
                                                new XElement("Losa",
                                                            new XElement("PesoVolumetrico", model.LosaNervadaAzoteaP.LosaPV.ToString()),
                                                            new XElement("Peso", model.LosaNervadaAzoteaP.LosaPE.ToString())),
                                                new XElement("Casetones",
                                                            new XElement("PesoVolumetrico", model.LosaNervadaAzoteaP.CasetonesPV.ToString()),
                                                            new XElement("Peso", model.LosaNervadaAzoteaP.CasetonesPE.ToString())),
                                                 new XElement("Aplanado",
                                                            new XElement("PesoVolumetrico", model.LosaNervadaAzoteaP.AplanadoPV.ToString()),
                                                            new XElement("Espesor", model.LosaNervadaAzoteaP.AplanadoES.ToString()),
                                                            new XElement("Peso", model.LosaNervadaAzoteaP.AplanadoPE.ToString())),
                                                  new XElement("Instalaciones",
                                                            new XElement("Peso", model.LosaNervadaAzoteaP.Instalaciones.ToString())),
                                                   new XElement("CargaAdicionalPorColado",
                                                            new XElement("Peso", model.LosaNervadaAzoteaP.CargaAdicionalPorColado.ToString())),
                                                    new XElement("CargaAdicionalPorMortero",
                                                            new XElement("Peso", model.LosaNervadaAzoteaP.CargaAdicionalPorMortero.ToString())),
                                                     new XElement("CargaMuertaTotal",
                                                            new XElement("CargaMuertaTotal", model.LosaNervadaAzoteaP.CargaMuerta.ToString())),
                                                      new XElement("CargaVivaMaxima",
                                                            new XElement("CargaVivaMaxima", model.LosaNervadaAzoteaP.CargaVivaMaxima.ToString())),
                                                         new XElement("CargaVivaInstantanea",
                                                            new XElement("CargaVivaInstantanea", model.LosaNervadaAzoteaP.CargaVivaInstantanea.ToString()))
                                                    )

                   );



            mydoc.Save(model.DocumentPath);

        }

        private void CreateViguetaBovedillaEntrepiso(XMLLoadAnalysisData model)
        {
            if (model.LosaViguetaBovedillaEntrepisoP == null)
                return;

            XDocument mydoc = XDocument.Load(model.DocumentPath);


            var storey = mydoc.Root.Elements("LoadAnalysis")
               .SelectMany(x => x.Elements("Storey"))
               .ToList();

            var st = storey.Find(x => x.Attribute("level").Value == model.Storey.ToString());

            if (st != null)
            {
                mydoc.Root.Elements("LoadAnalysis")
                 .SelectMany(x => x.Elements("Storey"))
                 .ToList().Find(x => x.Attribute("level").Value == model.Storey.ToString()).Remove();
            }


            mydoc.Descendants("LoadAnalysis").LastOrDefault().Add(
                   new XElement("Storey", new XAttribute("level", model.Storey.ToString()), new XAttribute("TipoLosa", model.LosaViguetaBovedillaEntrepisoP.GetType().ToString()),
                                            new XElement("EspesorCapaCompresion",
                                                            new XElement("Medida", model.LosaViguetaBovedillaEntrepisoP.EspesorCapaCompresion.ToString())),
                                             new XElement("PeralteTotalDeLosa",
                                                            new XElement("Medida", model.LosaViguetaBovedillaEntrepisoP.PeralteTotalDeLosa.ToString())),
                                             new XElement("AnchoPatin",
                                                            new XElement("Medida", model.LosaViguetaBovedillaEntrepisoP.AnchoPatin.ToString())),
                                             new XElement("PeraltePatin",
                                                            new XElement("Medida", model.LosaViguetaBovedillaEntrepisoP.PeraltePatin.ToString())),
                                               new XElement("AnchoBovedilla",
                                                            new XElement("Medida", model.LosaViguetaBovedillaEntrepisoP.AnchoBovedilla.ToString())),
                                                 new XElement("AnchoVigueta",
                                                            new XElement("Medida", model.LosaViguetaBovedillaEntrepisoP.AnchoVigueta.ToString())),
                                             new XElement("LongitudModulo",
                                                            new XElement("Medida", model.LosaViguetaBovedillaEntrepisoP.LongitudModulo.ToString())),
                                             new XElement("AreaConcreto",
                                                            new XElement("Medida", model.LosaViguetaBovedillaEntrepisoP.AreaConcreto.ToString())),
                                             new XElement("AreaBovedilla",
                                                            new XElement("Medida", model.LosaViguetaBovedillaEntrepisoP.AreaBovedilla.ToString())),

                                             new XElement("RellenoAdicional",
                                                            new XElement("PesoVolumetrico", model.LosaViguetaBovedillaEntrepisoP.RellenoAdicionalPV.ToString()),
                                                            new XElement("Espesor", model.LosaViguetaBovedillaEntrepisoP.RellenoAdicionalES.ToString()),
                                                            new XElement("Peso", model.LosaViguetaBovedillaEntrepisoP.RellenoAdicionalPE.ToString())),
                                             new XElement("Piso",
                                                            new XElement("PesoVolumetrico", model.LosaViguetaBovedillaEntrepisoP.PisoPV.ToString()),
                                                            new XElement("Espesor", model.LosaViguetaBovedillaEntrepisoP.PisoES.ToString()),
                                                            new XElement("Peso", model.LosaViguetaBovedillaEntrepisoP.PisoPE.ToString())),
                                              new XElement("Mortero",
                                                            new XElement("PesoVolumetrico", model.LosaViguetaBovedillaEntrepisoP.MorteroPV.ToString()),
                                                            new XElement("Espesor", model.LosaViguetaBovedillaEntrepisoP.MorteroES.ToString()),
                                                            new XElement("Peso", model.LosaViguetaBovedillaEntrepisoP.MorteroPE.ToString())),
                                               new XElement("Firme",
                                                            new XElement("PesoVolumetrico", model.LosaViguetaBovedillaEntrepisoP.FirmePV.ToString()),
                                                            new XElement("Espesor", model.LosaViguetaBovedillaEntrepisoP.FirmeES.ToString()),
                                                            new XElement("Peso", model.LosaViguetaBovedillaEntrepisoP.FirmePE.ToString())),
                                                new XElement("Losa",
                                                            new XElement("PesoVolumetrico", model.LosaViguetaBovedillaEntrepisoP.LosaPV.ToString()),
                                                            new XElement("Peso", model.LosaViguetaBovedillaEntrepisoP.LosaPE.ToString())),
                                                new XElement("Bovedillas",
                                                            new XElement("PesoVolumetrico", model.LosaViguetaBovedillaEntrepisoP.BovedillasPV.ToString()),
                                                            new XElement("Peso", model.LosaViguetaBovedillaEntrepisoP.BovedillasPE.ToString())),
                                                 new XElement("Aplanado",
                                                            new XElement("PesoVolumetrico", model.LosaViguetaBovedillaEntrepisoP.AplanadoPV.ToString()),
                                                            new XElement("Espesor", model.LosaViguetaBovedillaEntrepisoP.AplanadoES.ToString()),
                                                            new XElement("Peso", model.LosaViguetaBovedillaEntrepisoP.AplanadoPE.ToString())),
                                                  new XElement("Instalaciones",
                                                            new XElement("Peso", model.LosaViguetaBovedillaEntrepisoP.Instalaciones.ToString())),
                                                   new XElement("CargaAdicionalPorColado",
                                                            new XElement("Peso", model.LosaViguetaBovedillaEntrepisoP.CargaAdicionalPorColado.ToString())),
                                                    new XElement("CargaAdicionalPorMortero",
                                                            new XElement("Peso", model.LosaViguetaBovedillaEntrepisoP.CargaAdicionalPorMortero.ToString())),
                                                     new XElement("CargaMuertaTotal",
                                                            new XElement("CargaMuertaTotal", model.LosaViguetaBovedillaEntrepisoP.CargaMuerta.ToString())),
                                                      new XElement("CargaVivaMaxima",
                                                            new XElement("CargaVivaMaxima", model.LosaViguetaBovedillaEntrepisoP.CargaVivaMaxima.ToString())),
                                                         new XElement("CargaVivaInstantanea",
                                                            new XElement("CargaVivaInstantanea", model.LosaViguetaBovedillaEntrepisoP.CargaVivaInstantanea.ToString()))
                                                    )

                   );



            mydoc.Save(model.DocumentPath);

        }

        private void CreateViguetaBovedillaAzotea(XMLLoadAnalysisData model)
        {
            if (model.LosaViguetaBovedillaAzoteaP == null)
                return;

            XDocument mydoc = XDocument.Load(model.DocumentPath);


            var storey = mydoc.Root.Elements("LoadAnalysis")
               .SelectMany(x => x.Elements("Storey"))
               .ToList();

            var st = storey.Find(x => x.Attribute("level").Value == model.Storey.ToString());

            if (st != null)
            {
                mydoc.Root.Elements("LoadAnalysis")
                 .SelectMany(x => x.Elements("Storey"))
                 .ToList().Find(x => x.Attribute("level").Value == model.Storey.ToString()).Remove();
            }


            mydoc.Descendants("LoadAnalysis").LastOrDefault().Add(
                     new XElement("Storey", new XAttribute("level", model.Storey.ToString()), new XAttribute("TipoLosa", model.LosaViguetaBovedillaAzoteaP.GetType().ToString()),
                                              new XElement("EspesorCapaCompresion",
                                                              new XElement("Medida", model.LosaViguetaBovedillaAzoteaP.EspesorCapaCompresion.ToString())),
                                               new XElement("PeralteTotalDeLosa",
                                                              new XElement("Medida", model.LosaViguetaBovedillaAzoteaP.PeralteTotalDeLosa.ToString())),
                                               new XElement("AnchoPatin",
                                                              new XElement("Medida", model.LosaViguetaBovedillaAzoteaP.AnchoPatin.ToString())),
                                               new XElement("PeraltePatin",
                                                              new XElement("Medida", model.LosaViguetaBovedillaAzoteaP.PeraltePatin.ToString())),
                                                 new XElement("AnchoBovedilla",
                                                              new XElement("Medida", model.LosaViguetaBovedillaAzoteaP.AnchoBovedilla.ToString())),
                                                   new XElement("AnchoVigueta",
                                                              new XElement("Medida", model.LosaViguetaBovedillaAzoteaP.AnchoVigueta.ToString())),
                                               new XElement("LongitudModulo",
                                                              new XElement("Medida", model.LosaViguetaBovedillaAzoteaP.LongitudModulo.ToString())),
                                               new XElement("AreaConcreto",
                                                              new XElement("Medida", model.LosaViguetaBovedillaAzoteaP.AreaConcreto.ToString())),
                                               new XElement("AreaBovedilla",
                                                              new XElement("Medida", model.LosaViguetaBovedillaAzoteaP.AreaBovedilla.ToString())),

                                               new XElement("Impermeabilizante",
                                                              new XElement("Peso", model.LosaViguetaBovedillaAzoteaP.Impermeabilizante.ToString())),
                                                new XElement("Mortero",
                                                              new XElement("PesoVolumetrico", model.LosaViguetaBovedillaAzoteaP.MorteroPV.ToString()),
                                                              new XElement("Espesor", model.LosaViguetaBovedillaAzoteaP.MorteroES.ToString()),
                                                              new XElement("Peso", model.LosaViguetaBovedillaAzoteaP.MorteroPE.ToString())),
                                                 new XElement("Firme",
                                                              new XElement("PesoVolumetrico", model.LosaViguetaBovedillaAzoteaP.FirmePV.ToString()),
                                                              new XElement("Espesor", model.LosaViguetaBovedillaAzoteaP.FirmeES.ToString()),
                                                              new XElement("Peso", model.LosaViguetaBovedillaAzoteaP.FirmePE.ToString())),
                                                  new XElement("Losa",
                                                              new XElement("PesoVolumetrico", model.LosaViguetaBovedillaAzoteaP.LosaPV.ToString()),
                                                              new XElement("Peso", model.LosaViguetaBovedillaAzoteaP.LosaPE.ToString())),
                                                  new XElement("Bovedillas",
                                                              new XElement("PesoVolumetrico", model.LosaViguetaBovedillaAzoteaP.BovedillasPV.ToString()),
                                                              new XElement("Peso", model.LosaViguetaBovedillaAzoteaP.BovedillasPE.ToString())),
                                                   new XElement("Aplanado",
                                                              new XElement("PesoVolumetrico", model.LosaViguetaBovedillaAzoteaP.AplanadoPV.ToString()),
                                                              new XElement("Espesor", model.LosaViguetaBovedillaAzoteaP.AplanadoES.ToString()),
                                                              new XElement("Peso", model.LosaViguetaBovedillaAzoteaP.AplanadoPE.ToString())),
                                                    new XElement("Instalaciones",
                                                              new XElement("Peso", model.LosaViguetaBovedillaAzoteaP.Instalaciones.ToString())),
                                                     new XElement("CargaAdicionalPorColado",
                                                              new XElement("Peso", model.LosaViguetaBovedillaAzoteaP.CargaAdicionalPorColado.ToString())),
                                                      new XElement("CargaAdicionalPorMortero",
                                                              new XElement("Peso", model.LosaViguetaBovedillaAzoteaP.CargaAdicionalPorMortero.ToString())),
                                                       new XElement("CargaMuertaTotal",
                                                              new XElement("CargaMuertaTotal", model.LosaViguetaBovedillaAzoteaP.CargaMuerta.ToString())),
                                                        new XElement("CargaVivaMaxima",
                                                              new XElement("CargaVivaMaxima", model.LosaViguetaBovedillaAzoteaP.CargaVivaMaxima.ToString())),
                                                           new XElement("CargaVivaInstantanea",
                                                              new XElement("CargaVivaInstantanea", model.LosaViguetaBovedillaAzoteaP.CargaVivaInstantanea.ToString()))
                                                      )

                     );


            mydoc.Save(model.DocumentPath);

        }

        private void CreateOtroTipo(XMLLoadAnalysisData model)
        {
            if (model.LosaOtroTipoP == null)
                return;

            XDocument mydoc = XDocument.Load(model.DocumentPath);


            var storey = mydoc.Root.Elements("LoadAnalysis")
               .SelectMany(x => x.Elements("Storey"))
               .ToList();

            var st = storey.Find(x => x.Attribute("level").Value == model.Storey.ToString());

            if (st != null)
            {
                mydoc.Root.Elements("LoadAnalysis")
                 .SelectMany(x => x.Elements("Storey"))
                 .ToList().Find(x => x.Attribute("level").Value == model.Storey.ToString()).Remove();
            }


            mydoc.Descendants("LoadAnalysis").LastOrDefault().Add(
                      new XElement("Storey", new XAttribute("level", model.Storey.ToString()), new XAttribute("TipoLosa", model.LosaOtroTipoP.GetType().ToString()),
                                                  new XElement("CargaMuertaTotal",
                                                               new XElement("CargaMuertaTotal", model.LosaOtroTipoP.CargaMuerta.ToString())),
                                                  new XElement("CargaVivaMaxima",
                                                               new XElement("CargaVivaMaxima", model.LosaOtroTipoP.CargaVivaMaxima.ToString())),
                                                  new XElement("CargaVivaInstantanea",
                                                               new XElement("CargaVivaInstantanea", model.LosaOtroTipoP.CargaVivaInstantanea.ToString()))
                                                       )
                      );



            mydoc.Save(model.DocumentPath);

        }

        #endregion


    }//end of class
}//end of namespace
