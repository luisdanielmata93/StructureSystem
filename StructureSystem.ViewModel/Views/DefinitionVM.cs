using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.ViewModel.Shared;
using System.Collections.ObjectModel;
using System.Windows.Input;
using StructureSystem.Model;
using StructureSystem.Shared.BusinessRules;
using Microsoft.Win32;
using Notifications.Wpf.Controls;
using Notifications.Wpf;

namespace StructureSystem.ViewModel
{
    public class DefinitionVM : PropertyChangedViewModel
    {
        private readonly INotificationManager _manager;
        private NotificationViewModel notificationViewModel;

        #region Constructor
        public DefinitionVM(PropertyChangedViewModel mainViewModel)
        {
            this.data = new BusinessRules();
            this._manager = new NotificationManager();

            notificationViewModel = new NotificationViewModel(_manager);

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
            ExportExcelCommand = new RelayCommand(o => Save(), o => CanSave());
            ExportPDFCommand = new RelayCommand(o => Save(), o => CanSave());
            ImportCommand = new RelayCommand(o => doImportProject());
        }

        #endregion

        #region CommandMethods

        public void doImportProject()
        {

            var dialog = new OpenFileDialog { InitialDirectory = _defaultPath };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
            if (string.IsNullOrEmpty(SelectedPath))
                return;



            var ProjectData = data.ImportDocument(SelectedPath);

            this.notificationViewModel.ShowNotification(ProjectData);

            var dataImport = (Dictionary<string, object>)ProjectData.Data;

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

        private bool CanSearch()
        {
            return !string.IsNullOrEmpty(Address);
        }

        private void Search()
        {
            //  var th = this.SearchText;

        }

        private void NewProject()
        {
            var resultData = data.CreateDocument(this);
            notificationViewModel.ShowNotification(resultData);
            if (resultData.Code == "3")
            {
                data.UpdateDocument(this);
            }
        }

        private bool CanSave()
        {

            return FieldsValidation();

        }

        private void Save()
        {
            //  this.SelectedProduct = null;
        }
        #endregion


        #region methods
        private void SetInitialData()
        {
            _defaultPath = @"C:\StructureSystem";
            var BaseDefinitionData = data.GetDefinitionData();

            this.Groups = BaseDefinitionData["groups"].Cast<Group>().ToList();
            this.Regulations = BaseDefinitionData["regulations"].Cast<Regulation>().ToList();
            this.Usages = BaseDefinitionData["usages"].Cast<Usage>().ToList();
            this.Tests = BaseDefinitionData["tests"].Cast<Test>().ToList();
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
        #endregion


        #region Properties
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



        private readonly PropertyChangedViewModel _mainViewModel;
        private BusinessRules data;
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
