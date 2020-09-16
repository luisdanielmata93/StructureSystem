using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.Data.Model;

namespace StructureSystem.Data
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
       public IDocumentDataContext DocumentDataContext { get; }

      

        public UnitOfWorkRepository()
        {
            DocumentDataContext = new DocumentDataContext();
           
        }


    }//end of class
}//end of namespace
