using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
    public class XMLStructuralDesignData
    {
        public List<Storey> Storeys { get; set; }
        public string DocumentPath { get; set; }

        public XMLStructuralDesignData()
        {
            Storeys = new List<Storey>();
        }
    }
}
