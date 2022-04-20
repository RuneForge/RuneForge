using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class MovementComponentPrototypeReader : ComponentPrototypeReader<MovementComponentPrototype>
    {
        public override MovementComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            float movementSpeed = contentReader.ReadSingle();
            return new MovementComponentPrototype(movementSpeed);
        }
    }
}
