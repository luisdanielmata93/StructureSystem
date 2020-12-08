using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
    public class Storey //NIVEL o PISO
    {
        #region Constructor
        public Storey()
        {
            HorizontalWalls = new List<Wall>();
            VerticalWalls = new List<Wall>();
        }
        #endregion

        #region Properties
        public int StoreyNumber { get; set; }
        public List<Wall> HorizontalWalls { get; set; }
        public List<Wall> VerticalWalls { get; set; }

        public double RigidezEntrepisoHorizontal { get; set; }
        public double RigidezEntrepisoVertical { get; set; }

        public double MasasEntrepisos { get; set; }





        #endregion

    }//end of class
}//end of namespace
