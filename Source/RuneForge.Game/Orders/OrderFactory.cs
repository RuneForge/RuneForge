using RuneForge.Data.Orders;
using RuneForge.Game.Orders.Interfaces;

namespace RuneForge.Game.Orders
{
    public abstract class OrderFactory<TOrder, TOrderDto> : IOrderFactory<TOrder, TOrderDto>
        where TOrder : Order
        where TOrderDto : OrderDto
    {
        protected OrderFactory()
        {
        }

        public abstract TOrder CreateOrderFromDto(TOrderDto orderDto);

        public Order CreateOrderFromDto(OrderDto orderDto)
        {
            return CreateOrderFromDto((TOrderDto)orderDto);
        }
    }
}
