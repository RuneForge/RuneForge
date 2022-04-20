using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Maps;

namespace RuneForge.Game.Components.Factories
{
    public class DirectionComponentFactory : ComponentFactory<DirectionComponent, DirectionComponentPrototype>
    {
        public override DirectionComponent CreateComponentFromPrototype(DirectionComponentPrototype componentPrototype, DirectionComponentPrototype componentPrototypeOverride)
        {
            Directions? direction = componentPrototype.Direction;

            if (componentPrototypeOverride?.Direction.HasValue ?? false)
                direction = componentPrototypeOverride.Direction;

            return new DirectionComponent((Directions)direction);
        }
    }
}
