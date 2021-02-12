using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
     public class Storey //Nivel o entrepiso
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

        private List<Wall> HorizontalWalls_;
        public List<Wall> HorizontalWalls {
            get
            {
                return HorizontalWalls_;
            }
            set
            {
                if (value != HorizontalWalls_)
                {
                    HorizontalWalls_ = value;
                }
            }
        }


        private List<Wall> VerticalWalls_;
        public List<Wall> VerticalWalls {
            get
            {
                return VerticalWalls_;
            }
            set
            {
                if (value != VerticalWalls_)
                    VerticalWalls_ = value;
            }
        }

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

        public double MomentoVolteoEntrepisoY { get; set; }
        public double MomentoVolteoEntrepisoX { get; set; }

        private string Conclusion_;
        public string Conclusion 
        {
            get
            {
                return Conclusion_;
            }
            set
            {
                if (value != Conclusion_)
                    Conclusion_ = value;    
            }
        }


        #endregion

    }//end of class
}//end of namespace
