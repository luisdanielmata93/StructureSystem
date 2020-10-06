using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Data.Actions
{
    public interface IUpdatable<TEntity> where TEntity : class
    {
        bool Update(TEntity entity);
    
    }//end of interface
}//end of namespace
