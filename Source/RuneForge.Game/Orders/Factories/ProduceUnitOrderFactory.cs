using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using RuneForge.Data.Orders;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Orders.Attributes;
using RuneForge.Game.Orders.Implementations;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Orders.Factories
{
    [OrderDto(typeof(ProduceUnitOrderDto))]
    public class ProduceUnitOrderFactory : OrderFactory<ProduceUnitOrder, ProduceUnitOrderDto>
    {
        private readonly IServiceProvider m_serviceProvider;
        private readonly IGameSessionContext m_gameSessionContext;

        public ProduceUnitOrderFactory(IServiceProvider serviceProvider, IGameSessionContext gameSessionContext, IUnitService unitService, IBuildingService buildingService)
            : base(unitService, buildingService)
        {
            m_serviceProvider = serviceProvider;
            m_gameSessionContext = gameSessionContext;
        }

        public override ProduceUnitOrder CreateOrderFromDto(ProduceUnitOrderDto orderDto)
        {
            Entity entity = GetEntity(orderDto.EntityId);
            IPlayerService playerService = m_serviceProvider.GetRequiredService<IPlayerService>();
            UnitPrototype unitPrototype = m_gameSessionContext.Map.UnitPrototypes.Where(unitPrototype => unitPrototype.Code == orderDto.UnitPrototypeCode).Single();
            return new ProduceUnitOrder(entity, unitPrototype, (OrderState)orderDto.State, playerService);
        }
    }
}
