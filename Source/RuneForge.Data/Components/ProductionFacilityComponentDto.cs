using System;
using System.Collections.Generic;

namespace RuneForge.Data.Components
{
    [Serializable]
    public class ProductionFacilityComponentDto : ComponentDto
    {
        public List<string> UnitCodesProduced { get; set; }

        public string UnitCodeCurrentlyProduced { get; set; }

        public TimeSpan TimeElapsed { get; set; }

        public bool ProductionFinished { get; set; }
    }
}
