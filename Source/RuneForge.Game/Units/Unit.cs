using System.Collections.Generic;

using RuneForge.Game.Entities;
using RuneForge.Game.Entities.Interfaces;
using RuneForge.Game.Players;

namespace RuneForge.Game.Units
{
    public class Unit : Entity
    {
        public int Id { get; }

        public string Name { get; }

        public Player Owner { get; }

        public Unit(int id, string name, Player owner, IList<IComponent> components)
            : base(components)
        {
            Id = id;
            Name = name;
            Owner = owner;
        }
    }
}
