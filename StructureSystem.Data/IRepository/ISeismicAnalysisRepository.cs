using System.Collections.Generic;
using StructureSystem.Data.Actions;
using StructureSystem.Model;

namespace StructureSystem.Data.Model
{
   public interface ISeismicAnalysisRepository : IReadable<IList<Storey>, string>,
                                  ICreatable<XMLStructureData>, IUpdatable<XMLStructureData>
    {



    }//end of class
}//end of namespace
