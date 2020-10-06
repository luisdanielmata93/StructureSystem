using System;
using System.Collections.Generic;

namespace StructureSystem.Model
{
    public class XMLStructureData
    {
        public List<Storey> Storeys { get; set; }
        public string DocumentPath { get; set; }

        public XMLStructureData()
        {
            Storeys = new List<Storey>();
        }
    }//end of class
}//end of namespace
