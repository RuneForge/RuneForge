using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Factories;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentFactory(typeof(DurabilityComponentFactory))]
    [ComponentPrototypeReader(typeof(DurabilityComponentPrototypeReader))]
    public class DurabilityComponentPrototype : ComponentPrototype
    {
        public decimal Durability { get; }

        public decimal MaxDurability { get; }

        public DurabilityComponentPrototype(decimal durability, decimal maxDurability)
        {
            Durability = durability;
            MaxDurability = maxDurability;
        }
    }
}
