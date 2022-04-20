using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Game.Components;

namespace RuneForge.Game.Units
{
    public class UnitInstancePrototype
    {
        public Guid OwnerId { get; }

        public UnitPrototype EntityPrototype { get; }

        public ReadOnlyCollection<ComponentPrototype> ComponentPrototypeOverrides { get; }

        public UnitInstancePrototype(Guid ownerId, UnitPrototype entityPrototype, IList<ComponentPrototype> componentPrototypeOverrides)
        {
            OwnerId = ownerId;
            EntityPrototype = entityPrototype;
            ComponentPrototypeOverrides = new ReadOnlyCollection<ComponentPrototype>(componentPrototypeOverrides);
        }
    }
}
