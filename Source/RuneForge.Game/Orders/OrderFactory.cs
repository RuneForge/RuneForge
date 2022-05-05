using System;

using RuneForge.Data.Orders;
using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Entities;
using RuneForge.Game.Orders.Interfaces;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Orders
{
    public abstract class OrderFactory<TOrder, TOrderDto> : IOrderFactory<TOrder, TOrderDto>
        where TOrder : Order
        where TOrderDto : OrderDto
    {
        private readonly IUnitService m_unitService;
        private readonly IBuildingService m_buildingService;

        protected OrderFactory(IUnitService unitService, IBuildingService buildingService)
        {
            m_unitService = unitService;
            m_buildingService = buildingService;
        }

        public abstract TOrder CreateOrderFromDto(TOrderDto orderDto);

        public Order CreateOrderFromDto(OrderDto orderDto)
        {
            return CreateOrderFromDto((TOrderDto)orderDto);
        }

        protected Entity GetEntity(string typedEntityId)
        {
            string entityTypeName = typedEntityId.Split(":")[0];
            int id = int.Parse(typedEntityId.Split(":")[1]);

            return entityTypeName switch
            {
                nameof(Unit) => m_unitService.GetUnit(id),
                nameof(Building) => m_buildingService.GetBuilding(id),
                _ => throw new InvalidOperationException("Unknown entity type."),
            };
        }
    }
}
