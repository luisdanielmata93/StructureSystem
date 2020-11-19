using System.Collections.Generic;
using StructureSystem.Data.Actions;
using StructureSystem.Model;

namespace StructureSystem.Data.Model
{
    public interface IGeneralDataRepository : IReadable<IDictionary<string, object>, string>,
                                  ICreatable<XMLGeneralData>, IUpdatable<XMLGeneralData>
    {

    }//end of interface
}//end of namespace
