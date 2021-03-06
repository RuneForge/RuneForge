using System;

using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Factories;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentFactory(typeof(ProductionCostComponentFactory))]
    [ComponentPrototypeReader(typeof(ProductionCostComponentPrototypeReader))]
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
