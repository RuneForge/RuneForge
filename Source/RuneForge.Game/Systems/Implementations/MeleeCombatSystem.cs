using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Systems.Implementations
{
    public class MeleeCombatSystem : System
    {
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IUnitService m_unitService;
        private readonly IBuildingService m_buildingService;
        private readonly List<Entity> m_changedEntities;

        public MeleeCombatSystem(IGameSessionContext gameSessionContext, IUnitService unitService, IBuildingService buildingService)
        {
            m_gameSessionContext = gameSessionContext;
            m_unitService = unitService;
            m_buildingService = buildingService;
            m_changedEntities = new List<Entity>();
        }

        public override void Update(GameTime gameTime)
        {
            m_changedEntities.Clear();
            foreach (Unit unit in m_gameSessionContext.Units)
            {
                if (!unit.TryGetComponentOfType(out MeleeCombatComponent meleeCombatComponent))
                    continue;

                if (!meleeCombatComponent.CycleInProgress)
                    continue;

                Entity targetEntity = meleeCombatComponent.TargetEntity;
                LocationComponent locationComponent = unit.GetComponentOfType<LocationComponent>();
                LocationComponent targetLocationComponent = targetEntity.GetComponentOfType<LocationComponent>();

                m_changedEntities.Add(unit);
                if (unit.TryGetComponentOfType(out DirectionComponent directionComponent) && TryGetDirection(locationComponent.LocationCells, targetLocationComponent.LocationCells, out Directions direction))
                    directionComponent.Direction = direction;

                if (meleeCombatComponent.TimeElapsed >= meleeCombatComponent.ActionTime && !meleeCombatComponent.ActionTaken)
                {
                    if (targetEntity.TryGetComponentOfType(out HealthComponent healthComponent))
                    {
                        decimal damageDealt = Math.Min(healthComponent.Health, meleeCombatComponent.AttackPower);
                        healthComponent.Health -= damageDealt;
                    }
                    else if (targetEntity.TryGetComponentOfType(out DurabilityComponent durabilityComponent))
                    {
                        decimal damageDealt = Math.Min(durabilityComponent.Durability, meleeCombatComponent.AttackPower);
                        durabilityComponent.Durability -= damageDealt;
                    }
                    m_changedEntities.Add(targetEntity);
                    meleeCombatComponent.ActionTaken = true;
                }

                if (meleeCombatComponent.TimeElapsed >= meleeCombatComponent.CycleTime && meleeCombatComponent.CycleInProgress)
                    meleeCombatComponent.Reset();

                meleeCombatComponent.TimeElapsed += gameTime.ElapsedGameTime;
            }

            foreach (Entity changedEntity in m_changedEntities)
            {
                switch (changedEntity)
                {
                    case Unit unit:
                        m_unitService.RegisterUnitChanges(unit.Id);
                        break;
                    case Building building:
                        m_buildingService.RegisterBuildingChanges(building.Id);
                        break;
                    default:
                        throw new NotSupportedException($"Unable to register changes for an entity of the {changedEntity.GetType().Name} type.");
                }
            }

            base.Update(gameTime);
        }

        private bool TryGetDirection(Point originCell, Point destinationCell, out Directions direction)
        {
            int deltaCellX = destinationCell.X - originCell.X;
            int deltaCellY = destinationCell.Y - originCell.Y;

            direction = Directions.None;

            bool result = false;
            if (deltaCellX == 0 && deltaCellY == -1)
                (result, direction) = (true, Directions.North);
            if (deltaCellX == 0 && deltaCellY == +1)
                (result, direction) = (true, Directions.South);
            if (deltaCellX == -1 && deltaCellY == 0)
                (result, direction) = (true, Directions.West);
            if (deltaCellX == +1 && deltaCellY == 0)
                (result, direction) = (true, Directions.East);
            if (deltaCellX == -1 && deltaCellY == -1)
                (result, direction) = (true, Directions.NorthWest);
            if (deltaCellX == +1 && deltaCellY == -1)
                (result, direction) = (true, Directions.NorthEast);
            if (deltaCellX == -1 && deltaCellY == +1)
                (result, direction) = (true, Directions.SouthWest);
            if (deltaCellX == +1 && deltaCellY == +1)
                (result, direction) = (true, Directions.SouthEast);
            return result;
        }
    }
}
