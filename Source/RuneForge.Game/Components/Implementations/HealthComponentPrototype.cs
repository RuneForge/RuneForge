namespace RuneForge.Game.Components.Implementations
{
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
