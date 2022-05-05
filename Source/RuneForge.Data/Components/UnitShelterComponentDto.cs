using System;
using System.Collections.Generic;

namespace RuneForge.Data.Components
{
    [Serializable]
    public class UnitShelterComponentDto : ComponentDto
    {
        public int OccupantsLimit { get; set; }

        public List<int> OccupantIds { get; set; }
    }
}
