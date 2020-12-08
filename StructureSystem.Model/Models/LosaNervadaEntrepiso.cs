using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
    public class LosaNervadaEntrepiso : FlooringMaterial
    {

        public LosaNervadaEntrepiso()
        {
            this.CargaAdicionalPorColado = 20;
            this.CargaAdicionalPorMortero = 20;
        }

        private double EspesorCapaCompresion_;
        public double EspesorCapaCompresion
        {
            get
            {
                return EspesorCapaCompresion_;
            }
            set
            {
                if (value != EspesorCapaCompresion_)
                    EspesorCapaCompresion_ = value;
                CalcularAreaConcreto();
                CalcularAreaCaseton();
                OnPropertyChanged("EspesorCapaCompresion");
            }
        }

        private double PeralteTotalDeLosa_;
        public double PeralteTotalDeLosa
        {
            get
            {
                return PeralteTotalDeLosa_;
            }
            set
            {
                if (value != PeralteTotalDeLosa_)
                    PeralteTotalDeLosa_ = value;
                CalcularAreaConcreto();
                CalcularAreaCaseton();
                OnPropertyChanged("PeralteTotalDeLosa");
            }
        }

        private double AnchoNervadura_;
        public double AnchoNervadura
        {
            get
            {
                return AnchoNervadura_;
            }
            set
            {
                if (value != AnchoNervadura_)
                    AnchoNervadura_ = value;
                CalcularLongitudModulo();
                CalcularAreaConcreto();
                OnPropertyChanged("AnchoNervadura");
            }
        }

        private double AnchoCaseton_;
        public double AnchoCaseton
        {
            get
            {
                return AnchoCaseton_;
            }
            set
            {
                if (value != AnchoCaseton_)
                    AnchoCaseton_ = value;
                CalcularLongitudModulo();
                CalcularAreaCaseton();
                OnPropertyChanged("AnchoCaseton");
            }
        }

        private double LongitudModulo_;
        public double LongitudModulo
        {
            get
            {
                return LongitudModulo_;
            }
            set
            {
                if (value != LongitudModulo_)
                    LongitudModulo_ = value;
                CalcularAreaConcreto();
                CalcularPesoLosa();
                OnPropertyChanged("LongitudModulo");
            }
        }


        private double AreaConcreto_;
        public double AreaConcreto
        {
            get
            {
                return AreaConcreto_;
            }
            set
            {
                if (value != AreaConcreto_)
                    AreaConcreto_ = value;
                CalcularPesoLosa();
                OnPropertyChanged("AreaConcreto");
            }
        }

        private double AreaCaseton_;
        public double AreaCaseton
        {
            get
            {
                return AreaCaseton_;
            }
            set
            {
                if (value != AreaCaseton_)
                    AreaCaseton_ = value;
                OnPropertyChanged("AreaCaseton");
            }
        }



        private double RellenoAdicionalPV_;
        public double RellenoAdicionalPV
        {
            get
            {
                return RellenoAdicionalPV_;
            }
            set
            {
                if (value != RellenoAdicionalPV_)
                    RellenoAdicionalPV_ = value;
                RellenoAdicionalPE = RellenoAdicionalES_ * RellenoAdicionalPV_;
            }
        }

        private double RellenoAdicionalES_;
        public double RellenoAdicionalES
        {
            get
            {
                return RellenoAdicionalES_;
            }
            set
            {
                if (value != RellenoAdicionalES_)
                    RellenoAdicionalES_ = value;
                RellenoAdicionalPE = RellenoAdicionalES_ * RellenoAdicionalPV_;
            }
        }

        private double RellenoAdicionalPE_;
        public double RellenoAdicionalPE
        {
            get
            {
                return RellenoAdicionalPE_;
            }
            set
            {
                if (value != RellenoAdicionalPE_)
                    RellenoAdicionalPE_ = value;
                CalcularCargaMuerta();
                OnPropertyChanged("RellenoAdicionalPE");
            }
        }

        private double PisoPV_;
        public double PisoPV
        {
            get
            {
                return PisoPV_;
            }
            set
            {
                if (value != PisoPV_)
                    PisoPV_ = value;
                PisoPE = PisoPV_ * PisoES_;
            }
        }

        private double PisoES_;
        public double PisoES
        {
            get
            {
                return PisoES_;
            }
            set
            {
                if (value != PisoES_)
                    PisoES_ = value;
                PisoPE = PisoPV_ * PisoES_;

            }
        }

        private double PisoPE_;
        public double PisoPE
        {
            get
            {
                return PisoPE_;
            }
            set
            {
                if (value != PisoPE_)
                    PisoPE_ = value;
                CalcularCargaMuerta();
                OnPropertyChanged("PisoPE");
            }
        }

        private double MorteroPV_;
        public double MorteroPV
        {
            get
            {
                return MorteroPV_;
            }
            set
            {
                if (value != MorteroPV_)
                    MorteroPV_ = value;
                MorteroPE = MorteroPV_ * MorteroES_;
            }
        }

        private double MorteroES_;
        public double MorteroES
        {
            get
            {
                return MorteroES_;
            }
            set
            {
                if (value != MorteroES_)
                    MorteroES_ = value;
                MorteroPE = MorteroPV_ * MorteroES_;
            }
        }


        private double MorteroPE_;
        public double MorteroPE
        {
            get
            {
                return MorteroPE_;
            }
            set
            {
                if (value != MorteroPE_)
                    MorteroPE_ = value;
                CalcularCargaMuerta();
                OnPropertyChanged("MorteroPE");
            }
        }

        private double FirmePV_;
        public double FirmePV
        {
            get
            {
                return FirmePV_;
            }
            set
            {
                if (value != FirmePV_)
                    FirmePV_ = value;
                FirmePE = FirmePV_ * FirmeES_;
            }
        }

        private double FirmeES_;
        public double FirmeES
        {
            get
            {
                return FirmeES_;
            }
            set
            {
                if (value != FirmeES_)
                    FirmeES_ = value;
                FirmePE = FirmePV_ * FirmeES_;

            }
        }

        private double FirmePE_;
        public double FirmePE
        {
            get
            {
                return FirmePE_;
            }
            set
            {
                if (value != FirmePE_)
                    FirmePE_ = value;
                CalcularCargaMuerta();
                OnPropertyChanged("FirmePE");
            }
        }

        private double LosaPV_;
        public double LosaPV
        {
            get
            {
                return LosaPV_;
            }
            set
            {
                if (value != LosaPV_)
                    LosaPV_ = value;
                CalcularPesoLosa();
            }
        }

        private double LosaES_;
        public double LosaES
        {
            get
            {
                return LosaES_;
            }
            set
            {
                if (value != LosaES_)
                    LosaES_ = value;
                LosaPE = LosaPV_ * LosaES_;

            }
        }

        private double LosaPE_;
        public double LosaPE
        {
            get
            {
                return LosaPE_;
            }
            set
            {
                if (value != LosaPE_)
                    LosaPE_ = value;
                CalcularCargaMuerta();
                OnPropertyChanged("LosaPE");
            }
        }


        private double CasetonesPV_;
        public double CasetonesPV
        {
            get
            {
                return CasetonesPV_;
            }
            set
            {
                if (value != CasetonesPV_)
                    CasetonesPV_ = value;
                CalcularPesoCaseton();
                OnPropertyChanged("CasetonesPV");
            }
        }

        private double CasetonesPE_;
        public double CasetonesPE
        {
            get
            {
                return CasetonesPE_;
            }
            set
            {
                if (value != CasetonesPE_)
                    CasetonesPE_ = value;
                CalcularCargaMuerta();
                OnPropertyChanged("CasetonesPE");
            }
        }


        private double AplanadoPV_;
        public double AplanadoPV
        {
            get
            {
                return AplanadoPV_;
            }
            set
            {
                if (value != AplanadoPV_)
                    AplanadoPV_ = value;
                AplanadoPE = AplanadoPV_ * AplanadoES_;
            }
        }

        private double AplanadoES_;
        public double AplanadoES
        {
            get
            {
                return AplanadoES_;
            }
            set
            {
                if (value != AplanadoES_)
                    AplanadoES_ = value;
                AplanadoPE = AplanadoPV_ * AplanadoES_;

            }
        }


        private double AplanadoPE_;
        public double AplanadoPE
        {
            get
            {
                return AplanadoPE_;
            }
            set
            {
                if (value != AplanadoPE_)
                    AplanadoPE_ = value;
                CalcularCargaMuerta();
                OnPropertyChanged("AplanadoPE");
            }
        }

        private double Instalaciones_;
        public double Instalaciones
        {
            get
            {
                return Instalaciones_;
            }
            set
            {
                if (value != Instalaciones_)
                    Instalaciones_ = value;
                CalcularCargaMuerta();
                OnPropertyChanged("Instalaciones");
            }
        }

        private double CargaAdicionalPorColado_;
        public double CargaAdicionalPorColado
        {
            get
            {
                return CargaAdicionalPorColado_;
            }
            set
            {
                if (value != CargaAdicionalPorColado_)
                    CargaAdicionalPorColado_ = value;
                CalcularCargaMuerta();
                OnPropertyChanged("CargaAdicionalPorColado");
            }
        }

        private double CargaAdicionalPorMortero_;
        public double CargaAdicionalPorMortero
        {
            get
            {
                return CargaAdicionalPorMortero_;
            }
            set
            {
                if (value != CargaAdicionalPorMortero_)
                    CargaAdicionalPorMortero_ = value;
                CalcularCargaMuerta();
                OnPropertyChanged("CargaAdicionalPorMortero");
            }
        }


        private double CargaMuerta_;
        public double CargaMuerta
        {
            get
            {
                return CargaMuerta_;
            }
            set
            {
                if (value != CargaMuerta_)
                    CargaMuerta_ = value;
                OnPropertyChanged("CargaMuerta");
            }
        }

        private double CargaVivaMaxima_;
        public double CargaVivaMaxima
        {
            get
            {
                return CargaVivaMaxima_;
            }
            set
            {
                if (value != CargaVivaMaxima_)
                    CargaVivaMaxima_ = value;
                OnPropertyChanged("CargaVivaMaxima");
            }
        }

        private double CargaVivaInstantanea_;
        public double CargaVivaInstantanea
        {
            get
            {
                return CargaVivaInstantanea_;
            }
            set
            {
                if (value != CargaVivaInstantanea_)
                    CargaVivaInstantanea_ = value;
                OnPropertyChanged("CargaVivaInstantanea");
            }
        }






        #region Calculos
        private void CalcularCargaMuerta()
        {
            CargaMuerta = RellenoAdicionalPE_ + PisoPE_ + MorteroPE_ + FirmePE_ + LosaPE_ + CasetonesPE_ + AplanadoPE_ + Instalaciones_ + CargaAdicionalPorColado_ + CargaAdicionalPorMortero_;
        }

        private void CalcularLongitudModulo()
        {
            LongitudModulo = AnchoNervadura_ + AnchoCaseton_;
        }

        private void CalcularAreaConcreto()
        {
            AreaConcreto = AnchoNervadura_ * ((PeralteTotalDeLosa_ - EspesorCapaCompresion_) + (EspesorCapaCompresion_ * LongitudModulo_));
        }

        private void CalcularAreaCaseton()
        {
            AreaCaseton = AnchoCaseton_ * (PeralteTotalDeLosa_ - EspesorCapaCompresion_);
        }

        private void CalcularPesoLosa()
        {
            LosaPE = (LosaPV_ * AreaConcreto_) / LongitudModulo_;
        }

        private void CalcularPesoCaseton()
        {
            CasetonesPE = (CasetonesPV_ * AreaCaseton_) / LongitudModulo_;
        }
        #endregion











    }//end class
}//end namespace
