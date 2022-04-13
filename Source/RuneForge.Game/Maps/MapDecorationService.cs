using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using AutoMapper;

using RuneForge.Data.Maps;
using RuneForge.Data.Maps.Interfaces;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Game.Maps
{
    public class MapDecorationService : IMapDecorationService
    {
        private readonly IMapDecorationRepository m_mapDecorationRepository;
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IMapper m_mapper;
        private readonly Dictionary<int, int> m_mapDecorationsByIds;

        public MapDecorationService(IMapDecorationRepository mapDecorationRepository, IGameSessionContext gameSessionContext, IMapper mapper)
        {
            m_mapDecorationRepository = mapDecorationRepository;
            m_gameSessionContext = gameSessionContext;
            m_mapper = mapper;
            m_mapDecorationsByIds = new Dictionary<int, int>();
        }

        public MapDecoration GetMapDecoration(int mapDecorationId)
        {
            if (m_mapDecorationsByIds.TryGetValue(mapDecorationId, out int mapDecorationIndex))
                return m_gameSessionContext.MapDecorations[mapDecorationIndex];
            else
                throw new KeyNotFoundException("No map decoration was found by the specified Id.");
        }

        public ReadOnlyCollection<MapDecoration> GetMapDecorations()
        {
            return new ReadOnlyCollection<MapDecoration>(m_gameSessionContext.MapDecorations);
        }

        public void AddMapDecoration(MapDecoration mapDecoration)
        {
            if (mapDecoration.Id == 0)
                throw new ArgumentException("Unable to add a map decoration with an empty Id.");
            if (!m_mapDecorationsByIds.ContainsKey(mapDecoration.Id))
            {
                m_gameSessionContext.MapDecorations.Add(mapDecoration);
                m_mapDecorationsByIds.Add(mapDecoration.Id, m_gameSessionContext.MapDecorations.Count - 1);
                m_mapDecorationRepository.AddMapDecoration(m_mapper.Map<MapDecoration, MapDecorationDto>(mapDecoration));
            }
            else
                throw new InvalidOperationException("Unable to add a map decoration with an existing Id.");
        }

        public void RemoveMapDecoration(int mapDecorationId)
        {
            if (m_mapDecorationsByIds.TryGetValue(mapDecorationId, out int mapDecorationIndex))
            {
                m_gameSessionContext.MapDecorations.RemoveAt(mapDecorationIndex);
                for (int i = mapDecorationIndex; i < m_gameSessionContext.MapDecorations.Count; i++)
                    m_mapDecorationsByIds[m_gameSessionContext.MapDecorations[i].Id] = i;
                m_mapDecorationRepository.RemoveMapDecoration(mapDecorationId);
            }
            else
                throw new KeyNotFoundException("No map decoration was found by the specified Id.");
        }
    }
}
