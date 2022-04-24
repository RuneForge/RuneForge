using System;

using RuneForge.Game.Components.Entities;

namespace RuneForge.Game.Components.Implementations
{
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
