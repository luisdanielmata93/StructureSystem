using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
   public class Storey
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
        #endregion

    }//end of class
}//end of namespace
