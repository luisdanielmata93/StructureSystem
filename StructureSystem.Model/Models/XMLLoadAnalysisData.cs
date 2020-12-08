using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
   public class XMLLoadAnalysisData
    {


        public string DocumentPath { get; set; }

        private int _Storey;
        public int Storey
        {
            get
            { return _Storey; }
            set
            {
                _Storey = value;
            }
        }

        private LosaMacizaEntrepiso LosaMacizaEntrepisoP_;
        public LosaMacizaEntrepiso LosaMacizaEntrepisoP
        {
            get
            {
                return LosaMacizaEntrepisoP_;
            }
            set
            {
                if (value != LosaMacizaEntrepisoP_)
                    LosaMacizaEntrepisoP_ = value;
            }
        }

        private LosaMacizaAzotea LosaMacizaAzoteaP_;
        public LosaMacizaAzotea LosaMacizaAzoteaP
        {
            get
            {
                return LosaMacizaAzoteaP_;
            }
            set
            {
                if (value != LosaMacizaAzoteaP_)
                    LosaMacizaAzoteaP_ = value;
            }
        }

        private LosaNervadaEntrepiso LosaNervadaEntrepisoP_;
        public LosaNervadaEntrepiso LosaNervadaEntrepisoP
        {
            get
            {
                return LosaNervadaEntrepisoP_;
            }
            set
            {
                if (value != LosaNervadaEntrepisoP_)
                    LosaNervadaEntrepisoP_ = value;
            }
        }

        private LosaNervadaAzotea LosaNervadaAzoteaP_;
        public LosaNervadaAzotea LosaNervadaAzoteaP
        {
            get
            {
                return LosaNervadaAzoteaP_;
            }
            set
            {
                if (value != LosaNervadaAzoteaP_)
                    LosaNervadaAzoteaP_ = value;
            }
        }

        private LosaViguetaBovedillaEntrepiso LosaViguetaBovedillaEntrepisoP_;
        public LosaViguetaBovedillaEntrepiso LosaViguetaBovedillaEntrepisoP
        {
            get
            {
                return LosaViguetaBovedillaEntrepisoP_;
            }
            set
            {
                if (value != LosaViguetaBovedillaEntrepisoP_)
                    LosaViguetaBovedillaEntrepisoP_ = value;
            }
        }

        private LosaViguetaBovedillaAzotea LosaViguetaBovedillaAzoteaP_;
        public LosaViguetaBovedillaAzotea LosaViguetaBovedillaAzoteaP
        {
            get
            {
                return LosaViguetaBovedillaAzoteaP_;
            }
            set
            {
                if (value != LosaViguetaBovedillaAzoteaP_)
                    LosaViguetaBovedillaAzoteaP_ = value;
            }
        }

        private LosaOtroTipo LosaOtroTipoP_;
        public LosaOtroTipo LosaOtroTipoP
        {
            get
            {
                return LosaOtroTipoP_;
            }
            set
            {
                if (value != LosaOtroTipoP_)
                    LosaOtroTipoP_ = value;
            }
        }




    }//end of class
}//end of namespace
