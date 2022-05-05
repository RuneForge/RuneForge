using System;

using Microsoft.Extensions.DependencyInjection;

using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Orders.Implementations;
using RuneForge.Game.PathGenerators.Interfaces;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Core.Controllers
{
    public class UnitController
    {
        private readonly IServiceProvider m_serviceProvider;
        private readonly IUnitService m_unitService;

        public UnitController(IServiceProvider serviceProvider, IUnitService unitService)
        {
            m_serviceProvider = serviceProvider;
            m_unitService = unitService;
        }

        public void Move(Unit unit, int destinationCellX, int destinationCellY, bool addToQueue)
        {
            if (!unit.TryGetComponentOfType(out OrderQueueComponent orderQueueComponent) || !unit.HasComponentOfType<MovementComponent>())
                return;

            if (!addToQueue)
                orderQueueComponent.ClearOrderQueue();

            IPathGenerator pathGenerator = m_serviceProvider.GetRequiredService<IPathGenerator>();
            orderQueueComponent.EnqueueOrder(new MoveOrder(unit, destinationCellX, destinationCellY, pathGenerator));
            m_unitService.RegisterUnitChanges(unit.Id);
        }

        public void GatherResources(Unit unit, Building resourceSourceBuilding, bool addToQueue)
        {
            if (!unit.TryGetComponentOfType(out OrderQueueComponent orderQueueComponent) || !unit.HasComponentOfType<MovementComponent>())
                return;

            if (!addToQueue)
                orderQueueComponent.ClearOrderQueue();

            IGameSessionContext gameSessionContext = m_serviceProvider.GetRequiredService<IGameSessionContext>();
            IBuildingService buildingService = m_serviceProvider.GetRequiredService<IBuildingService>();
            IPathGenerator pathGenerator = m_serviceProvider.GetRequiredService<IPathGenerator>();
            orderQueueComponent.EnqueueOrder(new GatherResourcesOrder(unit, resourceSourceBuilding, gameSessionContext, buildingService, pathGenerator));
            m_unitService.RegisterUnitChanges(unit.Id);
        }

        public void Attack(Unit unit, Entity targetEntity, bool addToQueue)
        {
            if (!unit.TryGetComponentOfType(out OrderQueueComponent orderQueueComponent) || !unit.HasComponentOfType<MovementComponent>())
                return;

            if (!addToQueue)
                orderQueueComponent.ClearOrderQueue();

            IPathGenerator pathGenerator = m_serviceProvider.GetRequiredService<IPathGenerator>();
            orderQueueComponent.EnqueueOrder(new AttackOrder(unit, targetEntity, pathGenerator));
            m_unitService.RegisterUnitChanges(unit.Id);
        }

        public void ClearOrderQueue(Unit unit)
        {
            if (!unit.TryGetComponentOfType(out OrderQueueComponent orderQueueComponent))
                return;

            orderQueueComponent.ClearOrderQueue();
            m_unitService.RegisterUnitChanges(unit.Id);
        }
    }
}
