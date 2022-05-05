using RuneForge.Data.Components;
using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Maps;

namespace RuneForge.Game.Components.Factories
{
    [ComponentDto(typeof(DirectionComponentDto))]
    public class DirectionComponentFactory : ComponentFactory<DirectionComponent, DirectionComponentPrototype, DirectionComponentDto>
    {
        public override DirectionComponent CreateComponentFromPrototype(DirectionComponentPrototype componentPrototype, DirectionComponentPrototype componentPrototypeOverride)
        {
            Directions? direction = componentPrototype.Direction;

            if (componentPrototypeOverride?.Direction.HasValue ?? false)
                direction = componentPrototypeOverride.Direction;

            return new DirectionComponent((Directions)direction);
        }

        public override DirectionComponent CreateComponentFromDto(DirectionComponentDto componentDto)
        {
            return new DirectionComponent((Directions)componentDto.Direction);
        }
    }
}
