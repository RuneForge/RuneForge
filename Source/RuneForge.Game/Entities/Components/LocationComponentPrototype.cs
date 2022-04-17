namespace RuneForge.Game.Entities.Components
{
    public class LocationComponentPrototype : ComponentPrototype
    {
        public int XCells { get; }
        public int YCells { get; }

        public int WidthCells { get; }
        public int HeightCells { get; }

        public LocationComponentPrototype(int xCells, int yCells, int widthCells, int heightCells)
        {
            XCells = xCells;
            YCells = yCells;
            WidthCells = widthCells;
            HeightCells = heightCells;
        }
    }
}
