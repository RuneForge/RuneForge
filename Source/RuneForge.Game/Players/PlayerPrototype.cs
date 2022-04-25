using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Game.Components;

namespace RuneForge.Game.Players
{
    public class PlayerPrototype
    {
        public Guid Id { get; }

        public string Name { get; }

        public PlayerColor Color { get; }

        public ReadOnlyCollection<ComponentPrototype> ComponentPrototypes { get; }

        public PlayerPrototype(Guid id, string name, PlayerColor color, IList<ComponentPrototype> componentPrototypes)
        {
            Id = id;
            Name = name;
            Color = color;
            ComponentPrototypes = new ReadOnlyCollection<ComponentPrototype>(componentPrototypes);
        }
    }
}
