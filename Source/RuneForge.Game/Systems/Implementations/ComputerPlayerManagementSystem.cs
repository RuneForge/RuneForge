using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Components.Entities;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Orders;
using RuneForge.Game.Orders.Implementations;
using RuneForge.Game.PathGenerators.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Systems.Implementations
{
    public class ComputerPlayerManagementSystem : System
    {
        private const int c_miningPeasantsCount = 10;
        private const int c_goldRequiredForFootmanProduction = 600;
        private const int c_defenceRange = 16;
        private const int c_footmansCountToTriggerAttack = 10;
        private const int c_footmansSelectedForAttack = 5;
        private const int c_footmanAttackRange = 4;

        private static readonly TimeSpan s_checkPeriod = TimeSpan.FromSeconds(0.5);

        private readonly IServiceProvider m_serviceProvider;
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IUnitService m_unitService;
        private readonly IBuildingService m_buildingService;
        private readonly List<Player> m_computerManagedPlayers;
        private readonly List<Unit> m_unitsToAttack;
        private TimeSpan m_timeSincePreviousCheck;

        public ComputerPlayerManagementSystem(IServiceProvider serviceProvider, IGameSessionContext gameSessionContext, IUnitService unitService, IBuildingService buildingService)
        {
            m_serviceProvider = serviceProvider;
            m_gameSessionContext = gameSessionContext;
            m_computerManagedPlayers = new List<Player>();
            m_unitsToAttack = new List<Unit>();
            m_timeSincePreviousCheck = TimeSpan.Zero;
            m_unitService = unitService;
            m_buildingService = buildingService;
        }

        public override void Update(GameTime gameTime)
        {
            m_timeSincePreviousCheck += gameTime.ElapsedGameTime;
            if (m_timeSincePreviousCheck >= s_checkPeriod)
            {
                if (m_computerManagedPlayers.Count == 0)
                {
                    foreach (Player player in m_gameSessionContext.Players.Where(player => player.Id != m_gameSessionContext.Map.HumanPlayerId && player.Id != m_gameSessionContext.Map.NeutralPassivePlayerId))
                        m_computerManagedPlayers.Add(player);
                }
                foreach (Player player in m_computerManagedPlayers)
                {
                    ResourceContainerComponent resourceContainerComponent = player.GetComponentOfType<ResourceContainerComponent>();

                    Building townHall = m_gameSessionContext.Buildings.Where(building => building.Owner == player && building.Name == "Town Hall").FirstOrDefault();

                    // Train additional peasants if there are less than 5 mining gold.
                    int peasantsCount = m_gameSessionContext.Units.Where(unit => unit.Owner == player && unit.Name == "Peasant").Count();
                    if (peasantsCount < c_miningPeasantsCount)
                    {
                        if (townHall != null)
                        {
                            OrderQueueComponent townHallOrderQueueComponent = townHall.GetComponentOfType<OrderQueueComponent>();
                            if (townHallOrderQueueComponent.CurrentOrder?.State != OrderState.Scheduled && townHallOrderQueueComponent.CurrentOrder?.State != OrderState.InProgress)
                            {
                                townHallOrderQueueComponent.EnqueueOrder(CreateProducePeasantOrder(townHall));
                                m_buildingService.RegisterBuildingChanges(townHall.Id);
                            }
                        }
                    }

                    // Order free peasants to mine gold.
                    foreach (Unit peasant in m_gameSessionContext.Units.Where(unit => unit.Owner == player && unit.Name == "Peasant"))
                    {
                        LocationComponent peasantLocationComponent = peasant.GetComponentOfType<LocationComponent>();
                        OrderQueueComponent peasantOrderQueueComponent = peasant.GetComponentOfType<OrderQueueComponent>();
                        if (peasantOrderQueueComponent.CurrentOrder?.State != OrderState.Scheduled && peasantOrderQueueComponent.CurrentOrder?.State != OrderState.InProgress)
                        {
                            int minDistance = int.MaxValue;
                            Building closestGoldMine = null;
                            foreach (Building goldMine in m_gameSessionContext.Buildings.Where(building => building.Name == "Gold Mine"))
                            {
                                LocationComponent goldMineLocationComponent = goldMine.GetComponentOfType<LocationComponent>();

                                int distance = CalculateDistance(goldMineLocationComponent.LocationCells, peasantLocationComponent.LocationCells);
                                if (distance < minDistance)
                                {
                                    closestGoldMine = goldMine;
                                    minDistance = distance;
                                }
                            }

                            if (closestGoldMine != null)
                            {
                                peasantOrderQueueComponent.EnqueueOrder(CreateGatherResourcesOrder(peasant, closestGoldMine));
                                m_unitService.RegisterUnitChanges(peasant.Id);
                            }
                        }
                    }

                    // If there is additional gold, train footmans.
                    if (resourceContainerComponent.GetResourceAmount(ResourceTypes.Gold) >= c_goldRequiredForFootmanProduction)
                    {
                        Building barracks = m_gameSessionContext.Buildings.Where(building => building.Owner == player && building.Name == "Barracks").FirstOrDefault();
                        if (barracks != null)
                        {
                            OrderQueueComponent barracksOrderQueueComponent = barracks.GetComponentOfType<OrderQueueComponent>();
                            if (barracksOrderQueueComponent.CurrentOrder?.State != OrderState.Scheduled && barracksOrderQueueComponent.CurrentOrder?.State != OrderState.InProgress)
                            {
                                barracksOrderQueueComponent.EnqueueOrder(CreateProduceFootmanOrder(barracks));
                                m_buildingService.RegisterBuildingChanges(barracks.Id);
                            }
                        }
                    }

                    // If there is an enemy close to the town hall, defend it. Also schedule an order to return to the barracks.
                    m_unitsToAttack.Clear();
                    Building mainBuilding = townHall;
                    if (mainBuilding == null)
                        mainBuilding = m_gameSessionContext.Buildings.Where(building => building.Owner == player).FirstOrDefault();

                    if (mainBuilding != null)
                    {
                        LocationComponent mainBuildingLocationComponent = mainBuilding.GetComponentOfType<LocationComponent>();
                        foreach (Unit unit in m_gameSessionContext.Units.Where(unit => unit.Owner != player
                        && unit.TryGetComponentOfType(out LocationComponent locationComponent)
                        && CalculateDistance(locationComponent.LocationCells, mainBuildingLocationComponent.LocationCells) < c_defenceRange))
                            m_unitsToAttack.Add(unit);
                    }
                    if (m_unitsToAttack.Count > 0)
                    {
                        Building barracks = m_gameSessionContext.Buildings.Where(building => building.Owner == player && building.Name == "Barracks").FirstOrDefault();
                        foreach (Unit footman in m_gameSessionContext.Units.Where(unit => unit.Owner == player && unit.Name == "Footman"))
                        {
                            OrderQueueComponent footmanOrderQueueComponent = footman.GetComponentOfType<OrderQueueComponent>();
                            if (footmanOrderQueueComponent.CurrentOrder?.State != OrderState.Scheduled && footmanOrderQueueComponent.CurrentOrder?.State != OrderState.InProgress)
                            {
                                foreach (Unit target in m_unitsToAttack)
                                    footmanOrderQueueComponent.EnqueueOrder(CreateAttackOrder(footman, target));
                                if (barracks != null)
                                    footmanOrderQueueComponent.EnqueueOrder(CreateMoveOrder(footman, barracks));
                                m_unitService.RegisterUnitChanges(footman.Id);
                            }
                        }
                    }

                    // If there are more than 10 footmans, send five to attack the enemy.
                    if (m_gameSessionContext.Units.Where(unit => unit.Owner == player && unit.Name == "Footman").Count() >= c_footmansCountToTriggerAttack)
                    {
                        if (mainBuilding != null)
                        {
                            LocationComponent mainBuildingLocationComponent = mainBuilding.GetComponentOfType<LocationComponent>();

                            int minDistance = int.MaxValue;
                            Entity closestEnemyEntity = null;
                            foreach (Entity entity in m_gameSessionContext.Units.Where(unit => unit.Owner != player && unit.Owner.Id != m_gameSessionContext.Map.NeutralPassivePlayerId)
                                .Concat<Entity>(m_gameSessionContext.Buildings.Where(building => building.Owner != player && building.Owner.Id != m_gameSessionContext.Map.NeutralPassivePlayerId)))
                            {
                                LocationComponent entityLocationComponent = entity.GetComponentOfType<LocationComponent>();

                                int distance = CalculateDistance(entityLocationComponent.LocationCells, mainBuildingLocationComponent.LocationCells);
                                if (distance < minDistance)
                                {
                                    closestEnemyEntity = entity;
                                    minDistance = distance;
                                }
                            }

                            if (closestEnemyEntity != null)
                            {
                                foreach (Unit footman in m_gameSessionContext.Units.Where(unit => unit.Owner == player && unit.Name == "Footman").Take(c_footmansSelectedForAttack))
                                {
                                    OrderQueueComponent footmanOrderQueueComponent = footman.GetComponentOfType<OrderQueueComponent>();
                                    if (footmanOrderQueueComponent.CurrentOrder?.State != OrderState.Scheduled && footmanOrderQueueComponent.CurrentOrder?.State != OrderState.InProgress)
                                    {
                                        footmanOrderQueueComponent.EnqueueOrder(CreateAttackOrder(footman, closestEnemyEntity));
                                        m_unitService.RegisterUnitChanges(footman.Id);
                                    }
                                }
                            }
                        }
                    }

                    // If there is an enemy close to a footman, attack it.
                    foreach (Unit footman in m_gameSessionContext.Units.Where(unit => unit.Owner == player && unit.Name == "Footman"))
                    {
                        LocationComponent footmanLocationComponent = footman.GetComponentOfType<LocationComponent>();
                        foreach (Entity entity in m_gameSessionContext.Units.Where(unit => unit.Owner != player && unit.Owner.Id != m_gameSessionContext.Map.NeutralPassivePlayerId))
                        {
                            LocationComponent entityLocationComponent = entity.GetComponentOfType<LocationComponent>();
                            if (CalculateDistance(footmanLocationComponent.LocationCells, entityLocationComponent.LocationCells) <= c_footmanAttackRange)
                            {
                                OrderQueueComponent footmanOrderQueueComponent = footman.GetComponentOfType<OrderQueueComponent>();
                                if (footmanOrderQueueComponent.CurrentOrder?.State != OrderState.Scheduled && footmanOrderQueueComponent.CurrentOrder?.State != OrderState.InProgress)
                                {
                                    footmanOrderQueueComponent.EnqueueOrder(CreateAttackOrder(footman, entity));
                                    m_unitService.RegisterUnitChanges(footman.Id);
                                }
                            }
                        }
                    }
                }

                m_timeSincePreviousCheck = TimeSpan.Zero;
            }

            base.Update(gameTime);
        }

        private ProduceUnitOrder CreateProducePeasantOrder(Building building)
        {
            IPlayerService playerService = m_serviceProvider.GetRequiredService<IPlayerService>();
            UnitPrototype unitPrototype = m_gameSessionContext.Map.UnitPrototypes.Where(prototype => prototype.Code == "Peasant").Single();
            return new ProduceUnitOrder(building, unitPrototype, playerService);
        }

        private ProduceUnitOrder CreateProduceFootmanOrder(Building building)
        {
            IPlayerService playerService = m_serviceProvider.GetRequiredService<IPlayerService>();
            UnitPrototype unitPrototype = m_gameSessionContext.Map.UnitPrototypes.Where(prototype => prototype.Code == "Footman").Single();
            return new ProduceUnitOrder(building, unitPrototype, playerService);
        }

        private MoveOrder CreateMoveOrder(Unit unit, Building building)
        {
            IPathGenerator pathGenerator = m_serviceProvider.GetRequiredService<IPathGenerator>();
            LocationComponent buildingLocationComponent = building.GetComponentOfType<LocationComponent>();
            return new MoveOrder(unit, buildingLocationComponent.XCells, buildingLocationComponent.YCells, pathGenerator);
        }

        private AttackOrder CreateAttackOrder(Unit unit, Entity entity)
        {
            IPathGenerator pathGenerator = m_serviceProvider.GetRequiredService<IPathGenerator>();
            return new AttackOrder(unit, entity, pathGenerator);
        }

        private GatherResourcesOrder CreateGatherResourcesOrder(Unit unit, Building goldMine)
        {
            IPathGenerator pathGenerator = m_serviceProvider.GetRequiredService<IPathGenerator>();
            return new GatherResourcesOrder(unit, goldMine, m_gameSessionContext, m_buildingService, pathGenerator);
        }

        private int CalculateDistance(Point first, Point second)
        {
            int absX = Math.Abs(second.X - first.X);
            int absY = Math.Abs(second.Y - first.Y);
            return Math.Max(absX, absY);
        }
    }
}
