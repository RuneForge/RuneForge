using System;

namespace RuneForge.Data.Components
{
    [Serializable]
    public class DurabilityComponentDto : ComponentDto
    {
        public decimal Durability { get; set; }

        public decimal MaxDurability { get; set; }
    }
}
