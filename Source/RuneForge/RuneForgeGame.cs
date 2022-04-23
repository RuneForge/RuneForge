using System;
using System.Collections.Generic;

using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RuneForge.Configuration;
using RuneForge.Core.GameStates;
using RuneForge.Core.GameStates.Interfaces;
using RuneForge.Core.Input;
using RuneForge.Core.Input.EventProviders.Interfaces;

using XnaGame = Microsoft.Xna.Framework.Game;

namespace RuneForge
{
    public class RuneForgeGame : XnaGame
    {
        private readonly IGameStateService m_gameStateService;
        private readonly IKeyboardEventProvider m_keyboardEventProvider;
        private readonly Lazy<GraphicsDeviceManager> m_graphicsDeviceManagerProvider;
        private readonly GraphicsConfiguration m_graphicsConfiguration;

        public RuneForgeGame(
            IServiceProvider serviceProvider,
            GameWindow gameWindow,
            IGameStateService gameStateService,
            IKeyboardEventProvider keyboardEventProvider,
            Lazy<GraphicsDeviceManager> graphicsDeviceManagerProvider,
            IOptions<GraphicsConfiguration> graphicsConfigurationOptions,
            IEnumerable<IGameComponent> gameComponents
            )
            : base(serviceProvider, gameWindow)
        {
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

            m_gameStateService.RunGameState<MainMenuGameState>();
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
