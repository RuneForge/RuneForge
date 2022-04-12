using System;

namespace RuneForge.Data.Buildings
{
    public class BuildingDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }
    }
}
