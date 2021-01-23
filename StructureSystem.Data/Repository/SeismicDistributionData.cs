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
    public class SeismicDistributionData : ISeismicDistributionRepository
    {
        public bool Create(XMLSeismicDistributionData entity)
        {
            throw new NotImplementedException();
        }

        public XMLSeismicDistributionData Get(XMLSeismicDistributionData elementName)
        {
            throw new NotImplementedException();
        }

        public XMLSeismicDistributionData GetEntrepisos(XMLSeismicDistributionData model)
        {
            XMLSeismicDistributionData result = new XMLSeismicDistributionData();


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

        public bool Update(XMLSeismicDistributionData entity)
        {
            throw new NotImplementedException();
        }


        public Dictionary<string, double> GetStaticData(string document)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            try
            {
                XDocument doc = XDocument.Load(document);

                var root = doc.Root.Elements("GeneralData").LastOrDefault();

                bool existsObj = root.Attribute("c") != null || root.Attribute("Q") != null || root.Attribute("R") != null ? true : false;

                if (existsObj)
                {
                    result.Add("c",Convert.ToDouble((string)root.Attribute("c").Value));
                    result.Add("Q", Convert.ToDouble((string)root.Attribute("Q").Value));
                    result.Add("R", Convert.ToDouble((string)root.Attribute("R").Value));
                }

            }
            catch (Exception ex)
            {
            }

            return result;
        }



        #region Obtener valores de losa por nivel
        private void GetLosaMacizaAzotea(ref XMLSeismicDistributionData data, XElement storey)
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

        private void GetLosaMacizaEntrepiso(ref XMLSeismicDistributionData data, XElement storey)
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

        private void GetLosaNervadaAzotea(ref XMLSeismicDistributionData data, XElement storey)
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

        private void GetLosaNervadaEntrepiso(ref XMLSeismicDistributionData data, XElement storey)
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

        private void GetLosaViguetaBovedillaAzotea(ref XMLSeismicDistributionData data, XElement storey)
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

        private void GetLosaViguetaBovedillaEntrepiso(ref XMLSeismicDistributionData data, XElement storey)
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

        private void GetLosaOtroTipo(ref XMLSeismicDistributionData data, XElement storey)
        {
            data.LosaOtroTipoP = new LosaOtroTipo();
            data.LosaOtroTipoP.CargaMuerta = Convert.ToDouble((string)storey.Element("CargaMuertaTotal").Element("CargaMuertaTotal"));
            data.LosaOtroTipoP.CargaVivaMaxima = Convert.ToDouble((string)storey.Element("CargaVivaMaxima").Element("CargaVivaMaxima"));
            data.LosaOtroTipoP.CargaVivaInstantanea = Convert.ToDouble((string)storey.Element("CargaVivaInstantanea").Element("CargaVivaInstantanea"));
        }


        #endregion


    }//end of class
}//end of namespace
