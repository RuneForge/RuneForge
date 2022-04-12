using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Data.Buildings.Interfaces;

namespace RuneForge.Data.Buildings
{
    public class InMemoryBuildingRepository : IBuildingRepository
    {
        private readonly List<BuildingDto> m_buildings;
        private readonly Dictionary<int, int> m_buildingsByIds;

        public InMemoryBuildingRepository()
        {
            m_buildings = new List<BuildingDto>();
            m_buildingsByIds = new Dictionary<int, int>();
        }

        public BuildingDto GetBuilding(int buildingId)
        {
            if (m_buildingsByIds.TryGetValue(buildingId, out int buildingIndex))
                return m_buildings[buildingIndex];
            else
                throw new KeyNotFoundException("No building was found by the specified Id.");
        }

        public ReadOnlyCollection<BuildingDto> GetBuildings()
        {
            return m_buildings.AsReadOnly();
        }

        public void AddBuilding(BuildingDto building)
        {
            if (building.Id == 0)
                throw new ArgumentException("Unable to add a building with an empty Id.");
            if (!m_buildingsByIds.ContainsKey(building.Id))
            {
                m_buildings.Add(building);
                m_buildingsByIds.Add(building.Id, m_buildings.Count - 1);
            }
            else
                throw new InvalidOperationException("Unable to add a building with an existing Id.");
        }

        public void SaveBuilding(BuildingDto building)
        {
            if (m_buildingsByIds.TryGetValue(building.Id, out int buildingIndex))
                m_buildings[buildingIndex] = building;
            else
                throw new KeyNotFoundException("No building was found by the specified Id.");
        }

        public void RemoveBuilding(int buildingId)
        {
            if (m_buildingsByIds.TryGetValue(buildingId, out int buildingIndex))
            {
                m_buildings.RemoveAt(buildingIndex);
                for (int i = buildingIndex; i < m_buildings.Count; i++)
                    m_buildingsByIds[m_buildings[i].Id] = i;
            }
            else
                throw new KeyNotFoundException("No building was found by the specified Id.");
        }
    }
}
