using Microsoft.Xna.Framework;

using RuneForge.Core.GameStates.Interfaces;

namespace RuneForge.Core.GameStates.Components
{
    public class GameStateComponent : DrawableGameComponent
    {
        private readonly IGameStateService m_gameStateService;

        public GameStateComponent(IGameStateService gameStateService)
        {
            m_gameStateService = gameStateService;
        }

        public override void Update(GameTime gameTime)
        {
            m_gameStateService.CurrentGameState?.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            m_gameStateService.CurrentGameState?.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            m_gameStateService.LoadContent();
        }
        protected override void UnloadContent()
        {
            m_gameStateService.UnloadContent();
        }
    }
}
