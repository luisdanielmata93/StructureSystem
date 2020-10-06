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

namespace StructureSystem.ViewModel
{
    public class StructureVM : PropertyChangedViewModel
    {

        #region Constructor
        public StructureVM(PropertyChangedViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            SetCommands();
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


        private void doSaveChanges()
        {
            structureData.SaveStoreys(Storeys.ToList());
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
     
        
        #endregion


    } //end of class
}//end of namespace
