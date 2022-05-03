using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class ProductionCostComponentFactory : ComponentFactory<ProductionCostComponent, ProductionCostComponentPrototype>
    {
        public override ProductionCostComponent CreateComponentFromPrototype(ProductionCostComponentPrototype componentPrototype, ProductionCostComponentPrototype componentPrototypeOverride)
        {
            return new ProductionCostComponent(componentPrototype.GoldAmount, componentPrototype.ProductionTime);
        }
    }
}
