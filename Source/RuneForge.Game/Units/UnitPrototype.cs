using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Game.Components;

namespace RuneForge.Game.Units
{
    public class UnitPrototype
    {
        public string Name { get; }

        public ReadOnlyCollection<ComponentPrototype> ComponentPrototypes { get; }

        public UnitPrototype(string name, IList<ComponentPrototype> componentPrototypes)
        {
            Name = name;
            ComponentPrototypes = new ReadOnlyCollection<ComponentPrototype>(componentPrototypes);
        }
    }
}
