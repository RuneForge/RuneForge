namespace RuneForge.Data.Orders
{
    public class GatherResourcesOrderDto : OrderDto
    {
        public int ResourceSourceId { get; set; }

        public bool CancellationRequested { get; set; }
    }
}
