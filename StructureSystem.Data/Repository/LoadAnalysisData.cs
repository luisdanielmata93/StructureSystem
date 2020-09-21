using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.Data.Model;
using System.Configuration;
using System.Xml.Linq;
using StructureSystem.Model;

namespace StructureSystem.Data
{
    public class LoadAnalysisData : ILoadAnalysisRepository
    {
        public bool Create(XMLGeneralData entity)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> Get(string elementName)
        {
            throw new NotImplementedException();
        }

        public bool Update(XMLGeneralData entity)
        {
            throw new NotImplementedException();
        }
    }//end of class
}//end of namespace
