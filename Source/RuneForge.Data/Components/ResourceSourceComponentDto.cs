using System;

namespace RuneForge.Data.Components
{
    public class ResourceSourceComponentDto : ComponentDto
    {
        public int ResourceType { get; set; }

        public decimal AmountGiven { get; set; }

        public TimeSpan ExtractionTime { get; set; }
    }
}
