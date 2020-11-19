using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
    public class FlooringMaterial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        Enums.MaterialType Type { get; set; }
        public string _TEST;
        public string TEST {
            get
            {
                return _TEST;
            }
            set
            {
                _TEST = value;
            }
        }
    }//end of class
}//end of namespace
