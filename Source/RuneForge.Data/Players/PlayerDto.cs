using System;

namespace RuneForge.Data.Players
{
    public class PlayerDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public PlayerColorDto Color { get; set; }
    }
}
