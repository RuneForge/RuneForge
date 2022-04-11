using System;

namespace RuneForge.Game.Players
{
    public class Player
    {
        public Guid Id { get; }

        public string Name { get; }

        public PlayerColor Color { get; }

        public Player(Guid id, string name, PlayerColor color)
        {
            Id = id;
            Name = name;
            Color = color;
        }
    }
}
