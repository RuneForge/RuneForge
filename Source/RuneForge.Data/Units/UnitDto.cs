using System;
using System.Collections.Generic;

using RuneForge.Data.Components;

namespace RuneForge.Data.Units
{
    public class UnitDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public List<ComponentDto> Components { get; set; }
    }
}
