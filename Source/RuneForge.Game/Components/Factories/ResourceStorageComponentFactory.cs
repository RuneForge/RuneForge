using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class ResourceStorageComponentFactory : ComponentFactory<ResourceStorageComponent, ResourceStorageComponentPrototype>
    {
        public override ResourceStorageComponent CreateComponentFromPrototype(ResourceStorageComponentPrototype componentPrototype, ResourceStorageComponentPrototype componentPrototypeOverride)
        {
            return new ResourceStorageComponent(componentPrototype.AcceptedResourceTypes, componentPrototype.TransferTime);
        }
    }
}
