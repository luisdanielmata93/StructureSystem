using System.Collections.Generic;
using StructureSystem.Data.Actions;
using StructureSystem.Model;


namespace StructureSystem.Data
{
    public interface IStructuralDesignRepository: IReadable<IList<Storey>, string>,
                                  ICreatable<XMLStructuralDesignData>, IUpdatable<XMLStructuralDesignData>
    {



    }
}
