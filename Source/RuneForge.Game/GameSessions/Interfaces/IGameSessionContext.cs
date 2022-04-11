using System.Collections.ObjectModel;

using RuneForge.Game.Maps;
using RuneForge.Game.Players;

namespace RuneForge.Game.GameSessions.Interfaces
{
    public interface IGameSessionContext
    {
        public Map Map { get; }

        public Collection<Player> Players { get; }

        public bool Initialized { get; }

        public void Initialize(GameSessionParameters parameters);
    }
}
