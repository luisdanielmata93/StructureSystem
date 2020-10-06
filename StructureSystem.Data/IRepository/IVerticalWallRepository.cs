using System.Collections.Generic;
using StructureSystem.Data.Actions;
using StructureSystem.Model;


namespace StructureSystem.Data.Model
{
    public interface IVerticalWallRepository : IReadable<IDictionary<string, object>, string>,
                                  ICreatable<XMLWallData>, IUpdatable<XMLWallData>
    {


    }//end of interface
}//end of namespace