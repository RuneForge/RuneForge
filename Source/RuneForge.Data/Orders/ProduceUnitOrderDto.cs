using System;

namespace RuneForge.Data.Orders
{
    [Serializable]
    public class ProduceUnitOrderDto : OrderDto
    {
        public string UnitPrototypeCode { get; set; }
    }
}
