using RuneForge.Game.Players;

namespace RuneForge.Game.Buildings
{
    public class Building
    {
        public int Id { get; }

        public string Name { get; }

        public Player Owner { get; }

        public Building(int id, string name, Player owner)
        {
            Id = id;
            Name = name;
            Owner = owner;
        }
    }
}
