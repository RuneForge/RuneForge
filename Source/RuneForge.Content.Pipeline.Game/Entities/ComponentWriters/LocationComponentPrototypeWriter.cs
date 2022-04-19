using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Entities.Components;
using RuneForge.Content.Pipeline.Game.Extensions;

namespace RuneForge.Content.Pipeline.Game.Entities.ComponentWriters
{
    public class LocationComponentPrototypeWriter : ComponentPrototypeWriter<LocationComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, LocationComponentPrototype componentPrototype)
        {
            contentWriter.Write(componentPrototype.X, (contentWriter, value) => contentWriter.Write(value));
            contentWriter.Write(componentPrototype.Y, (contentWriter, value) => contentWriter.Write(value));
            contentWriter.Write(componentPrototype.Width, (contentWriter, value) => contentWriter.Write(value));
            contentWriter.Write(componentPrototype.Height, (contentWriter, value) => contentWriter.Write(value));
        }
    }
}
