using System;

namespace RuneForge.Data.Components
{
    [Serializable]
    public class HealthComponentDto : ComponentDto
    {
        public decimal Health { get; set; }

        public decimal MaxHealth { get; set; }
    }
}
