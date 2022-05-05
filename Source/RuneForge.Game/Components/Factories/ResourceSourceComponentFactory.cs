using RuneForge.Data.Components;
using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Entities;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    [ComponentDto(typeof(ResourceSourceComponentDto))]
    public class ResourceSourceComponentFactory : ComponentFactory<ResourceSourceComponent, ResourceSourceComponentPrototype, ResourceSourceComponentDto>
    {
        public override ResourceSourceComponent CreateComponentFromPrototype(ResourceSourceComponentPrototype componentPrototype, ResourceSourceComponentPrototype componentPrototypeOverride)
        {
            return new ResourceSourceComponent(componentPrototype.ResourceType, componentPrototype.AmountGiven, componentPrototype.ExtractionTime);
        }

        public override ResourceSourceComponent CreateComponentFromDto(ResourceSourceComponentDto componentDto)
        {
            return new ResourceSourceComponent((ResourceTypes)componentDto.ResourceType, componentDto.AmountGiven, componentDto.ExtractionTime);
        }
    }
}
