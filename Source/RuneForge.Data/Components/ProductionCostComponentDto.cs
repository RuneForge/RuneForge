using System;

namespace RuneForge.Data.Components
{
    [Serializable]
    public class ProductionCostComponentDto : ComponentDto
    {
        public decimal GoldAmount { get; set; }

        public TimeSpan ProductionTime { get; set; }
    }
}
