using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RuneForge.Game.Components.Implementations
{
    public class ProductionFacilityComponentPrototype : ComponentPrototype
    {
        public ReadOnlyCollection<string> UnitCodesProduced { get; }

        public ProductionFacilityComponentPrototype(IList<string> unitCodesProduced)
        {
            UnitCodesProduced = new ReadOnlyCollection<string>(unitCodesProduced);
        }
    }
}
