using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Data.Actions
{
    public interface IListable<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

    }//end of interface
}//end of namespace
