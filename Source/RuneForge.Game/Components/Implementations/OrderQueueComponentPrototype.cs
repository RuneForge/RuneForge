using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Factories;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentFactory(typeof(OrderQueueComponentFactory))]
    [ComponentPrototypeReader(typeof(OrderQueueComponentPrototypeReader))]
    public class OrderQueueComponentPrototype : ComponentPrototype
    {
    }
}
