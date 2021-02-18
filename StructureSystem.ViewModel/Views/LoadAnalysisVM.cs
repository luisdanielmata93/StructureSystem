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

namespace StructureSystem.ViewModel
{
    public class LoadAnalysisVM : PropertyChangedViewModel
    {

        #region Constructor
        public LoadAnalysisVM(PropertyChangedViewModel mainViewModel)
        {

            notificationViewModel = new NotificationViewModel();

            this._mainViewModel = mainViewModel;

            this.SetCommands();

            this._mainViewModel.ProcessCompleted += _mainViewModel_ProcessCompleted;
        }

       
        
        private void _mainViewModel_ProcessCompleted(object sender, bool e)
        {
            try
            {
                SetInitialData();
            }
            catch (Exception ex)
            {
            }
            
        }



        #endregion


        #region Commands

        public ICommand SaveLevelCommand { get; private set; }

        #endregion


        #region Set Command

        private void SetCommands()
        {
            SaveLevelCommand = new RelayCommand(o => OnSaveLevel());

        }

        private async void OnSaveLevel()
        {
            if (Storey == 0)
            {
                notificationViewModel.ShowAlert("No se tiene seleccionado ningun nivel.");
                return;
            }

            if (FlooringMaterial is null || FlooringMaterial.Id < 0)
            {
                notificationViewModel.ShowAlert("No se tiene seleccionado ningun sistema de entrepiso.");
                return;
            }


            if (IsSaved())
            {
                this.dialogCoordinator = new DialogCoordinator();
                var result = await this.dialogCoordinator.ShowMessageAsync(this, string.Concat("Este nivel ya se encuentra registrado. \n ¿Desea actualizar la información?"), "Se sobreescribirá la información guardada.", MessageDialogStyle.AffirmativeAndNegative);
                if (result != MessageDialogResult.Affirmative)
                {
                    return;
                }
            }

            if (await this.Save(Storey))
                notificationViewModel.ShowMessage("Inserción exitosa.");

            //BORRAR CAMPOOOSSS
        }

        #endregion

        #region Methods

