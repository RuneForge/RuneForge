using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentPrototypeReader(typeof(HealthComponentPrototypeReader))]
    public class HealthComponentPrototype : ComponentPrototype
    {
        public decimal Health { get; }

        public decimal MaxHealth { get; }

        public HealthComponentPrototype(decimal health, decimal maxHealth)
        {
            Health = health;
            MaxHealth = maxHealth;
        }
    }
}
