using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Factories;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentFactory(typeof(MovementComponentFactory))]
    [ComponentPrototypeReader(typeof(MovementComponentPrototypeReader))]
    public class MovementComponentPrototype : ComponentPrototype
    {
        public float MovementSpeed { get; }

        public MovementComponentPrototype(float movementSpeed)
        {
            MovementSpeed = movementSpeed;
        }
    }
}
