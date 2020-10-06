using System;

namespace StructureSystem.Model
{
    public class XMLGeneralData
    {
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

    }//end of class
}//end of namespace
