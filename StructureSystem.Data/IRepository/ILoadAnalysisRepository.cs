using StructureSystem.Data.Actions;
using StructureSystem.Model;
using System.Collections.Generic;

namespace StructureSystem.Data.Model
{
    public interface ILoadAnalysisRepository : IReadable<XMLLoadAnalysisData, XMLLoadAnalysisData>,
                                  ICreatable<XMLLoadAnalysisData>, IUpdatable<XMLLoadAnalysisData>
    {
        bool IsSaved(string documentPath, int Level);

    }//end of interface
}//end of namespace