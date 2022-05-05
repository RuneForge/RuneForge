using System;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Interface.Controls;
using RuneForge.Core.Interface.Interfaces;

namespace RuneForge.Core.Interface.Windows
{
    public partial class IngameMenuWindow : Window
    {
        private static readonly TimeSpan s_saveGameButtonThrottleTime = TimeSpan.FromSeconds(1);

        private readonly ISpriteFontProvider m_spriteFontProvider;
        private DateTime m_previousClickTime;

        public IngameMenuWindow(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, ISpriteFontProvider spriteFontProvider)
            : base(eventSource, contentManager, graphicsDevice, spriteBatch)
        {
            m_spriteFontProvider = spriteFontProvider;
            m_previousClickTime = DateTime.MinValue;
            CreateLayout();
        }

        public event EventHandler<EventArgs> GameResumed;
        public event EventHandler<EventArgs> GameSaved;
        public event EventHandler<EventArgs> ExitedToMainMenu;
        public event EventHandler<EventArgs> ExitedToDesktop;

        public override void LoadContent()
        {
            m_resumeGameButton.SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
            m_saveGameButton.SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
            m_exitToMainMenuButton.SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
            m_exitToDesktopButton.SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
            SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
            base.LoadContent();
        }

        protected virtual void OnGameResumed(EventArgs e)
        {
            GameResumed?.Invoke(this, e);
        }
        protected virtual void OnGameSaved(EventArgs e)
        {
            GameSaved?.Invoke(this, e);
        }
        protected virtual void OnExitedToMainMenu(EventArgs e)
        {
            ExitedToMainMenu?.Invoke(this, e);
        }
        protected virtual void OnExitedToDesktop(EventArgs e)
        {
            ExitedToDesktop?.Invoke(this, e);
        }
    }
}
