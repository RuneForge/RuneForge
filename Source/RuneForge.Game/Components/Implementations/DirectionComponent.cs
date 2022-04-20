using RuneForge.Game.Maps;

namespace RuneForge.Game.Components.Implementations
{
    public class DirectionComponent : Component
    {
        public Directions Direction { get; set; }

        public DirectionComponent(Directions direction)
        {
            Direction = direction;
        }
    }
}
