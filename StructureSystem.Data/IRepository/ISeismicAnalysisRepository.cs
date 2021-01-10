using System.Collections.Generic;
using StructureSystem.Data.Actions;
using StructureSystem.Model;

namespace StructureSystem.Data.Model
{
   public interface ISeismicAnalysisRepository : IReadable<IList<EspectroDisenio>, string>,
                                  ICreatable<XMLSeismicAnalysisData>, IUpdatable<XMLSeismicAnalysisData>
    {


    }//end of class
}//end of namespace
