using RuneForge.Data.Components;
using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Entities;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    [ComponentDto(typeof(MovementComponentDto))]
    public class MovementComponentFactory : ComponentFactory<MovementComponent, MovementComponentPrototype, MovementComponentDto>
    {
        public override MovementComponent CreateComponentFromPrototype(MovementComponentPrototype componentPrototype, MovementComponentPrototype componentPrototypeOverride)
        {
            return new MovementComponent(componentPrototype.MovementSpeed, componentPrototype.MovementType);
        }

        public override MovementComponent CreateComponentFromDto(MovementComponentDto componentDto)
        {
            return new MovementComponent(componentDto.MovementSpeed, (MovementFlags)componentDto.MovementType)
            {
                OriginCellX = componentDto.OriginCellX,
                OriginCellY = componentDto.OriginCellY,
                DestinationCellX = componentDto.DestinationCellX,
                DestinationCellY = componentDto.DestinationCellY,
                MovementScheduled = componentDto.MovementScheduled,
                MovementInProgress = componentDto.MovementInProgress,
                PathBlocked = componentDto.PathBlocked,
            };
        }
    }
}
