using System;

namespace StructureSystem.Data.Model
{
    public interface IUnitOfWorkAdapter : IDisposable
    {
        IUnitOfWorkRepository Repositories { get; }
        void SaveChanges();

    }//end of interface
}//end of namespace
