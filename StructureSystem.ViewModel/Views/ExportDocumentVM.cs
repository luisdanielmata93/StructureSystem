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
using System.Windows.Forms;
using MahApps.Metro.Controls.Dialogs;
using StructureSystem.BusinessRules.Services;
namespace StructureSystem.ViewModel
{
    public class ExportDocumentVM : PropertyChangedViewModel
    {


        public ExportDocumentVM(PropertyChangedViewModel mainViewModel)
        {
            notificationViewModel = new NotificationViewModel();
            this._mainViewModel = mainViewModel;
            this.SetCommands();
            this.DocumentPath = DocumentData.GetPathToSave();
            this._mainViewModel.ProcessCompleted += _mainViewModel_ProcessCompleted;
        }

        #region Eventos
        private void _mainViewModel_ShowLoadingAnimation(object sender, bool e)
        {
        }

        private void _mainViewModel_HideLoadingAnimation(object sender, bool e)
        {
        }


        private void _mainViewModel_ProcessCompleted(object sender, bool e)
        {

        }
        #endregion


        #region Commands  
        public ICommand ExportPDFCommand { get; private set; }
        public ICommand SavePathCommand { get; private set; }
        #endregion


        #region Set Command

        private void SetCommands()
        {
            ExportPDFCommand = new RelayCommand(o => ExportPDF(), o => canExport());
            SavePathCommand = new RelayCommand(o => SelectPath());
        }

        #endregion


        #region Methods
        private void SelectPath()
        {

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    this.DocumentPath = fbd.SelectedPath;
                    DocumentData.SetPathToSave(DocumentPath);
                }
            }

        }


        public async void FooProgress(int Option)
        {
            this.dialogCoordinator = new DialogCoordinator();
            ProgressDialogController controller;
            switch (Option)
            {
                case 1:
                    controller = await this.dialogCoordinator.ShowProgressAsync(this, "Exportando documento", "Se esta generando el documento a exportar, por favor espere...");

                    controller.SetIndeterminate();

                    var ImpResult = await Task.Run(Export);
                    await controller.CloseAsync();
                    notificationViewModel.ShowNotification(ImpResult);
                    break;               
                default:
                    break;
            }

        }



        private bool canExport()
        {
            bool result = false;
            if (!string.IsNullOrEmpty(DocumentPath))
                result = true;
            return result;
        }

        private void ExportPDF()
        {

            try
            {
                FooProgress(1);
            }
            catch (Exception ex)
            {
            }

        }
  
        private async Task<OperationResult> Export()
        {
            OperationResult result = new OperationResult();
            try
            {
                result = DocumentData.Export();
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion

        #region Properties
        private IDialogCoordinator dialogCoordinator;
        private NotificationViewModel notificationViewModel;
        private readonly PropertyChangedViewModel _mainViewModel;
        private readonly ExportDocumentService DocumentData = new ExportDocumentService();

        private string DocumentPath_;
        public string DocumentPath
        {
            get
            { return DocumentPath_; }
            set
            {
                if (value != DocumentPath_)
                {
                    DocumentPath_ = value;
                    OnPropertyChanged("DocumentPath");
                }
            }
        }
        #endregion

    }//end of class
}// end of namespace
