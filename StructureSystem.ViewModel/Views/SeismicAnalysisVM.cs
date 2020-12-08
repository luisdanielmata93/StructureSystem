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
            //this.ContentDT.Columns.Add("col0");
            //this.ContentDT.Columns.Add("col1");
            //this.ContentDT.Columns.Add("col2");

            //this.ContentDT.Rows.Add("data00", "data01", "data02");
            //this.ContentDT.Rows.Add("data10", "data11", "data22");
            //this.ContentDT.Rows.Add("data20", "data21", "data22");


            this.SetCommands();

        }
        #endregion
        #region Commands

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
        }

        #endregion

        #region Matrices
        private void ShowMatrizRigideces(Enums.SideType side)
        {

            this.ContentDT = DataService.GetMatrizRigideces(side);

        }

        private void ShowMatrizMasas(Enums.SideType side)
        {
            this.ContentDT = DataService.GetMatrizMasas(side);
        }

        #endregion


        #region Vectores
        private void ShowVectorMasas()
        {
            this.ContentDT = DataService.GetVectorMasas();

        }

        private void ShowVectorRigideces(Enums.SideType side)
        {

            this.ContentDT = DataService.GetVectorRigidez(side);

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
                DataService.GetInitialData();
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
