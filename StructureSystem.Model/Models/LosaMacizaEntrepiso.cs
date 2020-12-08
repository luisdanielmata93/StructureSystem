using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
    public class LosaMacizaEntrepiso : FlooringMaterial
    {

        public LosaMacizaEntrepiso() {
            CargaAdicionalPorMortero = 20;
            CargaAdicionalPorColado = 20;
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
                LosaPE = LosaPV_ * LosaES_;
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


        private void CalcularCargaMuerta()
        {
            CargaMuerta = RellenoAdicionalPE_ + PisoPE_ + MorteroPE_ + FirmePE_ + LosaPE_ + AplanadoPE_ + Instalaciones_ + CargaAdicionalPorColado_ + CargaAdicionalPorMortero_;
        }

    }//end of class
}//end of namespace
