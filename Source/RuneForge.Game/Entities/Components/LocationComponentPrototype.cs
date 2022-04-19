using RuneForge.Game.Entities.Attributes;
using RuneForge.Game.Entities.ComponentFactories;
using RuneForge.Game.Entities.ComponentReaders;

namespace RuneForge.Game.Entities.Components
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
