using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.ViewModel.Shared;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using StructureSystem.ViewModel;
using Microsoft.Win32;
using System.Windows.Input;
using StructureSystem.BusinessRules.Services;

namespace StructureSystem.ViewModel
{
    public class MainWindowVM : PropertyChangedViewModel
    {
        #region Properties

        private HamburgerMenuItemCollection _menuItems;
        private HamburgerMenuItemCollection _menuOptionItems;


        #endregion


        #region Constructor
        public MainWindowVM()
        {
            this.CreateMenuItems();
            SetCommands();
        }
        #endregion


        #region Commands
        public ICommand ImportCommand { get; private set; }
        #endregion



        #region Set Command

        private void SetCommands()
        {
        }

        #endregion
        

        #region Menu Config
        public void CreateMenuItems()
        {
            MenuItems = new HamburgerMenuItemCollection
            {
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.BallotOutline},
                    Label = "Definición",
                    ToolTip = "Definición general del proyecto",
                   Tag = new DefinitionVM(this)
                },
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.City},
                    Label = "Estructura",
                    ToolTip = "Definición de estructura y datos de muro",
                    Tag = new StructureVM(this)
                },
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.ChartAreaspline},
                    Label = "Analisis de carga",
                    ToolTip = "Analisis de carga",
                    Tag = new LoadAnalysisVM(this)
                },
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.ChartSankey},
                    Label = "Analisis sismico",
                    ToolTip = "Analisis sismico de la estructura",
                    Tag = new SeismicAnalysisVM(this)
                },
                 new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.GraphOutline},
                    Label = "Graficación",
                    ToolTip = "Graficación de estructuras",
                   // Tag = new SettingsViewModel(this)
                }
            };

            MenuOptionItems = new HamburgerMenuItemCollection
            {
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.Help},
                    Label = "Ayuda",
                    ToolTip = "Centro de ayuda.",
                   // Tag = new AboutViewModel(this)
                }
            };
        }

        public HamburgerMenuItemCollection MenuItems
        {
            get { return _menuItems; }
            set
            {
                if (Equals(value, _menuItems)) return;
                _menuItems = value;
                OnPropertyChanged();
            }
        }

        public HamburgerMenuItemCollection MenuOptionItems
        {
            get { return _menuOptionItems; }
            set
            {
                if (Equals(value, _menuOptionItems)) return;
                _menuOptionItems = value;
                OnPropertyChanged();
            }
        }
        #endregion


    }//End of class
}
