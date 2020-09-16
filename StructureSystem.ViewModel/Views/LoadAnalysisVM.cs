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


namespace StructureSystem.ViewModel
{
    public class LoadAnalysisVM : PropertyChangedViewModel
    {
        #region Constructor
        public LoadAnalysisVM(PropertyChangedViewModel mainViewModel)
        {

            notificationViewModel = new NotificationViewModel();

            this._mainViewModel = mainViewModel;

            this.SetInitialData();

            this.SetCommands();

        }
        #endregion


        #region Commands

        public ICommand SearchCommand { get; private set; }
        public ICommand SaveProjectCommand { get; private set; }
        public ICommand ExportExcelCommand { get; private set; }
        public ICommand ExportPDFCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }

        #endregion


        #region Set Command

        private void SetCommands()
        {
          
        }

        #endregion


        #region Methods
        private void SetInitialData()
        {

        }

        #endregion


        #region Properties
        private NotificationViewModel notificationViewModel;
        private readonly PropertyChangedViewModel _mainViewModel;
        #endregion

    }//end of class
}//end of namespace
