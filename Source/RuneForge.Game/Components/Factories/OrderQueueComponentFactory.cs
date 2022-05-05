using System;
using System.Collections.Generic;
using System.Linq;

using RuneForge.Data.Components;
using RuneForge.Data.Orders;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Orders;
using RuneForge.Game.Orders.Attributes;
using RuneForge.Game.Orders.Interfaces;

namespace RuneForge.Game.Components.Factories
{
    public class OrderQueueComponentFactory : ComponentFactory<OrderQueueComponent, OrderQueueComponentPrototype, OrderQueueComponentDto>
    {
        private readonly Dictionary<Type, IOrderFactory> m_orderFactoriesByDtoTypes;

        public OrderQueueComponentFactory(IEnumerable<IOrderFactory> orderFactories)
        {
            m_orderFactoriesByDtoTypes = new Dictionary<Type, IOrderFactory>();
            ProcessOrderFactories(orderFactories);
        }

        public override OrderQueueComponent CreateComponentFromPrototype(OrderQueueComponentPrototype componentPrototype, OrderQueueComponentPrototype componentPrototypeOverride)
        {
            return new OrderQueueComponent();
        }

        public override OrderQueueComponent CreateComponentFromDto(OrderQueueComponentDto componentDto)
        {
            Order currentOrder = CreateOrderFromDto(componentDto.CurrentOrder);
            Queue<Order> pendingOrders = new Queue<Order>();
            foreach (OrderDto orderDto in componentDto.PendingOrders)
                pendingOrders.Enqueue(CreateOrderFromDto(orderDto));
            return new OrderQueueComponent(currentOrder, pendingOrders);
        }

        private void ProcessOrderFactories(IEnumerable<IOrderFactory> orderFactories)
        {
            foreach (IOrderFactory orderFactory in orderFactories)
            {
                Type orderFactoryType = orderFactory.GetType();

                OrderDtoAttribute[] attributes = orderFactoryType
                    .GetCustomAttributes(typeof(OrderDtoAttribute), false)
                    .Cast<OrderDtoAttribute>()
                    .ToArray();

                if (attributes.Length == 0)
                    throw new InvalidOperationException($"Unable to find the {nameof(OrderDtoAttribute)} attribute on the {orderFactoryType.Name} type.");
                else
                {
                    OrderDtoAttribute attribute = attributes.Single();

                    Type orderDtoType = attribute.OrderDtoType;
                    m_orderFactoriesByDtoTypes.Add(orderDtoType, orderFactory);
                }
            }
        }

        private Order CreateOrderFromDto(OrderDto orderDto)
        {
            if (orderDto == null)
                return null;

            Type orderDtoType = orderDto.GetType();
            if (!m_orderFactoriesByDtoTypes.TryGetValue(orderDtoType, out IOrderFactory orderFactory))
                throw new InvalidOperationException($"Unable to find a factory for an order DTO of type {orderDtoType.Name}.");

            return orderFactory.CreateOrderFromDto(orderDto);
        }
    }
}
