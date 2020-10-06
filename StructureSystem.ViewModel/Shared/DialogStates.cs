using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.ViewModel.Shared
{
    public interface IClosable
    {
        void Close();
    }
    public interface IOpenable<T>
    {

    }

}
