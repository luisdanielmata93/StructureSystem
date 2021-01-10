using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
    public class XMLSeismicAnalysisData
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

       public List<EspectroDisenio> DisenioEspectral { get; set; }



    }
}
