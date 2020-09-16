using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureSystem.Data.Model;

namespace StructureSystem.Data
{
    public class DocumentDataContext : IDocumentDataContext
    {
        public IGeneralDataRepository GeneralData { get; private set; }
        public IHorizontalWallRepository HorizontalWallsData { get; private set; }
        public IVerticalWallRepository VerticalWallsData { get; private set; }
        public  DocumentDataContext()
        {
            GeneralData = new GeneralData();
            HorizontalWallsData = new HorizontalWallsData();
            VerticalWallsData = new VerticalWallsData();
        }
    }//end of class
}//end of namespace
