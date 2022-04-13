namespace RuneForge.Game.Maps
{
    public class MapDecoration
    {
        public int Id { get; }

        public string Name { get; }

        public int X { get; }
        public int Y { get; }

        public MapDecoration(int id, string name, int x, int y)
        {
            Id = id;
            Name = name;
            X = x;
            Y = y;
        }
    }
}
