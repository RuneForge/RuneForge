using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RuneForge.Core.GameStates;
using RuneForge.Core.GameStates.Interfaces;

namespace RuneForge
{
    public class RuneForgeGame : Game
    {
        private readonly IServiceProvider m_serviceProvider;
        private readonly IGameStateService m_gameStateService;

        public RuneForgeGame(IServiceProvider serviceProvider, GameWindow gameWindow, IEnumerable<IGameComponent> gameComponents)
            : base(serviceProvider, gameWindow)
        {
            m_serviceProvider = serviceProvider;
            m_gameStateService = serviceProvider.GetRequiredService<IGameStateService>();

            ContentManager.RootDirectory = "Content";
            FixedTimeStep = true;
            MouseVisible = true;

            foreach (IGameComponent gameComponent in gameComponents)
            {
                Components.Add(gameComponent);
            }
        }

        protected override void LoadContent()
        {
            GraphicsDevice.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
            base.LoadContent();

            MainMenuGameState mainMenuGameState = m_serviceProvider.GetRequiredService<MainMenuGameState>();
            m_gameStateService.RunGameState(mainMenuGameState);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Key.Escape))
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
