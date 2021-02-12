using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
    public class Structure
    {

        public Structure()
        {
            Storeys = new List<Storey>();
        }
        public List<Storey> Storeys { get; set; }

        public double MasaTotal { get; set; }

        public double PeriodosCirculares { get; set; }

        public double PeriodosNaturales { get; set; }

        public double Frecuencias { get; set; }


    }//end of class
}//end of namespace
