using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Game.Components;

namespace RuneForge.Game.Buildings
{
    public class BuildingPrototype
    {
        public string Name { get; }

        public ReadOnlyCollection<ComponentPrototype> ComponentPrototypes { get; }

        public BuildingPrototype(string name, IList<ComponentPrototype> componentPrototypes)
        {
            Name = name;
            ComponentPrototypes = new ReadOnlyCollection<ComponentPrototype>(componentPrototypes);
        }
    }
}
