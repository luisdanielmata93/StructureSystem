using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.ViewModel.Shared;
using System.Collections.ObjectModel;
using System.Windows.Input;
using StructureSystem.Model;
using StructureSystem.Shared.BusinessRules;

namespace StructureSystem.ViewModel
{
    public class StructureVM : PropertyChangedViewModel
    {

        #region Properties
        private readonly PropertyChangedViewModel _mainViewModel;
        private BusinessRules data;


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


        #region Constructor
        public StructureVM(PropertyChangedViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            data = new BusinessRules();
            SetCommands();
        }

        #endregion


        #region Commands
        public ICommand SaveWallInfo { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        #endregion



        #region Set Command

        private void SetCommands()
        {
            SaveWallInfo = new RelayCommand(o => setWallInfo());
            UpdateCommand = new RelayCommand(o => doUpdate());
        }

        #endregion

        #region CommandMethods
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
            try
            {
                var dataTemp = data.GetStoreysSpecification();

                this.Storeys = new ObservableCollection<Storey>((List<Storey>)dataTemp.Data);

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        #endregion

    } //end of class
}//end of namespace
