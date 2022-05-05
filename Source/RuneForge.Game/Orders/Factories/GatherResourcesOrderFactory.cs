using System;

using Microsoft.Extensions.DependencyInjection;

using RuneForge.Data.Orders;
using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Orders.Attributes;
using RuneForge.Game.Orders.Implementations;
using RuneForge.Game.PathGenerators.Interfaces;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Orders.Factories
{
    [OrderDto(typeof(GatherResourcesOrderDto))]
    public class GatherResourcesOrderFactory : OrderFactory<GatherResourcesOrder, GatherResourcesOrderDto>
    {
        private readonly IServiceProvider m_serviceProvider;
        private readonly IBuildingService m_buildingService;

        public GatherResourcesOrderFactory(IServiceProvider serviceProvider, IUnitService unitService, IBuildingService buildingService)
            : base(unitService, buildingService)
        {
            m_serviceProvider = serviceProvider;
            m_buildingService = buildingService;
        }

        public override GatherResourcesOrder CreateOrderFromDto(GatherResourcesOrderDto orderDto)
        {
            Entity entity = GetEntity(orderDto.EntityId);
            Building resourceSourceBuilding = m_buildingService.GetBuilding(orderDto.ResourceSourceId);
            IGameSessionContext gameSessionContext = m_serviceProvider.GetRequiredService<IGameSessionContext>();
            IBuildingService buildingService = m_serviceProvider.GetRequiredService<IBuildingService>();
            IPathGenerator pathGenerator = m_serviceProvider.GetRequiredService<IPathGenerator>();
            return new GatherResourcesOrder(entity, resourceSourceBuilding, (OrderState)orderDto.State, orderDto.CancellationRequested, gameSessionContext, buildingService, pathGenerator);
        }
    }
}
