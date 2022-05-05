using System.Collections.Generic;

using RuneForge.Data.Components;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Components.Factories
{
    public class UnitShelterComponentFactory : ComponentFactory<UnitShelterComponent, UnitShelterComponentPrototype, UnitShelterComponentDto>
    {
        private readonly IUnitService m_unitService;

        public UnitShelterComponentFactory(IUnitService unitService)
        {
            m_unitService = unitService;
        }

        public override UnitShelterComponent CreateComponentFromPrototype(UnitShelterComponentPrototype componentPrototype, UnitShelterComponentPrototype componentPrototypeOverride)
        {
            return new UnitShelterComponent(componentPrototype.OccupantsLimit);
        }

        public override UnitShelterComponent CreateComponentFromDto(UnitShelterComponentDto componentDto)
        {
            List<Unit> occupants = new List<Unit>();
            foreach (int unitId in componentDto.OccupantIds)
                occupants.Add(m_unitService.GetUnit(unitId));
            return new UnitShelterComponent(componentDto.OccupantsLimit, occupants);
        }
    }
}
