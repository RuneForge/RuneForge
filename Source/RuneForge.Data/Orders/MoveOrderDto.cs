namespace RuneForge.Data.Orders
{
    public class MoveOrderDto : OrderDto
    {
        public int DestinationX { get; set; }
        public int DestinationY { get; set; }

        public bool CancellationRequested { get; set; }
    }
}
