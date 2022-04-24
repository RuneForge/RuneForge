using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components.Implementations;

namespace RuneForge.Content.Pipeline.Game.Components.PrototypeWriters
{
    public class ResourceSourceComponentPrototypeWriter : ComponentPrototypeWriter<ResourceSourceComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, ResourceSourceComponentPrototype componentPrototype)
        {
            contentWriter.Write((int)componentPrototype.ResourceType);
            contentWriter.Write(componentPrototype.AmountGiven);
            contentWriter.Write(componentPrototype.ExtractionTimeMilliseconds);
        }
    }
}
