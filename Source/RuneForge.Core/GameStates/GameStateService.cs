using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;

using RuneForge.Core.GameStates.Interfaces;

namespace RuneForge.Core.GameStates
{
    public class GameStateService : IGameStateService
    {
        private readonly IServiceScopeFactory m_serviceScopeFactory;
        private readonly Lazy<ContentManager> m_contentManagerProvider;
        private bool m_globalContentLoaded;
        private bool m_globalContentUnloaded;

        public IServiceScope CurrentServiceScope { get; private set; }

        public GameState CurrentGameState { get; private set; }

        public GameStateService(IServiceScopeFactory serviceScopeFactory, Lazy<ContentManager> contentManagerProvider)
        {
            m_serviceScopeFactory = serviceScopeFactory;
            m_contentManagerProvider = contentManagerProvider;
        }

        public event EventHandler<EventArgs> GameStateChanging;
        public event EventHandler<EventArgs> GameStateChanged;

        public void RunGameState(IServiceScope serviceScope, GameState gameState)
        {
            RunGameState(serviceScope, gameState, false);
        }
        public void RunGameState(IServiceScope serviceScope, GameState gameState, bool unloadContent)
        {
            if (serviceScope == null)
                throw new ArgumentNullException(nameof(serviceScope));
            if (gameState == null)
                throw new ArgumentNullException(nameof(gameState));

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
                CurrentServiceScope.Dispose();
            }

            CurrentServiceScope = serviceScope;
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
        public void RunGameState<TGameState>()
            where TGameState : GameState
        {
            RunGameState<TGameState>(false);
        }
        public void RunGameState<TGameState>(bool unloadContent)
            where TGameState : GameState
        {
            IServiceScope serviceScope = m_serviceScopeFactory.CreateScope();
            TGameState gameState = serviceScope.ServiceProvider.GetRequiredService<TGameState>();
            RunGameState(serviceScope, gameState, unloadContent);
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
