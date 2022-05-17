using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Data.Units.Interfaces;

namespace RuneForge.Data.Units
{
    public class InMemoryUnitRepository : IUnitRepository
    {
        private readonly List<UnitDto> m_units;
        private readonly Dictionary<int, int> m_unitsByIds;

        public InMemoryUnitRepository()
        {
            m_units = new List<UnitDto>();
            m_unitsByIds = new Dictionary<int, int>();
        }

        public UnitDto GetUnit(int unitId)
        {
            if (m_unitsByIds.TryGetValue(unitId, out int unitIndex))
                return m_units[unitIndex];
            else
                throw new KeyNotFoundException("No unit was found by the specified Id.");
        }

        public ReadOnlyCollection<UnitDto> GetUnits()
        {
            return m_units.AsReadOnly();
        }

        public void AddUnit(UnitDto unit)
        {
            if (unit.Id == 0)
                throw new ArgumentException("Unable to add a unit with an empty Id.");
            if (!m_unitsByIds.ContainsKey(unit.Id))
            {
                m_units.Add(unit);
                m_unitsByIds.Add(unit.Id, m_units.Count - 1);
            }
            else
                throw new InvalidOperationException("Unable to add a unit with an existing Id.");
        }

        public void SaveUnit(UnitDto unit)
        {
            if (m_unitsByIds.TryGetValue(unit.Id, out int unitIndex))
                m_units[unitIndex] = unit;
            else
                throw new KeyNotFoundException("No unit was found by the specified Id.");
        }

        public void RemoveUnit(int unitId)
        {
            if (m_unitsByIds.TryGetValue(unitId, out int unitIndex))
            {
                m_units.RemoveAt(unitIndex);
                for (int i = unitIndex; i < m_units.Count; i++)
                    m_unitsByIds[m_units[i].Id] = i;
            }
            else
                throw new KeyNotFoundException("No unit was found by the specified Id.");
        }
    }
}
