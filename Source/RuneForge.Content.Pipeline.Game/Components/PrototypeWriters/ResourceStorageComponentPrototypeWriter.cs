using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components.Implementations;

namespace RuneForge.Content.Pipeline.Game.Components.PrototypeWriters
{
    public class ResourceStorageComponentPrototypeWriter : ComponentPrototypeWriter<ResourceStorageComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, ResourceStorageComponentPrototype componentPrototype)
        {
            contentWriter.Write((int)componentPrototype.AcceptedResourceTypes);
            contentWriter.Write(componentPrototype.TransferTimeMilliseconds);
        }
    }
}
