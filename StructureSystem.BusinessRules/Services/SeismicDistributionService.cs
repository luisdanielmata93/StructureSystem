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


        #region Constructor
        public SeismicDistributionService()
        {
            this.configData = new WebConfig();
            this.MaterialCollection = this.configData.GetMaterialsCollection();
        }
        #endregion


        #region Información inicial
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
                CalculateComplexData(ref structure);
                CalculateComplementData(ref structure);
            }
            catch (Exception ex)
            {
            }
            return structure;

        }

        private void CalculateInitialData(ref Structure structure)
        {
            try
            {

                for (int i = 0; i < structure.Storeys.Count; i++)
                {
                    foreach (var wall in structure.Storeys[i].HorizontalWalls)
                    {
                        wall.Inercia = Functions.Operations.CalcularInercia(wall);
                        wall.AreaLongitudinal = Functions.Operations.CalcularAreaLongitudinal(wall);
                        wall.RigidezLateral = Functions.Operations.CalcularRigidezLateral(wall, this.MaterialCollection.Single(x => x.Name == wall.Material));

                        wall.WMuro = ((wall.Length * wall.Thickness) / 100 * (wall.Height * 1.8) / 2) * 1000;
                        wall.WLosa = wall.TributaryArea * 0.59 * 1000;
                    }
                    foreach (var wall in structure.Storeys[i].VerticalWalls)
                    {
                        wall.Inercia = Functions.Operations.CalcularInercia(wall);
                        wall.AreaLongitudinal = Functions.Operations.CalcularAreaLongitudinal(wall);
                        wall.RigidezLateral = Functions.Operations.CalcularRigidezLateral(wall, this.MaterialCollection.Single(x => x.Name == wall.Material));

                        wall.WMuro = ((wall.Length * wall.Thickness) / 100 * (wall.Height * 1.8) / 2) * 1000;
                        wall.WLosa = wall.TributaryArea * 0.59 * 1000;
                    }

                }

                foreach (var storey in structure.Storeys)
                {
                    storey.RigidezEntrepisoHorizontal = Functions.Operations.CalcularRigidezEntrepiso(storey, Enums.SideType.Horizontal);
                    storey.RigidezEntrepisoVertical = Functions.Operations.CalcularRigidezEntrepiso(storey, Enums.SideType.Vertical);
                }

                structure.MasaTotal = 0;
                foreach (var storey in structure.Storeys)
                {
                    XMLLoadAnalysisData entrepiso = GetEntrepisoPorNivel(storey.StoreyNumber);
                    storey.MasasEntrepisos = Functions.Operations.CalcularMasasEntrepiso(MaterialCollection, storey, entrepiso);
                    structure.MasaTotal += storey.MasasEntrepisos;
                }


                int n = structure.Storeys.Count - 1;

                for (int i = n; i >= 0; i--)
                {
                    for (int j = structure.Storeys[i].HorizontalWalls.Count - 1; j >= 0; j--)
                    {
                        var wm = structure.Storeys[i].HorizontalWalls[j].WMuro;
                        var wl = structure.Storeys[i].HorizontalWalls[j].WLosa;
                        if (i == n)
                            structure.Storeys[i].HorizontalWalls[j].MSup = 0;
                        else
                            structure.Storeys[i].HorizontalWalls[j].MSup = structure.Storeys[i + 1].HorizontalWalls[j].WTotal;

                        structure.Storeys[i].HorizontalWalls[j].WTotal = wm + wl + structure.Storeys[i].HorizontalWalls[j].MSup;
                    }


                    for (int j = structure.Storeys[i].VerticalWalls.Count - 1; j >= 0; j--)
                    {
                        var wm = structure.Storeys[i].VerticalWalls[j].WMuro;
                        var wl = structure.Storeys[i].VerticalWalls[j].WLosa;
                        if (i == n)
                            structure.Storeys[i].VerticalWalls[j].MSup = 0;
                        else
                            structure.Storeys[i].VerticalWalls[j].MSup = structure.Storeys[i + 1].VerticalWalls[j].WTotal;

                        structure.Storeys[i].VerticalWalls[j].WTotal = wm + wl + structure.Storeys[i].VerticalWalls[j].MSup;
                    }
                }


            }
            catch (Exception ex)
            {

            }
        }

        #endregion



        #region Obtención de información compleja(Matrices y vectores)
        private void CalculateComplexData(ref Structure structure)
        {
            List<EspectroDisenio> espectroDisenio = GetEspectroDisenio();
            var ValoresEstaticos = ObtenerValoresEstaticos();
            int n = structure.Storeys.Count;
            double Q = Convert.ToDouble(ValoresEstaticos.Single(x => x.Key == "Q").Value);
            double c = Convert.ToDouble(ValoresEstaticos.Single(x => x.Key == "c").Value);
            double R = Convert.ToDouble(ValoresEstaticos.Single(x => x.Key == "R").Value);

            //Calcular vector de rigideces 
            kx = new double[n];
            ky = new double[n];
            m = new double[n];
            J = new double[n];
            for (int i = 0; i < structure.Storeys.Count; i++)
            {
                kx[i] = structure.Storeys[i].RigidezEntrepisoHorizontal;
                ky[i] = structure.Storeys[i].RigidezEntrepisoVertical;
                m[i] = structure.Storeys[i].MasasEntrepisos;
                J[i] = 1;
            }

            M = Operations.CalcularMatrizMasas(structure, Enums.SideType.Horizontal);
            //Calculos en X
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            wx = Operations.CalcularVectorPeriodosCirculares(M, Kx);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            Tx = Operations.CalcularVectorPeriodosNaturales(M, Kx);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            Fx = Operations.CalcularVectorFrecuencias(M, Kx);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            Phix = Operations.CalcularMatrizEigenVectores(M, Kx);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            Omegax = Operations.CalcularMatrizEspectral(M, Kx);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            mgx = Operations.CalcularMatrizMasasGeneralizada(M, Kx);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            kgx = Operations.CalcularMatrizRigidecesGeneralizada(M, Kx);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            Gammax = Operations.CalcularVectorParticipacionModal(M, Kx);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            PhiNx = Operations.CalcularMatrizModalNormalizada(M, Kx);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            Mex = Operations.CalcularVectorMasasEfectivas(M, Kx);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            GammaNx = Operations.CalcularMatrizFactoresParticipacionMasasModales(M, Kx);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            Pmx = Operations.CalcularParticipacionDeMasas(M, Kx, structure.MasaTotal);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            Vax = Operations.CalcularVectorDeAceleraciones(M, Kx, espectroDisenio);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            CoeModx = Operations.CalcularGamma(M, Kx);
            Kx = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Horizontal);
            ax = Operations.CalcularVectorDeAceleracionPorModo(Vax, CoeModx);
            Ax = Operations.CalcularVectoresDeAceleracionesPorModo(ax, Phix);
            Px = Operations.CalcularFuerzasFicticiasEquivalentesPorModo(Ax, m);
            Vx = Operations.CalcularVectoresDeFuerzasCortantesDeDisenio(Px);
            DeltaAzx = Operations.CalcularDesplazamientoEnAzotea(Ax, wx, Q);
            DeltaEntx = Operations.CalcularDesplazamientoEntrepiso(DeltaAzx, Phix);
            Acx = Operations.CalcularVectorDeAceleracionCombinada(Ax);
            Pcx = Operations.CalcularVectorDeFuerzasFicticiasEquivalentesCombinada(Px);
            Vcx = Operations.CalcularVectorDeFuerzasCortantesCombinada(Vx);
            DeltacX = Operations.CalcularVectorDeDesplazamientosCombinado(DeltaEntx);
            //Calculos en Y
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            wy = Operations.CalcularVectorPeriodosCirculares(M, Ky);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            Ty = Operations.CalcularVectorPeriodosNaturales(M, Ky);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            Fy = Operations.CalcularVectorFrecuencias(M, Ky);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            Phiy = Operations.CalcularMatrizEigenVectores(M, Ky);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            Omegay = Operations.CalcularMatrizEspectral(M, Ky);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            mgy = Operations.CalcularMatrizMasasGeneralizada(M, Ky);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            kgy = Operations.CalcularMatrizRigidecesGeneralizada(M, Ky);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            Gammay = Operations.CalcularVectorParticipacionModal(M, Ky);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            PhiNy = Operations.CalcularMatrizModalNormalizada(M, Ky);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            Mey = Operations.CalcularVectorMasasEfectivas(M, Ky);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            GammaNy = Operations.CalcularMatrizFactoresParticipacionMasasModales(M, Ky);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            Pmy = Operations.CalcularParticipacionDeMasas(M, Ky, structure.MasaTotal);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            Vay = Operations.CalcularVectorDeAceleraciones(M, Ky, espectroDisenio);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            CoeMody = Operations.CalcularGamma(M, Ky);
            Ky = Operations.CalcularMatrizRigideces(structure, Enums.SideType.Vertical);
            ay = Operations.CalcularVectorDeAceleracionPorModo(Vay, CoeMody);
            Ay = Operations.CalcularVectoresDeAceleracionesPorModo(ay, Phiy);
            Py = Operations.CalcularFuerzasFicticiasEquivalentesPorModo(Ay, m);
            Vy = Operations.CalcularVectoresDeFuerzasCortantesDeDisenio(Py);
            DeltaAzy = Operations.CalcularDesplazamientoEnAzotea(Ay, wy, Q);
            DeltaEnty = Operations.CalcularDesplazamientoEntrepiso(DeltaAzy, Phiy);
            Acy = Operations.CalcularVectorDeAceleracionCombinada(Ay);
            Pcy = Operations.CalcularVectorDeFuerzasFicticiasEquivalentesCombinada(Py);
            Vcy = Operations.CalcularVectorDeFuerzasCortantesCombinada(Vy);
            DeltacY = Operations.CalcularVectorDeDesplazamientosCombinado(DeltaEnty);
        }


        #endregion


        #region Información complementaria de los niveles ( 6.n)
        private void CalculateComplementData(ref Structure Structure)
        {
            try
            {
                XMLLoadAnalysisData entrepiso;
                double sumVcy = 0, sumVcx = 0, sumPcy = 0, sumPcx = 0, PaH = 0 , PaV = 0, CAUAx = 0, CAUAy= 0, CASAx = 0, CASAy = 0;

                for (int i = Structure.Storeys.Count - 1; i >= 0; i--)
                {
                    entrepiso = GetEntrepisoPorNivel(Structure.Storeys[i].StoreyNumber);
                    Structure.Storeys[i].CentroMasasY = Functions.Operations.CalcularCentroDeMasas(Structure.Storeys[i], Enums.Coordenate.X);
                    Structure.Storeys[i].CentroMasasX = Functions.Operations.CalcularCentroDeMasas(Structure.Storeys[i], Enums.Coordenate.Y);
                    if (i == Structure.Storeys.Count - 1)
                    {
                        Structure.Storeys[i].CentroCortantesX = Functions.Operations.CalcularCentroCortante(Vcy[Structure.Storeys.Count - 1 - i], Pcy[Structure.Storeys.Count - 1 - i], Structure.Storeys[i].CentroMasasX, Enums.Coordenate.X, true);
                        Structure.Storeys[i].CentroCortantesY = Functions.Operations.CalcularCentroCortante(Vcx[Structure.Storeys.Count - 1 - i], Pcx[Structure.Storeys.Count - 1 - i], Structure.Storeys[i].CentroMasasY, Enums.Coordenate.Y, true);
                    }
                    else
                    {
                        for (int k = i; k < Structure.Storeys.Count - 1; k++)
                        {
                            sumPcx += Pcx[k];
                            sumPcy += Pcy[k];
                        }
                        double CManteriorX = Structure.Storeys[i + 1].CentroMasasX;
                        double CManteriorY = Structure.Storeys[i + 1].CentroMasasY;
                        Structure.Storeys[i].CentroCortantesX = Functions.Operations.CalcularCentroCortante(Vcy[Structure.Storeys.Count - 1 - i], Pcy[Structure.Storeys.Count - 1 - i], Structure.Storeys[i].CentroMasasX, Enums.Coordenate.X, false, CManteriorX, sumPcy);
                        Structure.Storeys[i].CentroCortantesY = Functions.Operations.CalcularCentroCortante(Vcx[Structure.Storeys.Count - 1 - i], Pcx[Structure.Storeys.Count - 1 - i], Structure.Storeys[i].CentroMasasY, Enums.Coordenate.Y, false, CManteriorY, sumPcx);
                    }
                    Structure.Storeys[i].CentroTorsionY = Functions.Operations.CalcularCentroTorsion(Structure.Storeys[i], Enums.Coordenate.X);
                    Structure.Storeys[i].CentroTorsionX = Functions.Operations.CalcularCentroTorsion(Structure.Storeys[i], Enums.Coordenate.Y);
                    Structure.Storeys[i].ExcentricidadesEstaticasX = Functions.Operations.CalcularExcentricidadesEstaticas(Structure.Storeys[i].CentroMasasX, Structure.Storeys[i].CentroTorsionX);
                    Structure.Storeys[i].ExcentricidadesEstaticasY = Functions.Operations.CalcularExcentricidadesEstaticas(Structure.Storeys[i].CentroMasasY, Structure.Storeys[i].CentroTorsionY);
                    Structure.Storeys[i].ExcentricidadesAccidentalesX = Functions.Operations.CalcularExcentricidadesAccidentales(Structure.Storeys.Count, Structure.Storeys[i], Enums.Coordenate.X);
                    Structure.Storeys[i].ExcentricidadesAccidentalesY = Functions.Operations.CalcularExcentricidadesAccidentales(Structure.Storeys.Count, Structure.Storeys[i], Enums.Coordenate.Y);
                    Structure.Storeys[i].ExcentricidadesDisenioEntrepisoX = Functions.Operations.CalcularExcentricidadDisenioEntrepiso(Structure.Storeys[i].ExcentricidadesEstaticasX, Structure.Storeys[i].ExcentricidadesAccidentalesX);
                    Structure.Storeys[i].ExcentricidadesDisenioEntrepisoY = Functions.Operations.CalcularExcentricidadDisenioEntrepiso(Structure.Storeys[i].ExcentricidadesEstaticasY, Structure.Storeys[i].ExcentricidadesAccidentalesY);
                    Structure.Storeys[i].MomentosTorsionantesX = Functions.Operations.CalcularMomentosTorsionantes(Structure.Storeys[i].ExcentricidadesDisenioEntrepisoY, Pcx[Structure.Storeys.Count - 1 - i]);
                    Structure.Storeys[i].MomentosTorsionantesY = Functions.Operations.CalcularMomentosTorsionantes(Structure.Storeys[i].ExcentricidadesDisenioEntrepisoX, Pcy[Structure.Storeys.Count - 1 - i]);
                    Structure.Storeys[i].RigidezTorsionalEntrepiso = Functions.Operations.CalcularRigidezTorsionalEntrepiso(Structure.Storeys[i], Structure.Storeys[i].CentroTorsionX, Structure.Storeys[i].CentroTorsionY);
                    Structure.Storeys[i].MomentoVolteoEntrepisoX = Functions.Operations.CalcularMomentoVolteoEntrepiso(Structure.Storeys[i], Pcx[Structure.Storeys.Count - 1 - i]);
                    Structure.Storeys[i].MomentoVolteoEntrepisoY = Functions.Operations.CalcularMomentoVolteoEntrepiso(Structure.Storeys[i], Pcy[Structure.Storeys.Count - 1 - i]);


                    if (i < Structure.Storeys.Count - 1)
                    {
                        CAUAx += Structure.Storeys[i + 1].HorizontalWalls.First().CargaAxialMaxima;
                        CASAx += Structure.Storeys[i + 1].HorizontalWalls.First().CargaAxialSismo;
                    }


                    foreach (var wall in Structure.Storeys[i].HorizontalWalls)
                    {
                        wall.CortanteDirecto = Functions.Operations.CalcularFuerzasCortantesDirectas(wall, Vcx[Structure.Storeys.Count - 1 - i], Structure.Storeys[i].HorizontalWalls.Sum(x => x.RigidezLateral));
                        wall.Clasificacion = Functions.Operations.CalcularClasificacionMuro(Structure.Storeys[i].CentroMasasY, Structure.Storeys[i].CentroTorsionY, wall);
                        wall.ExcentricidadDisenioX = Functions.Operations.CalcularExcentricidadesDisenio(Structure.Storeys[i].ExcentricidadesEstaticasX, Structure.Storeys[i].ExcentricidadesAccidentalesX, wall.Clasificacion, wall.Side, Enums.Coordenate.X);
                        wall.ExcentricidadDisenioY = Functions.Operations.CalcularExcentricidadesDisenio(Structure.Storeys[i].ExcentricidadesEstaticasY, Structure.Storeys[i].ExcentricidadesAccidentalesY, wall.Clasificacion, wall.Side, Enums.Coordenate.Y);
                        wall.CortantePorTorsionX = Functions.Operations.CalcularCortanteTorsion(wall, Structure.Storeys[i].CentroTorsionX, Structure.Storeys[i].CentroTorsionY, Structure.Storeys[i].ExcentricidadesEstaticasX, Structure.Storeys[i].ExcentricidadesEstaticasY, Structure.Storeys[i].ExcentricidadesAccidentalesX, Structure.Storeys[i].ExcentricidadesAccidentalesY, Structure.Storeys[i].RigidezTorsionalEntrepiso, Pcx[Structure.Storeys.Count - 1 - i], Pcy[Structure.Storeys.Count - 1 - i], Enums.Coordenate.X);
                        wall.CortantePorTorsionY = Functions.Operations.CalcularCortanteTorsion(wall, Structure.Storeys[i].CentroTorsionX, Structure.Storeys[i].CentroTorsionY, Structure.Storeys[i].ExcentricidadesEstaticasX, Structure.Storeys[i].ExcentricidadesEstaticasY, Structure.Storeys[i].ExcentricidadesAccidentalesX, Structure.Storeys[i].ExcentricidadesAccidentalesY, Structure.Storeys[i].RigidezTorsionalEntrepiso, Pcx[Structure.Storeys.Count - 1 - i], Pcy[Structure.Storeys.Count - 1 - i], Enums.Coordenate.Y);
                        wall.CortanteTotales = Functions.Operations.CalcularCortantesTotales(wall);
                        wall.MomentoVolteo = Functions.Operations.CalcularMomentoVolteo(wall, Structure.Storeys[i].RigidezEntrepisoHorizontal, Structure.Storeys[i].MomentoVolteoEntrepisoX);
                        wall.Peso = wall.Thickness / 100 * wall.Height / 100 * wall.Length / 100 * Convert.ToDouble(MaterialCollection.Single(mat => mat.Name == wall.Material).PV);

                        wall.CargaAxialMaxima = Functions.Operations.CalcularCargaAxialUltima(wall, entrepiso, Structure.Storeys[i].StoreyNumber, CAUAx, Structure);
                        wall.CargaAxialSismo = Functions.Operations.CalcularCargaAxialDeSismo(wall, entrepiso, Structure.Storeys[i].StoreyNumber, CASAx, Structure);
                    }


                    if (i < Structure.Storeys.Count - 1)
                    {
                        CAUAy += Structure.Storeys[i + 1].VerticalWalls.First().CargaAxialMaxima;
                        CASAy += Structure.Storeys[i + 1].VerticalWalls.First().CargaAxialSismo;
                    }

                    foreach (var wall in Structure.Storeys[i].VerticalWalls)
                    {
                        wall.CortanteDirecto = Functions.Operations.CalcularFuerzasCortantesDirectas(wall, Vcy[Structure.Storeys.Count - 1 - i], Structure.Storeys[i].VerticalWalls.Sum(x => x.RigidezLateral));
                        wall.Clasificacion = Functions.Operations.CalcularClasificacionMuro(Structure.Storeys[i].CentroMasasY, Structure.Storeys[i].CentroTorsionY, wall);
                        wall.ExcentricidadDisenioX = Functions.Operations.CalcularExcentricidadesDisenio(Structure.Storeys[i].ExcentricidadesEstaticasX, Structure.Storeys[i].ExcentricidadesAccidentalesX, wall.Clasificacion, wall.Side, Enums.Coordenate.X);
                        wall.ExcentricidadDisenioY = Functions.Operations.CalcularExcentricidadesDisenio(Structure.Storeys[i].ExcentricidadesEstaticasY, Structure.Storeys[i].ExcentricidadesAccidentalesY, wall.Clasificacion, wall.Side, Enums.Coordenate.Y);
                        wall.CortantePorTorsionX = Functions.Operations.CalcularCortanteTorsion(wall, Structure.Storeys[i].CentroTorsionX, Structure.Storeys[i].CentroTorsionY, Structure.Storeys[i].ExcentricidadesEstaticasX, Structure.Storeys[i].ExcentricidadesEstaticasY, Structure.Storeys[i].ExcentricidadesAccidentalesX, Structure.Storeys[i].ExcentricidadesAccidentalesY, Structure.Storeys[i].RigidezTorsionalEntrepiso, Pcx[Structure.Storeys.Count - 1 - i], Pcy[Structure.Storeys.Count - 1 - i], Enums.Coordenate.X);
                        wall.CortantePorTorsionY = Functions.Operations.CalcularCortanteTorsion(wall, Structure.Storeys[i].CentroTorsionX, Structure.Storeys[i].CentroTorsionY, Structure.Storeys[i].ExcentricidadesEstaticasX, Structure.Storeys[i].ExcentricidadesEstaticasY, Structure.Storeys[i].ExcentricidadesAccidentalesX, Structure.Storeys[i].ExcentricidadesAccidentalesY, Structure.Storeys[i].RigidezTorsionalEntrepiso, Pcx[Structure.Storeys.Count - 1 - i], Pcy[Structure.Storeys.Count - 1 - i], Enums.Coordenate.Y);
                        wall.CortanteTotales = Functions.Operations.CalcularCortantesTotales(wall);
                        wall.MomentoVolteo = Functions.Operations.CalcularMomentoVolteo(wall, Structure.Storeys[i].RigidezEntrepisoVertical, Structure.Storeys[i].MomentoVolteoEntrepisoY);
                        wall.Peso = wall.Thickness / 100 * wall.Height / 100 * wall.Length / 100 * Convert.ToDouble(MaterialCollection.Single(mat => mat.Name == wall.Material).PV);

                        wall.CargaAxialMaxima = Functions.Operations.CalcularCargaAxialUltima(wall, entrepiso, Structure.Storeys[i].StoreyNumber, CAUAy, Structure);
                        wall.CargaAxialSismo = Functions.Operations.CalcularCargaAxialDeSismo(wall, entrepiso, Structure.Storeys[i].StoreyNumber, CASAy, Structure);

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion






        #region Obtención de entrepisos por nivel
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

        #endregion

        #region Metodos complementarios
        private string GetDocumentPath()
        {
            return configData.GetElementByName("LastDocument");
        }
     
        public List<EspectroDisenio> GetEspectroDisenio()
        {
            List<EspectroDisenio> result = new List<EspectroDisenio>();
            try
            {
                using (var data = UnitOfWork.Create())
                {
                    result = data.Repositories.DocumentDataContext.SeismicAnalysisData.GetEspectroDisenio(GetDocumentPath()).ToList();

                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public Dictionary<string, double> ObtenerValoresEstaticos()
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            try
            {
                using (var data = UnitOfWork.Create())
                {
                    result = data.Repositories.DocumentDataContext.SeismicDistributionData.GetStaticData(GetDocumentPath());
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region Properties

        private static double[] kx, ky, m, wx, wy, Tx, Ty, Fx, Fy, J, Gammax, Gammay, Mex, Mey, GammaNx, GammaNy, DeltaAzx, DeltaAzy;
        private static double[] Pmx, Pmy, Vax, Vay, CoeModx, CoeMody, ax, ay, Acx, Acy, Pcx, Pcy, Vcx, Vcy, DeltacX, DeltacY;
        private static double[,] M, Kx, Ky, Phix, Phiy, Omegax, Omegay, mgx, mgy, kgx, kgy, PhiNx, PhiNy, Ax, Ay, Px, Py, Vx, Vy, DeltaEntx, DeltaEnty;


        private WebConfig configData;
        private XMLSeismicDistributionData dataXml;
        private List<Material> MaterialCollection;
        #endregion

    }//end of class
}//end of namespace
