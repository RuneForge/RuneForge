using RuneForge.Game.Entities.Components;

namespace RuneForge.Game.Entities.ComponentFactories
{
    public class LocationComponentFactory : ComponentFactory<LocationComponent, LocationComponentPrototype>
    {
        public override LocationComponent CreateComponentFromPrototype(LocationComponentPrototype componentPrototype, LocationComponentPrototype componentPrototypeOverride)
        {
            int? xCells = componentPrototype.XCells;
            int? yCells = componentPrototype.YCells;
            int widthCells = (int)componentPrototype.WidthCells;
            int heightCells = (int)componentPrototype.HeightCells;

            if (componentPrototypeOverride != null)
            {
                if (componentPrototypeOverride.XCells.HasValue)
                    xCells = (int)componentPrototypeOverride.XCells;
                if (componentPrototypeOverride.YCells.HasValue)
                    yCells = (int)componentPrototypeOverride.YCells;
            }

            return LocationComponent.CreateFromCellLocation((int)xCells, (int)yCells, widthCells, heightCells);
        }
    }
}
