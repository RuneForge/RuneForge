using System;

using Microsoft.Extensions.DependencyInjection;

namespace RuneForge.Core.GameStates.Interfaces
{
    public interface IGameStateService
    {
        public IServiceScope CurrentServiceScope { get; }
        public GameState CurrentGameState { get; }

        public event EventHandler<EventArgs> GameStateChanging;
        public event EventHandler<EventArgs> GameStateChanged;

        public void RunGameState(IServiceScope serviceScope, GameState gameState);
        public void RunGameState(IServiceScope serviceScope, GameState gameState, bool unloadContent);
        public void RunGameState<TGameState>() where TGameState : GameState;
        public void RunGameState<TGameState>(bool unloadContent) where TGameState : GameState;

        public void LoadContent();
        public void UnloadContent();
    }
}
