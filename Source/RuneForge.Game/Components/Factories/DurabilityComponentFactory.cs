using RuneForge.Data.Components;
using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    [ComponentDto(typeof(DurabilityComponentDto))]
    public class DurabilityComponentFactory : ComponentFactory<DurabilityComponent, DurabilityComponentPrototype, DurabilityComponentDto>
    {
        public override DurabilityComponent CreateComponentFromPrototype(DurabilityComponentPrototype componentPrototype, DurabilityComponentPrototype componentPrototypeOverride)
        {
            return new DurabilityComponent()
            {
                Durability = componentPrototype.Durability,
                MaxDurability = componentPrototype.MaxDurability,
            };
        }

        public override DurabilityComponent CreateComponentFromDto(DurabilityComponentDto componentDto)
        {
            return new DurabilityComponent()
            {
                Durability = componentDto.Durability,
                MaxDurability = componentDto.MaxDurability,
            };
        }
    }
}
