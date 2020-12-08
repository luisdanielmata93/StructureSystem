using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
    public class LosaMacizaAzotea : FlooringMaterial
    {

        public LosaMacizaAzotea()
        {
            CargaAdicionalPorColado = 20;
            CargaAdicionalPorMortero = 20;
        }
       
        private double Impermeabilizante_;
        public double Impermeabilizante
        {
            get
            {
                return Impermeabilizante_;
            }
            set
            {
                if (value != Impermeabilizante_)
                    Impermeabilizante_ = value;
                CalcularCargaMuerta();
                OnPropertyChanged("Impermeabilizante");
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
                OnPropertyChanged("MorteroPV");

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
                OnPropertyChanged("MorteroES");
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

        private double RellenoPV_;
        public double RellenoPV
        {
            get
            {
                return RellenoPV_;
            }
            set
                {
                if (value != RellenoPV_)
                    RellenoPV_ = value;
                RellenoPE = RellenoPV_ * RellenoES_;
                CalcularCargaMuerta();
                OnPropertyChanged("RellenoPV");
            }
        }

        private double RellenoES_;
        public double RellenoES
        {
            get
            {
                return RellenoES_;
            }
            set
            {
                if (value != RellenoES_)
                    RellenoES_ = value;
                RellenoPE = RellenoPV_ * RellenoES_;
                CalcularCargaMuerta();
                OnPropertyChanged("RellenoES");
            }
        }

        private double RellenoPE_;
        public double RellenoPE
        {
            get
            {
                return RellenoPE_;
            }
            set
            {
                if (value != RellenoPE_)
                    RellenoPE_ = value;
                CalcularCargaMuerta();
                OnPropertyChanged("RellenoPE");
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
            CargaMuerta = Impermeabilizante_ + MorteroPE_ + RellenoPE_ + LosaPE_ + AplanadoPE_ + Instalaciones_ + CargaAdicionalPorColado_ + CargaAdicionalPorMortero_;
        }







    }//end class
}//end namespace
