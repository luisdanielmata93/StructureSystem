using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.Data.Model;

namespace StructureSystem.Data
{
    public class UnitOfWorkAdapter : IUnitOfWorkAdapter
    {
      
        public IUnitOfWorkRepository Repositories { get; private set; }

        public UnitOfWorkAdapter()
        {
            try
            {
                Repositories = new UnitOfWorkRepository();
            }
            catch (Exception) { throw; }
        }

        public void Dispose()
        {
            Repositories = null;
        }

        public void SaveChanges()
        {
        }

    }//end of class
}//end of namespace
