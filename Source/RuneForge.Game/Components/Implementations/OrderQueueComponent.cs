using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Game.Orders;

namespace RuneForge.Game.Components.Implementations
{
    public class OrderQueueComponent : Component
    {
        private readonly Queue<Order> m_pendingOrders;
        private ReadOnlyCollection<Order> m_pendingOrdersCache;
        private bool m_cacheInvalidated;

        public Order CurrentOrder { get; private set; }

        public OrderQueueComponent()
            : this(null, new Queue<Order>())
        {
        }
        public OrderQueueComponent(Order currentOrder, Queue<Order> pendingOrders)
        {
            m_pendingOrders = pendingOrders;
            m_pendingOrdersCache = null;
            m_cacheInvalidated = true;
            CurrentOrder = currentOrder;
        }

        public ReadOnlyCollection<Order> PendingOrders
        {
            get
            {
                if (!m_cacheInvalidated)
                    return m_pendingOrdersCache;
                else
                {
                    m_cacheInvalidated = false;
                    return m_pendingOrdersCache = new ReadOnlyCollection<Order>(m_pendingOrders.ToArray());
                }
            }
        }

        public void EnqueueOrder(Order order)
        {
            m_cacheInvalidated = true;
            m_pendingOrders.Enqueue(order);
            if (CurrentOrder == null)
                CurrentOrder = m_pendingOrders.Dequeue();
        }

        public void ClearOrderQueue()
        {
            m_cacheInvalidated = true;
            m_pendingOrders.Clear();
            CurrentOrder?.Cancel();
        }

        public void ExecuteNextOrder()
        {
            m_cacheInvalidated = true;
            CurrentOrder = m_pendingOrders.Dequeue();
            CurrentOrder?.Execute();
        }
    }
}
