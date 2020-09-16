using System.Collections.Generic;
using StructureSystem.Data.Actions;
using StructureSystem.Model;


namespace StructureSystem.Data.Model
{
    public interface IVerticalWallRepository : IReadable<IDictionary<string, object>, string>,
                                  ICreatable<XMLVerticalWallData>, IUpdatable<XMLVerticalWallData>
    {


    }//end of interface
}//end of namespace