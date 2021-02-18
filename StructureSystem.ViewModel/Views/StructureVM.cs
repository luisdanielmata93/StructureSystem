using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.ViewModel.Shared;
using System.Collections.ObjectModel;
using System.Windows.Input;
using StructureSystem.Model;
using StructureSystem.BusinessRules.Services;
using MahApps.Metro.Controls.Dialogs;

namespace StructureSystem.ViewModel
{
    public class StructureVM : PropertyChangedViewModel
    {

        #region Constructor
        public StructureVM(PropertyChangedViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            notificationViewModel = new NotificationViewModel();
            SetCommands();

            this._mainViewModel.ProcessCompleted += _mainViewModel_ProcessCompleted;

        }

        private void _mainViewModel_ProcessCompleted(object sender, bool e)
        {
            try
            {
                doUpdate();
            }
            catch (Exception ex)
            {

            }
        }

        #endregion


        #region Commands
        public ICommand SaveWallInfo { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand DeleteVCommand { get; private set; }
        public ICommand SaveChangeCommand { get; private set; }
        public ICommand RegisterWallsCommand { get;private set; }
        #endregion



        #region Set Command

        private void SetCommands()
        {
            SaveWallInfo = new RelayCommand(o => setWallInfo());
            UpdateCommand = new RelayCommand(o => doUpdate());
            SaveChangeCommand = new RelayCommand(o => doSaveChanges());
            RegisterWallsCommand = new RelayCommand(o => doWallRegister());
        }

        private void doWallRegister()
        {
           // window.showWindow(new DataWallVM());
        }

        #endregion

        #region CommandMethods

        private async Task<bool> CanUpdateStoreys()
        {
            bool result = true;
            bool hasWalls = true;
            foreach (var storey in Storeys)
            {
                if (storey.HorizontalWalls.Count <= 0 || storey.VerticalWalls.Count <= 0)
                    hasWalls = false;
            }

            if (!hasWalls)
            {
                this.dialogCoordinator = new DialogCoordinator();
                var resultMsg = await this.dialogCoordinator.ShowMessageAsync(this, "Se eliminaran los niveles sin pisos asignados. \n ¿Desea continuar?", "Se sobreescribirá la información guardada.", MessageDialogStyle.AffirmativeAndNegative);
                if (resultMsg == MessageDialogResult.Affirmative)
                {
                    result = true;
                }
                else
                    result = false;
            }
          
            return result;
        }
        private async void doSaveChanges()
        {
            if (!await CanUpdateStoreys())
                return;

            notificationViewModel.ShowNotification(structureData.SaveStoreys(Storeys.ToList()));
        }

        private void doUpdate()
        {
            FillData();

        }

        private bool setWallInfo()
        {

            return false;
        }

        #endregion

        #region Methods
        private void FillData()
        {

            var dataTemp = structureData.GetStoreys();

            this.Storeys = new ObservableCollection<Storey>((List<Storey>)dataTemp.Data);
        }

        #endregion



        #region Properties
       // private readonly WindowService window = new WindowService();
        private readonly PropertyChangedViewModel _mainViewModel;
        private readonly StructureDataService structureData = new StructureDataService();
        private NotificationViewModel notificationViewModel;

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

        public ObservableCollection<Storey> _Storeys;
        public ObservableCollection<Storey> Storeys
        {
            get { return _Storeys; }
            set
            {
                _Storeys = value;
                OnPropertyChanged("Storeys");
            }
        }
        private IDialogCoordinator dialogCoordinator;


        #endregion


    } //end of class
}//end of namespace
