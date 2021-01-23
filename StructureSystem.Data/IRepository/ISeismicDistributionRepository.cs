using StructureSystem.Data.Actions;
using StructureSystem.Model;
using System.Collections.Generic;

namespace StructureSystem.Data.Model
{
    public interface ISeismicDistributionRepository : IReadable<XMLSeismicDistributionData, XMLSeismicDistributionData>,
                                  ICreatable<XMLSeismicDistributionData>, IUpdatable<XMLSeismicDistributionData>
    {

         XMLSeismicDistributionData GetEntrepisos(XMLSeismicDistributionData data);
        Dictionary<string, double> GetStaticData(string document);
    }
}
