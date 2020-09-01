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
    public class StructureVM : PropertyChangedViewModel
    {

        #region Properties
        private readonly PropertyChangedViewModel _mainViewModel;



        #endregion
        public StructureVM(PropertyChangedViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;


        }

    }
}
