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

namespace StructureSystem.ViewModel
{
    public class DefinitionVM : PropertyChangedViewModel
    {

        #region Constructor
        public DefinitionVM(PropertyChangedViewModel mainViewModel)
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
            SearchCommand = new RelayCommand(o => Search(), o => CanSearch());
            SaveProjectCommand = new RelayCommand(o => NewProject(), o => CanSave());
            ExportExcelCommand = new RelayCommand(o => ExportExcel(), o => CanSave());
            ExportPDFCommand = new RelayCommand(o => ExportPDF(), o => CanSave());
            ImportCommand = new RelayCommand(o => ImportProject());
        }

        #endregion

        #region CommandMethods

        public void ImportProject()
        {

            SetImportDocument();
            if (string.IsNullOrEmpty(SelectedPath))
                return;

            var InitialProjectData = DocumentData.ImportDocument(SelectedPath);

            this.notificationViewModel.ShowNotification(InitialProjectData);

            SetImportData(InitialProjectData);
           
        }

        private void NewProject()
        {

                var resultData = DocumentData.CreateDocument(this);

                if (resultData.Error && resultData.ActionType == Enums.ActionType.Update)
                    UpdateProject();
                else
                    notificationViewModel.ShowNotification(resultData);

            this.IsAlert = "Hidden";
        }

        private async void UpdateProject()
        {
            this.dialogCoordinator = new DialogCoordinator();
            var result = await this.dialogCoordinator.ShowMessageAsync(this, string.Concat("Ya existe un proyecto con el nombre ",this.ProjectName," \n ¿Desea actualizar el proyecto existente?"), "Se sobreescribirá la información guardada.", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                var updateResult = DocumentData.UpdateDocument(this);
                notificationViewModel.ShowNotification(updateResult);
            }
        }


        private void ExportExcel()
        {
            OnPropertyChanged();
        }

        private void ExportPDF()
        {

        }


        private void Search()
        {
            //  var th = this.SearchText;

        }

   

        #endregion


        #region methods

        private void SetImportData(OperationResult InitialProjectData)
        {
            var dataImport = (Dictionary<string, object>)InitialProjectData.Data;

            this.ClientName = dataImport["Client"].ToString();
            this.ProjectName = dataImport["Project"].ToString();
            this.Address = dataImport["Address"].ToString();
            this.Storeys = Convert.ToInt32(dataImport["Storeys"]);
            this.Surface = Convert.ToInt32(dataImport["Surface"]);
            this.Regulation = Regulations.Find(x => x.Name == dataImport["Regulation"].ToString());
            this.Group = Groups.Find(x => x.Name == dataImport["Group"].ToString());
            this.Usage = Usages.Find(x => x.Name == dataImport["Usage"].ToString());
            this.Test = Tests.Find(x => x.Name == dataImport["Test"].ToString());
            this.StartDate = Convert.ToDateTime(dataImport["Date"]);

            this.IsAlert = "Hidden";
        }

        private void SetImportDocument()
        {
            var dialog = new OpenFileDialog { InitialDirectory = _defaultPath };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
        }

        private void SetInitialData()
        {
            _defaultPath = DocumentData.GetDocumentPath();

            var BaseDefinitionData = DocumentData.GetConfigElements();

            IDictionary<string, IList<object>> Data = (IDictionary<string, IList<object>>)BaseDefinitionData.Data;
            this.Groups = Data["groups"].Cast<Group>().ToList();
            this.Regulations = Data["regulations"].Cast<Regulation>().ToList();
            this.Usages = Data["usages"].Cast<Usage>().ToList();
            this.Tests = Data["tests"].Cast<Test>().ToList();

        }

        private bool FieldsValidation()
        {
            bool result = false;
            if (!string.IsNullOrEmpty(ClientName) && !string.IsNullOrEmpty(ProjectName) && !string.IsNullOrEmpty(Address) &&
                (Storeys > 0) && (Surface > 0) && (Regulation != null) && (Group != null) && (Usage != null) && (Test != null))
            {
                result = true;
            }

            return result;

        }

        private bool CanSearch()
        {
            return !string.IsNullOrEmpty(Address);
        }

        private bool CanSave()
        {

            return FieldsValidation();

        }

        #endregion


        #region Properties

        private IDialogCoordinator dialogCoordinator;
        private NotificationViewModel notificationViewModel;
        private readonly PropertyChangedViewModel _mainViewModel;
        private readonly GeneralDataService DocumentData = new GeneralDataService();

        private string _selectedPath;
        public string SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                _selectedPath = value;
                OnPropertyChanged("SelectedPath");
            }
        }

        private string _defaultPath;

        private string _IsAlert;
        public string IsAlert
        {
            get { return _IsAlert; }
            set
            {
                if (value != _IsAlert)
                    _IsAlert = value;
                OnPropertyChanged("IsAlert");
            }
        }

        private string _clientName;
        public string ClientName
        {
            get
            {
                return _clientName;
            }
            set
            {
                if (value != _clientName)
                    _clientName = value;
                OnPropertyChanged("ClientName");
            }
        }

        private string _projectName;
        public string ProjectName
        {
            get
            {
                return _projectName;
            }
            set
            {
                if (value != _projectName)
                    _projectName = value;
                OnPropertyChanged("ProjectName");
            }
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set
            {
                if (value != _Address)
                    _Address = value;
                OnPropertyChanged("Address");
            }
        }

        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged("StartDate"); }
        }

        private int _storeys;
        public int Storeys
        {
            get { return _storeys; }
            set
            {
                if (value != _storeys)
                    _storeys = value;
                OnPropertyChanged("Storeys");
            }
        }

        private double _Surface;
        public double Surface
        {
            get { return _Surface; }

            set
            {
                if (value != _Surface)
                    _Surface = value;
                OnPropertyChanged("Surface");
            }
        }


        private Regulation _Regulation;
        public Regulation Regulation
        {
            get { return _Regulation; }
            set
            {
                if (value != _Regulation)
                    _Regulation = value;
                OnPropertyChanged("Regulation");
            }
        }


        private List<Regulation> _Regulations;
        public List<Regulation> Regulations
        {
            get { return _Regulations; }
            set
            {
                if (value != _Regulations)
                    _Regulations = value;
                OnPropertyChanged("Regulations");
            }
        }

        private Group _Group;
        public Group Group
        {
            get { return _Group; }
            set
            {
                if (value != _Group)
                    _Group = value;
                OnPropertyChanged("Group");

            }
        }

        private List<Group> _Groups;
        public List<Group> Groups
        {
            get { return _Groups; }
            set
            {
                if (value != _Groups)
                    _Groups = value;
                OnPropertyChanged("Groups");
            }
        }


        private Usage _Usage;
        public Usage Usage
        {
            get { return _Usage; }
            set
            {
                if (value != _Usage)
                    _Usage = value;
                OnPropertyChanged("Usage");
            }
        }

        private List<Usage> _Usages;
        public List<Usage> Usages
        {
            get { return _Usages; }
            set
            {
                if (value != _Usages)
                    _Usages = value;
                OnPropertyChanged("Usages");
            }
        }


        private Test _Test;
        public Test Test
        {
            get { return _Test; }
            set
            {
                if (value != _Test)
                    _Test = value;
                OnPropertyChanged("Test");
            }
        }


        private List<Test> _Tests;
        public List<Test> Tests
        {
            get { return _Tests; }
            set
            {
                if (value != _Tests)
                    _Tests = value;
                OnPropertyChanged("Tests");
            }
        }

        #endregion

    }//end of class
}//end of namespace
