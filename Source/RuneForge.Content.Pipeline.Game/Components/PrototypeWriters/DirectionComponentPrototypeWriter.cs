using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components.Implementations;
using RuneForge.Content.Pipeline.Game.Extensions;

namespace RuneForge.Content.Pipeline.Game.Components.PrototypeWriters
{
    public class DirectionComponentPrototypeWriter : ComponentPrototypeWriter<DirectionComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, DirectionComponentPrototype componentPrototype)
        {
            contentWriter.Write(componentPrototype.Direction, (contentWriter, value) => contentWriter.Write((int)value));
        }
    }
}
