using System.Collections.Generic;

namespace RuneForge.Data.Components
{
    public class UnitShelterComponentDto : ComponentDto
    {
        public int OccupantsLimit { get; set; }

        public List<int> OccupantIds { get; set; }
    }
}
