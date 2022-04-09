using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Interface.Controls;
using RuneForge.Core.Interface.Interfaces;
using RuneForge.Core.Interface.Windows;

using XnaGame = Microsoft.Xna.Framework.Game;

namespace RuneForge.Core.GameStates
{
    public class MainMenuGameState : GameState
    {
        private readonly Lazy<XnaGame> m_gameProvider;
        private readonly Lazy<ContentManager> m_contentManagerProvider;
        private readonly Lazy<GraphicsDevice> m_graphicsDeviceProvider;
        private readonly ISpriteFontProvider m_spriteFontProvider;
        private readonly IGraphicsInterfaceService m_graphicsInterfaceService;
        private SpriteBatch m_spriteBatch;
        private MainMenuWindow m_mainMenuWindow;
        private Label m_titleLabel;

        public MainMenuGameState(
            Lazy<XnaGame> gameProvider,
            Lazy<ContentManager> contentManagerProvider,
            Lazy<GraphicsDevice> graphicsDeviceProvider,
            ISpriteFontProvider spriteFontProvider,
            IGraphicsInterfaceService graphicsInterfaceService
            )
        {
            m_gameProvider = gameProvider;
            m_contentManagerProvider = contentManagerProvider;
            m_graphicsDeviceProvider = graphicsDeviceProvider;
            m_spriteFontProvider = spriteFontProvider;
            m_graphicsInterfaceService = graphicsInterfaceService;
        }

        public override void Run()
        {
            GraphicsDevice graphicsDevice = m_graphicsDeviceProvider.Value;
            m_graphicsInterfaceService.Viewport = new Viewport(0, 0, graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight);
            m_graphicsInterfaceService.RegisterControl(m_titleLabel);
            m_graphicsInterfaceService.RegisterControl(m_mainMenuWindow);
            base.Run();
        }
        public override void Stop()
        {
            m_graphicsInterfaceService.UnregisterControl(m_titleLabel);
            m_graphicsInterfaceService.UnregisterControl(m_mainMenuWindow);
            base.Stop();
        }

        public override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(m_graphicsDeviceProvider.Value);
            m_titleLabel = new Label(null, m_contentManagerProvider.Value, m_graphicsDeviceProvider.Value, m_spriteBatch)
            {
                X = 32,
                Y = 32,
                Width = 384,
                Height = 96,
                Text = "RuneForge",
                TextAlignment = Alignment.Center,
                TextColor = Color.LightGoldenrodYellow,
                ShadowColor = Color.Transparent,
                SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Foundation", 48),
            };
            m_mainMenuWindow = new MainMenuWindow(new ControlEventSource(), m_contentManagerProvider.Value, m_graphicsDeviceProvider.Value, m_spriteBatch, m_spriteFontProvider)
            {
                X = m_titleLabel.X,
                Y = m_titleLabel.Y + m_titleLabel.Height + 16,
            };

            m_mainMenuWindow.ExitGameButtonClicked += (sender, e) => m_gameProvider.Value.Exit();

            m_titleLabel.LoadContent();
            m_mainMenuWindow.LoadContent();
            base.LoadContent();
        }
    }
}
