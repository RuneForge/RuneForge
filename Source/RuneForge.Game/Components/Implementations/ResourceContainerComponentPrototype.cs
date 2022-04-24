using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Factories;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentFactory(typeof(ResourceContainerComponentFactory))]
    [ComponentPrototypeReader(typeof(ResourceContainerComponentPrototypeReader))]
    public class ResourceContainerComponentPrototype : ComponentPrototype
    {
        public decimal GoldAmount { get; }

        public ResourceContainerComponentPrototype(decimal goldAmount)
        {
            GoldAmount = goldAmount;
        }
    }
}
