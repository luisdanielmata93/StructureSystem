using System;

namespace StructureSystem.Model
{
    public class XMLGeneralData
    {
        #region Definition Data

        public string DocumentPath { get; set; }

        public string ClientName { get; set; }

        public string ProjectName { get; set; }

        public string Address { get; set; }

        public DateTime StartDate { get; set; }

        public int Storeys { get; set; }

        public double Surface { get; set; }

        public Regulation Regulation { get; set; }

        public Group Group { get; set; }

        public Usage Usage { get; set; }

        public Test Test { get; set; }

        #endregion


        #region Save Walls

        public int Storey { get; set; }

        public int CountH { get; set; } = 1;

        public int CountV { get; set; } = 1;

        public Material MaterialV { get; set; }

        public Material MaterialH { get; set; }

        public double Length { get; set; }

        public double TributaryArea { get; set; }

        public double Thickness { get; set; }

        public double Height { get; set; }

        public double PositionX { get; set; }
        
        public double PositionY { get; set; }
       
        #endregion

    }//end of class
}//end of namespace
