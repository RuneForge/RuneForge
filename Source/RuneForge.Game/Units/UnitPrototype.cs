using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Game.Components;

namespace RuneForge.Game.Units
{
    public class UnitPrototype
    {
        public string Name { get; }

        public string Code { get; }

        public ReadOnlyCollection<ComponentPrototype> ComponentPrototypes { get; }

        public UnitPrototype(string name, string code, IList<ComponentPrototype> componentPrototypes)
        {
            Name = name;
            Code = code;
            ComponentPrototypes = new ReadOnlyCollection<ComponentPrototype>(componentPrototypes);
        }
    }
}
