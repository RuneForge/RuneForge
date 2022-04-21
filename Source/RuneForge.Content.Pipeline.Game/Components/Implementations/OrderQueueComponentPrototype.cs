using RuneForge.Content.Pipeline.Game.Components.Attributes;
using RuneForge.Content.Pipeline.Game.Components.PrototypeWriters;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    [ComponentPrototypeWriter(typeof(OrderQueueComponentPrototypeWriter))]
    public class OrderQueueComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.OrderQueueComponentPrototype, RuneForge.Game";

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
