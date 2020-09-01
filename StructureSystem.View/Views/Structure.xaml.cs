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

namespace StructureSystem.View.Views
{
    /// <summary>
    /// Lógica de interacción para Structure.xaml
    /// </summary>
    public partial class Structure : UserControl
    {
        public Structure()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataWall dataWall = new DataWall();
            dataWall.Show();
        }
    }
}
