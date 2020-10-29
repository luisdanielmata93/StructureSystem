using System;
using StructureSystem.Model;

namespace StructureSystem.Model
{
   public class XMLWallData
    {
        public string DocumentPath { get; set; }

        public int Storey { get; set; }

        public int CountH { get; set; }

        public int CountV { get; set; }

        public Material Material { get; set; }

        public double Length { get; set; }

        public double TributaryArea { get; set; }

        public double Thickness { get; set; }

        public double Height { get; set; }

        public double PositionX { get; set; }

        public double PositionY { get; set; }

        public Enums.SideType Side { get; set; }
    }//end of class
}//end of namespace
