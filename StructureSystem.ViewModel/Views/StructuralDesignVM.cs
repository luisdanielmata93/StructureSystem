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
using System.ComponentModel;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

namespace StructureSystem.ViewModel
{
    public class StructuralDesignVM : PropertyChangedViewModel
    {
        #region Constructor

        public StructuralDesignVM(PropertyChangedViewModel mainViewModel)
        {
            notificationViewModel = new NotificationViewModel();

            this._mainViewModel = mainViewModel;
            this.SetCommands();
            this._mainViewModel.ProcessCompleted += _mainViewModel_ProcessCompleted; ;

        }

        private void _mainViewModel_ProcessCompleted(object sender, bool e)
        {
            setInitialData();
        }



        #endregion

        #region Commands

        public ICommand UpdateCommand { get; private set; }
        public ICommand GraphicCommand { get; private set; }
        #endregion


        #region Set Command

        private void SetCommands()
        {
            UpdateCommand = new RelayCommand(o => setInitialData());
            GraphicCommand = new RelayCommand(o => ShowGraph());

        }
        private void ShowGraph()
        {
            if (SelectedWall != null)
            {
                DataService.Graficar(SelectedWall);
                SelectedWall = null;
            }
            else
                notificationViewModel.ShowAlert("Seleccione un muro para visualizar su gráfica. ");
        }

        private void setInitialData()
        {
            try
            {
                this.Storeys = new ObservableCollection<Storey>((List<Storey>)DataService.GetStructure().Storeys);
                
            }
            catch (Exception ex)
            {

            }
        }

        #endregion


        private void Refresh()
        {
            try
            {
                var st = new ObservableCollection<Storey>((List<Storey>)DataService.Update(this.Storeys.ToList()).Storeys);
                this.Storeys = st;

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
                {
                    _SelectedWall = value;
                    Refresh();
                    OnPropertyChanged("SelectedWall");
                }
            }
        }



        public ObservableCollection<Storey> _Storeys;
        public ObservableCollection<Storey> Storeys
        {
            get { return _Storeys; }
            set
            {
                if (value != _Storeys)
                {                   
                    _Storeys = value;                  
                    OnPropertyChanged("Storeys");
                }

            }
        }


        private readonly StructuralDesignService DataService = new StructuralDesignService();
        private NotificationViewModel notificationViewModel;
        private readonly PropertyChangedViewModel _mainViewModel;
        #endregion


    }//end of class
}//end of namespace
