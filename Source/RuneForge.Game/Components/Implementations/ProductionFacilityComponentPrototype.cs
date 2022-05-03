using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentPrototypeReader(typeof(ProductionFacilityComponentPrototypeReader))]
    public class ProductionFacilityComponentPrototype : ComponentPrototype
    {
        public ReadOnlyCollection<string> UnitCodesProduced { get; }

        public ProductionFacilityComponentPrototype(IList<string> unitCodesProduced)
        {
            UnitCodesProduced = new ReadOnlyCollection<string>(unitCodesProduced);
        }
    }
}
