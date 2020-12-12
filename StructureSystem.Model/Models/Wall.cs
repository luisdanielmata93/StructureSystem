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


        public double Inercia { get; set; }
        public double AreaLongitudinal { get; set; }
        public double RigidezLateral { get; set; }

        public double CortanteDirecto { get; set; }
        
        public string Clasificacion { get; set; }
        public double ExcentricidadDisenioX { get; set; }
        public double ExcentricidadDisenioY { get; set; }

        public double CortantePorTorsion { get; set; }
        public double CortanteTotales { get; set; }
        public double MomentoVolteo { get; set; }
        public double CargaAxialMaxima { get; set; }
        public double CargaAxialSismo { get; set; }


    }//end of class
}//end of namespace
