using System.Collections.Generic;

using RuneForge.Game.Components.Interfaces;
using RuneForge.Game.Entities;
using RuneForge.Game.Players;

namespace RuneForge.Game.Buildings
{
    public class Building : Entity
    {
        public int Id { get; }

        public string Name { get; }

        public Player Owner { get; }

        public Building(int id, string name, Player owner, IList<IComponent> components)
            : base(components)
        {
            Id = id;
            Name = name;
            Owner = owner;
        }
    }
}
