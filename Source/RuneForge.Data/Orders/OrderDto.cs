using System;

namespace RuneForge.Data.Orders
{
    [Serializable]
    public abstract class OrderDto
    {
        public string EntityId { get; set; }

        public int State { get; set; }

        protected OrderDto()
        {
        }
    }
}
