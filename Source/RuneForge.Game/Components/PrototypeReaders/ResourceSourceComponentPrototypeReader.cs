using System;

using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Entities;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class ResourceSourceComponentPrototypeReader : ComponentPrototypeReader<ResourceSourceComponentPrototype>
    {
        public override ResourceSourceComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            ResourceTypes resourceType = (ResourceTypes)contentReader.ReadInt32();
            decimal amountGiven = contentReader.ReadDecimal();
            float extractionTimeMilliseconds = contentReader.ReadSingle();
            return new ResourceSourceComponentPrototype(resourceType, amountGiven, TimeSpan.FromMilliseconds(extractionTimeMilliseconds));
        }
    }
}
