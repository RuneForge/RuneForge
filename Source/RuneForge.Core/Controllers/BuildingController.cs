using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Orders.Implementations;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Units;

namespace RuneForge.Core.Controllers
{
    public class BuildingController
    {
        private readonly IServiceProvider m_serviceProvider;
        private readonly IBuildingService m_buildingService;

        public BuildingController(IServiceProvider serviceProvider, IBuildingService buildingService)
        {
            m_serviceProvider = serviceProvider;
            m_buildingService = buildingService;
        }

        public void ProduceUnit(Building building, string unitPrototypeCode, bool addToQueue)
        {
            if (!building.TryGetComponentOfType(out OrderQueueComponent orderQueueComponent))
                return;

            if (!building.TryGetComponentOfType(out ProductionFacilityComponent productionFacilityComponent))
                return;

            UnitPrototype unitPrototype = productionFacilityComponent.UnitsProduced.Where(unitProduced => unitProduced.Code == unitPrototypeCode).SingleOrDefault();
            if (unitPrototype == null)
                return;

            if (!addToQueue)
                orderQueueComponent.ClearOrderQueue();

            IPlayerService playerService = m_serviceProvider.GetRequiredService<IPlayerService>();
            orderQueueComponent.EnqueueOrder(new ProduceUnitOrder(building, unitPrototype, playerService));
            m_buildingService.RegisterBuildingChanges(building.Id);
        }

        public void ClearOrderQueue(Building building)
        {
            if (!building.TryGetComponentOfType(out OrderQueueComponent orderQueueComponent))
                return;

            orderQueueComponent.ClearOrderQueue();
            m_buildingService.RegisterBuildingChanges(building.Id);
        }
    }
}
