using System;
using System.Collections.Generic;

using RuneForge.Data.Components;

namespace RuneForge.Data.Players
{
    public class PlayerDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public PlayerColorDto Color { get; set; }

        public List<ComponentDto> Components { get; set; }
    }
}
