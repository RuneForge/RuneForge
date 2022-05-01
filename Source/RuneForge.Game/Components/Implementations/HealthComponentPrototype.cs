using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Factories;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentFactory(typeof(HealthComponentFactory))]
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
