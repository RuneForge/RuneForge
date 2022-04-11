using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using AutoMapper;

using RuneForge.Data.Players;
using RuneForge.Data.Players.Interfaces;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Players.Interfaces;

namespace RuneForge.Game.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository m_playerRepository;
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IMapper m_mapper;
        private readonly Dictionary<Guid, int> m_playersByIds;

        public PlayerService(IPlayerRepository playerRepository, IGameSessionContext gameSessionContext, IMapper mapper)
        {
            m_playerRepository = playerRepository;
            m_gameSessionContext = gameSessionContext;
            m_mapper = mapper;
            m_playersByIds = new Dictionary<Guid, int>();
        }

        public Player GetPlayer(Guid playerId)
        {
            if (m_playersByIds.TryGetValue(playerId, out int playerIndex))
                return m_gameSessionContext.Players[playerIndex];
            else
                throw new KeyNotFoundException("No player was found by the specified Id.");
        }

        public ReadOnlyCollection<Player> GetPlayers()
        {
            return new ReadOnlyCollection<Player>(m_gameSessionContext.Players);
        }

        public void AddPlayer(Player player)
        {
            if (player.Id == Guid.Empty)
                throw new ArgumentException("Unable to add a player with an empty Id.");
            if (!m_playersByIds.ContainsKey(player.Id))
            {
                m_gameSessionContext.Players.Add(player);
                m_playersByIds.Add(player.Id, m_gameSessionContext.Players.Count - 1);
                m_playerRepository.AddPlayer(m_mapper.Map<Player, PlayerDto>(player));
            }
            else
                throw new InvalidOperationException("Unable to add a player with an existing Id.");
        }

        public void RemovePlayer(Guid playerId)
        {
            if (m_playersByIds.TryGetValue(playerId, out int playerIndex))
            {
                m_gameSessionContext.Players.RemoveAt(playerIndex);
                for (int i = playerIndex; i < m_gameSessionContext.Players.Count; i++)
                    m_playersByIds[m_gameSessionContext.Players[i].Id] = i;
                m_playerRepository.RemovePlayer(playerId);
            }
            else
                throw new KeyNotFoundException("No player was found by the specified Id.");
        }
    }
}
