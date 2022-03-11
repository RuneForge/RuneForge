using System;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Interface.Controls;
using RuneForge.Core.Interface.Interfaces;

namespace RuneForge.Core.Interface.Windows
{
    public partial class MainMenuWindow : Window
    {
        private readonly ISpriteFontProvider m_spriteFontProvider;

        public MainMenuWindow(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, ISpriteFontProvider spriteFontProvider)
            : base(eventSource, contentManager, graphicsDevice, spriteBatch, DefaultEnabledValue, DefaultVisibleValue, DefaultDrawOrder)
        {
            m_spriteFontProvider = spriteFontProvider;
            CreateLayout();
        }

        public event EventHandler<EventArgs> StartGameButtonClicked;
        public event EventHandler<EventArgs> ExitGameButtonClicked;

        public override void LoadContent()
        {
            SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
            m_newGameButton.SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
            m_exitGameButton.SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
            base.LoadContent();
        }
    }
}
