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

        public double CentroMasasX { get; set; }
        public double CentroMasasY { get; set; }


        public double CentroCortantesX { get; set; }
        public double CentroCortantesY { get; set; }


        public double CentroTorsionX { get; set; }
        public double CentroTorsionY { get; set; }


        public double ExcentricidadesEstaticasX { get; set; }
        public double ExcentricidadesEstaticasY { get; set; }

        public double ExcentricidadesAccidentalesX { get; set; }
        public double ExcentricidadesAccidentalesY { get; set; }


        public double ExcentricidadesDisenioEntrepisoX { get; set; }
        public double ExcentricidadesDisenioEntrepisoY { get; set; }


        public double MomentosTorsionantesX { get; set; }
        public double MomentosTorsionantesY { get; set; }


        public double RigidezTorsionalEntrepiso { get; set; }

        public double MomentoVolteoEntrepiso { get; set; }


        #endregion

    }//end of class
}//end of namespace
