using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RuneForge.Core.Input;

namespace RuneForge.Core.Interface.Controls
{
    public abstract class ButtonBase : GraphicsControl
    {
        protected const MouseButtons c_allowedMouseButtons = MouseButtons.LeftButton;

        private bool m_pressed;

        private MouseButtons m_button;

        public event EventHandler<EventArgs> Clicked;

        protected ButtonBase(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
            : this(eventSource, contentManager, graphicsDevice, spriteBatch, DefaultEnabledValue, DefaultVisibleValue, DefaultDrawOrder)
        {
        }

        protected ButtonBase(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, bool enabled, bool visible, int drawOrder)
            : base(eventSource, contentManager, graphicsDevice, spriteBatch, enabled, visible, drawOrder)
        {
        }

        protected virtual void OnPressed(MouseEventArgs e)
        {
        }

        protected virtual void OnReleased(MouseEventArgs e)
        {
        }

        protected virtual void OnClicked(MouseEventArgs e)
        {
            Clicked?.Invoke(this, e);
        }

        protected override void OnMouseButtonPressed(object sender, MouseEventArgs e)
        {
            base.OnMouseButtonPressed(sender, e);
            Point locationInsideViewport = e.GetLocationInsideViewport();
            if (!m_pressed && e.Button == (e.Button & MouseButtons.LeftButton) && new Rectangle(0, 0, Width, Height).Contains(locationInsideViewport))
            {
                OnPressedInternal(e);
            }
            else if (m_pressed)
            {
                OnReleasedInternal(e, clicked: false);
            }
        }

        protected override void OnMouseButtonReleased(object sender, MouseEventArgs e)
        {
            base.OnMouseButtonReleased(sender, e);
            Point locationInsideViewport = e.GetLocationInsideViewport();
            if (m_pressed && m_button == e.Button && new Rectangle(0, 0, Width, Height).Contains(locationInsideViewport))
            {
                OnReleasedInternal(e, clicked: true);
            }
            else if (m_pressed && m_button == e.Button)
            {
                OnReleasedInternal(e, clicked: false);
            }
        }

        private void OnPressedInternal(MouseEventArgs e)
        {
            m_pressed = true;
            m_button = e.Button;
            OnPressed(e);
        }

        private void OnReleasedInternal(MouseEventArgs e, bool clicked)
        {
            m_pressed = false;
            OnReleased(e);
            if (clicked)
            {
                OnClicked(e);
            }
        }
    }
}
