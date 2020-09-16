using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.Data.Model;

namespace StructureSystem.Data
{
    public class UnitOfWork
    {

        public static IUnitOfWorkAdapter Create()
        {
            return new UnitOfWorkAdapter();
        }

    }//end of class
}//end of namespace
