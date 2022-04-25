using System;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Interface.Controls;
using RuneForge.Core.Interface.Interfaces;

namespace RuneForge.Core.Interface.Windows
{
    public partial class OrderConfirmationDialogWindow : Window
    {
        private readonly ISpriteFontProvider m_spriteFontProvider;

        public OrderConfirmationDialogWindow(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, ISpriteFontProvider spriteFontProvider)
            : base(eventSource, contentManager, graphicsDevice, spriteBatch)
        {
            m_spriteFontProvider = spriteFontProvider;
            CreateLayout();
        }

        public event EventHandler<EventArgs> OrderCancelled;

        public override void LoadContent()
        {
            SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
            m_tooltipLabel.SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
            m_cancelOrderButton.SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
            base.LoadContent();
        }

        protected virtual void OnOrderCancelled(EventArgs e)
        {
            OrderCancelled?.Invoke(this, e);
        }
    }
}
