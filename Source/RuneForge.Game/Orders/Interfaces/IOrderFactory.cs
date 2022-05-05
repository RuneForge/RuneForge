using RuneForge.Data.Orders;

namespace RuneForge.Game.Orders.Interfaces
{
    public interface IOrderFactory
    {
        public Order CreateOrderFromDto(OrderDto orderDto);
    }

    public interface IOrderFactory<TOrder, TOrderDto> : IOrderFactory
        where TOrder : Order
        where TOrderDto : OrderDto
    {
        public TOrder CreateOrderFromDto(TOrderDto orderDto);
    }
}
