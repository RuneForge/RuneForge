using RuneForge.Data.Components;
using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    [ComponentDto(typeof(ProductionCostComponentDto))]
    public class ProductionCostComponentFactory : ComponentFactory<ProductionCostComponent, ProductionCostComponentPrototype, ProductionCostComponentDto>
    {
        public override ProductionCostComponent CreateComponentFromPrototype(ProductionCostComponentPrototype componentPrototype, ProductionCostComponentPrototype componentPrototypeOverride)
        {
            return new ProductionCostComponent(componentPrototype.GoldAmount, componentPrototype.ProductionTime);
        }

        public override ProductionCostComponent CreateComponentFromDto(ProductionCostComponentDto componentDto)
        {
            return new ProductionCostComponent(componentDto.GoldAmount, componentDto.ProductionTime);
        }
    }
}
