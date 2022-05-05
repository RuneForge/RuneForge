using System;

namespace RuneForge.Data.Orders
{
    [Serializable]
    public abstract class OrderDto
    {
        public int State { get; set; }

        protected OrderDto()
        {
        }
    }
}
