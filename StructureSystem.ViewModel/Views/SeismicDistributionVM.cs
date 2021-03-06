﻿using System;
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
    public class SeismicDistributionVM : PropertyChangedViewModel
    {
        #region Constructor
        public SeismicDistributionVM(PropertyChangedViewModel mainViewModel)
        {
            notificationViewModel = new NotificationViewModel();

            this._mainViewModel = mainViewModel;

            this._mainViewModel.ProcessCompleted += _mainViewModel_ProcessCompleted;
            this.SetCommands();
        }

        private void _mainViewModel_ProcessCompleted(object sender, bool e)
        {
            try
            {
                setInitialData();
            }
            catch (Exception ex)
            {

            }
        }
      
        
        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }
        public ICommand SaveProjectCommand { get; private set; }


        #endregion


        #region Set Command

        private void SetCommands()
        {
            SearchCommand = new RelayCommand(o => setInitialData());
        }

        #endregion


        private void setInitialData()
        {
            try
            {
                this.StructureP = DataService.GetStructure();
                this.Storeys = new ObservableCollection<Storey>((List<Storey>)StructureP.Storeys);
            }
            catch (Exception ex)
            {

            }
        }




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


        private readonly SeismicDistributionService DataService = new SeismicDistributionService();
        private NotificationViewModel notificationViewModel;
        private readonly PropertyChangedViewModel _mainViewModel;
        #endregion
    }//end of class
}//end of namespace
