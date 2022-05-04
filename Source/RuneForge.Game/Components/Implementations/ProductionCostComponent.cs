using System;

namespace RuneForge.Game.Components.Implementations
{
    public class ProductionCostComponent : Component
    {
        public decimal GoldAmount { get; }

        public TimeSpan ProductionTime { get; }

        public ProductionCostComponent(decimal goldAmount, TimeSpan productionTime)
        {
            GoldAmount = goldAmount;
            ProductionTime = productionTime;
        }
    }
}
