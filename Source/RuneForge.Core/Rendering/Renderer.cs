﻿using System;

using Microsoft.Xna.Framework;

using RuneForge.Core.Rendering.Interfaces;

namespace RuneForge.Core.Rendering
{
    public abstract class Renderer : IRenderer
    {
        private bool m_visible;
        private int m_drawOrder;

        protected Renderer()
        {
            m_visible = true;
            m_drawOrder = 0;
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

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public abstract void Draw(GameTime gameTime);
        public abstract void LoadContent();

        protected virtual void OnVisibleChanged(EventArgs e)
        {
            VisibleChanged?.Invoke(this, e);
        }
        protected virtual void OnDrawOrderChanged(EventArgs e)
        {
            DrawOrderChanged?.Invoke(this, e);
        }
    }
}
