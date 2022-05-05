using RuneForge.Data.Components;
using RuneForge.Game.Components.Entities;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class ResourceStorageComponentFactory : ComponentFactory<ResourceStorageComponent, ResourceStorageComponentPrototype, ResourceStorageComponentDto>
    {
        public override ResourceStorageComponent CreateComponentFromPrototype(ResourceStorageComponentPrototype componentPrototype, ResourceStorageComponentPrototype componentPrototypeOverride)
        {
            return new ResourceStorageComponent(componentPrototype.AcceptedResourceTypes, componentPrototype.TransferTime);
        }

        public override ResourceStorageComponent CreateComponentFromDto(ResourceStorageComponentDto componentDto)
        {
            return new ResourceStorageComponent((ResourceTypes)componentDto.AcceptedResourceTypes, componentDto.TransferTime);
        }
    }
}
