using System.Collections.Generic;
using StructureSystem.Data.Actions;
using StructureSystem.Model;

namespace StructureSystem.Data.Model
{
    public interface IHorizontalWallRepository : IReadable<IDictionary<string, object>, string>,
                                  ICreatable<XMLHorizontalWallData>, IUpdatable<XMLHorizontalWallData>
    {



    }//end of interface
}//end of namespace
