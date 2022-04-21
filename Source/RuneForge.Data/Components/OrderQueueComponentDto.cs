using System.Collections.Generic;

using RuneForge.Data.Orders;

namespace RuneForge.Data.Components
{
    public class OrderQueueComponentDto : ComponentDto
    {
        public OrderDto CurrentOrder { get; set; }

        public List<OrderDto> PendingOrders { get; set; }
    }
}
