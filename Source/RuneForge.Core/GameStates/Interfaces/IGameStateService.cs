using System;

namespace RuneForge.Core.GameStates.Interfaces
{
    public interface IGameStateService
    {
        public GameState CurrentGameState { get; }

        public event EventHandler<EventArgs> GameStateChanging;
        public event EventHandler<EventArgs> GameStateChanged;

        public void RunGameState(GameState gameState);
        public void RunGameState(GameState gameState, bool unloadContent);

        public void LoadContent();
        public void UnloadContent();
    }
}
