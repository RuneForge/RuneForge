namespace RuneForge.Data.Orders
{
    public class AttackOrderDto : OrderDto
    {
        public string TargetEntityId { get; }

        public bool CompletingRequested { get; private set; }
        public bool CancellationRequested { get; private set; }
    }
}
