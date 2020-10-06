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

    }//end of class
}//end of namespace
