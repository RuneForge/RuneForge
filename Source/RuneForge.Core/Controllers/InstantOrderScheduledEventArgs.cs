using System;

namespace RuneForge.Core.Controllers
{
    public class InstantOrderScheduledEventArgs : EventArgs
    {
        public Type OrderType { get; }

        public object OrderData { get; }

        public InstantOrderScheduledEventArgs(Type orderType)
            : this(orderType, null)
        {
        }
        public InstantOrderScheduledEventArgs(Type orderType, object orderData)
        {
            OrderType = orderType;
            OrderData = orderData;
        }
    }
}
