using RuneForge.Data.Components;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class UnitShelterOccupantComponentFactory : ComponentFactory<UnitShelterOccupantComponent, UnitShelterOccupantComponentPrototype, UnitShelterOccupantComponentDto>
    {
        public override UnitShelterOccupantComponent CreateComponentFromPrototype(UnitShelterOccupantComponentPrototype componentPrototype, UnitShelterOccupantComponentPrototype componentPrototypeOverride)
        {
            return new UnitShelterOccupantComponent();
        }

        public override UnitShelterOccupantComponent CreateComponentFromDto(UnitShelterOccupantComponentDto componentDto)
        {
            return new UnitShelterOccupantComponent()
            {
                InsideShelter = componentDto.InsideShelter,
                TimeSinceEntering = componentDto.TimeSinceEntering,
                EnteredFromX = componentDto.EnteredFromX,
                EnteredFromY = componentDto.EnteredFromY,
            };
        }
    }
}
