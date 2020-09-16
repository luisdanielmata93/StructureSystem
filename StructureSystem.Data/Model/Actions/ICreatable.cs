using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Data.Actions
{
    public interface ICreatable<TEntity> where TEntity : class
    {
        bool Create(TEntity entity);
  
    }//end of interface
}//end of namespace
