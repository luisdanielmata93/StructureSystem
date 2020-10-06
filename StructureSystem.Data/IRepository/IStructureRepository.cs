using System.Collections.Generic;
using StructureSystem.Data.Actions;
using StructureSystem.Model;


namespace StructureSystem.Data.Model
{
    public interface IStructureRepository : IReadable<IList<Storey>, string>,
                                  ICreatable<XMLStructureData>, IUpdatable<XMLStructureData>
    {


    }//end of interface
}//end of namespace
