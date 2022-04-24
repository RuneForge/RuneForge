using System;

using RuneForge.Game.Components.Entities;

namespace RuneForge.Game.Components.Implementations
{
    public class ResourceStorageComponent : Component
    {
        public ResourceTypes AcceptedResourceTypes { get; }

        public TimeSpan TransferTime { get; }

        public ResourceStorageComponent(ResourceTypes acceptedResourceTypes, TimeSpan transferTime)
        {
            AcceptedResourceTypes = acceptedResourceTypes;
            TransferTime = transferTime;
        }
    }
}
