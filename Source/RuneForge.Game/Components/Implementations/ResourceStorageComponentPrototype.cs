using System;

using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Entities;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentPrototypeReader(typeof(ResourceStorageComponentPrototypeReader))]
    public class ResourceStorageComponentPrototype : ComponentPrototype
    {
        public ResourceTypes AcceptedResourceTypes { get; }

        public TimeSpan TransferTime { get; }

        public ResourceStorageComponentPrototype(ResourceTypes acceptedResourceTypes, TimeSpan transferTime)
        {
            AcceptedResourceTypes = acceptedResourceTypes;
            TransferTime = transferTime;
        }
    }
}
