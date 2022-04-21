namespace RuneForge.Data.Orders
{
    public abstract class OrderDto
    {
        public int State { get; set; }

        protected OrderDto()
        {
        }
    }
}
