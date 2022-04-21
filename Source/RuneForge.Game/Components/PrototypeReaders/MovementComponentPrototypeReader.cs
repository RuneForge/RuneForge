using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Entities;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class MovementComponentPrototypeReader : ComponentPrototypeReader<MovementComponentPrototype>
    {
        public override MovementComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            float movementSpeed = contentReader.ReadSingle();
            MovementFlags movementType = (MovementFlags)contentReader.ReadInt32();
            return new MovementComponentPrototype(movementSpeed, movementType);
        }
    }
}
