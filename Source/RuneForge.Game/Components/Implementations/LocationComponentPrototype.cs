using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Factories;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentFactory(typeof(LocationComponentFactory))]
    [ComponentPrototypeReader(typeof(LocationComponentPrototypeReader))]
    public class LocationComponentPrototype : ComponentPrototype
    {
        public int? XCells { get; }
        public int? YCells { get; }

        public int? WidthCells { get; }
        public int? HeightCells { get; }

        public LocationComponentPrototype(int? xCells, int? yCells, int? widthCells, int? heightCells)
        {
            XCells = xCells;
            YCells = yCells;
            WidthCells = widthCells;
            HeightCells = heightCells;
        }
    }
}
