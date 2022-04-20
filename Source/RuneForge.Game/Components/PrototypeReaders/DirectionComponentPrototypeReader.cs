using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Extensions;
using RuneForge.Game.Maps;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class DirectionComponentPrototypeReader : ComponentPrototypeReader<DirectionComponentPrototype>
    {
        public override DirectionComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            Directions? direction = contentReader.ReadNullable(contentReader => (Directions)contentReader.ReadInt32());
            return new DirectionComponentPrototype(direction);
        }
    }
}
