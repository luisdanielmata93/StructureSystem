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
    public class LoadAnalysisVM : PropertyChangedViewModel
    {
        #region Constructor
        public LoadAnalysisVM(PropertyChangedViewModel mainViewModel)
        {

            notificationViewModel = new NotificationViewModel();

            this._mainViewModel = mainViewModel;

            this.SetInitialData();

            this.SetCommands();

        }
        #endregion


        #region Commands

        public ICommand SaveLevelCommand { get; private set; }

        #endregion


        #region Set Command

        private void SetCommands()
        {
            SaveLevelCommand = new RelayCommand(o => OnSaveLevel());

        }

        private async void OnSaveLevel()
        {
           
            if (IsSaved())
            {
                this.dialogCoordinator = new DialogCoordinator();
                var result = await this.dialogCoordinator.ShowMessageAsync(this, string.Concat("Este nivel ya se encuentra registrado. \n ¿Desea actualizar la información?"), "Se sobreescribirá la información guardada.", MessageDialogStyle.AffirmativeAndNegative);
                if (result != MessageDialogResult.Affirmative)
                {
                    return;
                }
            }

            DataService.Save();
            notificationViewModel.ShowMessage("Inserción exitosa.");
            //BORRAR CAMPOOOSSS
        }



        #endregion

        #region Methods

        private bool IsSaved()
        {
            bool result = (bool)DataService.IsSaved(Storey).Data;
            if (result)
                IsAlert = "Hidden";
            else
                IsAlert = "Visible";

            return result;

        }

        private void SetInitialData()
        {
            this.HiddenTabs();
            this.FlooringMaterials = (List<FlooringMaterial>)DataService.GetFlooringMaterials().Data;
            this.StoreyList = new ObservableCollection<int>();
        }

        private double CalculateDeadLoad()
        {
            double result = 0;

            switch (MaterialType)
            {
                case Enums.MaterialType.MacisaEntrepiso:
                    result = RellenoAdicionalPE + PisoPE + MorteroPE + FirmePE + LosaPE + AplanadoPE + InstalacionesPE + CargaAdicionalColadoPE + CargaAdicionalMorteroPE;
                    return Math.Round(result, 4);
                    break;
                case Enums.MaterialType.MacisaAzotea:
                    result = ImpermeabilizantePE + MorteroPE + RellenoPE + LosaPE + AplanadoPE + InstalacionesPE + CargaAdicionalColadoPE + CargaAdicionalMorteroPE;
                    return Math.Round(result, 4);
                    break;
                case Enums.MaterialType.NervadaEntrepiso:
                    result = RellenoAdicionalPE + PisoPE + MorteroPE + FirmePE + LosaPE + CasetonesPE + AplanadoPE + InstalacionesPE + CargaAdicionalColadoPE + CargaAdicionalMorteroPE;
                    return Math.Round(result, 4);
                    break;
                case Enums.MaterialType.NervadaAzotea:
                    result = ImpermeabilizantePE + MorteroPE + RellenoPE + LosaPE + CasetonesPE + AplanadoPE + InstalacionesPE + CargaAdicionalColadoPE + CargaAdicionalMorteroPE;
                    return Math.Round(result, 4);
                    break;
                case Enums.MaterialType.ViguetaBovedillaEntrepiso:
                    result = RellenoAdicionalPE + PisoPE + MorteroPE + FirmePE + LosaPE;
                    return Math.Round(result, 4);
                    break;
                case Enums.MaterialType.ViguetaBovedillaAzotea:
                    return result;

                    break;
                case Enums.MaterialType.Otra:
                    return result;

                    break;
                default:
                    return result;

                    break;

            }
        }

        private double CalculateWeight(double PesoVolumetrico, double Espesor)
        {
            double result = 0;
            if (PesoVolumetrico > 0 && Espesor > 0)
                result = PesoVolumetrico * Espesor;
            return Math.Round(result, 4);
            //switch (MaterialType)
            //{
            //    case Enums.MaterialType.MacisaEntrepiso:
            //        if (PesoVolumetrico > 0 && Espesor > 0)
            //            return Math.Round((PesoVolumetrico * Espesor), 4);
            //        break;
            //    case Enums.MaterialType.MacisaAzotea:
            //        double result = 0;
            //        if (PesoVolumetrico > 0 && Espesor > 0)
            //            result = PesoVolumetrico * Espesor;
            //        return Math.Round(result, 4);
            //        break;
            //    case Enums.MaterialType.NervadaEntrepiso:
            //        break;
            //    case Enums.MaterialType.NervadaAzotea:
            //        break;
            //    case Enums.MaterialType.ViguetaBovedillaEntrepiso:
            //        break;
            //    case Enums.MaterialType.ViguetaBovedillaAzotea:
            //        break;
            //    case Enums.MaterialType.Otra:
            //        break;
            //    default:
            //        break;
            //}

        }

        private void SelectTabItem()
        {
            //BORRAR CAMPOOOSSSS
            switch (FlooringMaterial.Id)
            {
                case 1:
                    if (IsEntrepiso)
                    {
                        this.HiddenTabs();
                        this.TabIndex = 0;
                        this.MaterialType = Enums.MaterialType.MacisaEntrepiso;
                    }
                    else
                    {
                        this.HiddenTabs();
                        this.TabIndex = 1;
                        this.MaterialType = Enums.MaterialType.MacisaAzotea;
                    }
                    break;
                case 2:
                    if (IsEntrepiso)
                    {
                        this.HiddenTabs();
                        this.TabIndex = 2;
                        this.MaterialType = Enums.MaterialType.NervadaEntrepiso;
                    }
                    else
                    {
                        this.HiddenTabs();
                        this.TabIndex = 3;
                        this.MaterialType = Enums.MaterialType.NervadaAzotea;
                    }
                    break;
                case 3:
                    if (IsEntrepiso)
                    {
                        this.HiddenTabs();
                        this.TabIndex = 4;
                        this.MaterialType = Enums.MaterialType.ViguetaBovedillaEntrepiso;
                    }
                    else
                    {
                        this.HiddenTabs();
                        this.TabIndex = 5;
                        this.MaterialType = Enums.MaterialType.ViguetaBovedillaAzotea;
                    }
                    break;
                case 4:
                    if (IsEntrepiso)
                    {
                        this.HiddenTabs();
                        this.TabIndex = 6;
                        this.MaterialType = Enums.MaterialType.Otra;
                    }
                    else
                    {
                        this.HiddenTabs();
                        this.TabIndex = 6;
                        this.MaterialType = Enums.MaterialType.Otra;
                    }
                    break;
                default:
                    break;
            }
        }

        private void HiddenTabs()
        {
            this.LosaMacisaEntrepiso = "Hidden";
            this.LosaMacisaAzotea = "Hidden";
            this.LosaNervadaEntrepiso = "Hidden";
            this.LosaNervadaAzotea = "Hidden";
            this.ViguetaBovedillaEntrepiso = "Hidden";
            this.ViguetaBovedillaAzotea = "Hidden";
            this.Otra = "Hidden";
        }


        private ObservableCollection<int> GetStoreys()
        {
            ObservableCollection<int> result = new ObservableCollection<int>((List<int>)DataService.GetStoreyList().Data);

            return result;
        }
        #endregion

        #region Properties
        private IDialogCoordinator dialogCoordinator;
        private NotificationViewModel notificationViewModel;
        private Enums.MaterialType MaterialType { get; set; }

        private string _IsAlert = "Visible";
        public string IsAlert
        {
            get
            {
                return _IsAlert;
            }
            set
            {
                if (value != _IsAlert)
                    _IsAlert = value;

                OnPropertyChanged("IsAlert");
            }
        }

        private int _TabIndex = -1;
        public int TabIndex
        {
            get { return _TabIndex; }
            set
            {
                if (value != _TabIndex)
                    _TabIndex = value;
                OnPropertyChanged("TabIndex");
            }
        }

        private bool _IsEntrepiso;
        public bool IsEntrepiso
        {
            get { return _IsEntrepiso; }
            set
            {
                _IsEntrepiso = true;

                _IsAzotea = false;
                this.SelectTabItem();
                OnPropertyChanged("IsAzotea");
                OnPropertyChanged("IsEntrepiso");


            }
        }
        private bool _IsAzotea;
        public bool IsAzotea
        {
            get { return _IsAzotea; }
            set
            {
                _IsAzotea = true;

                _IsEntrepiso = false;
                this.SelectTabItem();
                OnPropertyChanged("IsEntrepiso");
                OnPropertyChanged("IsAzotea");

            }
        }

        private int _Storey;
        public int Storey
        {
            get
            { return _Storey; }
            set
            {
                _Storey = value;
                IsSaved();
                OnPropertyChanged("Storey");
            }
        }


        private ObservableCollection<int> _storeyList;
        public ObservableCollection<int> StoreyList
        {
            get
            {
                return GetStoreys();
            }
            set { if (value != _storeyList) _storeyList = value; OnPropertyChanged("StoreyList"); }
        }


        private FlooringMaterial _flooringMaterial;
        public FlooringMaterial FlooringMaterial
        {
            get
            {
                return _flooringMaterial;
            }
            set
            {
                if (value != _flooringMaterial)
                {
                    _flooringMaterial = value;
                    this.IsEntrepiso = true;
                }

                SelectTabItem();

                OnPropertyChanged("FlooringMaterial");
            }
        }

        private List<FlooringMaterial> _flooringMaterials;
        public List<FlooringMaterial> FlooringMaterials
        {
            get { return _flooringMaterials; }
            set
            {
                if (value != _flooringMaterials)
                    _flooringMaterials = value;
                OnPropertyChanged("FlooringMaterials");
            }
        }

        private readonly LoadAnalysisService DataService = new LoadAnalysisService();
        private readonly PropertyChangedViewModel _mainViewModel;
        #endregion

        #region FlooringMaterials Properties
        private double _RellenoAdicionalPV;
        public double RellenoAdicionalPV
        {
            get { return _RellenoAdicionalPV; }
            set
            {
                if (value != _RellenoAdicionalPV)
                {
                    _RellenoAdicionalPV = value;
                    RellenoAdicionalPE = CalculateWeight(_RellenoAdicionalPV, _RellenoAdicionalES);
                }
                OnPropertyChanged("RellenoAdicionalPV");
            }
        }
        private double _RellenoAdicionalES;
        public double RellenoAdicionalES
        {
            get { return _RellenoAdicionalES; }
            set
            {
                if (value != _RellenoAdicionalES)
                {
                    _RellenoAdicionalES = value;
                    RellenoAdicionalPE = CalculateWeight(_RellenoAdicionalPV, _RellenoAdicionalES);

                }
                OnPropertyChanged("RellenoAdicionalES");
            }
        }
        private double _RellenoAdicionalPE;
        public double RellenoAdicionalPE
        {
            get { return _RellenoAdicionalPE; }
            set
            {
                if (value != _RellenoAdicionalPE)
                {
                    _RellenoAdicionalPE = value;
                    CargaMuertaTotalPE = CalculateDeadLoad();
                }
                OnPropertyChanged("RellenoAdicionalPE");
            }
        }

        private double _PisoPV;
        public double PisoPV
        {
            get { return _PisoPV; }
            set
            {
                if (value != _PisoPV)
                {
                    _PisoPV = value;
                    PisoPE = CalculateWeight(_PisoPV, _PisoES);
                }
                OnPropertyChanged("PisoPV");
            }
        }
        private double _PisoES;
        public double PisoES
        {
            get { return _PisoES; }
            set
            {
                if (value != _PisoES)
                {
                    _PisoES = value;
                    PisoPE = CalculateWeight(_PisoPV, _PisoES);
                }
                OnPropertyChanged("PisoES");
            }
        }
        private double _PisoPE;
        public double PisoPE
        {
            get { return _PisoPE; }
            set
            {
                if (value != _PisoPE)
                {
                    _PisoPE = value;
                    CargaMuertaTotalPE = CalculateDeadLoad();
                }
                OnPropertyChanged("PisoPE");
            }
        }

        private double _MorteroPV;
        public double MorteroPV
        {
            get { return _MorteroPV; }
            set
            {
                if (value != _MorteroPV)
                {
                    _MorteroPV = value;
                    MorteroPE = CalculateWeight(_MorteroPV, _MorteroES);
                }
                OnPropertyChanged("MorteroPV");
            }
        }
        private double _MorteroES;
        public double MorteroES
        {
            get { return _MorteroES; }
            set
            {
                if (value != _MorteroES)
                {
                    _MorteroES = value;
                    MorteroPE = CalculateWeight(_MorteroPV, _MorteroES);
                }

                OnPropertyChanged("MorteroES");
            }
        }
        private double _MorteroPE;
        public double MorteroPE
        {
            get { return _MorteroPE; }
            set
            {
                if (value != _MorteroPE)
                {
                    _MorteroPE = value;
                    CargaMuertaTotalPE = CalculateDeadLoad();
                }
                OnPropertyChanged("MorteroPE");
            }
        }


        private double _FirmePV;
        public double FirmePV
        {
            get { return _FirmePV; }
            set
            {
                if (value != _FirmePV)
                {
                    _FirmePV = value;
                    FirmePE = CalculateWeight(_FirmePV, _FirmeES);
                }
                OnPropertyChanged("FirmePV");
            }
        }
        private double _FirmeES;
        public double FirmeES
        {
            get { return _FirmeES; }
            set
            {
                if (value != _FirmeES)
                {
                    _FirmeES = value;
                    FirmePE = CalculateWeight(_FirmePV, _FirmeES);
                }
                OnPropertyChanged("FirmeES");
            }
        }
        private double _FirmePE;
        public double FirmePE
        {
            get { return _FirmePE; }
            set
            {
                if (value != _FirmePE)
                {
                    _FirmePE = value;
                    CargaMuertaTotalPE = CalculateDeadLoad();
                }
                OnPropertyChanged("FirmePE");
            }
        }

        private double _LosaPV;
        public double LosaPV
        {
            get { return _LosaPV; }
            set
            {
                if (value != _LosaPV)
                {
                    _LosaPV = value;
                    LosaPE = CalculateWeight(_LosaPV, _LosaES);
                }
                OnPropertyChanged("LosaPV");
            }
        }
        private double _LosaES;
        public double LosaES
        {
            get { return _LosaES; }
            set
            {
                if (value != _LosaES)
                {
                    _LosaES = value;
                    LosaPE = CalculateWeight(_LosaPV, _LosaES);
                }
                OnPropertyChanged("LosaES");
            }
        }
        private double _LosaPE;
        public double LosaPE
        {
            get { return _LosaPE; }
            set
            {
                if (value != _LosaPE)
                {
                    _LosaPE = value;
                    CargaMuertaTotalPE = CalculateDeadLoad();
                }
                OnPropertyChanged("LosaPE");
            }
        }

        private double _AplanadoPV;
        public double AplanadoPV
        {
            get { return _AplanadoPV; }
            set
            {
                if (value != _AplanadoPV)
                {
                    _AplanadoPV = value;
                    AplanadoPE = CalculateWeight(_AplanadoPV, _AplanadoES);
                }
                OnPropertyChanged("AplanadoPV");
            }
        }
        private double _AplanadoES;
        public double AplanadoES
        {
            get { return _AplanadoES; }
            set
            {
                if (value != _AplanadoES)
                {
                    _AplanadoES = value;
                    AplanadoPE = CalculateWeight(_AplanadoPV, _AplanadoES);
                }
                OnPropertyChanged("AplanadoES");
            }
        }
        private double _AplanadoPE;
        public double AplanadoPE
        {
            get { return _AplanadoPE; }
            set
            {
                if (value != _AplanadoPE)
                {
                    _AplanadoPE = value;
                    CargaMuertaTotalPE = CalculateDeadLoad();
                }
                OnPropertyChanged("AplanadoPE");
            }
        }

        private double _InstalacionesPE;
        public double InstalacionesPE
        {
            get { return _InstalacionesPE; }
            set
            {
                if (value != _InstalacionesPE)
                {
                    _InstalacionesPE = value;
                    CargaMuertaTotalPE = CalculateDeadLoad();
                }
                OnPropertyChanged("InstalacionesPE");
            }
        }

        private double _CargaAdicionalColadoPE;
        public double CargaAdicionalColadoPE
        {
            get { return _CargaAdicionalColadoPE; }
            set
            {
                if (value != _CargaAdicionalColadoPE)
                {
                    _CargaAdicionalColadoPE = value;
                    CargaMuertaTotalPE = CalculateDeadLoad();
                }
                OnPropertyChanged("CargaAdicionalColadoPE");
            }
        }

        private double _CargaAdicionalMorteroPE;
        public double CargaAdicionalMorteroPE
        {
            get { return _CargaAdicionalMorteroPE; }
            set
            {
                if (value != _CargaAdicionalMorteroPE)
                {
                    _CargaAdicionalMorteroPE = value;
                    CargaMuertaTotalPE = CalculateDeadLoad();
                }
                OnPropertyChanged("CargaAdicionalMorteroPE");
            }
        }

        private double _CargaMuertaTotalPE;
        public double CargaMuertaTotalPE
        {
            get { return _CargaMuertaTotalPE; }
            set
            {
                if (value != _CargaMuertaTotalPE)
                    _CargaMuertaTotalPE = value;

                OnPropertyChanged("CargaMuertaTotalPE");
            }
        }

        private double _CargaVivaMaximaPE;
        public double CargaVivaMaximaPE
        {
            get { return _CargaVivaMaximaPE; }
            set
            {
                if (value != _CargaVivaMaximaPE)
                    _CargaVivaMaximaPE = value;
                OnPropertyChanged("CargaVivaMaximaPE");
            }
        }

        private double _CargaVivaInstantaneaPE;
        public double CargaVivaInstantaneaPE
        {
            get { return _CargaVivaInstantaneaPE; }
            set
            {
                if (value != _CargaVivaInstantaneaPE)
                    _CargaVivaInstantaneaPE = value;
                OnPropertyChanged("CargaVivaInstantaneaPE");
            }
        }

        private double _ImpermeabilizantePV;
        public double ImpermeabilizantePV
        {
            get { return _ImpermeabilizantePV; }
            set
            {
                if (value != _ImpermeabilizantePV)
                {
                    _ImpermeabilizantePV = value;
                    ImpermeabilizantePE = CalculateWeight(_ImpermeabilizantePV, _ImpermeabilizanteES);

                }
                OnPropertyChanged("ImpermeabilizantePV");
            }
        }
        private double _ImpermeabilizanteES;
        public double ImpermeabilizanteES
        {
            get { return _ImpermeabilizanteES; }
            set
            {
                if (value != _ImpermeabilizanteES)
                {
                    _ImpermeabilizanteES = value;
                    ImpermeabilizantePE = CalculateWeight(_ImpermeabilizantePV, _ImpermeabilizanteES);

                }
                OnPropertyChanged("ImpermeabilizanteES");
            }
        }
        private double _ImpermeabilizantePE;
        public double ImpermeabilizantePE
        {
            get { return _ImpermeabilizantePE; }
            set
            {
                if (value != _ImpermeabilizantePE)
                {
                    _ImpermeabilizantePE = value;
                    CargaMuertaTotalPE = CalculateDeadLoad();
                }
                OnPropertyChanged("ImpermeabilizantePE");
            }
        }


        private double _RellenoPV;
        public double RellenoPV
        {
            get { return _RellenoPV; }
            set
            {
                if (value != _RellenoPV)
                {
                    _RellenoPV = value;
                    RellenoPE = CalculateWeight(_RellenoPV, _RellenoES);
                }
                OnPropertyChanged("RellenoPV");
            }
        }
        private double _RellenoES;
        public double RellenoES
        {
            get { return _RellenoES; }
            set
            {
                if (value != _RellenoES)
                {
                    _RellenoES = value;
                    RellenoPE = CalculateWeight(_RellenoES, _RellenoES);
                }
                OnPropertyChanged("RellenoES");
            }
        }
        private double _RellenoPE;
        public double RellenoPE
        {
            get { return _RellenoPE; }
            set
            {
                if (value != _RellenoPE)
                    _RellenoPE = value;
                OnPropertyChanged("RellenoPE");
            }
        }

        private double _EspesorCapaCompresion;
        public double EspesorCapaCompresion
        {
            get
            {
                return _EspesorCapaCompresion;
            }
            set
            {
                if (value != _EspesorCapaCompresion)
                    _EspesorCapaCompresion = value;


                OnPropertyChanged("EspesorCapaCompresion");
            }
        }

        private double _PeralteTotalLosa;
        public double PeralteTotalLosa
        {
            get
            {
                return _PeralteTotalLosa;
            }
            set
            {
                if (value != _PeralteTotalLosa)
                    _PeralteTotalLosa = value;


                OnPropertyChanged("PeralteTotalLosa");
            }
        }

        private double _AnchoNervadura;
        public double AnchoNervadura
        {
            get
            {
                return _AnchoNervadura;
            }
            set
            {
                if (value != _AnchoNervadura)
                    _AnchoNervadura = value;


                OnPropertyChanged("AnchoNervadura");
            }
        }

        private double _AnchoCaseton;
        public double AnchoCaseton
        {
            get
            {
                return _AnchoCaseton;
            }
            set
            {
                if (value != _AnchoCaseton)
                    _AnchoCaseton = value;


                OnPropertyChanged("AnchoCaseton");
            }
        }

        private double _LongitudModulo;
        public double LongitudModulo
        {
            get
            {
                return _LongitudModulo;
            }
            set
            {
                if (value != _LongitudModulo)
                    _LongitudModulo = value;


                OnPropertyChanged("LongitudModulo");
            }
        }

        private double _AreaConcreto;
        public double AreaConcreto
        {
            get
            {
                return _AreaConcreto;
            }
            set
            {
                if (value != _AreaConcreto)
                    _AreaConcreto = value;


                OnPropertyChanged("AreaConcreto");
            }
        }

        private double _AreaCaseton;
        public double AreaCaseton
        {
            get
            {
                return _AreaCaseton;
            }
            set
            {
                if (value != _AreaCaseton)
                    _AreaCaseton = value;


                OnPropertyChanged("AreaCaseton");
            }
        }


        private double _CasetonesPV;
        public double CasetonesPV
        {
            get
            {
                return _CasetonesPV;
            }
            set
            {
                if (_CasetonesPV != value)
                    _CasetonesPV = value;

                OnPropertyChanged("CasetonesPV");
            }
        }

        private double _CasetonesES;
        public double CasetonesES
        {
            get
            {
                return _CasetonesES;
            }
            set
            {
                if (_CasetonesES != value)
                    _CasetonesES = value;

                OnPropertyChanged("CasetonesES");
            }
        }

        private double _CasetonesPE;
        public double CasetonesPE
        {
            get
            {
                return _CasetonesPE;
            }
            set
            {
                if (_CasetonesPE != value)
                    _CasetonesPE = value;

                OnPropertyChanged("CasetonesPE");
            }
        }





        #region VisibleProperties
        private string _LosaMacisaEntrepiso = "Hidden";
        public string LosaMacisaEntrepiso
        {
            get { return _LosaMacisaEntrepiso; }
            set
            {
                _LosaMacisaEntrepiso = value;
                OnPropertyChanged("LosaMacisaEntrepiso");
            }
        }

        private string _LosaMacisaAzotea = "Hidden";
        public string LosaMacisaAzotea
        {
            get
            { return _LosaMacisaAzotea; }
            set
            {
                _LosaMacisaAzotea = value;
                OnPropertyChanged("LosaMacisaAzotea");
            }
        }

        private string _LosaNervadaEntrepiso = "Hidden";
        public string LosaNervadaEntrepiso
        {
            get { return _LosaNervadaEntrepiso; }
            set
            {
                _LosaNervadaEntrepiso = value;
                OnPropertyChanged("LosaNervadaEntrepiso");
            }
        }

        private string _LosaNervadaAzotea = "Hidden";
        public string LosaNervadaAzotea
        {
            get { return _LosaNervadaAzotea; }
            set
            {
                _LosaNervadaAzotea = value;
                OnPropertyChanged("LosaNervadaAzotea");
            }
        }

        private string _ViguetaBovedillaEntrepiso = "Hidden";
        public string ViguetaBovedillaEntrepiso
        {
            get { return _ViguetaBovedillaEntrepiso; }

            set
            {
                _ViguetaBovedillaEntrepiso = value;
                OnPropertyChanged("ViguetaBovedillaEntrepiso");
            }
        }

        private string _ViguetaBovedillaAzotea = "Hidden";
        public string ViguetaBovedillaAzotea
        {
            get { return _ViguetaBovedillaAzotea; }
            set
            {
                _ViguetaBovedillaAzotea = value;
                OnPropertyChanged("ViguetaBovedillaAzotea");
            }
        }

        private string _Otra = "Hidden";
        public string Otra
        {
            get
            { return _Otra; }
            set
            {
                _Otra = value; OnPropertyChanged("Otra");
                ;
            }
        }
        #endregion


        #endregion
    }//end of class
}//end of namespace
