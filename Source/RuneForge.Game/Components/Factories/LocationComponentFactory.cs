using RuneForge.Data.Components;
using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    [ComponentDto(typeof(LocationComponentDto))]
    public class LocationComponentFactory : ComponentFactory<LocationComponent, LocationComponentPrototype, LocationComponentDto>
    {
        public override LocationComponent CreateComponentFromPrototype(LocationComponentPrototype componentPrototype, LocationComponentPrototype componentPrototypeOverride)
        {
            int xCells = componentPrototype.XCells ?? 0;
            int yCells = componentPrototype.YCells ?? 0;
            int widthCells = (int)componentPrototype.WidthCells;
            int heightCells = (int)componentPrototype.HeightCells;

            if (componentPrototypeOverride != null)
            {
                if (componentPrototypeOverride.XCells.HasValue)
                    xCells = (int)componentPrototypeOverride.XCells;
                if (componentPrototypeOverride.YCells.HasValue)
                    yCells = (int)componentPrototypeOverride.YCells;
            }

            return LocationComponent.CreateFromCellLocation(xCells, yCells, widthCells, heightCells);
        }

        public override LocationComponent CreateComponentFromDto(LocationComponentDto componentDto)
        {
            return new LocationComponent(componentDto.X, componentDto.Y, componentDto.Width, componentDto.Height);
        }
    }
}
