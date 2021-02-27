using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.Model;
using System.ComponentModel;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

namespace StructureSystem.Model
{
    public class Wall //: INotifyPropertyChanged
    {
        public Wall()
        {
            this.Tipos = new ObservableCollection<string> { "Interior", "Extremo" };
            this.Tipo = "Interior";
            this.Fy = 4200; //temp
            this.EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal = 6000;


        }

        public int WallNumber { get; set; }
        public int Storey { get; set; }
        public string Material { get; set; }
        public double Length { get; set; }
        public double TributaryArea { get; set; }
        public double Thickness { get; set; }
        public double Height { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public Enums.SideType Side { get; set; }

        public double Inercia { get; set; }
        public double AreaLongitudinal { get; set; }
        public double RigidezLateral { get; set; }

        public double WMuro { get; set; }
        public double WLosa { get; set; }
        public double MSup { get; set; }
        public double WTotal { get; set; }


        public double CortanteDirecto { get; set; }

        public string Clasificacion { get; set; }
        public double ExcentricidadDisenioX { get; set; }
        public double ExcentricidadDisenioY { get; set; }

        public double CortantePorTorsionX { get; set; }
        public double CortantePorTorsionY { get; set; }


        public double CortanteTotales { get; set; }
        public double MomentoVolteo { get; set; }

        public double CargaAxialMaxima { get; set; }
        public double CargaAxialSismo { get; set; }

        public double Peso { get; set; }

        #region Public Properties

        private double ExcentricidadDeCarga_;
        public double ExcentricidadDeCarga
        {
            get
            {
                return ExcentricidadDeCarga_;
            }
            set
            {
                if (value != ExcentricidadDeCarga_)
                {
                    ExcentricidadDeCarga_ = value;
                }
            }
        }


        private double AnchoDeApoyo_;
        public double AnchoDeApoyo
        {
            get
            {
                return AnchoDeApoyo_;
            }
            set
            {
                if (value != AnchoDeApoyo_)
                {
                    AnchoDeApoyo_ = value;
                }
            }
        }


        private ObservableCollection<string> Tipos_;
        public ObservableCollection<string> Tipos
        {
            get
            {
                return Tipos_;
            }
            set
            {
                Tipos_ = value;
            }
        }

        private string Tipo_;
        public string Tipo
        {
            get
            {
                return Tipo_;
            }
            set
            {
                if (value != Tipo_)
                {
                    Tipo_ = value;
                }
            }
        }

        private bool RestringidoSuperiorInferior_;
        public bool RestringidoSuperiorInferior
        {
            get
            {
                return RestringidoSuperiorInferior_;
            }
            set
            {
                if (value != RestringidoSuperiorInferior_)
                {
                    RestringidoSuperiorInferior_ = value;

                    if (value)
                    {
                        IsReadOnlyLCI = true;
                        IsReadOnlyLCD = true;
                    }
                    else
                    {
                        IsReadOnlyLCI = false;
                        IsReadOnlyLCD = false;
                    }
                }
            }
        }

        private bool AceroDeRefuerzo_;
        public bool AceroDeRefuerzo
        {
            get
            {
                return AceroDeRefuerzo_;
            }
            set
            {
                if (value != AceroDeRefuerzo_)
                    AceroDeRefuerzo_ = value;
            }
        }


        private double Ash_;
        public double Ash
        {
            get
            {
                return Ash_;
            }
            set
            {
                if (value != Ash_)
                    Ash_ = value;
            }
        }

        private double Sh_;
        public double Sh
        {
            get
            {
                return Sh_;
            }
            set
            {
                if (value != Sh_)
                    Sh_ = value;
            }
        }

        private double FE_;
        public double FE
        {
            get
            {
                return FE_;
            }
            set
            {
                if (value != FE_)
                {
                    FE_ = value;
                }
            }
        }

        private bool IsReadOnlyLCI_ = false;
        public bool IsReadOnlyLCI
        {
            get
            {
                return IsReadOnlyLCI_;
            }
            set
            {
                if (value != IsReadOnlyLCI_)
                    IsReadOnlyLCI_ = value;
            }
        }

        private double LongClaroIzquierdo_;
        public double LongClaroIzquierdo
        {
            get
            {
                return LongClaroIzquierdo_;
            }
            set
            {
                if (value != LongClaroIzquierdo_)
                {
                    LongClaroIzquierdo_ = value;
                }
            }
        }


        private bool IsReadOnlyLCD_ = false;
        public bool IsReadOnlyLCD
        {
            get
            {
                return IsReadOnlyLCD_;
            }
            set
            {
                if (value != IsReadOnlyLCD_)
                    IsReadOnlyLCD_ = value;
            }
        }

        private double LongClaroDerecho_;
        public double LongClaroDerecho
        {
            get
            {
                return LongClaroDerecho_;
            }
            set
            {
                if (value != LongClaroDerecho_)
                {
                    LongClaroDerecho_ = value;
                }
            }
        }

        private double FactorAlturaEfectiva_;
        public double FactorAlturaEfectiva
        {
            get
            {
                return FactorAlturaEfectiva_;
            }
            set
            {
                if (value != FactorAlturaEfectiva_)
                {
                    FactorAlturaEfectiva_ = value;
                }
            }
        }


        private double Fy_;
        public double Fy
        {
            get
            {
                return Fy_;
            }
            set
            {
                if (value != Fy_)
                    Fy_ = value;

            }
        }

        private double PR_;
        public double PR
        {
            get
            {
                return PR_;
            }
            set
            {
                if (value != PR_)
                    PR_ = value;

            }
        }


        private double Mo_;
        public double Mo
        {
            get
            {
                return Mo_;
            }
            set
            {
                if (value != Mo_)
                    Mo_ = value;
            }
        }

        private double P1X_;
        public double P1X
        {
            get
            {
                return P1X_;
            }
            set
            {
                if (value != P1X_)
                    P1X_ = value;
            }
        }

        private double P1Y_;
        public double P1Y
        {
            get
            {
                return P1Y_;
            }
            set
            {
                if (value != P1Y_)
                    P1Y_ = value;
            }
        }


        private double P2X_;
        public double P2X
        {
            get
            {
                return P2X_;
            }
            set
            {
                if (value != P2X_)
                    P2X_ = value;
            }
        }


        private double P2Y_;
        public double P2Y
        {
            get
            {
                return P2Y_;
            }
            set
            {
                if (value != P2Y_)
                    P2Y_ = value;
            }
        }


        private double P3X_;
        public double P3X
        {
            get
            {
                return P3X_;
            }
            set
            {
                if (value != P3X_)
                    P3X_ = value;
            }
        }


        private double P3Y_;
        public double P3Y
        {
            get
            {
                return P3Y_;
            }
            set
            {
                if (value != P3Y_)
                    P3Y_ = value;
            }
        }


        private double P4X_;
        public double P4X
        {
            get
            {
                return P4X_;
            }
            set
            {
                if (value != P4X_)
                    P4X_ = value;
            }
        }

        private double P4Y_;
        public double P4Y
        {
            get
            {
                return P4Y_;
            }
            set
            {
                if (value != P4Y_)
                    P4Y_ = value;
            }
        }


        private double P5X_;
        public double P5X
        {
            get
            {
                return P5X_;
            }
            set
            {
                if (value != P5X_)
                    P5X_ = value;
            }
        }

        private double P5Y_;
        public double P5Y
        {
            get
            {
                return P5Y_;
            }
            set
            {
                if (value != P5Y_)
                    P5Y_ = value;
            }
        }


        private double TP1P2_;
        public double TP1P2
        {
            get
            {
                return TP1P2_;
            }
            set
            {
                if (value != TP1P2_)
                    TP1P2_ = value;
            }
        }

        private double TP2P3_;
        public double TP2P3
        {
            get
            {
                return TP2P3_;
            }
            set
            {
                if (value != TP2P3_)
                    TP2P3_ = value;
            }
        }

        private double TP3P4_;
        public double TP3P4
        {
            get
            {
                return TP3P4_;
            }
            set
            {
                if (value != TP3P4_)
                    TP3P4_ = value;
            }
        }


        private double TP4P5_;
        public double TP4P5
        {
            get
            {
                return TP4P5_;
            }
            set
            {
                if (value != TP4P5_)
                    TP4P5_ = value;
            }
        }

        private double As_;
        public double As
        {
            get
            {
                return As_;
            }
            set
            {
                if (value != As_)
                    As_ = value;
            }
        }

        private double bc_;
        public double bc
        {
            get
            {
                return bc_;
            }
            set
            {
                if (value != bc_)
                    bc_ = value;
            }
        }


        private double ResMamposteriaCortante_;
        public double ResMamposteriaCortante
        {
            get
            {
                return ResMamposteriaCortante_;
            }
            set
            {
                if (value != ResMamposteriaCortante_)
                    ResMamposteriaCortante_ = value;
            }
        }

        private double ResAceroRefuerzoHorizontalCortante_;
        public double ResAceroRefuerzoHorizontalCortante
        {
            get
            {
                return ResAceroRefuerzoHorizontalCortante_;
            }
            set
            {
                if (value != ResAceroRefuerzoHorizontalCortante_)
                    ResAceroRefuerzoHorizontalCortante_ = value;
            }
        }

        private double EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal_;
        public double EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal
        {
            get
            {
                return EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal_;
            }
            set
            {
                if (value != EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal_)
                    EsfuerzoDeFluenciaDelAceroRefuerzoHorizontal_ = value;
            }
        }

        private double Dfc_;
        public double Dfc
        {
            get
            {
                return Dfc_;
            }
            set
            {
                if (value != Dfc_)
                    Dfc_ = value;
            }
        }

        private double ResistenciaTotalACortante_;
        public double ResistenciaTotalACortante
        {
            get
            {
                return ResistenciaTotalACortante_;
            }
            set
            {
                if (value != ResistenciaTotalACortante_)
                    ResistenciaTotalACortante_ = value;
            }
        }


        private double AreaAceroDeRefuerzoCortante_;
        public double AreaAceroDeRefuerzoCortante
        {
            get
            {
                return AreaAceroDeRefuerzoCortante_;
            }
            set
            {
                if (value != AreaAceroDeRefuerzoCortante_)
                    AreaAceroDeRefuerzoCortante_ = value;
            }
        }

        private double SeparacionRefuerzoHorizontal_;
        public double SeparacionRefuerzoHorizontal
        {
            get
            {
                return SeparacionRefuerzoHorizontal_;
            }
            set
            {
                if (value != SeparacionRefuerzoHorizontal_)
                    SeparacionRefuerzoHorizontal_ = value;
            }
        }

        private double ResistenciaCortanteConcreto_;
        public double ResistenciaCortanteConcreto
        {
            get
            {
                return ResistenciaCortanteConcreto_;
            }
            set
            {
                if (value != ResistenciaCortanteConcreto_)
                    ResistenciaCortanteConcreto_ = value;
            }
        }

        private bool Concreto_;
        public bool Concreto
        {
            get
            {
                return Concreto_;
            }
            set
            {
                if (value != Concreto_)
                    Concreto_ = value;
            }
        }

        private bool Mamposteria_;
        public bool Mamposteria
        {
            get
            {
                return Mamposteria_;
            }
            set
            {
                if (value != Mamposteria_)
                    Mamposteria_ = value;
            }
        }

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



        #region Metodos

        //public void EsConcretos(ref Wall wall)
        //{
        //    if (wall.Material.Equals("Concreto"))
        //    {
        //        wall.Fy = 4200;
        //        wall.Concreto = true;
        //        wall.Mamposteria = false;
        //    }
        //    else
        //    {
        //        wall.Concreto = false;
        //        wall.Mamposteria = true;
        //    }

        //}

        //Predicate<Wall> EsConcreto = wall => wall.Material.Equals("Concreto") ? wall.Fy=4200
        #endregion

    }//end of class
}//end of namespace
