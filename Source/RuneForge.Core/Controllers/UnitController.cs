﻿using System;

using Microsoft.Extensions.DependencyInjection;

using RuneForge.Game.Components.Implementations;
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
            orderQueueComponent.EnqueueOrder(new MoveOrder(unit, destinationCellX, destinationCellY, false, pathGenerator));
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
