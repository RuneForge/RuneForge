using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Microsoft.Xna.Framework;

using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Components.Entities;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.PathGenerators.Interfaces;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Systems.Implementations
{
    public class UnitProductionSystem : System
    {
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IBuildingService m_buildingService;
        private readonly IUnitService m_unitService;
        private readonly IUnitFactory m_unitFactory;
        private readonly IPathGenerator m_pathGenerator;
        private readonly List<Building> m_changedBuildings = new List<Building>();

        public UnitProductionSystem(IGameSessionContext gameSessionContext, IBuildingService buildingService, IUnitService unitService, IUnitFactory unitFactory, IPathGenerator pathGenerator)
        {
            m_gameSessionContext = gameSessionContext;
            m_buildingService = buildingService;
            m_unitService = unitService;
            m_unitFactory = unitFactory;
            m_pathGenerator = pathGenerator;
        }

        public override void Update(GameTime gameTime)
        {
            m_changedBuildings.Clear();
            foreach (Building building in m_gameSessionContext.Buildings)
            {
                if (!building.TryGetComponentOfType(out ProductionFacilityComponent productionFacilityComponent))
                    continue;

                if (productionFacilityComponent.UnitCurrentlyProduced == null)
                    continue;

                UnitPrototype unitPrototype = productionFacilityComponent.UnitCurrentlyProduced;
                ProductionCostComponentPrototype productionCostComponentPrototype = unitPrototype.ComponentPrototypes
                    .OfType<ProductionCostComponentPrototype>()
                    .Single();

                m_changedBuildings.Add(building);
                if (productionFacilityComponent.TimeElapsed >= productionCostComponentPrototype.ProductionTime)
                {
                    if (!TryCreateUnit(building, unitPrototype))
                        continue;

                    productionFacilityComponent.UnitCurrentlyProduced = null;
                    productionFacilityComponent.TimeElapsed = TimeSpan.Zero;
                    productionFacilityComponent.ProductionFinished = true;
                }
                else
                    productionFacilityComponent.TimeElapsed += gameTime.ElapsedGameTime;
            }

            foreach (Building building in m_changedBuildings)
                m_buildingService.RegisterBuildingChanges(building.Id);

            base.Update(gameTime);
        }

        private bool TryCreateUnit(Building productionFacility, UnitPrototype unitPrototype)
        {
            if (!productionFacility.TryGetComponentOfType(out LocationComponent buildingLocationComponent))
                return false;

            ReadOnlyCollection<Point> surroundingCells = buildingLocationComponent.GetSurroundingCells();
            Point[] freeCells = surroundingCells.Where(cell => m_pathGenerator.IsCellFree(cell, MovementFlags.LandMovementRequired)).ToArray();

            if (freeCells.Length == 0)
                return false;
            else
            {
                Point randomFreeCell = freeCells[m_gameSessionContext.RandomNumbersGenerator.Next(freeCells.Length)];
                Unit unit = m_unitFactory.CreateFromPrototype(unitPrototype, productionFacility.Owner);

                if (!unit.TryGetComponentOfType(out LocationComponent unitLocationComponent))
                    return false;

                unitLocationComponent.LocationCells = randomFreeCell;
                m_unitService.AddUnit(unit);
                return true;
            }
        }
    }
}
