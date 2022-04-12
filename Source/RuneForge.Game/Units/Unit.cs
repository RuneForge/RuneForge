using RuneForge.Game.Players;

namespace RuneForge.Game.Units
{
    public class Unit
    {
        public int Id { get; }

        public string Name { get; }

        public Player Owner { get; }

        public Unit(int id, string name, Player owner)
        {
            Id = id;
            Name = name;
            Owner = owner;
        }
    }
}
