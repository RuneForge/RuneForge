using System;
using System.Collections.Generic;

using RuneForge.Game.Components.Interfaces;
using RuneForge.Game.Entities;

namespace RuneForge.Game.Players
{
    public class Player : Entity
    {
        public Guid Id { get; }

        public string Name { get; }

        public PlayerColor Color { get; }

        public Player(Guid id, string name, PlayerColor color, IList<IComponent> components)
            : base(components)
        {
            Id = id;
            Name = name;
            Color = color;
        }
    }
}
