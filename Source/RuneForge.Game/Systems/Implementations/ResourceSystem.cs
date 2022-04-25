using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Components.Entities;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.PathGenerators.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Systems.Implementations
{
    public class ResourceSystem : System
    {
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IUnitService m_unitService;
        private readonly IBuildingService m_buildingService;
        private readonly IPlayerService m_playerService;
        private readonly IPathGenerator m_pathGenerator;

        public ResourceSystem(IGameSessionContext gameSessionContext, IUnitService unitService, IBuildingService buildingService, IPlayerService playerService, IPathGenerator pathGenerator)
        {
            m_gameSessionContext = gameSessionContext;
            m_unitService = unitService;
            m_buildingService = buildingService;
            m_playerService = playerService;
            m_pathGenerator = pathGenerator;
        }

        public override void Update(GameTime gameTime)
        {
            // Process resource extraction.
            foreach (Building building in m_gameSessionContext.Buildings)
            {
                if (!building.TryGetComponentOfType(out LocationComponent buildingLocationComponent)
                    || !building.TryGetComponentOfType(out ResourceSourceComponent buildingResourceSourceComponent)
                    || !building.TryGetComponentOfType(out ResourceContainerComponent buildingResourceContainerComponent)
                    || !building.TryGetComponentOfType(out UnitShelterComponent buildingShelterComponent)
                    || buildingShelterComponent.Occupants.Count == 0)
                    continue;

                List<Unit> occupantsToRemove = new List<Unit>();
                foreach (Unit unit in buildingShelterComponent.Occupants)
                {
                    LocationComponent unitLocationComponent = unit.GetComponentOfType<LocationComponent>();
                    MovementComponent unitMovementComponent = unit.GetComponentOfType<MovementComponent>();
                    ResourceContainerComponent unitResourceContainerComponent = unit.GetComponentOfType<ResourceContainerComponent>();
                    UnitShelterOccupantComponent unitShelterOccupantComponent = unit.GetComponentOfType<UnitShelterOccupantComponent>();

                    if (unitShelterOccupantComponent.TimeSinceEntering >= buildingResourceSourceComponent.ExtractionTime)
                    {
                        if (m_pathGenerator.TryGetClosestFreeCell(unitShelterOccupantComponent.EnteredFrom, buildingLocationComponent.GetSurroundingCells(), unitMovementComponent.MovementType, out Point exitPoint))
                        {
                            unitLocationComponent.LocationCells = exitPoint;
                            unitShelterOccupantComponent.InsideShelter = false;
                            unitShelterOccupantComponent.TimeSinceEntering = TimeSpan.Zero;

                            ResourceTypes resourceType = buildingResourceSourceComponent.ResourceType;
                            decimal resourceAmount = buildingResourceContainerComponent.GetResourceAmount(resourceType);
                            if (resourceAmount > 0)
                            {
                                resourceAmount = Math.Min(resourceAmount, buildingResourceSourceComponent.AmountGiven - unitResourceContainerComponent.GetResourceAmount(resourceType));
                                buildingResourceContainerComponent.WithdrawResource(resourceType, resourceAmount);
                                unitResourceContainerComponent.AddResource(resourceType, resourceAmount);
                            }

                            occupantsToRemove.Add(unit);
                        }
                    }
                    unitShelterOccupantComponent.TimeSinceEntering += gameTime.ElapsedGameTime;
                    m_unitService.RegisterUnitChanges(unit.Id);
                }
                if (occupantsToRemove.Any())
                {
                    foreach (Unit occupantToRemove in occupantsToRemove)
                        buildingShelterComponent.RemoveOccupant(occupantToRemove);
                    m_buildingService.RegisterBuildingChanges(building.Id);
                }
            }

            // Process resource returning.
            foreach (Building building in m_gameSessionContext.Buildings)
            {
                if (!building.TryGetComponentOfType(out LocationComponent buildingLocationComponent)
                    || !building.TryGetComponentOfType(out ResourceStorageComponent buildingResourceStorageComponent)
                    || !building.TryGetComponentOfType(out UnitShelterComponent buildingShelterComponent)
                    || buildingShelterComponent.Occupants.Count == 0)
                    continue;

                List<Unit> occupantsToRemove = new List<Unit>();
                foreach (Unit unit in buildingShelterComponent.Occupants)
                {
                    LocationComponent unitLocationComponent = unit.GetComponentOfType<LocationComponent>();
                    MovementComponent unitMovementComponent = unit.GetComponentOfType<MovementComponent>();
                    ResourceContainerComponent unitResourceContainerComponent = unit.GetComponentOfType<ResourceContainerComponent>();
                    UnitShelterOccupantComponent unitShelterOccupantComponent = unit.GetComponentOfType<UnitShelterOccupantComponent>();

                    if (unitShelterOccupantComponent.TimeSinceEntering >= buildingResourceStorageComponent.TransferTime)
                    {
                        if (m_pathGenerator.TryGetClosestFreeCell(unitShelterOccupantComponent.EnteredFrom, buildingLocationComponent.GetSurroundingCells(), unitMovementComponent.MovementType, out Point exitPoint))
                        {
                            unitLocationComponent.LocationCells = exitPoint;
                            unitShelterOccupantComponent.InsideShelter = false;
                            unitShelterOccupantComponent.TimeSinceEntering = TimeSpan.Zero;

                            Player buildingOwner = building.Owner;
                            ResourceContainerComponent playerResourceContainerComponent = buildingOwner.GetComponentOfType<ResourceContainerComponent>();
                            foreach (ResourceTypes resourceType in unitResourceContainerComponent.GetResourceTypes())
                            {
                                decimal resourceAmount = unitResourceContainerComponent.GetResourceAmount(resourceType);
                                unitResourceContainerComponent.WithdrawResource(resourceType, resourceAmount);
                                playerResourceContainerComponent.AddResource(resourceType, resourceAmount);
                            }

                            occupantsToRemove.Add(unit);
                            m_playerService.RegisterPlayerChanges(buildingOwner.Id);
                        }
                    }
                    unitShelterOccupantComponent.TimeSinceEntering += gameTime.ElapsedGameTime;
                    m_unitService.RegisterUnitChanges(unit.Id);
                }
                if (occupantsToRemove.Any())
                {
                    foreach (Unit occupantToRemove in occupantsToRemove)
                        buildingShelterComponent.RemoveOccupant(occupantToRemove);
                    m_buildingService.RegisterBuildingChanges(building.Id);
                }
            }

            base.Update(gameTime);
        }
    }
}
