using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Data.Players.Interfaces;

namespace RuneForge.Data.Players
{
    public class InMemoryPlayerRepository : IPlayerRepository
    {
        private List<PlayerDto> m_players;
        private Dictionary<Guid, int> m_playersByIds;

        public InMemoryPlayerRepository()
        {
            m_players = new List<PlayerDto>();
            m_playersByIds = new Dictionary<Guid, int>();
        }

        public PlayerDto GetPlayer(Guid playerId)
        {
            if (m_playersByIds.TryGetValue(playerId, out int playerIndex))
                return m_players[playerIndex];
            else
                throw new KeyNotFoundException("No player was found by the specified Id.");
        }

        public ReadOnlyCollection<PlayerDto> GetPlayers()
        {
            return m_players.AsReadOnly();
        }

        public void AddPlayer(PlayerDto player)
        {
            if (player.Id == Guid.Empty)
                throw new ArgumentException("Unable to add a player with an empty Id.");
            if (!m_playersByIds.ContainsKey(player.Id))
            {
                m_players.Add(player);
                m_playersByIds.Add(player.Id, m_players.Count - 1);
            }
            else
                throw new InvalidOperationException("Unable to add a player with an existing Id.");
        }

        public void SavePlayer(PlayerDto player)
        {
            if (m_playersByIds.TryGetValue(player.Id, out int playerIndex))
                m_players[playerIndex] = player;
            else
                throw new KeyNotFoundException("No player was found by the specified Id.");
        }

        public void RemovePlayer(Guid playerId)
        {
            if (m_playersByIds.TryGetValue(playerId, out int playerIndex))
            {
                m_players.RemoveAt(playerIndex);
                for (int i = playerIndex; i < m_players.Count; i++)
                    m_playersByIds[m_players[i].Id] = i;
            }
            else
                throw new KeyNotFoundException("No player was found by the specified Id.");
        }
    }
}
