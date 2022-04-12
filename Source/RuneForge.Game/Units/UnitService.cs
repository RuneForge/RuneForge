using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using AutoMapper;

using RuneForge.Data.Units;
using RuneForge.Data.Units.Interfaces;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Units
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository m_unitRepository;
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IMapper m_mapper;
        private readonly Dictionary<int, int> m_unitsByIds;

        public UnitService(IUnitRepository unitRepository, IGameSessionContext gameSessionContext, IMapper mapper)
        {
            m_unitRepository = unitRepository;
            m_gameSessionContext = gameSessionContext;
            m_mapper = mapper;
            m_unitsByIds = new Dictionary<int, int>();
        }

        public Unit GetUnit(int unitId)
        {
            if (m_unitsByIds.TryGetValue(unitId, out int unitIndex))
                return m_gameSessionContext.Units[unitIndex];
            else
                throw new KeyNotFoundException("No unit was found by the specified Id.");
        }

        public ReadOnlyCollection<Unit> GetUnits()
        {
            return new ReadOnlyCollection<Unit>(m_gameSessionContext.Units);
        }

        public void AddUnit(Unit unit)
        {
            if (unit.Id == 0)
                throw new ArgumentException("Unable to add a unit with an empty Id.");
            if (!m_unitsByIds.ContainsKey(unit.Id))
            {
                m_gameSessionContext.Units.Add(unit);
                m_unitsByIds.Add(unit.Id, m_gameSessionContext.Units.Count - 1);
                m_unitRepository.AddUnit(m_mapper.Map<Unit, UnitDto>(unit));
            }
            else
                throw new InvalidOperationException("Unable to add a unit with an existing Id.");
        }

        public void RemoveUnit(int unitId)
        {
            if (m_unitsByIds.TryGetValue(unitId, out int unitIndex))
            {
                m_gameSessionContext.Units.RemoveAt(unitIndex);
                for (int i = unitIndex; i < m_gameSessionContext.Units.Count; i++)
                    m_unitsByIds[m_gameSessionContext.Units[i].Id] = i;
                m_unitRepository.RemoveUnit(unitId);
            }
            else
                throw new KeyNotFoundException("No unit was found by the specified Id.");
        }
    }
}
