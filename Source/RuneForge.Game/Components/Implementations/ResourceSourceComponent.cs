using System;

using RuneForge.Game.Components.Entities;

namespace RuneForge.Game.Components.Implementations
{
    public class ResourceSourceComponent : Component
    {
        public ResourceTypes ResourceType { get; }

        public decimal AmountGiven { get; }

        public TimeSpan ExtractionTime { get; }

        public ResourceSourceComponent(ResourceTypes resourceType, decimal amountGiven, TimeSpan extractionTime)
        {
            ResourceType = resourceType;
            AmountGiven = amountGiven;
            ExtractionTime = extractionTime;
        }
    }
}
