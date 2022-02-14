using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RuneForge
{
    public class RuneForgeGame : Game
    {
        public RuneForgeGame(IServiceProvider serviceProvider, GameWindow gameWindow, IEnumerable<IGameComponent> gameComponents)
            : base(serviceProvider, gameWindow)
        {
            ContentManager.RootDirectory = "Content";
            FixedTimeStep = true;
            MouseVisible = true;

            foreach (IGameComponent gameComponent in gameComponents)
                Components.Add(gameComponent);
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
