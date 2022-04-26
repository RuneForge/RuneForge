namespace RuneForge.Game.Components.Implementations
{
    public class DurabilityComponentPrototype : ComponentPrototype
    {
        public decimal Durability { get; }

        public decimal MaxDurability { get; }

        public DurabilityComponentPrototype(decimal durability, decimal maxDurability)
        {
            Durability = durability;
            MaxDurability = durability;
        }
    }
}
