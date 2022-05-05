using RuneForge.Data.Components;
using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    [ComponentDto(typeof(HealthComponentDto))]
    public class HealthComponentFactory : ComponentFactory<HealthComponent, HealthComponentPrototype, HealthComponentDto>
    {
        public override HealthComponent CreateComponentFromPrototype(HealthComponentPrototype componentPrototype, HealthComponentPrototype componentPrototypeOverride)
        {
            return new HealthComponent()
            {
                Health = componentPrototype.Health,
                MaxHealth = componentPrototype.MaxHealth,
            };
        }

        public override HealthComponent CreateComponentFromDto(HealthComponentDto componentDto)
        {
            return new HealthComponent()
            {
                Health = componentDto.Health,
                MaxHealth = componentDto.MaxHealth,
            };
        }
    }
}
