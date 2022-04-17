using System;
using System.Collections.Generic;

using RuneForge.Data.Components;

namespace RuneForge.Data.Buildings
{
    public class BuildingDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public List<ComponentDto> Components { get; set; }
    }
}
