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
    public class SeismicAnalysisVM : PropertyChangedViewModel
    {
        #region Constructor
        public SeismicAnalysisVM(PropertyChangedViewModel mainViewModel)
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

        private void SetInitialData()
        {
            
            DataService.Test();
            //this.Storeys = new ObservableCollection<Storey>((List<Storey>)response.Data);

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

        private double _CcX;
        public double CcX
        {
            get { return _CcX; }
            set
            {
                if (value != _CcX)
                    _CcX = value;
                OnPropertyChanged("CcX");
            }
        }
        private double _CcY;
        public double CcY
        {
            get { return _CcY; }
            set
            {
                if (value != _CcY)
                    _CcY = value;
                OnPropertyChanged("CcY");
            }
        }

        private readonly SeismicAnalysisService DataService = new SeismicAnalysisService();
        private NotificationViewModel notificationViewModel;
        private readonly PropertyChangedViewModel _mainViewModel;
        #endregion
    }//end of class
}//end of namespace
