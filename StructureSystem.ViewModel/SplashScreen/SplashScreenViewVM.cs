using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using StructureSystem.ViewModel.Shared;

namespace StructureSystem.ViewModel
{
    public class SplashScreenViewVM : PropertyChangedViewModel
    {
        BackgroundWorker _Worker;

        private double _CurrentProgress;
        private string _StatusMessage;

        public string StatusMessage
        {
            get { return _StatusMessage; }
            set
            {
                if (_StatusMessage != value)
                    _StatusMessage = value;
                OnPropertyChanged("StatusMessage");
            }
        }
        public double CurrentProgress
        {
            get { return _CurrentProgress; }
            private set
            {
                if (_CurrentProgress != value)
                {
                    _CurrentProgress = value;
                    OnPropertyChanged("CurrentProgress");
                }
            }
        }

        public SplashScreenViewVM()
        {
            StatusMessage = "Iniciando...";
            _Worker = new BackgroundWorker();
            _Worker.WorkerReportsProgress = true;
            _Worker.DoWork += DoWork;
            _Worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            _Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerCompleted);
            _Worker.RunWorkerAsync();
        }


        private void DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 15; i++)
            {
                Thread.Sleep(300);
                _Worker.ReportProgress(i * (100 / 5));
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _CurrentProgress = e.ProgressPercentage;
            OnPropertyChanged("CurrentProgress");
        }

       public void workerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _StatusMessage = "Bienvenido";
            OnPropertyChanged("StatusMessage");
        }


    }
}
