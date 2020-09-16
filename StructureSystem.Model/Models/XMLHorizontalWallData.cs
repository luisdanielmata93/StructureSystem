namespace StructureSystem.Model
{
    public class XMLHorizontalWallData
    {
        public string DocumentPath { get; set; }

        public int Storey { get; set; }

        public int Count { get; set; } = 1;

        public Material MaterialH { get; set; }

        public double Length { get; set; }

        public double TributaryArea { get; set; }

        public double Thickness { get; set; }

        public double Height { get; set; }

        public double PositionX { get; set; }

        public double PositionY { get; set; }



    }//end of class
}//end of namespace
