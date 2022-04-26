using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class DurabilityComponentFactory : ComponentFactory<DurabilityComponent, DurabilityComponentPrototype>
    {
        public override DurabilityComponent CreateComponentFromPrototype(DurabilityComponentPrototype componentPrototype, DurabilityComponentPrototype componentPrototypeOverride)
        {
            return new DurabilityComponent()
            {
                Durability = componentPrototype.Durability,
                MaxDurability = componentPrototype.MaxDurability,
            };
        }
    }
}
