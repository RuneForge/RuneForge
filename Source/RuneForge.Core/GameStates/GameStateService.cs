using System;

using Microsoft.Xna.Framework.Content;

using RuneForge.Core.GameStates.Interfaces;

namespace RuneForge.Core.GameStates
{
    public class GameStateService : IGameStateService
    {
        private readonly Lazy<ContentManager> m_contentManagerProvider;
        private bool m_globalContentLoaded;
        private bool m_globalContentUnloaded;

        public GameState CurrentGameState { get; private set; }

        public GameStateService(Lazy<ContentManager> contentManagerProvider)
        {
            m_contentManagerProvider = contentManagerProvider;
        }

        public event EventHandler<EventArgs> GameStateChanging;
        public event EventHandler<EventArgs> GameStateChanged;

        public void RunGameState(GameState gameState)
        {
            RunGameState(gameState, false);
        }
        public void RunGameState(GameState gameState, bool unloadContent)
        {
            OnGameStateChanging(EventArgs.Empty);

            if (CurrentGameState != null)
            {
                CurrentGameState.Stop();
                if (m_globalContentLoaded && !m_globalContentUnloaded && unloadContent)
                {
                    CurrentGameState.UnloadContent();
                    UnloadGlobalContent();
                }
                CurrentGameState.Dispose();
            }

            CurrentGameState = gameState;
            if (CurrentGameState != null)
            {
                if (m_globalContentLoaded)
                {
                    m_globalContentUnloaded = false;
                    CurrentGameState.LoadContent();
                }
                CurrentGameState.Run();
            }

            OnGameStateChanged(EventArgs.Empty);
        }

        public void LoadContent()
        {
            if (!m_globalContentLoaded)
            {
                m_globalContentLoaded = true;
                m_globalContentUnloaded = false;
                CurrentGameState?.LoadContent();
            }
        }
        public void UnloadContent()
        {
            if (!m_globalContentUnloaded)
            {
                m_globalContentUnloaded = true;
                CurrentGameState?.UnloadContent();
            }
        }

        protected void OnGameStateChanging(EventArgs e)
        {
            GameStateChanging?.Invoke(this, e);
        }
        protected void OnGameStateChanged(EventArgs e)
        {
            GameStateChanged?.Invoke(this, e);
        }

        private void UnloadGlobalContent()
        {
            m_globalContentUnloaded = true;
            ContentManager contentManager = m_contentManagerProvider.Value;
            contentManager.Unload();
        }
    }
}
