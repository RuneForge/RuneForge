using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class ResourceContainerComponentFactory : ComponentFactory<ResourceContainerComponent, ResourceContainerComponentPrototype>
    {
        public override ResourceContainerComponent CreateComponentFromPrototype(ResourceContainerComponentPrototype componentPrototype, ResourceContainerComponentPrototype componentPrototypeOverride)
        {
            return new ResourceContainerComponent()
            {
                GoldAmount = componentPrototype.GoldAmount,
            };
        }
    }
}
