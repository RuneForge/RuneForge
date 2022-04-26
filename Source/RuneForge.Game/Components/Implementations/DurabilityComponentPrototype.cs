using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
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
