using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
   public class LosaOtroTipo : FlooringMaterial
    {

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


    }
}
