using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components.Implementations;
using RuneForge.Content.Pipeline.Game.Extensions;

namespace RuneForge.Content.Pipeline.Game.Components.PrototypeWriters
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
