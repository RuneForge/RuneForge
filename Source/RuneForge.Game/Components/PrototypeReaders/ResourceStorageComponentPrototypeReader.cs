using System;

using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Entities;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class ResourceStorageComponentPrototypeReader : ComponentPrototypeReader<ResourceStorageComponentPrototype>
    {
        public override ResourceStorageComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            ResourceTypes acceptedResourceTypes = (ResourceTypes)contentReader.ReadInt32();
            float transferTimeMilliseconds = contentReader.ReadSingle();
            return new ResourceStorageComponentPrototype(acceptedResourceTypes, TimeSpan.FromMilliseconds(transferTimeMilliseconds));
        }
    }
}
