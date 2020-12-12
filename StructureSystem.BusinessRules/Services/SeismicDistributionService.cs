using System;
using System.Collections.Generic;
using System.Linq;
using StructureSystem.Model;
using StructureSystem.Data;
using StructureSystem.BusinessRules.Configuration;
using System.IO;
using StructureSystem.BusinessRules.Functions;
using System.Threading.Tasks;
using System.Data;

namespace StructureSystem.BusinessRules.Services
{
   public class SeismicDistributionService
    {

        public SeismicDistributionService()
        {
            this.configData = new WebConfig();
            this.MaterialCollection = this.configData.GetMaterialsCollection();
        }


        public Structure GetStructure()
        {
            Structure structure = new Structure();
            try
            {
                var document = GetDocumentPath();

                using (var data = UnitOfWork.Create())
                {
                    structure.Storeys = (List<Storey>)data.Repositories.DocumentDataContext.SeismicAnalysisData.Get(document);

                }
                CalculateInitialData(ref structure);
            }
            catch (Exception ex)
            {
            }
            return structure;

        }

        private void CalculateInitialData(ref Structure Structure)
        {
            try
            {
                for (int i = 0; i < Structure.Storeys.Count; i++)
                {
                    foreach (var wall in Structure.Storeys[i].HorizontalWalls)
                    {
                        wall.Inercia = Functions.Operations.CalcularInercia(wall);
                        wall.AreaLongitudinal = Functions.Operations.CalcularAreaLongitudinal(wall);
                        wall.RigidezLateral = Functions.Operations.CalcularRigidezLateral(wall, this.MaterialCollection.Single(x => x.Name == wall.Material));
                        
                        wall.CortanteDirecto = Functions.Operations.CalcularFuerzasCortantesDirectas(wall.Side);
                        wall.Clasificacion = Functions.Operations.CalcularClasificacionMuro(wall.Side);
                        wall.ExcentricidadDisenioX = Functions.Operations.CalcularExcentricidadesDisenio(Enums.Coordenate.X, wall.Side);
                        wall.ExcentricidadDisenioY = Functions.Operations.CalcularExcentricidadesDisenio(Enums.Coordenate.Y, wall.Side);
                        wall.CortantePorTorsion = Functions.Operations.CalcularCortanteTorsion(wall.Side);
                        wall.CortanteTotales = Functions.Operations.CalcularCortantesTotales(wall.Side);
                        wall.CargaAxialMaxima = Functions.Operations.CalcularCargaAxialUltima(wall.Side);
                        wall.CargaAxialSismo = Functions.Operations.CalcularCargaAxialDeSismo(wall.Side);
                    }
                    foreach (var wall in Structure.Storeys[i].VerticalWalls)
                    {
                        wall.Inercia = Functions.Operations.CalcularInercia(wall);
                        wall.AreaLongitudinal = Functions.Operations.CalcularAreaLongitudinal(wall);
                        wall.RigidezLateral = Functions.Operations.CalcularRigidezLateral(wall, this.MaterialCollection.Single(x => x.Name == wall.Material));

                        wall.CortanteDirecto = Functions.Operations.CalcularFuerzasCortantesDirectas(wall.Side);
                        wall.Clasificacion = Functions.Operations.CalcularClasificacionMuro(wall.Side);
                        wall.ExcentricidadDisenioX = Functions.Operations.CalcularExcentricidadesDisenio(Enums.Coordenate.X, wall.Side);
                        wall.ExcentricidadDisenioY = Functions.Operations.CalcularExcentricidadesDisenio(Enums.Coordenate.Y, wall.Side);
                        wall.CortantePorTorsion = Functions.Operations.CalcularCortanteTorsion(wall.Side);
                        wall.CortanteTotales = Functions.Operations.CalcularCortantesTotales(wall.Side);
                        wall.CargaAxialMaxima = Functions.Operations.CalcularCargaAxialUltima(wall.Side);
                        wall.CargaAxialSismo = Functions.Operations.CalcularCargaAxialDeSismo(wall.Side);
                    }

                    Structure.Storeys[i].CentroCortantesX = Functions.Operations.CalcularCentroCortante(Structure.Storeys[i], Enums.Coordenate.X);
                    Structure.Storeys[i].CentroCortantesY = Functions.Operations.CalcularCentroCortante(Structure.Storeys[i], Enums.Coordenate.Y);
                    Structure.Storeys[i].CentroTorsionX = Functions.Operations.CalcularCentroTorsion(Structure.Storeys[i], Enums.Coordenate.X);
                    Structure.Storeys[i].CentroTorsionY = Functions.Operations.CalcularCentroTorsion(Structure.Storeys[i], Enums.Coordenate.Y);
                    Structure.Storeys[i].ExcentricidadesEstaticasX = Functions.Operations.CalcularExcentricidadesEstaticas(Structure.Storeys[i], Enums.Coordenate.X);
                    Structure.Storeys[i].ExcentricidadesEstaticasY = Functions.Operations.CalcularExcentricidadesEstaticas(Structure.Storeys[i], Enums.Coordenate.Y);
                    Structure.Storeys[i].ExcentricidadesAccidentalesX = Functions.Operations.CalcularExcentricidadesAccidentales(Structure.Storeys[i], Enums.Coordenate.X);
                    Structure.Storeys[i].ExcentricidadesAccidentalesY = Functions.Operations.CalcularExcentricidadesAccidentales(Structure.Storeys[i], Enums.Coordenate.Y);
                    Structure.Storeys[i].ExcentricidadesDisenioEntrepisoX = Functions.Operations.CalcularExcentricidadDisenioEntrepiso(Structure.Storeys[i], Enums.Coordenate.X);
                    Structure.Storeys[i].ExcentricidadesDisenioEntrepisoY = Functions.Operations.CalcularExcentricidadDisenioEntrepiso(Structure.Storeys[i], Enums.Coordenate.Y);
                    Structure.Storeys[i].MomentosTorsionantesX = Functions.Operations.CalcularMomentosTorsionantes(Structure.Storeys[i], Enums.Coordenate.X);
                    Structure.Storeys[i].MomentosTorsionantesY = Functions.Operations.CalcularMomentosTorsionantes(Structure.Storeys[i], Enums.Coordenate.Y);
                    Structure.Storeys[i].RigidezTorsionalEntrepiso = Functions.Operations.CalcularRigidezTorsionalEntrepiso(Structure.Storeys[i]);
                    Structure.Storeys[i].CentroMasasX = Functions.Operations.CalcularCentroDeMasas(Structure.Storeys[i], Enums.Coordenate.X);
                    Structure.Storeys[i].CentroMasasY = Functions.Operations.CalcularCentroDeMasas(Structure.Storeys[i], Enums.Coordenate.Y);

                    //Structure.Storeys[i].MomentoVolteoEntrepiso = Functions.Operations.CalcularMomentoVolteo(Structure.Storeys[i]);

                }

            }
            catch (Exception ex)
            {

            }
        }

        private XMLLoadAnalysisData GetEntrepisoPorNivel(int Storey)
        {
            XMLLoadAnalysisData result = new XMLLoadAnalysisData();

            try
            {
                XMLLoadAnalysisData DocumentData = new XMLLoadAnalysisData();
                DocumentData.DocumentPath = GetDocumentPath();
                DocumentData.Storey = Storey;

                using (var data = UnitOfWork.Create())
                {
                    result = data.Repositories.DocumentDataContext.LoadAnalysisData.Get(DocumentData);
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }


        private string GetDocumentPath()
        {
            return configData.GetElementByName("LastDocument");
        }



        #region Properties
        private WebConfig configData;
        private XMLStructureData dataXml;
        private List<Material> MaterialCollection;
        #endregion

    }//end of class
}//end of namespace
