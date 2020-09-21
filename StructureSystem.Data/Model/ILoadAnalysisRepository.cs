using StructureSystem.Data.Actions;
using StructureSystem.Model;
using System.Collections.Generic;

namespace StructureSystem.Data.Model
{
    public interface ILoadAnalysisRepository : IReadable<IDictionary<string, object>, string>,
                                  ICreatable<XMLGeneralData>, IUpdatable<XMLGeneralData>
    {


    }//end of interface
}//end of namespace