using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using StructureSystem.ViewModel.Shared;

namespace StructureSystem.View.Views
{
    /// <summary>
    /// Lógica de interacción para DataWall.xaml
    /// </summary>
    public partial class DataWall : MetroWindow, IClosable
    {
        public DataWall()
        {
            InitializeComponent();

        }
    }
}
