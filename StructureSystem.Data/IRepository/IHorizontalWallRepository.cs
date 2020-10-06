using System.Collections.Generic;
using StructureSystem.Data.Actions;
using StructureSystem.Model;

namespace StructureSystem.Data.Model
{
    public interface IHorizontalWallRepository : IReadable<IDictionary<string, object>, string>,
                                  ICreatable<XMLWallData>, IUpdatable<XMLWallData>, IDelete<XMLWallData>
    {



    }//end of interface
}//end of namespace
