using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RuneForge.Configuration;
using RuneForge.Core.GameStates;
using RuneForge.Core.GameStates.Implementations;
using RuneForge.Core.GameStates.Interfaces;
using RuneForge.Core.Input;
using RuneForge.Core.Input.EventProviders.Interfaces;
using RuneForge.Game.GameSessions;

using XnaGame = Microsoft.Xna.Framework.Game;

namespace RuneForge
{
#error Sample error.
    public class RuneForgeGame : XnaGame
    {
        private static readonly string s_defaultMapAssetName = Path.Combine("Maps", "Southshore");

        private readonly IConfiguration m_configuration;
        private readonly IServiceScopeFactory m_serviceScopeFactory;
        private readonly IGameStateService m_gameStateService;
        private readonly IKeyboardEventProvider m_keyboardEventProvider;
        private readonly Lazy<GraphicsDeviceManager> m_graphicsDeviceManagerProvider;
        private readonly GraphicsConfiguration m_graphicsConfiguration;

        public RuneForgeGame(
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            IServiceScopeFactory serviceScopeFactory,
            GameWindow gameWindow,
            IGameStateService gameStateService,
            IKeyboardEventProvider keyboardEventProvider,
            Lazy<GraphicsDeviceManager> graphicsDeviceManagerProvider,
            IOptions<GraphicsConfiguration> graphicsConfigurationOptions,
            IEnumerable<IGameComponent> gameComponents
            )
            : base(serviceProvider, gameWindow)
        {
            m_configuration = configuration;
            m_serviceScopeFactory = serviceScopeFactory;
            m_gameStateService = gameStateService;
            m_keyboardEventProvider = keyboardEventProvider;
            m_graphicsDeviceManagerProvider = graphicsDeviceManagerProvider;
            m_graphicsConfiguration = graphicsConfigurationOptions.Value;

            ContentManager.RootDirectory = "Content";
            FixedTimeStep = true;
            MouseVisible = true;

            foreach (IGameComponent gameComponent in gameComponents)
                Components.Add(gameComponent);
        }

        protected override void Initialize()
        {
            GraphicsDeviceManager graphicsDeviceManager = m_graphicsDeviceManagerProvider.Value;
            graphicsDeviceManager.PreferredBackBufferWidth = m_graphicsConfiguration.BackBufferWidth;
            graphicsDeviceManager.PreferredBackBufferHeight = m_graphicsConfiguration.BackBufferHeight;
            graphicsDeviceManager.IsFullScreen = m_graphicsConfiguration.UseFullScreen;
            graphicsDeviceManager.HardwareModeSwitch = m_graphicsConfiguration.UseHardwareFullScreen;
            graphicsDeviceManager.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            GraphicsDevice.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
            base.LoadContent();

            bool loadSavedGame = m_configuration.GetValue<bool>("LoadGame");

            if (!loadSavedGame)
                m_gameStateService.RunGameState<MainMenuGameState>();
            else
            {
                bool loadSavedGameInPausedState = m_configuration.GetValue<bool>("LoadGame:LoadPaused");
                string savedGameFileName = m_configuration.GetValue<string>("LoadGame:FileName");

                if (string.IsNullOrEmpty(savedGameFileName))
                    throw new InvalidOperationException("No saved game file name was provided.");

                IServiceScope serviceScope = m_serviceScopeFactory.CreateScope();
                GameSessionParameters gameSessionParameters = serviceScope.ServiceProvider.GetRequiredService<GameSessionParameters>();
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                using (FileStream fileStream = new FileStream(savedGameFileName, FileMode.Open))
                {
                    gameSessionParameters.Type = GameSessionType.LoadedGame;
                    gameSessionParameters.MapAssetName = s_defaultMapAssetName;
                    gameSessionParameters.GameSessionContext = (SerializableGameSessionContext)binaryFormatter.Deserialize(fileStream);
                    gameSessionParameters.StartPaused = loadSavedGameInPausedState;
                }

                m_gameStateService.RunGameState(serviceScope, serviceScope.ServiceProvider.GetRequiredService<GameplayGameState>());
            }
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardStateEx keyboardState = m_keyboardEventProvider.GetState();
            if (keyboardState.WasKeyJustPressed(Key.Q) && (keyboardState.GetModifierKeys() & ModifierKeys.Control) == ModifierKeys.Control)
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
