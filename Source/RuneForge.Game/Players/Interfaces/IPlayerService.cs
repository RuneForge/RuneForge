using System;
using System.Collections.ObjectModel;

namespace RuneForge.Game.Players.Interfaces
{
    public interface IPlayerService
    {
        public Player GetPlayer(Guid playerId);

        public ReadOnlyCollection<Player> GetPlayers();

        public void AddPlayer(Player player);

        public void RemovePlayer(Guid playerId);
    }
}
