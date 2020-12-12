using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
    public class Enums
    {
        public enum ActionType
        {
            Create,
            Update,
            Delete,
            Get,
            NonAction

        }


        public enum MaterialType
        {
            None,
            MacisaEntrepiso,
            MacisaAzotea,
            NervadaEntrepiso,
            NervadaAzotea,
            ViguetaBovedillaEntrepiso,
            ViguetaBovedillaAzotea,
            Otra
        }

        public enum SideType { Vertical, Horizontal }

        public enum Coordenate { X, Y }
    }
}
