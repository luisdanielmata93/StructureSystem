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
        public List<Wall> HorizontalWalls
        {
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
        public List<Wall> VerticalWalls
        {
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

        private double EsfuerzoNormalPromedio_;
        public double EsfuerzoNormalPromedio
        {
            get
            {
                return EsfuerzoNormalPromedio_;
            }
            set
            {
                if (value != EsfuerzoNormalPromedio_)
                    EsfuerzoNormalPromedio_ = value;
            }
        }

        private double CortanteResistenteEntrepisoMamposteria_;
        public double CortanteResistenteEntrepisoMamposteria
        {
            get
            {
                return CortanteResistenteEntrepisoMamposteria_;
            }
            set
            {
                if (value != CortanteResistenteEntrepisoMamposteria_)
                    CortanteResistenteEntrepisoMamposteria_ = value;
            }
        }

        private double CortanteResistenteEntrepisoConcreto_;
        public double CortanteResistenteEntrepisoConcreto
        {
            get
            {
                return CortanteResistenteEntrepisoConcreto_;
            }
            set
            {
                if (value != CortanteResistenteEntrepisoConcreto_)
                    CortanteResistenteEntrepisoConcreto_ = value;
            }
        }


        private double CortanteEntrepisoTotal_;
        public double CortanteEntrepisoTotal
        {
            get
            {
                return CortanteEntrepisoTotal_;
            }
            set
            {
                if (value != CortanteEntrepisoTotal_)
                    CortanteEntrepisoTotal_ = value;
            }
        }

        private string ConclusionX_;
        public string ConclusionX
        {
            get
            {
                return ConclusionX_;
            }
            set
            {
                if (value != ConclusionX_)
                    ConclusionX_ = value;
            }
        }

        private string ConclusionY_;
        public string ConclusionY
        {
            get
            {
                return ConclusionY_;
            }

            set
            {
                if (value != ConclusionY_)
                    ConclusionY_ = value;
            }
        }

        #endregion

    }//end of class
}//end of namespace
