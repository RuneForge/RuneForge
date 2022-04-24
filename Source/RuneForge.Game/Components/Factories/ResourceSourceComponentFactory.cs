using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class ResourceSourceComponentFactory : ComponentFactory<ResourceSourceComponent, ResourceSourceComponentPrototype>
    {
        public override ResourceSourceComponent CreateComponentFromPrototype(ResourceSourceComponentPrototype componentPrototype, ResourceSourceComponentPrototype componentPrototypeOverride)
        {
            return new ResourceSourceComponent(componentPrototype.ResourceType, componentPrototype.AmountGiven, componentPrototype.ExtractionTime);
        }
    }
}
