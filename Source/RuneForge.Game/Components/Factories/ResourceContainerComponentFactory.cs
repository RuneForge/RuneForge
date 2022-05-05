using RuneForge.Data.Components;
using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    [ComponentDto(typeof(ResourceContainerComponentDto))]
    public class ResourceContainerComponentFactory : ComponentFactory<ResourceContainerComponent, ResourceContainerComponentPrototype, ResourceContainerComponentDto>
    {
        public override ResourceContainerComponent CreateComponentFromPrototype(ResourceContainerComponentPrototype componentPrototype, ResourceContainerComponentPrototype componentPrototypeOverride)
        {
            return new ResourceContainerComponent()
            {
                GoldAmount = componentPrototype.GoldAmount,
            };
        }

        public override ResourceContainerComponent CreateComponentFromDto(ResourceContainerComponentDto componentDto)
        {
            return new ResourceContainerComponent()
            {
                GoldAmount = componentDto.GoldAmount,
            };
        }
    }
}
