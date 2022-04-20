using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Factories;
using RuneForge.Game.Components.PrototypeReaders;
using RuneForge.Game.Maps;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentFactory(typeof(DirectionComponentFactory))]
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
