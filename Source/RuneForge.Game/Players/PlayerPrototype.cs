using System;

namespace RuneForge.Game.Players
{
    public class PlayerPrototype
    {
        public Guid Id { get; }

        public string Name { get; }

        public PlayerColor Color { get; }

        public PlayerPrototype(Guid id, string name, PlayerColor color)
        {
            Id = id;
            Name = name;
            Color = color;
        }
    }
}
