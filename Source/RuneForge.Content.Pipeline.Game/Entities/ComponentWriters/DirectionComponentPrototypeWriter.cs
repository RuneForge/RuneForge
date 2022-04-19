using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Entities.Components;
using RuneForge.Content.Pipeline.Game.Extensions;

namespace RuneForge.Content.Pipeline.Game.Entities.ComponentWriters
{
    public class DirectionComponentPrototypeWriter : ComponentPrototypeWriter<DirectionComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, DirectionComponentPrototype componentPrototype)
        {
            contentWriter.Write(componentPrototype.Direction, (contentWriter, value) => contentWriter.Write((int)value));
        }
    }
}
