using System.Collections.Generic;

using Microsoft.Xna.Framework;

using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Systems.Implementations
{
    public class EntityLifetimeSystem : System
    {
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IUnitService m_unitService;
        private readonly IBuildingService m_buildingService;
        private readonly List<Unit> m_unitsToRemove;
        private readonly List<Building> m_buildingsToRemove;

        public EntityLifetimeSystem(IGameSessionContext gameSessionContext, IUnitService unitService, IBuildingService buildingService)
        {
            m_gameSessionContext = gameSessionContext;
            m_unitService = unitService;
            m_buildingService = buildingService;
            m_unitsToRemove = new List<Unit>();
            m_buildingsToRemove = new List<Building>();
        }

        public override void Update(GameTime gameTime)
        {
            m_unitsToRemove.Clear();
            m_buildingsToRemove.Clear();
            foreach (Unit unit in m_gameSessionContext.Units)
            {
                if (CheckIfEntityShouldBeRemoved(unit))
                    m_unitsToRemove.Add(unit);
            }
            foreach (Building building in m_gameSessionContext.Buildings)
            {
                if (CheckIfEntityShouldBeRemoved(building))
                    m_buildingsToRemove.Add(building);
            }
            foreach (Unit unit in m_unitsToRemove)
                m_unitService.RemoveUnit(unit.Id);
            foreach (Building building in m_buildingsToRemove)
                m_buildingService.RemoveBuilding(building.Id);

            base.Update(gameTime);
        }

        public bool CheckIfEntityShouldBeRemoved(Entity entity)
        {
            bool shouldRemoveEntity = false;
            if (entity.TryGetComponentOfType(out HealthComponent healthComponent) && healthComponent.Health == 0)
                shouldRemoveEntity = true;
            if (entity.TryGetComponentOfType(out DurabilityComponent durabilityComponent) && durabilityComponent.Durability == 0)
                shouldRemoveEntity = true;
            return shouldRemoveEntity;
        }
    }
}
