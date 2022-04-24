using System;

namespace RuneForge.Core.Controllers
{
    public class InstantOrderScheduledEventArgs : EventArgs
    {
        public Type OrderType { get; }

        public InstantOrderScheduledEventArgs(Type orderType)
        {
            OrderType = orderType;
        }
    }
}
