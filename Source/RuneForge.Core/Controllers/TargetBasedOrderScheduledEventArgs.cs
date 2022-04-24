using System;

namespace RuneForge.Core.Controllers
{
    public class TargetBasedOrderScheduledEventArgs : EventArgs
    {
        private bool m_completed;
        private bool m_cancelled;

        public Type OrderType { get; }

        public TargetBasedOrderScheduledEventArgs(Type orderType)
        {
            m_cancelled = false;
            OrderType = orderType;
        }

        public event EventHandler<EventArgs> Completed;
        public event EventHandler<EventArgs> Cancelled;

        public void Complete()
        {
            if (!m_completed && !m_cancelled)
            {
                m_completed = true;
                OnCompleted(Empty);
            }
        }
        public void Cancel()
        {
            if (!m_completed && !m_cancelled)
            {
                m_cancelled = true;
                OnCancelled(Empty);
            }
        }

        protected virtual void OnCompleted(EventArgs e)
        {
            Completed?.Invoke(this, e);
        }
        protected virtual void OnCancelled(EventArgs e)
        {
            Cancelled?.Invoke(this, e);
        }
    }
}
