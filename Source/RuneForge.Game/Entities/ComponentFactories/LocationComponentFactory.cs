using RuneForge.Game.Entities.Components;

namespace RuneForge.Game.Entities.ComponentFactories
{
    public class LocationComponentFactory : ComponentFactory<LocationComponent, LocationComponentPrototype>
    {
        public override LocationComponent CreateComponentFromPrototype(LocationComponentPrototype componentPrototype, LocationComponentPrototype componentPrototypeOverride)
        {
            int xCells = componentPrototypeOverride.XCells;
            int yCells = componentPrototypeOverride.YCells;
            int widthCells = componentPrototype.WidthCells;
            int heightCells = componentPrototype.HeightCells;
            return LocationComponent.CreateFromCellLocation(xCells, yCells, widthCells, heightCells);
        }
    }
}
