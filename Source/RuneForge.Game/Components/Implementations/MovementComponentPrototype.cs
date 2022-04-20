using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
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
