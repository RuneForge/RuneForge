using System;

namespace RuneForge.Data.Components
{
    [Serializable]
    public class ResourceContainerComponentDto : ComponentDto
    {
        public decimal GoldAmount { get; set; }
    }
}
