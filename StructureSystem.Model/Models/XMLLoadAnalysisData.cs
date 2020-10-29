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

        private bool _IsEntrepiso;
        public bool IsEntrepiso
        {
            get { return _IsEntrepiso; }
            set
            {
                _IsEntrepiso = true;
                _IsAzotea = false;             
            }
        }
        private bool _IsAzotea;
        public bool IsAzotea
        {
            get { return _IsAzotea; }
            set
            {
                _IsAzotea = true;
                _IsEntrepiso = false;
             
            }
        }

        private FlooringMaterials _flooringMaterial;
        public FlooringMaterials FlooringMaterial
        {
            get
            {
                return _flooringMaterial;
            }
            set
            {
                if (value != _flooringMaterial)
                {
                    _flooringMaterial = value;
                }

            }
        }

    }//end of class
}//end of namespace
