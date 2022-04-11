using System;
using System.Collections.ObjectModel;

namespace RuneForge.Data.Players.Interfaces
{
    public interface IPlayerRepository
    {
        public PlayerDto GetPlayer(Guid playerId);

        public ReadOnlyCollection<PlayerDto> GetPlayers();

        public void AddPlayer(PlayerDto player);

        public void SavePlayer(PlayerDto player);

        public void RemovePlayer(Guid playerId);
    }
}
