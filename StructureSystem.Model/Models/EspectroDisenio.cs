using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;


namespace StructureSystem.Model
{
    public class EspectroDisenio : INotifyPropertyChanged
    {
      
        private double T_;
        public double T
        {
            get
            {
                return T_;
            }
            set
            {
                if (value != T_)
                    T_ = value;
                OnPropertyChanged("T");
            }
        }

        private double S_;
        public double S
        {
            get
            {
                return S_;
            }
            set
            {

                if (value != S_)
                    S_ = value;
                OnPropertyChanged("S");
            }
        }


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
