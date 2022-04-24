using System;

using RuneForge.Game.Components.Entities;

namespace RuneForge.Game.Components.Implementations
{
    public class ResourceSourceComponentPrototype : ComponentPrototype
    {
        public ResourceTypes ResourceType { get; }

        public decimal AmountGiven { get; }

        public TimeSpan ExtractionTime { get; }

        public ResourceSourceComponentPrototype(ResourceTypes resourceType, decimal amountGiven, TimeSpan extractionTime)
        {
            ResourceType = resourceType;
            AmountGiven = amountGiven;
            ExtractionTime = extractionTime;
        }
    }
}
