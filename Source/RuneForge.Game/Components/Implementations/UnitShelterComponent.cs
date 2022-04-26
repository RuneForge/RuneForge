using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using RuneForge.Game.Units;

namespace RuneForge.Game.Components.Implementations
{
    public class UnitShelterComponent : Component
    {
        private readonly List<Unit> m_occupants;

        public int OccupantsLimit { get; }

        public UnitShelterComponent(int occupantsLimit)
            : this(occupantsLimit, Array.Empty<Unit>())
        {
        }
        public UnitShelterComponent(int occupantsLimit, IList<Unit> units)
        {
            m_occupants = new List<Unit>(units);
            OccupantsLimit = occupantsLimit;
        }

        public ReadOnlyCollection<Unit> Occupants
        {
            get => m_occupants.AsReadOnly();
        }

        public void AddOccupant(Unit unit)
        {
            m_occupants.Add(unit);
        }
        public void RemoveOccupant(Unit unit)
        {
            m_occupants.Remove(unit);
        }
        public void ClearOccupants()
        {
            m_occupants.Clear();
        }
    }
}
