using System;

using Microsoft.Extensions.DependencyInjection;

using RuneForge.Data.Orders;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Entities;
using RuneForge.Game.Orders.Attributes;
using RuneForge.Game.Orders.Implementations;
using RuneForge.Game.PathGenerators.Interfaces;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Orders.Factories
{
    [OrderDto(typeof(MoveOrderDto))]
    public class MoveOrderFactory : OrderFactory<MoveOrder, MoveOrderDto>
    {
        private readonly IServiceProvider m_serviceProvider;

        public MoveOrderFactory(IServiceProvider serviceProvider, IUnitService unitService, IBuildingService buildingService)
            : base(unitService, buildingService)
        {
            m_serviceProvider = serviceProvider;
        }

        public override MoveOrder CreateOrderFromDto(MoveOrderDto orderDto)
        {
            Entity entity = GetEntity(orderDto.EntityId);
            IPathGenerator pathGenerator = m_serviceProvider.GetRequiredService<IPathGenerator>();
            return new MoveOrder(entity, orderDto.DestinationX, orderDto.DestinationY, (OrderState)orderDto.State, orderDto.CancellationRequested, pathGenerator);
        }
    }
}
