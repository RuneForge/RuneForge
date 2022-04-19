using RuneForge.Game.Entities.Attributes;
using RuneForge.Game.Entities.ComponentReaders;
using RuneForge.Game.Maps;

namespace RuneForge.Game.Entities.Components
{
    [ComponentPrototypeReader(typeof(DirectionComponentPrototypeReader))]
    public class DirectionComponentPrototype : ComponentPrototype
    {
        public Directions? Direction { get; }

        public DirectionComponentPrototype(Directions? direction)
        {
            Direction = direction;
        }
    }
}
