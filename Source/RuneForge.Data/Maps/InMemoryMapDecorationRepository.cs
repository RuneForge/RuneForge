using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Data.Maps.Interfaces;

namespace RuneForge.Data.Maps
{
    public class InMemoryMapDecorationRepository : IMapDecorationRepository
    {
        private readonly List<MapDecorationDto> m_mapDecorations;
        private readonly Dictionary<int, int> m_mapDecorationsByIds;

        public InMemoryMapDecorationRepository()
        {
            m_mapDecorations = new List<MapDecorationDto>();
            m_mapDecorationsByIds = new Dictionary<int, int>();
        }

        public MapDecorationDto GetMapDecoration(int mapDecorationId)
        {
            if (m_mapDecorationsByIds.TryGetValue(mapDecorationId, out int mapDecorationIndex))
                return m_mapDecorations[mapDecorationIndex];
            else
                throw new KeyNotFoundException("No map decoration was found by the specified Id.");
        }

        public ReadOnlyCollection<MapDecorationDto> GetMapDecorations()
        {
            return m_mapDecorations.AsReadOnly();
        }

        public void AddMapDecoration(MapDecorationDto mapDecoration)
        {
            if (mapDecoration.Id == 0)
                throw new ArgumentException("Unable to add a map decoration with an empty Id.");
            if (!m_mapDecorationsByIds.ContainsKey(mapDecoration.Id))
            {
                m_mapDecorations.Add(mapDecoration);
                m_mapDecorationsByIds.Add(mapDecoration.Id, m_mapDecorations.Count - 1);
            }
            else
                throw new InvalidOperationException("Unable to add a map decoration with an existing Id.");
        }

        public void SaveMapDecoration(MapDecorationDto mapDecoration)
        {
            if (m_mapDecorationsByIds.TryGetValue(mapDecoration.Id, out int mapDecorationIndex))
                m_mapDecorations[mapDecorationIndex] = mapDecoration;
            else
                throw new KeyNotFoundException("No map decoration was found by the specified Id.");
        }

        public void RemoveMapDecoration(int mapDecorationId)
        {
            if (m_mapDecorationsByIds.TryGetValue(mapDecorationId, out int mapDecorationIndex))
            {
                m_mapDecorations.RemoveAt(mapDecorationIndex);
                for (int i = mapDecorationIndex; i < m_mapDecorations.Count; i++)
                    m_mapDecorationsByIds[m_mapDecorations[i].Id] = i;
            }
            else
                throw new KeyNotFoundException("No map decoration was found by the specified Id.");
        }
    }
}
