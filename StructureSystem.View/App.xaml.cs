﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using StructureSystem.Shared;
using StructureSystem.Shared.Resources;
using System.Drawing;
using System.Threading;
using StructureSystem.View.SplashScreenView;
using MahApps.Metro.Controls;
using StructureSystem.ViewModel.Dialog;
using StructureSystem.ViewModel;
using StructureSystem.View.Views;

namespace StructureSystem.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //IDialogService dialogService = new DialogService(MainWindow);
            //dialogService.Register<DataWallVM, DataWall>();

            //var splashScreen = new SplashScreenWindow();
           // this.MainWindow = splashScreen;
           // splashScreen.Show();

            Task.Factory.StartNew(() =>
            {
                //Thread.Sleep(5000);

                this.Dispatcher.Invoke(() =>
                {
                    var mainWindow = new MainWindow();
                    this.MainWindow = mainWindow;
                    mainWindow.Show();

                   // splashScreen.Close();

                });
            });


        }

       
    }
}