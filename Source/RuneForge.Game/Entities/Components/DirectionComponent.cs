using RuneForge.Game.Maps;

namespace RuneForge.Game.Entities.Components
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
