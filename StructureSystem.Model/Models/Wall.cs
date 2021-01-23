using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.Model;
using System.ComponentModel;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

namespace StructureSystem.Model
{
    public class Wall : INotifyPropertyChanged
    {
        public Wall()
        {
            this.WallTypes = new List<string> { "Interior", "Extremo" };
        }

        public int WallNumber { get; set; }
        public int Storey { get; set; }
        public string Material { get; set; }
        public double Length { get; set; }
        public double TributaryArea { get; set; }
        public double Thickness { get; set; }
        public double Height { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public Enums.SideType Side { get; set; }

        public double Inercia { get; set; }
        public double AreaLongitudinal { get; set; }
        public double RigidezLateral { get; set; }

        public double WMuro { get; set; }
        public double WLosa { get; set; }
        public double MSup { get; set; }
        public double WTotal { get; set; }


        public double CortanteDirecto { get; set; }

        public string Clasificacion { get; set; }
        public double ExcentricidadDisenioX { get; set; }
        public double ExcentricidadDisenioY { get; set; }

        public double CortantePorTorsionX { get; set; }
        public double CortantePorTorsionY { get; set; }


        public double CortanteTotales { get; set; }
        public double MomentoVolteo { get; set; }

        public double CargaAxialMaxima { get; set; }
        public double CargaAxialSismo { get; set; }


        public double Peso { get; set; }

        private List<string> WallTypes_;
        public List<string> WallTypes
        {
            get
            {
                return WallTypes_;
            }
            set
            {
                WallTypes_ = value;
                OnPropertyChanged("WallTypes");
            }
        }

        private string WallType_;
        public string WallType
        {
            get
            {
                return WallType_;
            }
            set
            {
                WallType_ = value;
                OnPropertyChanged("WallType");

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }//end of class
}//end of namespace
