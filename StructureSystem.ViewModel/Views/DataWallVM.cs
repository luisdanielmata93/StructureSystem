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
using StructureSystem.ViewModel.Dialog;

namespace StructureSystem.ViewModel
{
    public class DataWallVM : PropertyChangedViewModel, IDialogRequestClose
    {

        #region Constructor

        public DataWallVM()
        {
            this.data = new BusinessRules();

            InitialData();
            SetCommands();
        }
        #endregion


        #region Commands

        public ICommand InitLevelCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand SaveHorizontalCommand { get; private set; }
        public ICommand SaveVerticalCommand { get; private set; }
        public ICommand FinishCommand { get;private set; }
        #endregion



        #region Set Command

        private void SetCommands()
        {
            NextCommand = new RelayCommand(o => OnNextTab());
            PreviousCommand = new RelayCommand(o => OnPreviousTab());
            SaveHorizontalCommand = new RelayCommand(o => OnSaveHorizontalWall());
            SaveVerticalCommand = new RelayCommand(o => OnSaveVerticalWall());
            InitLevelCommand = new RelayCommand(o => InitRegister());
            FinishCommand = new RelayCommand(o => FinishRegister(), o => CanNewRegister());

        }
       
        #endregion

        #region CommandMethods

        private bool CanNewRegister()
        {
            if (Storeys == Storey)
                return false;
            else
                return true;
        }

        private void FinishRegister()
        {
            Storey += 1;
            SelectedTag = 0;
        }

        private void InitRegister()
        {
            SelectedTag = 1;

            
        }

        private void OnSaveHorizontalWall()
        {
            var result = data.SaveHorizontalWalls(this);
            if (!result.Error)
                CountH += 1;

            Refresh();
        }

        private void OnSaveVerticalWall()
        {
            var result = data.SaveVerticalWalls(this);
            if (!result.Error)
                CountV += 1;

            Refresh();
        }

        private void OnNextTab()
        {
            if (this.SelectedTag == 1)
                this.SelectedTag += 1;

            this.Refresh();
        }

        private void OnPreviousTab()
        {
            if (this.SelectedTag == 2)
                this.SelectedTag -= 1;

            this.Refresh();
        }


        private void Refresh()
        {
            this.Length = 0;
            this.TributaryArea = 0;
            this.Thickness = 0;
            this.Height = 0;
            this.PositionX = 0;
            this.PositionY = 0;
        }
        #endregion


        #region Methods


        private void InitialData()
        {
            try
            {
                var WallsData = data.GetDWallData();
                this.Materials = WallsData["materials"].Cast<Material>().ToList();
                this.Storeys = Convert.ToInt32(data.GetGeneralDataNode("Storeys").Data);

            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        #endregion

        #region Events
        public event EventHandler<DialogRequestedCloseEventArgs> CloseRequested;

        #endregion

        #region Properties

        private BusinessRules data;

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
            get { return _countH; }
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
            get { return _countV; }
            set
            {
                if (value != _countV)
                    _countV = value;
                OnPropertyChanged("CountV");
            }
        }

        private Material _materialH;
        public Material MaterialH
        {
            get { return _materialH; }
            set
            {
                if (value != _materialH)
                    _materialH = value;
                OnPropertyChanged("MaterialH");
            }
        }

        private Material _materialV;
        public Material MaterialV
        {
            get { return _materialV; }
            set
            {
                if (value != _materialV)
                    _materialV = value;
                OnPropertyChanged("MaterialV");
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
