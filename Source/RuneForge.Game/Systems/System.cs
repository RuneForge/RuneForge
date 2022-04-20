using System;

using Microsoft.Xna.Framework;

using RuneForge.Game.Systems.Interfaces;

namespace RuneForge.Game.Systems
{
    public abstract class System : ISystem
    {
        private bool m_enabled;
        private int m_updateOrder;

        public System()
        {
            m_enabled = true;
            m_updateOrder = 0;
        }

        public bool Enabled
        {
            get => m_enabled;
            set
            {
                if (m_enabled != value)
                {
                    m_enabled = value;
                    OnEnabledChanged(EventArgs.Empty);
                }
            }
        }
        public int UpdateOrder
        {
            get => m_updateOrder;
            set
            {
                if (m_updateOrder != value)
                {
                    m_updateOrder = value;
                    OnUpdateOrderChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public virtual void Update(GameTime gameTime)
        {
        }

        protected virtual void OnEnabledChanged(EventArgs e)
        {
            EnabledChanged?.Invoke(this, e);
        }
        protected virtual void OnUpdateOrderChanged(EventArgs e)
        {
            UpdateOrderChanged?.Invoke(this, e);
        }
    }
}
