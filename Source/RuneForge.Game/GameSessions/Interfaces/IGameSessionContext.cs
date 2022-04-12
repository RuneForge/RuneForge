using System.Collections.ObjectModel;

using RuneForge.Game.Buildings;
using RuneForge.Game.Maps;
using RuneForge.Game.Players;
using RuneForge.Game.Units;

namespace RuneForge.Game.GameSessions.Interfaces
{
    public interface IGameSessionContext
    {
        public Map Map { get; }

        public Collection<Player> Players { get; }

        public Collection<Unit> Units { get; }
        public Collection<Building> Buildings { get; }

        public bool Initialized { get; }

        public void Initialize(GameSessionParameters parameters);
    }
}
