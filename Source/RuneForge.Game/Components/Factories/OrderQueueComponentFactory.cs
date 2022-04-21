using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class OrderQueueComponentFactory : ComponentFactory<OrderQueueComponent, OrderQueueComponentPrototype>
    {
        public override OrderQueueComponent CreateComponentFromPrototype(OrderQueueComponentPrototype componentPrototype, OrderQueueComponentPrototype componentPrototypeOverride)
        {
            return new OrderQueueComponent();
        }
    }
}
