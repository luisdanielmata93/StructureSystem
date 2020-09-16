using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Data.Model
{
    public interface IUnitOfWorkRepository
    {
      IDocumentDataContext DocumentDataContext { get; }
    }//end of interface
}//end of namespace
