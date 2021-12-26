using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RuneForge
{
    public class RuneForgeGame : Game
    {
        public RuneForgeGame(IServiceProvider serviceProvider, GameWindow gameWindow)
            : base(serviceProvider, gameWindow)
        {
            ContentManager.RootDirectory = "Content";
            FixedTimeStep = true;
            MouseVisible = true;
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