        private async Task<bool> Save(int Storey)
        {
            bool result = true;
            try
            {
                switch (this.MaterialType)
                {
                    case Enums.MaterialType.MacisaEntrepiso:
                        DataService.Save(this.LosaMacizaEntrepisoP, this.MaterialType, Storey);
                        break;
                    case Enums.MaterialType.MacisaAzotea:
                        DataService.Save(this.LosaMacizaAzoteaP, this.MaterialType, Storey);
                        break;
                    case Enums.MaterialType.NervadaEntrepiso:
                        DataService.Save(this.LosaNervadaEntrepisoP, this.MaterialType, Storey);
                        break;
                    case Enums.MaterialType.NervadaAzotea:
                        DataService.Save(this.LosaNervadaAzoteaP, this.MaterialType, Storey);
                        break;
                    case Enums.MaterialType.ViguetaBovedillaEntrepiso:
                        DataService.Save(this.LosaViguetaBovedillaEntrepisoP, this.MaterialType, Storey);
                        break;
                    case Enums.MaterialType.ViguetaBovedillaAzotea:
                        DataService.Save(this.LosaViguetaBovedillaAzoteaP, this.MaterialType, Storey);
                        break;
                    case Enums.MaterialType.Otra:
                        DataService.Save(this.LosaOtroTipoP, this.MaterialType, Storey);
                        break;
                    default:
                        notificationViewModel.ShowAlert("No se tiene seleccionado ningun tipo de losa.");
                        result = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }



        private bool IsSaved()
        {
            bool result = (bool)DataService.IsSaved(Storey).Data;
            if (result)
                IsAlert = "Hidden";
            else
                IsAlert = "Visible";

            return result;

        }

        private void SetInitialData()
        {
            this.HiddenTabs();
            this.FlooringMaterials = (List<FlooringMaterial>)DataService.GetFlooringMaterials().Data;
            this.StoreyList = new ObservableCollection<int>();

        }

        private void SelectTabItem()
        {

            if (FlooringMaterial is null || FlooringMaterial.Id < 0)
                return;

            switch (FlooringMaterial.Id)
            {
                case 1:
                    if (IsEntrepiso)
                    {
                        this.HiddenTabs();
                        this.TabIndex = 0;
                        this.MaterialType = Enums.MaterialType.MacisaEntrepiso;
                        this.LosaMacizaEntrepisoP = new LosaMacizaEntrepiso();
                    }
                    else
                    {
                        this.HiddenTabs();
                        this.TabIndex = 1;
                        this.MaterialType = Enums.MaterialType.MacisaAzotea;
                        this.LosaMacizaAzoteaP = new LosaMacizaAzotea();
                    }
                    break;
                case 2:
                    if (IsEntrepiso)
                    {
                        this.HiddenTabs();
                        this.TabIndex = 2;
                        this.MaterialType = Enums.MaterialType.NervadaEntrepiso;
                        this.LosaNervadaEntrepisoP = new LosaNervadaEntrepiso();
                    }
                    else
                    {
                        this.HiddenTabs();
                        this.TabIndex = 3;
                        this.MaterialType = Enums.MaterialType.NervadaAzotea;
                        this.LosaNervadaAzoteaP = new LosaNervadaAzotea();
                    }
                    break;
                case 3:
                    if (IsEntrepiso)
                    {
                        this.HiddenTabs();
                        this.TabIndex = 4;
                        this.MaterialType = Enums.MaterialType.ViguetaBovedillaEntrepiso;
                        this.LosaViguetaBovedillaEntrepisoP = new LosaViguetaBovedillaEntrepiso();

                    }
                    else
                    {
                        this.HiddenTabs();
                        this.TabIndex = 5;
                        this.MaterialType = Enums.MaterialType.ViguetaBovedillaAzotea;
                        this.LosaViguetaBovedillaAzoteaP = new LosaViguetaBovedillaAzotea();
                    }
                    break;
                case 4:
                    if (IsEntrepiso)
                    {
                        this.HiddenTabs();
                        this.TabIndex = 6;
                        this.MaterialType = Enums.MaterialType.Otra;
                        this.LosaOtroTipoP = new LosaOtroTipo();
                    }
                    else
                    {
                        this.HiddenTabs();
                        this.TabIndex = 6;
                        this.MaterialType = Enums.MaterialType.Otra;
                        this.LosaOtroTipoP = new LosaOtroTipo();
                    }
                    break;
                default:
                    break;
            }
        }

        private void HiddenTabs()
        {
            this.LosaMacisaEntrepiso = "Hidden";
            this.LosaMacisaAzotea = "Hidden";
            this.LosaNervadaEntrepiso = "Hidden";
            this.LosaNervadaAzotea = "Hidden";
            this.ViguetaBovedillaEntrepiso = "Hidden";
            this.ViguetaBovedillaAzotea = "Hidden";
            this.Otra = "Hidden";
        }


        private ObservableCollection<int> GetStoreys()
        {
            ObservableCollection<int> result = new ObservableCollection<int>((List<int>)DataService.GetStoreyList().Data);

            return result;
        }
        #endregion

        #region Properties
        private IDialogCoordinator dialogCoordinator;
        private NotificationViewModel notificationViewModel;
        private Enums.MaterialType MaterialType { get; set; }


        private bool IsLoadProject_;
        public bool IsLoadProject
        {
            get
            {
                return IsLoadProject_;
            }
            set
            {
                if (value != IsLoadProject_)
                {
                    IsLoadProject_ = value;
                }
                OnPropertyChanged("IsLoadProject");

            }
        }


        private string _IsAlert = "Visible";
        public string IsAlert
        {
            get
            {
                return _IsAlert;
            }
            set
            {
                if (value != _IsAlert)
                    _IsAlert = value;

                OnPropertyChanged("IsAlert");
            }
        }

        private int _TabIndex = -1;
        public int TabIndex
        {
            get { return _TabIndex; }
            set
            {
                if (value != _TabIndex)
                    _TabIndex = value;
                OnPropertyChanged("TabIndex");
            }
        }

        private bool _IsEntrepiso;
        public bool IsEntrepiso
        {
            get { return _IsEntrepiso; }
            set
            {
                _IsEntrepiso = true;

                _IsAzotea = false;
                this.SelectTabItem();
                OnPropertyChanged("IsAzotea");
                OnPropertyChanged("IsEntrepiso");


            }
        }
        private bool _IsAzotea;
        public bool IsAzotea
        {
            get { return _IsAzotea; }
            set
            {
                _IsAzotea = true;

                _IsEntrepiso = false;
                this.SelectTabItem();
                OnPropertyChanged("IsEntrepiso");
                OnPropertyChanged("IsAzotea");

            }
        }

        private int _Storey;
        public int Storey
        {
            get
            { return _Storey; }
            set
            {
                _Storey = value;
                IsSaved();
                OnPropertyChanged("Storey");
            }
        }


        private ObservableCollection<int> _storeyList;
        public ObservableCollection<int> StoreyList
        {
            get
            {
                return GetStoreys();
            }
            set { if (value != _storeyList) _storeyList = value; OnPropertyChanged("StoreyList"); }
        }


        private LosaMacizaEntrepiso LosaMacizaEntrepisoP_;
        public LosaMacizaEntrepiso LosaMacizaEntrepisoP
        {
            get
            {
                return LosaMacizaEntrepisoP_;
            }
            set
            {
                if (value != LosaMacizaEntrepisoP_)
                    LosaMacizaEntrepisoP_ = value;
                OnPropertyChanged("LosaMacizaEntrepisoP");
            }
        }

        private LosaMacizaAzotea LosaMacizaAzoteaP_;
        public LosaMacizaAzotea LosaMacizaAzoteaP
        {
            get
            {
                return LosaMacizaAzoteaP_;
            }
            set
            {
                if (value != LosaMacizaAzoteaP_)
                    LosaMacizaAzoteaP_ = value;
                OnPropertyChanged("LosaMacizaAzoteaP");
            }
        }

        private LosaNervadaEntrepiso LosaNervadaEntrepisoP_;
        public LosaNervadaEntrepiso LosaNervadaEntrepisoP
        {
            get
            {
                return LosaNervadaEntrepisoP_;
            }
            set
            {
                if (value != LosaNervadaEntrepisoP_)
                    LosaNervadaEntrepisoP_ = value;
                OnPropertyChanged("LosaNervadaEntrepisoP");
            }
        }

        private LosaNervadaAzotea LosaNervadaAzoteaP_;
        public LosaNervadaAzotea LosaNervadaAzoteaP
        {
            get
            {
                return LosaNervadaAzoteaP_;
            }
            set
            {
                if (value != LosaNervadaAzoteaP_)
                    LosaNervadaAzoteaP_ = value;
                OnPropertyChanged("LosaNervadaAzoteaP");
            }
        }

        private LosaViguetaBovedillaEntrepiso LosaViguetaBovedillaEntrepisoP_;
        public LosaViguetaBovedillaEntrepiso LosaViguetaBovedillaEntrepisoP
        {
            get
            {
                return LosaViguetaBovedillaEntrepisoP_;
            }
            set
            {
                if (value != LosaViguetaBovedillaEntrepisoP_)
                    LosaViguetaBovedillaEntrepisoP_ = value;
                OnPropertyChanged("LosaViguetaBovedillaEntrepisoP");
            }
        }

        private LosaViguetaBovedillaAzotea LosaViguetaBovedillaAzoteaP_;
        public LosaViguetaBovedillaAzotea LosaViguetaBovedillaAzoteaP
        {
            get
            {
                return LosaViguetaBovedillaAzoteaP_;
            }
            set
            {
                if (value != LosaViguetaBovedillaAzoteaP_)
                    LosaViguetaBovedillaAzoteaP_ = value;
                OnPropertyChanged("LosaViguetaBovedillaAzoteaP");
            }
        }

        private LosaOtroTipo LosaOtroTipoP_;
        public LosaOtroTipo LosaOtroTipoP
        {
            get
            {
                return LosaOtroTipoP_;
            }
            set
            {
                if (value != LosaOtroTipoP_)
                    LosaOtroTipoP_ = value;
                OnPropertyChanged("LosaOtroTipoP");
            }
        }

        private FlooringMaterial _flooringMaterial;
        public FlooringMaterial FlooringMaterial
        {
            get
            {
                return _flooringMaterial;
            }
            set
            {
                if (value != _flooringMaterial)
                {
                    _flooringMaterial = value;
                    this.IsEntrepiso = true;
                }

                SelectTabItem();

                OnPropertyChanged("FlooringMaterial");
            }
        }

        private List<FlooringMaterial> _flooringMaterials;
        public List<FlooringMaterial> FlooringMaterials
        {
            get { return _flooringMaterials; }
            set
            {
                if (value != _flooringMaterials)
                    _flooringMaterials = value;
                OnPropertyChanged("FlooringMaterials");
            }
        }

        private readonly LoadAnalysisService DataService = new LoadAnalysisService();
        private readonly PropertyChangedViewModel _mainViewModel;
        #endregion

        #region VisibleProperties
        private string _LosaMacisaEntrepiso = "Hidden";
        public string LosaMacisaEntrepiso
        {
            get { return _LosaMacisaEntrepiso; }
            set
            {
                _LosaMacisaEntrepiso = value;
                OnPropertyChanged("LosaMacisaEntrepiso");
            }
        }

        private string _LosaMacisaAzotea = "Hidden";
        public string LosaMacisaAzotea
        {
            get
            { return _LosaMacisaAzotea; }
            set
            {
                _LosaMacisaAzotea = value;
                OnPropertyChanged("LosaMacisaAzotea");
            }
        }

        private string _LosaNervadaEntrepiso = "Hidden";
        public string LosaNervadaEntrepiso
        {
            get { return _LosaNervadaEntrepiso; }
            set
            {
                _LosaNervadaEntrepiso = value;
                OnPropertyChanged("LosaNervadaEntrepiso");
            }
        }

        private string _LosaNervadaAzotea = "Hidden";
        public string LosaNervadaAzotea
        {
            get { return _LosaNervadaAzotea; }
            set
            {
                _LosaNervadaAzotea = value;
                OnPropertyChanged("LosaNervadaAzotea");
            }
        }

        private string _ViguetaBovedillaEntrepiso = "Hidden";
        public string ViguetaBovedillaEntrepiso
        {
            get { return _ViguetaBovedillaEntrepiso; }

            set
            {
                _ViguetaBovedillaEntrepiso = value;
                OnPropertyChanged("ViguetaBovedillaEntrepiso");
            }
        }

        private string _ViguetaBovedillaAzotea = "Hidden";
        public string ViguetaBovedillaAzotea
        {
            get { return _ViguetaBovedillaAzotea; }
            set
            {
                _ViguetaBovedillaAzotea = value;
                OnPropertyChanged("ViguetaBovedillaAzotea");
            }
        }

        private string _Otra = "Hidden";
        public string Otra
        {
            get
            { return _Otra; }
            set
            {
                _Otra = value; OnPropertyChanged("Otra");
                ;
            }
        }
        #endregion


    }//end of class
}//end of namespace
