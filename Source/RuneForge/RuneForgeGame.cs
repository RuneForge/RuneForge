using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RuneForge.Core.GameStates;
using RuneForge.Core.GameStates.Interfaces;

using XnaGame = Microsoft.Xna.Framework.Game;

namespace RuneForge
{
    public class RuneForgeGame : XnaGame
    {
        private readonly IGameStateService m_gameStateService;

        public RuneForgeGame(IServiceProvider serviceProvider, GameWindow gameWindow, IEnumerable<IGameComponent> gameComponents)
            : base(serviceProvider, gameWindow)
        {
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

            m_gameStateService.RunGameState<MainMenuGameState>();
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
