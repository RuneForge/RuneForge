using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Game.Components;

namespace RuneForge.Game.Buildings
{
    public class BuildingInstancePrototype
    {
        public Guid OwnerId { get; }

        public BuildingPrototype EntityPrototype { get; }

        public ReadOnlyCollection<ComponentPrototype> ComponentPrototypeOverrides { get; }

        public BuildingInstancePrototype(Guid ownerId, BuildingPrototype entityPrototype, IList<ComponentPrototype> componentPrototypeOverrides)
        {
            OwnerId = ownerId;
            EntityPrototype = entityPrototype;
            ComponentPrototypeOverrides = new ReadOnlyCollection<ComponentPrototype>(componentPrototypeOverrides);
        }
    }
}
