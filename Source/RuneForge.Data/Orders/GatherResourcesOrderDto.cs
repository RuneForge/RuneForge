using System;

namespace RuneForge.Data.Orders
{
    [Serializable]
    public class GatherResourcesOrderDto : OrderDto
    {
        public int ResourceSourceId { get; set; }

        public bool CancellationRequested { get; set; }
    }
}
