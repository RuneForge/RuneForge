using System;

using Microsoft.Xna.Framework;

using RuneForge.Core.Rendering.Interfaces;

namespace RuneForge.Core.Rendering
{
    public abstract class Renderer : IRenderer
    {
        private bool m_enabled;
        private bool m_visible;
        private int m_updateOrder;
        private int m_drawOrder;

        protected Renderer()
        {
            m_visible = true;
            m_drawOrder = 0;
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
        public bool Visible
        {
            get => m_visible;
            set
            {
                if (m_visible != value)
                {
                    m_visible = value;
                    OnVisibleChanged(EventArgs.Empty);
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
        public int DrawOrder
        {
            get => m_drawOrder;
            set
            {
                if (m_drawOrder != value)
                {
                    m_drawOrder = value;
                    OnDrawOrderChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        public event EventHandler<EventArgs> DrawOrderChanged;

        public virtual void Update(GameTime gameTime)
        {
        }
        public virtual void Draw(GameTime gameTime)
        {
        }
        public virtual void LoadContent()
        {
        }

        protected virtual void OnEnabledChanged(EventArgs e)
        {
            EnabledChanged?.Invoke(this, e);
        }
        protected virtual void OnVisibleChanged(EventArgs e)
        {
            VisibleChanged?.Invoke(this, e);
        }
        protected virtual void OnUpdateOrderChanged(EventArgs e)
        {
            UpdateOrderChanged?.Invoke(this, e);
        }
        protected virtual void OnDrawOrderChanged(EventArgs e)
        {
            DrawOrderChanged?.Invoke(this, e);
        }
    }
}
