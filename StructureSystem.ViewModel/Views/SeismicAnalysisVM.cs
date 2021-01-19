using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.ViewModel.Shared;
using System.Collections.ObjectModel;
using System.Windows.Input;
using StructureSystem.Model;
using Microsoft.Win32;
using Notifications.Wpf.Controls;
using Notifications.Wpf;
using System.Configuration;
using MahApps.Metro.Controls.Dialogs;
using StructureSystem.BusinessRules.Services;
using System.Data;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace StructureSystem.ViewModel
{
    public class SeismicAnalysisVM : PropertyChangedViewModel
    {
        #region Constructor
        public SeismicAnalysisVM(PropertyChangedViewModel mainViewModel)
        {

            notificationViewModel = new NotificationViewModel();


            this._mainViewModel = mainViewModel;


            ContentDT = new DataTable("table");
            this.SetCommands();

        }

        #endregion
        #region Commands

        public ICommand EliminarEspectroDisCommand { get; private set; }
        public ICommand GuardarEspectroDisXCommand { get; private set; }
        public ICommand GuardarEspectroDisYCommand { get; private set; }


        //Comandos en X (Horizontales)
        public ICommand VectorRigidecesXCommand { get; private set; }
        public ICommand VectorMasasXCommand { get; private set; }
        public ICommand MatrizMasasXCommand { get; private set; }
        public ICommand MatrizRigidecesXCommand { get; private set; }
        public ICommand VectorPeriodosCircXCommand { get; private set; }
        public ICommand VectorPeriodosNaturalesXCommand { get; private set; }
        public ICommand VectorFrecuenciasXCommand { get; private set; }
        public ICommand EigenVectoresXCommand { get; private set; }
        public ICommand MatrizEspectralXCommand { get; private set; }
        public ICommand MatrizMasasGeneralizadaXCommand { get; private set; }
        public ICommand MatrizRigidecesGeneralizadaXCommand { get; private set; }
        public ICommand VectorUnosXCommand { get; private set; }
        public ICommand ParticipacionModalXCommand { get; private set; }
        public ICommand MatrizModalNormalizadoXCommand { get; private set; }
        public ICommand VectorMasasEfectivasXCommand { get; private set; }
        public ICommand MatrizFactPartMasasModalesXCommand { get; private set; }
        public ICommand ParticipacionMasasXCommand { get; private set; }
        public ICommand EspectroDisenioXCommand { get; private set; }
        public ICommand VectorAceleracionesXCommand { get; private set; }
        public ICommand GammaXCommand { get; private set; }
        public ICommand AceleracionPorModoXCommand { get; private set; }
        public ICommand VectAceleracionPorModoXCommand { get; private set; }
        public ICommand FuerzasFicticiasEquivPorModoXCommand { get; private set; }
        public ICommand VectorFuerzasCortantsDisenioXCommand { get; private set; }
        public ICommand DesplazamientoAzoteaPorModoXCommand { get; private set; }
        public ICommand DesplazamientoEntrepisoPorModoXCommand { get; private set; }
        public ICommand VectorAceleracionCombinadaXCommand { get; private set; }
        public ICommand VectFuerzasFicticiasEquivCombinadaXCommand { get; private set; }
        public ICommand VectFuerzasCortantesDisenioCombinadaXCommand { get; private set; }
        public ICommand VectDesplazamientosSSRSXCommand { get; private set; }
        public ICommand VectorFuerzasCortantsDisenioEstaticoXCommand { get; private set; }
        public ICommand DesplazamientosLateralesXCommand { get; private set; }
        public ICommand EstimPeriodoFundamentalEstructXCommand { get; private set; }
        public ICommand FuerzasCortantesXCommand { get; private set; }


        //Comandos en Y (Verticales)
        public ICommand VectorRigidecesYCommand { get; private set; }
        public ICommand VectorMasasYCommand { get; private set; }
        public ICommand MatrizMasasYCommand { get; private set; }
        public ICommand MatrizRigidecesYCommand { get; private set; }
        public ICommand VectorPeriodosCircYCommand { get; private set; }
        public ICommand VectorPeriodosNaturalesYCommand { get; private set; }
        public ICommand VectorFrecuenciasYCommand { get; private set; }
        public ICommand EigenVectoresYCommand { get; private set; }
        public ICommand MatrizEspectralYCommand { get; private set; }
        public ICommand MatrizMasasGeneralizadaYCommand { get; private set; }
        public ICommand MatrizRigidecesGeneralizadaYCommand { get; private set; }
        public ICommand VectorUnosYCommand { get; private set; }
        public ICommand ParticipacionModalYCommand { get; private set; }
        public ICommand MatrizModalNormalizadoYCommand { get; private set; }
        public ICommand VectorMasasEfectivasYCommand { get; private set; }
        public ICommand MatrizFactPartMasasModalesYCommand { get; private set; }
        public ICommand ParticipacionMasasYCommand { get; private set; }
        public ICommand EspectroDisenioYCommand { get; private set; }
        public ICommand VectorAceleracionesYCommand { get; private set; }
        public ICommand GammaYCommand { get; private set; }
        public ICommand AceleracionPorModoYCommand { get; private set; }
        public ICommand VectAceleracionPorModoYCommand { get; private set; }
        public ICommand FuerzasFicticiasEquivPorModoYCommand { get; private set; }
        public ICommand VectorFuerzasCortantsDisenioYCommand { get; private set; }
        public ICommand DesplazamientoAzoteaPorModoYCommand { get; private set; }
        public ICommand DesplazamientoEntrepisoPorModoYCommand { get; private set; }
        public ICommand VectorAceleracionCombinadaYCommand { get; private set; }
        public ICommand VectFuerzasFicticiasEquivCombinadaYCommand { get; private set; }
        public ICommand VectFuerzasCortantesDisenioCombinadaYCommand { get; private set; }
        public ICommand VectDesplazamientosSSRSYCommand { get; private set; }
        public ICommand VectorFuerzasCortantsDisenioEstaticoYCommand { get; private set; }
        public ICommand DesplazamientosLateralesYCommand { get; private set; }
        public ICommand EstimPeriodoFundamentalEstructYCommand { get; private set; }
        public ICommand FuerzasCortantesYCommand { get; private set; }

        #endregion


        #region Set Command

        private void SetCommands()
        {
            EliminarEspectroDisCommand = new RelayCommand(o => EliminarEspectroDisenioData());
            GuardarEspectroDisXCommand = new RelayCommand(o => GuardarEspectroDisenioData(Enums.SideType.Horizontal));
            GuardarEspectroDisYCommand = new RelayCommand(o => GuardarEspectroDisenioData(Enums.SideType.Vertical));
           
            //Comandos X(Horizontales)
            VectorRigidecesXCommand = new RelayCommand(o => ShowVectorRigideces(Enums.SideType.Horizontal));
            VectorMasasXCommand = new RelayCommand(o => ShowVectorMasas());
            MatrizMasasXCommand = new RelayCommand(o => ShowMatrizMasas(Enums.SideType.Horizontal));
            MatrizRigidecesXCommand = new RelayCommand(o => ShowMatrizRigideces(Enums.SideType.Horizontal));
            VectorPeriodosCircXCommand = new RelayCommand(o => ShowVectorPeriodosCirculares(Enums.SideType.Horizontal));
            VectorPeriodosNaturalesXCommand = new RelayCommand(o => ShowVectorPeriodosNaturales(Enums.SideType.Horizontal));
            VectorFrecuenciasXCommand = new RelayCommand(o => ShowVectorFrecuencias(Enums.SideType.Horizontal));
            EigenVectoresXCommand = new RelayCommand(o => ShowMatrizEigenVectores(Enums.SideType.Horizontal));
            MatrizEspectralXCommand = new RelayCommand(o => ShowMatrizEspectral(Enums.SideType.Horizontal));
            MatrizMasasGeneralizadaXCommand = new RelayCommand(o => ShowMatrizMasasGeneralizada(Enums.SideType.Horizontal));
            MatrizRigidecesGeneralizadaXCommand = new RelayCommand(o => ShowMatrizRigidecesGeneralizada(Enums.SideType.Horizontal));
            VectorUnosXCommand = new RelayCommand(o => ShowVectorUnos());
            ParticipacionModalXCommand = new RelayCommand(o => ShowVectorParticipacionModal(Enums.SideType.Horizontal));
            MatrizModalNormalizadoXCommand = new RelayCommand(o => ShowMatrizModalNormalizada(Enums.SideType.Horizontal));
            VectorMasasEfectivasXCommand = new RelayCommand(o => ShowVectorMasasEfectivas(Enums.SideType.Horizontal));
            MatrizFactPartMasasModalesXCommand = new RelayCommand(o => ShowMatrizFactParticipacionMasasModales(Enums.SideType.Horizontal));
            ParticipacionMasasXCommand = new RelayCommand(o => ShowVectorParticipacionMasas(Enums.SideType.Horizontal));
            EspectroDisenioXCommand = new RelayCommand(o => ShowEspectroDisenio(Enums.SideType.Horizontal));
            VectorAceleracionesXCommand = new RelayCommand(o => ShowVectorAcceleraciones(Enums.SideType.Horizontal));
            GammaXCommand = new RelayCommand(o => ShowGamma(Enums.SideType.Horizontal));
            AceleracionPorModoXCommand = new RelayCommand(o => ShowAceleracionPorModo(Enums.SideType.Horizontal));
            VectAceleracionPorModoXCommand = new RelayCommand(o => ShowVectAceleracionPorModo(Enums.SideType.Horizontal));
            FuerzasFicticiasEquivPorModoXCommand = new RelayCommand(o => ShowFuerzasFicticiasEquivPorModo(Enums.SideType.Horizontal));
            VectorFuerzasCortantsDisenioXCommand = new RelayCommand(o => ShowVectorFuerzasCortantesDisenio(Enums.SideType.Horizontal));
            DesplazamientoAzoteaPorModoXCommand = new RelayCommand(o => ShowDesplazamientoAzoteaPorModo(Enums.SideType.Horizontal));
            DesplazamientoEntrepisoPorModoXCommand = new RelayCommand(o => ShowDesplazamientoEntrepisoPorModo(Enums.SideType.Horizontal));
            VectorAceleracionCombinadaXCommand = new RelayCommand(o => ShowVectorAceleracionCombinada(Enums.SideType.Horizontal));
            VectFuerzasFicticiasEquivCombinadaXCommand = new RelayCommand(o => ShowVectorFuerzasFicticiasEquivCombinada(Enums.SideType.Horizontal));
            VectFuerzasCortantesDisenioCombinadaXCommand = new RelayCommand(o => ShowVectorFuerzasCortantesDisenioCombinada(Enums.SideType.Horizontal));
            VectDesplazamientosSSRSXCommand = new RelayCommand(o => ShowVectorDesplazamientosSSRS(Enums.SideType.Horizontal));
            VectorFuerzasCortantsDisenioEstaticoXCommand = new RelayCommand(o => ShowVectorDeterminacionFuerzasCortantes(Enums.SideType.Horizontal));
            DesplazamientosLateralesXCommand = new RelayCommand(o => ShowVectorDesplazamientosLaterales(Enums.SideType.Horizontal));
            EstimPeriodoFundamentalEstructXCommand = new RelayCommand(o => ShowEstimacionPeriodoFundamentalEstructura(Enums.SideType.Horizontal));
            FuerzasCortantesXCommand = new RelayCommand(o => ShowFuerzasCortantes(Enums.SideType.Horizontal));

            //Comandos Y(Verticales)
            VectorRigidecesYCommand = new RelayCommand(o => ShowVectorRigideces(Enums.SideType.Vertical));
            VectorMasasYCommand = new RelayCommand(o => ShowVectorMasas());
            MatrizMasasYCommand = new RelayCommand(o => ShowMatrizMasas(Enums.SideType.Vertical));
            MatrizRigidecesYCommand = new RelayCommand(o => ShowMatrizRigideces(Enums.SideType.Vertical));
            VectorPeriodosCircYCommand = new RelayCommand(o => ShowVectorPeriodosCirculares(Enums.SideType.Vertical));
            VectorPeriodosNaturalesYCommand = new RelayCommand(o => ShowVectorPeriodosNaturales(Enums.SideType.Vertical));
            VectorFrecuenciasYCommand = new RelayCommand(o => ShowVectorFrecuencias(Enums.SideType.Vertical));
            EigenVectoresYCommand = new RelayCommand(o => ShowMatrizEigenVectores(Enums.SideType.Vertical));
            MatrizEspectralYCommand = new RelayCommand(o => ShowMatrizEspectral(Enums.SideType.Vertical));
            MatrizMasasGeneralizadaYCommand = new RelayCommand(o => ShowMatrizMasasGeneralizada(Enums.SideType.Vertical));
            MatrizRigidecesGeneralizadaYCommand = new RelayCommand(o => ShowMatrizRigidecesGeneralizada(Enums.SideType.Vertical));
            VectorUnosYCommand = new RelayCommand(o => ShowVectorUnos());
            ParticipacionModalYCommand = new RelayCommand(o => ShowVectorParticipacionModal(Enums.SideType.Vertical));
            MatrizModalNormalizadoYCommand = new RelayCommand(o => ShowMatrizModalNormalizada(Enums.SideType.Vertical));
            VectorMasasEfectivasYCommand = new RelayCommand(o => ShowVectorMasasEfectivas(Enums.SideType.Vertical));
            MatrizFactPartMasasModalesYCommand = new RelayCommand(o => ShowMatrizFactParticipacionMasasModales(Enums.SideType.Vertical));
            ParticipacionMasasYCommand = new RelayCommand(o => ShowVectorParticipacionMasas(Enums.SideType.Vertical));
            EspectroDisenioYCommand = new RelayCommand(o => ShowEspectroDisenio(Enums.SideType.Vertical));
            VectorAceleracionesYCommand = new RelayCommand(o => ShowVectorAcceleraciones(Enums.SideType.Vertical));
            GammaYCommand = new RelayCommand(o => ShowGamma(Enums.SideType.Vertical));
            AceleracionPorModoYCommand = new RelayCommand(o => ShowAceleracionPorModo(Enums.SideType.Vertical));
            VectAceleracionPorModoYCommand = new RelayCommand(o => ShowVectAceleracionPorModo(Enums.SideType.Vertical));
            FuerzasFicticiasEquivPorModoYCommand = new RelayCommand(o => ShowFuerzasFicticiasEquivPorModo(Enums.SideType.Vertical));
            VectorFuerzasCortantsDisenioYCommand = new RelayCommand(o => ShowVectorFuerzasCortantesDisenio(Enums.SideType.Vertical));
            DesplazamientoAzoteaPorModoYCommand = new RelayCommand(o => ShowDesplazamientoAzoteaPorModo(Enums.SideType.Vertical));
            DesplazamientoEntrepisoPorModoYCommand = new RelayCommand(o => ShowDesplazamientoEntrepisoPorModo(Enums.SideType.Vertical));
            VectorAceleracionCombinadaYCommand = new RelayCommand(o => ShowVectorAceleracionCombinada(Enums.SideType.Vertical));
            VectFuerzasFicticiasEquivCombinadaYCommand = new RelayCommand(o => ShowVectorFuerzasFicticiasEquivCombinada(Enums.SideType.Vertical));
            VectFuerzasCortantesDisenioCombinadaYCommand = new RelayCommand(o => ShowVectorFuerzasCortantesDisenioCombinada(Enums.SideType.Vertical));
            VectDesplazamientosSSRSYCommand = new RelayCommand(o => ShowVectorDesplazamientosSSRS(Enums.SideType.Vertical));
            VectorFuerzasCortantsDisenioEstaticoYCommand = new RelayCommand(o => ShowVectorDeterminacionFuerzasCortantes(Enums.SideType.Vertical));
            DesplazamientosLateralesYCommand = new RelayCommand(o => ShowVectorDesplazamientosLaterales(Enums.SideType.Vertical));
            EstimPeriodoFundamentalEstructYCommand = new RelayCommand(o => ShowEstimacionPeriodoFundamentalEstructura(Enums.SideType.Vertical));
            FuerzasCortantesYCommand = new RelayCommand(o => ShowFuerzasCortantes(Enums.SideType.Vertical));

        }

        #endregion




        #region Metodos operacionales

        private void ShowAceleracionPorModo(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetAceleracionPorModo(side);
        }

        private void ShowVectAceleracionPorModo(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectoresDeAceleracionPorModo(side);
        }

        private void ShowFuerzasFicticiasEquivPorModo(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetFuerzasFicticiasEquivPorModo(side);
        }

        private void ShowDesplazamientoAzoteaPorModo(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.QVisible = "Visible";
            this.SideTemp = side;
        }

        private void ShowDesplazamientoEntrepisoPorModo(Enums.SideType side)
        {

            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetDesplazamientoEntrepisoPorModo(side);
        }

        private void ShowVectorAceleracionCombinada(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorAceleracionCombinada(side);
        }

        private void ShowVectorFuerzasFicticiasEquivCombinada(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorFuerzasFicticiasEquivCombinada(side);
        }

        private void ShowVectorFuerzasCortantesDisenioCombinada(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorFuerzasCortantesDisenioCombinada(side);
        }

        private void ShowVectorDesplazamientosSSRS(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorDesplazamientosCombinados(side);
        }

        private void EliminarEspectroDisenioData()
        {
            this.EspectroDisenio.Clear();
        }

        private void GuardarEspectroDisenioData(Enums.SideType side)
        {
            var data = this.DataService.SetEspectroDisenio(this.EspectroDisenio.ToList(), side);

            this.notificationViewModel.ShowNotification(data);

        }


        private void ShowVectorMasas()
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorMasas();

        }

        private void ShowVectorRigideces(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorRigidez(side);

        }

        private void ShowMatrizRigideces(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetMatrizRigideces(side);

        }

        private void ShowMatrizMasas(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetMatrizMasas(side);
        }

        private void ShowVectorPeriodosCirculares(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorPeriodosCirculares(side);
        }

        private void ShowVectorPeriodosNaturales(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorPeriodosNaturales(side);
        }

        private void ShowVectorFrecuencias(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorFrecuencias(side);
        }

        private void ShowMatrizEigenVectores(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetMatrizEigenVectoresNormalizados(side);
        }

        private void ShowMatrizEspectral(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetMatrizEspectral(side);
        }

        private void ShowMatrizMasasGeneralizada(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetMatrizMasasGeneralizada(side);
        }

        private void ShowMatrizRigidecesGeneralizada(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetMatrizRigidecesGeneralizada(side);
        }

        private void ShowVectorUnos()
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorUnos();
        }

        private void ShowVectorParticipacionModal(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorParticipacionModal(side);
        }

        private void ShowMatrizModalNormalizada(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetMatrizModalNormalizada(side);
        }

        private void ShowVectorMasasEfectivas(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorMasasEfectivas(side);
        }

        private void ShowMatrizFactParticipacionMasasModales(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetMatrizFactParticipacionMasasModales(side);
        }

        private void ShowVectorParticipacionMasas(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorParticipacionMasas(side);
        }

        private void ShowGamma(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorGamma(side);
        }

        private void ShowEspectroDisenio(Enums.SideType side)
        {

            this.HideStaticMethod();
            this.ShowSetDataGrid();
            var datos = this.DataService.GetEspectroDisenio();

            if (datos.Count > 0)
                this.EspectroDisenio = new ObservableCollection<EspectroDisenio>(datos);
            else
                this.EspectroDisenio = new ObservableCollection<EspectroDisenio>();


            this.EspectroDisenio.CollectionChanged += EspectroDisenio_CollectionChanged;
        }

        private void EspectroDisenio_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

        }

        private void ShowVectorAcceleraciones(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorAceleraciones(side);
        }

        private void ShowVectorFuerzasFicticiasEquivalentes(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorFuerzasFicticiasEquivalentes(side);
        }

        private void ShowVectorFuerzasCortantesDisenio(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorFuerzasCortantesDisenio(side);
        }

        private void ShowVectorDeterminacionFuerzasCortantes(Enums.SideType side)
        {
            this.ShowGetDataTable();
            ShowStaticMethod();
            this.SideTemp = side;
        }

        private void ShowVectorDesplazamientosLaterales(Enums.SideType side)
        {

            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetVectorDesplazamientosLaterales(side);
        }

        private void ShowEstimacionPeriodoFundamentalEstructura(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetEstimacionPeriodoFundamentalEstructura(side);
        }

        private void ShowFuerzasCortantes(Enums.SideType side)
        {
            this.ShowGetDataTable();
            this.HideStaticMethod();
            this.ContentDT = DataService.GetFuerzasCortantes(side);
        }

        private void ShowGetDataTable()
        {
            this.ContentDT.Clear();
            this.DTContentSet = "Hidden";
            this.DTContentGet = "Visible";
        }

        private void ShowSetDataGrid()
        {
            this.ContentDT.Clear();
            this.DTContentSet = "Visible";
            this.DTContentGet = "Hidden";
            this.IsEspectroDisenio = "Visible";
        }

        private void ShowStaticMethod()
        {

            this.IsStaticV = "Visible";
            this.IsEspectroDisenio = "Hidden";
            this.QVisible = "Visible";
        }

        private void HideStaticMethod()
        {
            this.IsStaticV = "Hidden";
            this.QVisible = "Hidden";
            this.IsEspectroDisenio = "Hidden";
        }
        #endregion




        #region Properties

        Enums.SideType SideTemp { get; set; }
        private double c_;
        public double c
        {
            get
            {
                return c_;
            }
            set
            {
                if (c_ != value)
                    c_ = value;

                if (c > 0 && Q > 0 & R > 0)
                    this.ContentDT = DataService.GetVectorDeterminacionFuerzasCortantes(c, Q, R, SideTemp);

                OnPropertyChanged("c");
            }
        }
        private double Q_;
        public double Q
        {
            get
            {
                return Q_;
            }
            set
            {
                if (value != Q_)
                    Q_ = value;

                if (QVisible == "Visible" && IsStaticV == "Visible")
                {
                    if (c > 0 && Q > 0 & R > 0)
                        this.ContentDT = DataService.GetVectorDeterminacionFuerzasCortantes(c, Q, R, SideTemp);
                }
                else if (QVisible == "Visible" && IsStaticV == "Hidden")
                {
                    this.ContentDT = DataService.GetDesplazamientoAzoteaPorModo(SideTemp, Q_);
                }

                OnPropertyChanged("Q");
            }
        }
        private double R_;
        public double R
        {
            get
            {
                return R_;
            }
            set
            {
                if (value != R_)
                    R_ = value;
                if (c > 0 && Q > 0 & R > 0)
                    this.ContentDT = DataService.GetVectorDeterminacionFuerzasCortantes(c, Q, R, SideTemp);

                OnPropertyChanged("R");
            }
        }

        private string IsEspectroDisenio_ = "Hidden";
        public string IsEspectroDisenio
        {
            get
            {
                return IsEspectroDisenio_;
            }
            set
            {
                if (value != IsEspectroDisenio_)
                    IsEspectroDisenio_ = value;
                OnPropertyChanged("IsEspectroDisenio");
            }
        }

        private string IsStaticV_ = "Hidden";
        public string IsStaticV
        {
            get
            {
                return IsStaticV_;
            }

            set
            {
                IsStaticV_ = value;
                OnPropertyChanged("IsStaticV");
            }
        }

        private string QVisible_ = "Hidden";
        public string QVisible
        {
            get
            {
                return QVisible_;
            }

            set
            {
                QVisible_ = value;
                OnPropertyChanged("QVisible");
            }
        }

        private Wall _SelectedWall;
        public Wall SelectedWall
        {
            get
            {
                return _SelectedWall;
            }
            set
            {
                if (value != _SelectedWall)
                    _SelectedWall = value;
                OnPropertyChanged("SelectedWall");
            }
        }

        private Structure Structure_;
        public Structure StructureP
        {
            get
            {
                return Structure_;
            }
            set
            {
                if (value != Structure_)
                    Structure_ = value;

                OnPropertyChanged("StructureP");
            }
        }

        private int SelectedIndex_ = 0;
        public int SelectedIndex
        {
            get
            {
                return SelectedIndex_;
            }
            set
            {
                SelectedIndex_ = value;

                OnPropertyChanged("SelectedIndex");
            }
        }

        private string Content_;
        public string Content
        {
            get
            {
                return Content_;
            }
            set
            {
                if (value != Content_)
                    Content_ = value;
                OnPropertyChanged("Content");
            }
        }

        private DataTable ContentDT_;
        public DataTable ContentDT
        {
            get
            {
                return ContentDT_;
            }
            set
            {
                ContentDT_ = value;
                OnPropertyChanged("ContentDT");
            }
        }

        public bool IsStatic_;
        public bool IsStatic
        {
            get
            {
                return DataService.ValidateMethod();
            }
            set
            {
                IsStatic_ = value;
                OnPropertyChanged("IsStatic");
            }
        }

        public ObservableCollection<Storey> _Storeys;
        public ObservableCollection<Storey> Storeys
        {
            get { return _Storeys; }
            set
            {
                if (value != _Storeys)
                    _Storeys = value;
                OnPropertyChanged("Storeys");
            }
        }




        public ObservableCollection<EspectroDisenio> EspectroDisenio_;
        public ObservableCollection<EspectroDisenio> EspectroDisenio
        {
            get { return EspectroDisenio_; }
            set
            {
                if (value != EspectroDisenio_)
                    EspectroDisenio_ = value;
                OnPropertyChanged("EspectroDisenio");
            }
        }

        private string DTContentSet_ = "Hidden";
        public string DTContentSet
        {
            get
            {
                return DTContentSet_;
            }
            set
            {
                if (value != DTContentSet_)
                    DTContentSet_ = value;
                OnPropertyChanged("DTContentSet");
            }
        }

        private string DTContentGet_ = "Visible";
        public string DTContentGet
        {
            get
            {
                return DTContentGet_;
            }
            set
            {
                if (value != DTContentGet_)
                    DTContentGet_ = value;
                OnPropertyChanged("DTContentGet");
            }
        }




        private readonly SeismicAnalysisService DataService = new SeismicAnalysisService();
        private NotificationViewModel notificationViewModel;
        private readonly PropertyChangedViewModel _mainViewModel;
        #endregion
    }//end of class
}//end of namespace
