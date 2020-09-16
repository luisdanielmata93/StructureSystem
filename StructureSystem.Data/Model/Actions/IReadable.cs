using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Data.Actions
{
    public interface IReadable<TEntity,Tid> where TEntity : class
    {
        TEntity Get(Tid elementName);
    
    }//end of interface
}//end of namespace
