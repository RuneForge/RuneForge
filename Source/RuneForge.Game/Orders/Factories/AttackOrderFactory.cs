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
    [OrderDto(typeof(AttackOrderDto))]
    public class AttackOrderFactory : OrderFactory<AttackOrder, AttackOrderDto>
    {
        private readonly IServiceProvider m_serviceProvider;

        public AttackOrderFactory(IServiceProvider serviceProvider, IUnitService unitService, IBuildingService buildingService)
            : base(unitService, buildingService)
        {
            m_serviceProvider = serviceProvider;
        }

        public override AttackOrder CreateOrderFromDto(AttackOrderDto orderDto)
        {
            Entity entity = GetEntity(orderDto.EntityId);
            Entity targetEntity = GetEntity(orderDto.TargetEntityId);
            IPathGenerator pathGenerator = m_serviceProvider.GetRequiredService<IPathGenerator>();
            return new AttackOrder(entity, targetEntity, orderDto.CompletingRequested, orderDto.CancellationRequested, pathGenerator);
        }
    }
}
