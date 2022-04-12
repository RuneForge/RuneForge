using System;

namespace RuneForge.Data.Units
{
    public class UnitDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }
    }
}
