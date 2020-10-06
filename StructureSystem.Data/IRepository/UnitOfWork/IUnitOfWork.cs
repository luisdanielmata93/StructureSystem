using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Data.Model
{
  public interface IUnitOfWork
    {
        IUnitOfWorkAdapter Create();

    }//end of interface
}//end of namespace
