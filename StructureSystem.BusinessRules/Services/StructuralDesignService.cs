﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.Model;
using System.ComponentModel;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using StructureSystem.BusinessRules.Configuration;
using StructureSystem.Data;
using StructureSystem.BusinessRules.Functions;

namespace StructureSystem.BusinessRules.Services
{
    public class StructuralDesignService : INotifyPropertyChanged
    {


        #region Constructor
        public StructuralDesignService()
        {
            this.configData = new WebConfig();
            this.MaterialCollection = this.configData.GetMaterialsCollection();

        }
        #endregion


        #region Información inicial
        public Structure GetStructure()
        {
            structure = new Structure();
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
                CalculateEstructuralDesign(ref structure);
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
                double sumVcy = 0, sumVcx = 0, sumPcy = 0, sumPcx = 0, PaH = 0, PaV = 0;

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
                    Structure.Storeys[i].MomentoVolteoEntrepisoX = Functions.Operations.CalcularMomentoVolteoEntrepiso(Structure.Storeys[i], Vcx[Structure.Storeys.Count - 1 - i]);
                    Structure.Storeys[i].MomentoVolteoEntrepisoY = Functions.Operations.CalcularMomentoVolteoEntrepiso(Structure.Storeys[i], Vcy[Structure.Storeys.Count - 1 - i]);

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

                        wall.CargaAxialMaxima = Functions.Operations.CalcularCargaAxialUltima(wall, entrepiso, Structure.Storeys[i].StoreyNumber, Structure, MaterialCollection);
                        wall.CargaAxialSismo = Functions.Operations.CalcularCargaAxialDeSismo(wall, entrepiso, Structure.Storeys[i].StoreyNumber, Structure, MaterialCollection);
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

                        wall.CargaAxialMaxima = Functions.Operations.CalcularCargaAxialUltima(wall, entrepiso, Structure.Storeys[i].StoreyNumber, Structure, MaterialCollection);
                        wall.CargaAxialSismo = Functions.Operations.CalcularCargaAxialDeSismo(wall, entrepiso, Structure.Storeys[i].StoreyNumber, Structure, MaterialCollection);

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion


        #region Información diseño estructural (7.n)
        private void CalculateEstructuralDesign(ref Structure structure)
        {
            try
            {
                XMLLoadAnalysisData entrepiso;
                for (int i = structure.Storeys.Count - 1; i >= 0; i--)
                {
                    entrepiso = GetEntrepisoPorNivel(structure.Storeys[i].StoreyNumber);

                    foreach (var wall in structure.Storeys[i].HorizontalWalls)
                    {
                        wall.ExcentricidadDeCarga = Operations.CalcularExcentricidadDeCarga(wall);
                        wall.FactorAlturaEfectiva = Operations.CalcularFactorDeAlturaEfectiva(wall);
                        wall.FE = Operations.CalcularFactorReduccionExcentricidadEsbeltez(wall, entrepiso);
                        //do
                        //{
                        wall.PR = Operations.CalcularResistenciaCompresionPura(wall, MaterialCollection.Single(x => x.Name == wall.Material));
                        wall.Mo = Operations.CalcularResistenciaFlexionPura(wall);

                        wall.P1X = Operations.CalcularP1X(wall);
                        wall.P1Y = Operations.CalcularP1Y(wall);
                        wall.P2X = Operations.CalcularP2X(wall);
                        wall.P2Y = Operations.CalcularP2Y(wall);
                        wall.P3X = Operations.CalcularP3X(wall);
                        wall.P3Y = Operations.CalcularP3Y(wall);
                        wall.P4X = Operations.CalcularP4X(wall);
                        wall.P4Y = Operations.CalcularP4Y(wall);
                        wall.P5X = Operations.CalcularP5X(wall);
                        wall.P5Y = Operations.CalcularP5Y(wall);

                        wall.TP1P2 = Operations.CalcularTP1P2(wall);
                        wall.TP2P3 = Operations.CalcularTP2P3(wall);

                        wall.ResMamposteriaCortante = Operations.CalcularResistenciaMamposteriaCortante(wall, MaterialCollection.Single(x => x.Name == wall.Material));
                        wall.ResAceroRefuerzoHorizontalCortante = Operations.CalcularResistenciaAceroRefuerzoHorizontalCortante(wall, MaterialCollection.Single(x => x.Name == wall.Material));


                        wall.ResistenciaTotalACortante = Operations.CalcularResistenciaTotalACortante(wall);
                        //Si el muro es de concreto solo calculas esta... sino, calculas lo anterior y este no.
                        wall.ResistenciaCortanteConcreto = Operations.CalcularResistenciaCortanteConcreto(wall);

                        //    wall.As += 1;
                        //    if (Operations.CalcularBc(wall))
                        //        wall.bc += 5;

                        //} while (wall.Conclusion.Equals("NO PASA"));



                    }

                    foreach (var wall in structure.Storeys[i].VerticalWalls)
                    {
                        wall.ExcentricidadDeCarga = Operations.CalcularExcentricidadDeCarga(wall);
                        wall.FactorAlturaEfectiva = Operations.CalcularFactorDeAlturaEfectiva(wall);
                        wall.FE = Operations.CalcularFactorReduccionExcentricidadEsbeltez(wall, entrepiso);
                        //do
                        //{
                        wall.PR = Operations.CalcularResistenciaCompresionPura(wall, MaterialCollection.Single(x => x.Name == wall.Material));
                        wall.Mo = Operations.CalcularResistenciaFlexionPura(wall);

                        wall.P1X = Operations.CalcularP1X(wall);
                        wall.P1Y = Operations.CalcularP1Y(wall);
                        wall.P2X = Operations.CalcularP2X(wall);
                        wall.P2Y = Operations.CalcularP2Y(wall);
                        wall.P3X = Operations.CalcularP3X(wall);
                        wall.P3Y = Operations.CalcularP3Y(wall);
                        wall.P4X = Operations.CalcularP4X(wall);
                        wall.P4Y = Operations.CalcularP4Y(wall);
                        wall.P5X = Operations.CalcularP5X(wall);
                        wall.P5Y = Operations.CalcularP5Y(wall);

                        wall.TP1P2 = Operations.CalcularTP1P2(wall);
                        wall.TP2P3 = Operations.CalcularTP2P3(wall);

                        wall.ResMamposteriaCortante = Operations.CalcularResistenciaMamposteriaCortante(wall, MaterialCollection.Single(x => x.Name == wall.Material));
                        wall.ResAceroRefuerzoHorizontalCortante = Operations.CalcularResistenciaAceroRefuerzoHorizontalCortante(wall, MaterialCollection.Single(x => x.Name == wall.Material));

                        wall.ResistenciaTotalACortante = Operations.CalcularResistenciaTotalACortante(wall);

                        //Si el muro es de concreto solo calculas esta... sino, calculas lo anterior y este no.
                        wall.ResistenciaCortanteConcreto = Operations.CalcularResistenciaCortanteConcreto(wall);
                    }


                    string materialName = structure.Storeys[i].HorizontalWalls.FirstOrDefault().Material;
                    structure.Storeys[i].EsfuerzoNormalPromedio = Operations.CalcularEsfuerzoNormalPromedioDeEntrepiso(structure.Storeys[i], MaterialCollection.Single(x => x.Name == materialName));
                    structure.Storeys[i].CortanteResistenteEntrepisoMamposteria = Operations.CalcularCortanteResistenteEntrepisoMamposteria(structure.Storeys[i], MaterialCollection.Single(x => x.Name == materialName));

                }


            }
            catch (Exception ex)
            {

            }

        }


        public Structure Update(List<Storey> storeys)
        {
            try
            {
                XMLLoadAnalysisData entrepiso;

                structure.Storeys.Clear();
                structure.Storeys = storeys;
                for (int i = structure.Storeys.Count - 1; i >= 0; i--)
                {
                    entrepiso = GetEntrepisoPorNivel(structure.Storeys[i].StoreyNumber);

                    //AQUI SE CALCULAN LOS NIVELES

                    foreach (var wall in structure.Storeys[i].HorizontalWalls)
                    {

                        wall.ExcentricidadDeCarga = Operations.CalcularExcentricidadDeCarga(wall);
                        wall.FactorAlturaEfectiva = Operations.CalcularFactorDeAlturaEfectiva(wall);
                        wall.FE = Operations.CalcularFactorReduccionExcentricidadEsbeltez(wall, entrepiso);
                        
                        for (int index = 0; index < 100 && !Operations.ValidarGrafica(wall); index++)
                        {
                            wall.PR = Operations.CalcularResistenciaCompresionPura(wall, MaterialCollection.Single(x => x.Name == wall.Material));
                            wall.Mo = Operations.CalcularResistenciaFlexionPura(wall);

                            wall.P1X = Operations.CalcularP1X(wall);
                            wall.P1Y = Operations.CalcularP1Y(wall);
                            wall.P2X = Operations.CalcularP2X(wall);
                            wall.P2Y = Operations.CalcularP2Y(wall);
                            wall.P3X = Operations.CalcularP3X(wall);
                            wall.P3Y = Operations.CalcularP3Y(wall);
                            wall.P4X = Operations.CalcularP4X(wall);
                            wall.P4Y = Operations.CalcularP4Y(wall);
                            wall.P5X = Operations.CalcularP5X(wall);
                            wall.P5Y = Operations.CalcularP5Y(wall);


                            if (Operations.CalcularBc(wall))
                                wall.bc += 5;
                            wall.As += 1;
                           

                        }

                        wall.ResMamposteriaCortante = Operations.CalcularResistenciaMamposteriaCortante(wall, MaterialCollection.Single(x => x.Name == wall.Material));
                        wall.ResAceroRefuerzoHorizontalCortante = Operations.CalcularResistenciaAceroRefuerzoHorizontalCortante(wall, MaterialCollection.Single(x => x.Name == wall.Material));
                        wall.ResistenciaTotalACortante = Operations.CalcularResistenciaTotalACortante(wall);

                        //Si el muro es de concreto solo calculas esta... sino, calculas lo anterior y este no.
                        wall.ResistenciaCortanteConcreto = Operations.CalcularResistenciaCortanteConcreto(wall);
                        wall.Conclusion = Operations.CalcularConclusionPorCortante(wall);

                    }
                    foreach (var wall in structure.Storeys[i].VerticalWalls)
                    {
                        wall.ExcentricidadDeCarga = Operations.CalcularExcentricidadDeCarga(wall);
                        wall.FactorAlturaEfectiva = Operations.CalcularFactorDeAlturaEfectiva(wall);
                        wall.FE = Operations.CalcularFactorReduccionExcentricidadEsbeltez(wall, entrepiso);

                        for (int index = 0; index < 100 && !Operations.ValidarGrafica(wall); index++)
                        {
                            wall.PR = Operations.CalcularResistenciaCompresionPura(wall, MaterialCollection.Single(x => x.Name == wall.Material));
                            wall.Mo = Operations.CalcularResistenciaFlexionPura(wall);

                            wall.P1X = Operations.CalcularP1X(wall);
                            wall.P1Y = Operations.CalcularP1Y(wall);
                            wall.P2X = Operations.CalcularP2X(wall);
                            wall.P2Y = Operations.CalcularP2Y(wall);
                            wall.P3X = Operations.CalcularP3X(wall);
                            wall.P3Y = Operations.CalcularP3Y(wall);
                            wall.P4X = Operations.CalcularP4X(wall);
                            wall.P4Y = Operations.CalcularP4Y(wall);
                            wall.P5X = Operations.CalcularP5X(wall);
                            wall.P5Y = Operations.CalcularP5Y(wall);

                            wall.As += 1;
                            if (Operations.CalcularBc(wall))
                                wall.bc += 5;
                        }


                        wall.ResMamposteriaCortante = Operations.CalcularResistenciaMamposteriaCortante(wall, MaterialCollection.Single(x => x.Name == wall.Material));
                        wall.ResAceroRefuerzoHorizontalCortante = Operations.CalcularResistenciaAceroRefuerzoHorizontalCortante(wall, MaterialCollection.Single(x => x.Name == wall.Material));
                        wall.ResistenciaTotalACortante = Operations.CalcularResistenciaTotalACortante(wall);

                        //Si el muro es de concreto solo calculas esta... sino, calculas lo anterior y este no.
                        wall.ResistenciaCortanteConcreto = Operations.CalcularResistenciaCortanteConcreto(wall);

                        wall.Conclusion = Operations.CalcularConclusionPorCortante(wall);

                    } //Fin de calculo para muros verticales
                    //INICIA CALCULO PARA NIVELES (ENTREPISOS)

                    string materialName = structure.Storeys[i].HorizontalWalls.FirstOrDefault().Material;
                    structure.Storeys[i].EsfuerzoNormalPromedio = Operations.CalcularEsfuerzoNormalPromedioDeEntrepiso(structure.Storeys[i], MaterialCollection.Single(x => x.Name == materialName));
                    structure.Storeys[i].CortanteResistenteEntrepisoMamposteria = Operations.CalcularCortanteResistenteEntrepisoMamposteria(structure.Storeys[i], MaterialCollection.Single(x => x.Name == materialName));
                    structure.Storeys[i].CortanteResistenteEntrepisoConcreto = Operations.CalcularCortanteResistenteEntrepisoConcreto(structure.Storeys[i]);
                    structure.Storeys[i].CortanteEntrepisoTotal = Operations.CalcularCortanteEntrepisoTotal(structure.Storeys[i]);
                    structure.Storeys[i].ConclusionX = Operations.CalcularConclusionPorCortanteDeEntrepiso(Vcx[structure.Storeys.Count - 1 - i], structure.Storeys[i].CortanteEntrepisoTotal);
                    structure.Storeys[i].ConclusionY = Operations.CalcularConclusionPorCortanteDeEntrepiso(Vcy[structure.Storeys.Count - 1 - i], structure.Storeys[i].CortanteEntrepisoTotal);

                }

                //XMLStructuralDesignData document = new XMLStructuralDesignData();
                //document.DocumentPath = GetDocumentPath();

                //using (var data = UnitOfWork.Create())
                //{
                //    data.Repositories.DocumentDataContext.StructuralDesignData.Create(document);
                //}
            }
            catch (Exception ex)
            {

            }

            return structure;
        }



        public Structure UpdateWall(Wall selectedWall)
        {
            try
            {
                XMLLoadAnalysisData entrepiso;

                var SelectedStorey = structure.Storeys.Single(x => x.StoreyNumber == selectedWall.Storey);

                entrepiso = GetEntrepisoPorNivel(SelectedStorey.StoreyNumber);


                for (int index = 0; index < 100 && !Operations.ValidarGrafica(selectedWall); index++)
                {
                    selectedWall.ExcentricidadDeCarga = Operations.CalcularExcentricidadDeCarga(selectedWall);
                    selectedWall.FactorAlturaEfectiva = Operations.CalcularFactorDeAlturaEfectiva(selectedWall);
                    selectedWall.FE = Operations.CalcularFactorReduccionExcentricidadEsbeltez(selectedWall, entrepiso);

                    selectedWall.PR = Operations.CalcularResistenciaCompresionPura(selectedWall, MaterialCollection.Single(x => x.Name == selectedWall.Material));
                    selectedWall.Mo = Operations.CalcularResistenciaFlexionPura(selectedWall);

                    selectedWall.P1X = Operations.CalcularP1X(selectedWall);
                    selectedWall.P1Y = Operations.CalcularP1Y(selectedWall);
                    selectedWall.P2X = Operations.CalcularP2X(selectedWall);
                    selectedWall.P2Y = Operations.CalcularP2Y(selectedWall);
                    selectedWall.P3X = Operations.CalcularP3X(selectedWall);
                    selectedWall.P3Y = Operations.CalcularP3Y(selectedWall);
                    selectedWall.P4X = Operations.CalcularP4X(selectedWall);
                    selectedWall.P4Y = Operations.CalcularP4Y(selectedWall);
                    selectedWall.P5X = Operations.CalcularP5X(selectedWall);
                    selectedWall.P5Y = Operations.CalcularP5Y(selectedWall);

                    selectedWall.As += 1;
                    if (Operations.CalcularBc(selectedWall))
                        selectedWall.bc += 5;

                }



                selectedWall.ResMamposteriaCortante = Operations.CalcularResistenciaMamposteriaCortante(selectedWall, MaterialCollection.Single(x => x.Name == selectedWall.Material));
                selectedWall.ResAceroRefuerzoHorizontalCortante = Operations.CalcularResistenciaAceroRefuerzoHorizontalCortante(selectedWall, MaterialCollection.Single(x => x.Name == selectedWall.Material));
                selectedWall.ResistenciaTotalACortante = Operations.CalcularResistenciaTotalACortante(selectedWall);

                //Si el muro es de concreto solo calculas esta... sino, calculas lo anterior y este no.
                selectedWall.ResistenciaCortanteConcreto = Operations.CalcularResistenciaCortanteConcreto(selectedWall);
                selectedWall.Conclusion = Operations.CalcularConclusionPorCortante(selectedWall);



                //INICIA CALCULO PARA NIVELES (ENTREPISOS)

                string materialName =selectedWall.Material;
                SelectedStorey.EsfuerzoNormalPromedio = Operations.CalcularEsfuerzoNormalPromedioDeEntrepiso(SelectedStorey, MaterialCollection.Single(x => x.Name == materialName));
                SelectedStorey.CortanteResistenteEntrepisoMamposteria = Operations.CalcularCortanteResistenteEntrepisoMamposteria(SelectedStorey, MaterialCollection.Single(x => x.Name == materialName));
                SelectedStorey.CortanteResistenteEntrepisoConcreto = Operations.CalcularCortanteResistenteEntrepisoConcreto(SelectedStorey);
                SelectedStorey.CortanteEntrepisoTotal = Operations.CalcularCortanteEntrepisoTotal(SelectedStorey);
               
                double VcxR = (double)Vcx.Reverse().ToArray().GetValue(SelectedStorey.StoreyNumber);
                double VcyR = (double)Vcy.Reverse().ToArray().GetValue(SelectedStorey.StoreyNumber);

                SelectedStorey.ConclusionX = Operations.CalcularConclusionPorCortanteDeEntrepiso(VcxR, SelectedStorey.CortanteEntrepisoTotal);
                SelectedStorey.ConclusionY = Operations.CalcularConclusionPorCortanteDeEntrepiso(VcyR, SelectedStorey.CortanteEntrepisoTotal);



                XMLStructuralDesignData document = new XMLStructuralDesignData();
                document.DocumentPath = GetDocumentPath();

                using (var data = UnitOfWork.Create())
                {
                    data.Repositories.DocumentDataContext.StructuralDesignData.Create(document);
                }
            }
            catch (Exception ex)
            {

            }

            return structure;
        }

        #endregion

        #region Graficacion
        public void Graficar(Wall wall)
        {
            try
            {
                if (Operations.ValidarGrafica(wall))
                    Operations.Graficar(wall);

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



        #region Private Properties

        private static double[] kx, ky, m, wx, wy, Tx, Ty, Fx, Fy, J, Gammax, Gammay, Mex, Mey, GammaNx, GammaNy, DeltaAzx, DeltaAzy;
        private static double[] Pmx, Pmy, Vax, Vay, CoeModx, CoeMody, ax, ay, Acx, Acy, Pcx, Pcy, Vcx, Vcy, DeltacX, DeltacY;
        private static double[,] M, Kx, Ky, Phix, Phiy, Omegax, Omegay, mgx, mgy, kgx, kgy, PhiNx, PhiNy, Ax, Ay, Px, Py, Vx, Vy, DeltaEntx, DeltaEnty;

        public Structure structure;
        private WebConfig configData;
        private XMLSeismicDistributionData dataXml;
        private List<Material> MaterialCollection;



        #endregion

        #region Propertie Changed
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }//end of class
}//end of namespace
