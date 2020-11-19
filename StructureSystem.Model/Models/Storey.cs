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


        ///Vector de rigidez en direccion X (un solo vector por dirección para toda la estructura)
        ///Vector de rigidez de primer a ultimo nivel

        private double _StiffnessVector_X = 1029850.33;
        public double StiffnessVector_X
        {
            get
            {
                return _StiffnessVector_X;
            }

            set
            {
                if (value != StiffnessVector_X)
                    _StiffnessVector_X = value;
            }
        }

        ///Vector de rigidez en direccion Y (un solo vector por dirección para toda la estructura)
        ///Vector de rigidez de primer a ultimo nivel

        private double _StiffnessVector_Y;
        public double StiffnessVector_Y
        {
            get { return _StiffnessVector_Y; }
            set
            {
                if (value != _StiffnessVector_Y)
                    _StiffnessVector_Y = value;
            }
        }


        #endregion

    }//end of class
}//end of namespace
