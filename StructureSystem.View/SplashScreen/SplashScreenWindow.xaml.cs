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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;

namespace StructureSystem.View.SplashScreenView
{
    /// <summary>
    /// Lógica de interacción para SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : Window, IDisposable
    {
        public SplashScreenWindow()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
