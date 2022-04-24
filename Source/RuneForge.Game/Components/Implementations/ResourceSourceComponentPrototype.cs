using System;

using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Entities;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentPrototypeReader(typeof(ResourceSourceComponentPrototypeReader))]
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
