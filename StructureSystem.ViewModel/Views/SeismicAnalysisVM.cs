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
        public ICommand VectorFuerzasFicticiasEquivXCommand { get; private set; }
        public ICommand VectorFuerzasCortantsDisenioXCommand { get; private set; }
        public ICommand DetermFuerzasCortantesDisenioXCommand { get; private set; }
        public ICommand DesplazamientosLateralesXCommand { get; private set; }
        public ICommand EstimPeriodoFundamentalEstructXCommand { get; private set; }

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
        public ICommand VectorFuerzasFicticiasEquivYCommand { get; private set; }
        public ICommand VectorFuerzasCortantsDisenioYCommand { get; private set; }
        public ICommand DetermFuerzasCortantesDisenioYCommand { get; private set; }
        public ICommand DesplazamientosLateralesYCommand { get; private set; }
        public ICommand EstimPeriodoFundamentalEstructYCommand { get; private set; }
        #endregion


        #region Set Command

        private void SetCommands()
        {
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
            VectorFuerzasFicticiasEquivXCommand = new RelayCommand(o => ShowVectorFuerzasFicticiasEquivalentes(Enums.SideType.Horizontal));
            VectorFuerzasCortantsDisenioXCommand = new RelayCommand(o => ShowVectorFuerzasCortantesDisenio(Enums.SideType.Horizontal));
            DetermFuerzasCortantesDisenioXCommand = new RelayCommand(o => ShowVectorDeterminacionFuerzasCortantes(Enums.SideType.Horizontal));
            DesplazamientosLateralesXCommand = new RelayCommand(o => ShowVectorDesplazamientosLaterales(Enums.SideType.Horizontal));
            EstimPeriodoFundamentalEstructXCommand = new RelayCommand(o => ShowEstimacionPeriodoFundamentalEstructura(Enums.SideType.Horizontal));

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
            VectorFuerzasFicticiasEquivYCommand = new RelayCommand(o => ShowVectorFuerzasFicticiasEquivalentes(Enums.SideType.Vertical));
            VectorFuerzasCortantsDisenioYCommand = new RelayCommand(o => ShowVectorFuerzasCortantesDisenio(Enums.SideType.Vertical));
            DetermFuerzasCortantesDisenioYCommand = new RelayCommand(o => ShowVectorDeterminacionFuerzasCortantes(Enums.SideType.Vertical));
            DesplazamientosLateralesYCommand = new RelayCommand(o => ShowVectorDesplazamientosLaterales(Enums.SideType.Vertical));
            EstimPeriodoFundamentalEstructYCommand = new RelayCommand(o => ShowEstimacionPeriodoFundamentalEstructura(Enums.SideType.Vertical));

        }

        #endregion




        #region Metodos operacionales
        private void ShowVectorMasas()
        {
            this.ContentDT = DataService.GetVectorMasas();

        }

        private void ShowVectorRigideces(Enums.SideType side)
        {

            this.ContentDT = DataService.GetVectorRigidez(side);

        }

        private void ShowMatrizRigideces(Enums.SideType side)
        {

            this.ContentDT = DataService.GetMatrizRigideces(side);

        }

        private void ShowMatrizMasas(Enums.SideType side)
        {
            this.ContentDT = DataService.GetMatrizMasas(side);
        }

        private void ShowVectorPeriodosCirculares(Enums.SideType side)
        {
            this.ContentDT = DataService.GetVectorPeriodosCirculares(side);
        }

        private void ShowVectorPeriodosNaturales(Enums.SideType side)
        {
            this.ContentDT = DataService.GetVectorPeriodosNaturales(side);
        }

        private void ShowVectorFrecuencias(Enums.SideType side)
        {
            this.ContentDT = DataService.GetVectorFrecuencias(side);
        }

        private void ShowMatrizEigenVectores(Enums.SideType side)
        {
            this.ContentDT = DataService.GetMatrizEigenVectoresNormalizados(side);
        }

        private void ShowMatrizEspectral(Enums.SideType side)
        {
            this.ContentDT = DataService.GetMatrizEspectral(side);
        }

        private void ShowMatrizMasasGeneralizada(Enums.SideType side)
        {
            this.ContentDT = DataService.GetMatrizMasasGeneralizada(side);
        }

        private void ShowMatrizRigidecesGeneralizada(Enums.SideType side)
        {
            this.ContentDT = DataService.GetMatrizRigidecesGeneralizada(side);
        }

        private void ShowVectorUnos()
        {
            this.ContentDT = DataService.GetVectorUnos();
        }

        private void ShowVectorParticipacionModal(Enums.SideType side)
        {
            this.ContentDT = DataService.GetVectorParticipacionModal(side);
        }

        private void ShowMatrizModalNormalizada(Enums.SideType side)
        {
            this.ContentDT = DataService.GetMatrizModalNormalizada(side);
        }

        private void ShowVectorMasasEfectivas(Enums.SideType side)
        {
            this.ContentDT = DataService.GetVectorMasasEfectivas(side);
        }

        private void ShowMatrizFactParticipacionMasasModales(Enums.SideType side)
        {
            this.ContentDT = DataService.GetMatrizFactParticipacionMasasModales(side);
        }

        private void ShowVectorParticipacionMasas(Enums.SideType side)
        {
            this.ContentDT = DataService.GetVectorParticipacionMasas(side);
        }

        private void ShowEspectroDisenio(Enums.SideType side)
        {
            this.ContentDT = DataService.GetEspectroDisenio(side);
        }

        private void ShowVectorAcceleraciones(Enums.SideType side)
        {
            this.ContentDT = DataService.GetVectorAceleraciones(side);
        }

        private void ShowVectorFuerzasFicticiasEquivalentes(Enums.SideType side)
        {
            this.ContentDT = DataService.GetVectorFuerzasFicticiasEquivalentes(side);
        }

        private void ShowVectorFuerzasCortantesDisenio(Enums.SideType side)
        {
            this.ContentDT = DataService.GetVectorFuerzasCortantesDisenio(side);
        }

        private void ShowVectorDeterminacionFuerzasCortantes(Enums.SideType side)
        {
            this.ContentDT = DataService.GetVectorDeterminacionFuerzasCortantes(side);
        }

        private void ShowVectorDesplazamientosLaterales(Enums.SideType side)
        {
            this.ContentDT = DataService.GetVectorDesplazamientosLaterales(side);
        }

        private void ShowEstimacionPeriodoFundamentalEstructura(Enums.SideType side)
        {
            this.ContentDT = DataService.GetEstimacionPeriodoFundamentalEstructura(side);
        }

        #endregion



        #region Properties

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

        private readonly SeismicAnalysisService DataService = new SeismicAnalysisService();
        private NotificationViewModel notificationViewModel;
        private readonly PropertyChangedViewModel _mainViewModel;
        #endregion
    }//end of class
}//end of namespace
