using System.Collections.Generic;
using StructureSystem.Data.Actions;
using StructureSystem.Model;

namespace StructureSystem.Data.Model
{
    public interface ISeismicAnalysisRepository : IReadable<IList<Storey>, string>,
                                   ICreatable<XMLSeismicAnalysisData>, IUpdatable<XMLSeismicAnalysisData>
    {

        IList<EspectroDisenio> GetEspectroDisenio(string XMLPath);
        bool GuardarProps(string document, double c, double Q, double R);
    }//end of class
}//end of namespace
