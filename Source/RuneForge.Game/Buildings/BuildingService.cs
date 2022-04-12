using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using AutoMapper;

using RuneForge.Data.Buildings;
using RuneForge.Data.Buildings.Interfaces;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.GameSessions.Interfaces;

namespace RuneForge.Game.Buildings
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository m_buildingRepository;
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IMapper m_mapper;
        private readonly Dictionary<int, int> m_buildingsByIds;

        public BuildingService(IBuildingRepository buildingRepository, IGameSessionContext gameSessionContext, IMapper mapper)
        {
            m_buildingRepository = buildingRepository;
            m_gameSessionContext = gameSessionContext;
            m_mapper = mapper;
            m_buildingsByIds = new Dictionary<int, int>();
        }

        public Building GetBuilding(int buildingId)
        {
            if (m_buildingsByIds.TryGetValue(buildingId, out int buildingIndex))
                return m_gameSessionContext.Buildings[buildingIndex];
            else
                throw new KeyNotFoundException("No building was found by the specified Id.");
        }

        public ReadOnlyCollection<Building> GetBuildings()
        {
            return new ReadOnlyCollection<Building>(m_gameSessionContext.Buildings);
        }

        public void AddBuilding(Building building)
        {
            if (building.Id == 0)
                throw new ArgumentException("Unable to add a building with an empty Id.");
            if (!m_buildingsByIds.ContainsKey(building.Id))
            {
                m_gameSessionContext.Buildings.Add(building);
                m_buildingsByIds.Add(building.Id, m_gameSessionContext.Buildings.Count - 1);
                m_buildingRepository.AddBuilding(m_mapper.Map<Building, BuildingDto>(building));
            }
            else
                throw new InvalidOperationException("Unable to add a building with an existing Id.");
        }

        public void RemoveBuilding(int buildingId)
        {
            if (m_buildingsByIds.TryGetValue(buildingId, out int buildingIndex))
            {
                m_gameSessionContext.Buildings.RemoveAt(buildingIndex);
                for (int i = buildingIndex; i < m_gameSessionContext.Buildings.Count; i++)
                    m_buildingsByIds[m_gameSessionContext.Buildings[i].Id] = i;
                m_buildingRepository.RemoveBuilding(buildingId);
            }
            else
                throw new KeyNotFoundException("No building was found by the specified Id.");
        }
    }
}
