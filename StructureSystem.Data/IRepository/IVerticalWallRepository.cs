using System.Collections.Generic;
using StructureSystem.Data.Actions;
using StructureSystem.Model;


namespace StructureSystem.Data.Model
{
    public interface IVerticalWallRepository : IReadable<IDictionary<string, object>, string>,
                                  ICreatable<XMLWallData>, IUpdatable<XMLWallData>
    {
        int Count(string documentPath, int storey);

    }//end of interface
}//end of namespace