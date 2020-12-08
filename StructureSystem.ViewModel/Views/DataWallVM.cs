using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.ViewModel.Shared;
using System.Collections.ObjectModel;
using System.Windows.Input;
using StructureSystem.Model;
using StructureSystem.BusinessRules.Services;
using Caliburn.Micro;

namespace StructureSystem.ViewModel
{
    public class DataWallVM : PropertyChangedViewModel
    {

        #region Constructor

        public DataWallVM()
        {
            InitialData();
            SetCommands();
        }
        #endregion


        #region Commands
        public ICommand InitLevelCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand NextStoreyCommand { get; private set; }
        public ICommand SaveHorizontalCommand { get; private set; }
        public ICommand SaveVerticalCommand { get; private set; }

        ICommand finishCommand;
        public ICommand FinishCommand => finishCommand ?? (finishCommand = new RelayCommand(FinishRegister));

        #endregion



        #region Set Command

        private void SetCommands()
        {
            PreviousCommand = new RelayCommand(o => OnPrevious());
            NextCommand = new RelayCommand(o => OnNextTab());
            NextStoreyCommand = new RelayCommand(o => OnNextStorey(), o => CanNewRegister());
            SaveHorizontalCommand = new RelayCommand(o => OnSaveHorizontalWall());
            SaveVerticalCommand = new RelayCommand(o => OnSaveVerticalWall());
            InitLevelCommand = new RelayCommand(o => InitRegister());
        }

        #endregion

        #region CommandMethods

        private void OnPrevious()
        {
            if (this.SelectedTag == 2)
                this.SelectedTag--;
        }

        private void InitRegister()
        {
            SelectedTag = 1;
            VerticalTab = "Visible";
            HorizontalTab = "Visible";
        }

        private void OnSaveHorizontalWall()
        {
            Data.CreateHorizontalWall(this);

            Refresh();
        }

        private void OnSaveVerticalWall()
        {
            Data.CreateVerticalWall(this);

            Refresh();
        }

        private void OnNextTab()
        {
            if (this.SelectedTag == 1)
                this.SelectedTag++;

            this.Refresh();
        }

        private void OnNextStorey()
        {
            Storey++;
            SelectedTag = 0;
            VerticalTab = "Hidden";
            HorizontalTab = "Hidden";
            this.Refresh();
        }


        private void Refresh()
        {
            //this.Length = 0;
            //this.TributaryArea = 0;
            //this.Thickness = 0;
            //this.Height = 0;
            //this.PositionX = 0;
            //this.PositionY = 0;
            OnPropertyChanged("CountH");
            OnPropertyChanged("CountV");
        }
        #endregion


        #region Methods


        private bool CanNewRegister()
        {
            if (Storeys == Storey)
            {
                FinishBtnVisibility = "Visible";
                return false;
            }
            else
            {
                FinishBtnVisibility = "Hidden";
                return true;
            }
        }

        private void FinishRegister(object obj)
        {

            var myWindow = (IClosable)obj;
            myWindow.Close();

        }

        private void InitialData()
        {
            var configData = Data.GetConfigElements();
            IDictionary<string, IList<object>> DataView = (IDictionary<string, IList<object>>)configData.Data;
            this.Materials = DataView["materials"].Cast<Material>().ToList();
            this.Storeys = Convert.ToInt32(Data.GetStoreys().Data);
        }
        #endregion



        #region Properties
        private WallDataService Data = new WallDataService();
        private int _Storeys;
        public int Storeys
        {
            get
            { return _Storeys; }
            set
            {
                if (value != _Storeys)
                    _Storeys = value;
                OnPropertyChanged("Storeys");
            }
        }


        private int _Storey = 1;
        public int Storey
        {
            get
            { return _Storey; }
            set
            {
                if (value != _Storey)
                    _Storey = value;
                OnPropertyChanged("Storey");
            }
        }


        private int _selectedTag;
        public int SelectedTag
        {
            get { return _selectedTag; }
            set
            {
                if (value != _selectedTag)
                    _selectedTag = value;
                OnPropertyChanged("SelectedTag");
            }
        }


        #region Horizontal Properties

        private int _countH;
        public int CountH
        {
            get { return Convert.ToInt32(Data.GetHorizontalWalls(Storey).Data); }
            set
            {
                if (value != _countH)
                    _countH = value;
                OnPropertyChanged("CountH");
            }
        }


        private int _countV;
        public int CountV
        {
            get { return Convert.ToInt32(Data.GetVerticalWalls(Storey).Data); }
            set
            {
                if (value != _countV)
                    _countV = value;
                OnPropertyChanged("CountV");
            }
        }

        private Material _material;
        public Material Material
        {
            get { return _material; }
            set
            {
                if (value != _material)
                    _material = value;
                OnPropertyChanged("Material");
            }
        }

        private string _HorizontalTab = "Hidden";
        public string HorizontalTab
        {
            get { return _HorizontalTab; }
            set
            {
                if (value != _HorizontalTab)
                    _HorizontalTab = value;
                OnPropertyChanged("HorizontalTab");
            }
        }

        private string _VerticalTab = "Hidden";
        public string VerticalTab
        {
            get { return _VerticalTab; }
            set
            {
                if (value != _VerticalTab)
                    _VerticalTab = value;
                OnPropertyChanged("VerticalTab");
            }
        }

        private string _FinishBtnVisibility = "Hidden";
        public string FinishBtnVisibility
        {
            get
            {
                return _FinishBtnVisibility;
            }
            set
            {
                if (value != _FinishBtnVisibility)
                    _FinishBtnVisibility = value;
                OnPropertyChanged("FinishBtnVisibility");
            }
        }
        private List<Material> _Materials;
        public List<Material> Materials
        {
            get { return _Materials; }
            set
            {
                if (value != _Materials)
                    _Materials = value;
                OnPropertyChanged("Materials");
            }
        }

        private double _length;
        public double Length
        {
            get { return _length; }
            set
            {
                if (value != _length)
                    _length = value;
                OnPropertyChanged("Length");
            }
        }

        private double _tributaryArea;
        public double TributaryArea
        {
            get { return _tributaryArea; }
            set
            {
                if (value != _tributaryArea)
                    _tributaryArea = value;
                OnPropertyChanged("TributaryArea");
            }
        }


        private double _thickness;
        public double Thickness
        {
            get { return _thickness; }
            set
            {
                if (value != _thickness)
                    _thickness = value;
                OnPropertyChanged("Thickness");
            }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set
            {
                if (value != _height)
                    _height = value;
                OnPropertyChanged("Height");
            }
        }

        private double _positionX;
        public double PositionX
        {
            get { return _positionX; }
            set
            {
                if (value != _positionX)
                    _positionX = value;
                OnPropertyChanged("PositionX");
            }
        }

        private double _positionY;
        public double PositionY
        {
            get
            {
                return _positionY;
            }
            set
            {
                if (value != _positionY)
                    _positionY = value;
                OnPropertyChanged("PositionY");
            }
        }
        #endregion

        #endregion






    }//end of clase
}//end of namespace
