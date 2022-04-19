using RuneForge.Game.Maps;

namespace RuneForge.Game.Entities.Components
{
    public class DirectionComponentPrototype : ComponentPrototype
    {
        public Directions? Direction { get; }

        public DirectionComponentPrototype(Directions? direction)
        {
            Direction = direction;
        }
    }
}
