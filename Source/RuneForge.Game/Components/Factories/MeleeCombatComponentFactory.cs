using System;

using RuneForge.Data.Components;
using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Components.Factories
{
    [ComponentDto(typeof(MeleeCombatComponentDto))]
    public class MeleeCombatComponentFactory : ComponentFactory<MeleeCombatComponent, MeleeCombatComponentPrototype, MeleeCombatComponentDto>
    {
        private readonly IUnitService m_unitService;
        private readonly IBuildingService m_buildingService;

        public MeleeCombatComponentFactory(IUnitService unitService, IBuildingService buildingService)
        {
            m_unitService = unitService;
            m_buildingService = buildingService;
        }

        public override MeleeCombatComponent CreateComponentFromPrototype(MeleeCombatComponentPrototype componentPrototype, MeleeCombatComponentPrototype componentPrototypeOverride)
        {
            return new MeleeCombatComponent(componentPrototype.AttackPower, componentPrototype.CycleTime, componentPrototype.ActionTime);
        }

        public override MeleeCombatComponent CreateComponentFromDto(MeleeCombatComponentDto componentDto)
        {
            Entity targetEntity = null;
            if (!string.IsNullOrEmpty(componentDto.TargetEntityId))
            {
                string entityTypeName = componentDto.TargetEntityId.Split(":")[0];
                int id = int.Parse(componentDto.TargetEntityId.Split(":")[1]);

                switch (entityTypeName)
                {
                    case nameof(Unit):
                        targetEntity = m_unitService.GetUnit(id);
                        break;
                    case nameof(Building):
                        targetEntity = m_buildingService.GetBuilding(id);
                        break;
                    default:
                        throw new InvalidOperationException("Unknown entity type.");
                }
            }

            return new MeleeCombatComponent(componentDto.AttackPower, componentDto.CycleTime, componentDto.ActionTime)
            {
                TargetEntity = targetEntity,
                TimeElapsed = componentDto.TimeElapsed,
                CycleInProgress = componentDto.CycleInProgress,
                ActionTaken = componentDto.ActionTaken,
            };
        }
    }
}
