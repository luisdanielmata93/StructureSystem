using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.ViewModel.Shared;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace StructureSystem.ViewModel
{
    public class DefinitionVM : PropertyChangedViewModel
    {

        #region Properties
        private readonly PropertyChangedViewModel _mainViewModel;

       

        #endregion


        #region Constructor
        public DefinitionVM(PropertyChangedViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            this.SetCommands();

        }
        #endregion


        #region Commands

        public ICommand SearchCommand { get; private set; }
        public ICommand NewProductCommand { get; private set; }
        public ICommand SaveEditionCommand { get; private set; }
        #endregion



        #region Set Command

        private void SetCommands()
        {
            SearchCommand = new RelayCommand(o => Search(), o => CanSearch());
            NewProductCommand = new RelayCommand(o => NewProduct());
            SaveEditionCommand = new RelayCommand(o => Save(), o => CanSave());
        }

        #endregion

        #region CommandMethods
        private bool CanSearch()
        {
            //return !string.IsNullOrEmpty(SearchText);
            return false;
        }

        private void Search()
        {
          //  var th = this.SearchText;

        }

        private void NewProduct()
        {
           // OpenAddProduct = true;
        }

        private bool CanSave()
        {
            //  return !(SelectedProduct is null);
            return false;
        }
        private void Save()
        {
          //  this.SelectedProduct = null;
        }
        #endregion



    }//end of class
}//end of namespace
