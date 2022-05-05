using System;

namespace RuneForge.Data.Components
{
    [Serializable]
    public class UnitShelterOccupantComponentDto : ComponentDto
    {
        public bool InsideShelter { get; set; }

        public TimeSpan TimeSinceEntering { get; set; }

        public int EnteredFromX { get; set; }
        public int EnteredFromY { get; set; }
    }
}
