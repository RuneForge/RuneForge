using System;
using System.IO;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.GameStates.Implementations;
using RuneForge.Core.GameStates.Interfaces;
using RuneForge.Core.Interface.Controls;
using RuneForge.Core.Interface.Interfaces;
using RuneForge.Core.Interface.Windows;
using RuneForge.Game.GameSessions;

using XnaGame = Microsoft.Xna.Framework.Game;

namespace RuneForge.Core.GameStates
{
    public class MainMenuGameState : GameState
    {
        private static readonly string s_defaultMapAssetName = Path.Combine("Maps", "Southshore");

        private readonly IServiceScopeFactory m_serviceScopeFactory;
        private readonly IGraphicsInterfaceService m_graphicsInterfaceService;
        private readonly ISpriteFontProvider m_spriteFontProvider;
        private readonly IGameStateService m_gameStateService;
        private readonly Lazy<XnaGame> m_gameProvider;
        private readonly Lazy<ContentManager> m_contentManagerProvider;
        private readonly Lazy<GraphicsDevice> m_graphicsDeviceProvider;
        private SpriteBatch m_spriteBatch;
        private MainMenuWindow m_mainMenuWindow;
        private Label m_titleLabel;

        public MainMenuGameState(
            IServiceScopeFactory serviceScopeFactory,
            IGraphicsInterfaceService graphicsInterfaceService,
            ISpriteFontProvider spriteFontProvider,
            IGameStateService gameStateService,
            Lazy<XnaGame> gameProvider,
            Lazy<ContentManager> contentManagerProvider,
            Lazy<GraphicsDevice> graphicsDeviceProvider
            )
        {
            m_serviceScopeFactory = serviceScopeFactory;
            m_graphicsInterfaceService = graphicsInterfaceService;
            m_spriteFontProvider = spriteFontProvider;
            m_gameStateService = gameStateService;
            m_gameProvider = gameProvider;
            m_contentManagerProvider = contentManagerProvider;
            m_graphicsDeviceProvider = graphicsDeviceProvider;
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

            m_mainMenuWindow.StartGameButtonClicked += (sender, e) => OnStartGameButtonClicked(sender, e);
            m_mainMenuWindow.ExitGameButtonClicked += (sender, e) => OnExitGameButtonClicked(sender, e);

            m_titleLabel.LoadContent();
            m_mainMenuWindow.LoadContent();
            base.LoadContent();
        }

        private void OnStartGameButtonClicked(object sender, EventArgs e)
        {
            IServiceScope serviceScope = m_serviceScopeFactory.CreateScope();
            GameSessionParameters gameSessionParameters = serviceScope.ServiceProvider.GetRequiredService<GameSessionParameters>();

            gameSessionParameters.Type = GameSessionType.NewGame;
            gameSessionParameters.MapAssetName = s_defaultMapAssetName;
            gameSessionParameters.StartPaused = false;
            m_gameStateService.RunGameState(serviceScope, serviceScope.ServiceProvider.GetRequiredService<GameplayGameState>());
        }

        private void OnExitGameButtonClicked(object sender, EventArgs e)
        {
            XnaGame game = m_gameProvider.Value;
            game.Exit();
        }
    }
}
