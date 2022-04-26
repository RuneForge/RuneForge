using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class HealthComponentFactory : ComponentFactory<HealthComponent, HealthComponentPrototype>
    {
        public override HealthComponent CreateComponentFromPrototype(HealthComponentPrototype componentPrototype, HealthComponentPrototype componentPrototypeOverride)
        {
            return new HealthComponent()
            {
                Health = componentPrototype.Health,
                MaxHealth = componentPrototype.MaxHealth,
            };
        }
    }
}
