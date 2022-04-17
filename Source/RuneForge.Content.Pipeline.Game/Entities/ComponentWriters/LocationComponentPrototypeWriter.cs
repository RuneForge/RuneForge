using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Entities.Components;

namespace RuneForge.Content.Pipeline.Game.Entities.ComponentWriters
{
    public class LocationComponentPrototypeWriter : ComponentPrototypeWriter<LocationComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, LocationComponentPrototype componentPrototype)
        {
            contentWriter.Write(componentPrototype.X);
            contentWriter.Write(componentPrototype.Y);
            contentWriter.Write(componentPrototype.Width);
            contentWriter.Write(componentPrototype.Height);
        }
    }
}
