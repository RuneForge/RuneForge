using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Game.Units;

namespace RuneForge.Game.Components.Implementations
{
    public class ProductionFacilityComponent : Component
    {
        public ReadOnlyCollection<UnitPrototype> UnitsProduced { get; }

        public UnitPrototype UnitCurrentlyProduced { get; set; }

        public TimeSpan TimeElapsed { get; set; }

        public bool ProductionFinished { get; set; }

        public ProductionFacilityComponent(IList<UnitPrototype> unitsProduced)
        {
            UnitsProduced = new ReadOnlyCollection<UnitPrototype>(unitsProduced);
            UnitCurrentlyProduced = null;
            TimeElapsed = TimeSpan.Zero;
            ProductionFinished = false;
        }
    }
}
