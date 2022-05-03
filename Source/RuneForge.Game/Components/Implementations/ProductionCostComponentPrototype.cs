using System;

namespace RuneForge.Game.Components.Implementations
{
    public class ProductionCostComponentPrototype : ComponentPrototype
    {
        public decimal GoldAmount { get; }

        public TimeSpan ProductionTime { get; }

        public ProductionCostComponentPrototype(decimal goldAmount, TimeSpan productionTime)
        {
            GoldAmount = goldAmount;
            ProductionTime = productionTime;
        }
    }
}
