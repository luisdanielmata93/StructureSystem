using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.Model;

namespace StructureSystem.Model
{
    public class Wall
    {
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

        /// <summary>
        /// Propiedades por cada muro en X
        /// </summary>
        //Inercia
        public double Inertia_X { get { return (this.Thickness * Math.Pow(this.Length, 3) / 12);  } }
        //AreaLongitudinal
        public double LongitudinalArea_X { get { return this.Thickness* this.Length; } }
        //Rigidez lateral
        public double LateralStiffness_X { get
            {
                double Km = 0;

                return Km;
            } }
        //Rigidez de entrepiso
        public double MezzanineStiffness_X {
            get
            {
                return 0;
            }
        }
        
        /// <summary>
        /// Propiedades por cada muro en Y
        /// </summary>
        
        //Inercia
        public double Inertia_Y { get; set; }
        //AreaLongitudinal
        public double LongitudinalArea_Y { get; set; }
        //Rigidez lateral
        public double LateralStiffness_Y { get; set; }
        //Rigidez de entrepiso
        public double MezzanineStiffness_Y { get; set; }


    }//end of class
}//end of namespace
