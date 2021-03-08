using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Threading;
using StructureSystem.View.SplashScreenView;
using MahApps.Metro.Controls;
using StructureSystem.ViewModel;
using StructureSystem.View.Views;

namespace StructureSystem.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += App_Startup;
        }

        private async void App_Startup(object sender, StartupEventArgs e)
        {
            var main = new MainWindow();
            var splash = new SplashScreenWindow();
            splash.Show();

            await InitializeAsync();

            main.Show();
            MainWindow = main;

            splash.Close();
        }

        private Task InitializeAsync()
        {
            return Task.Delay(5000);
        }

    }
}
