using System;

namespace RuneForge.Game.Buildings
{
    public class BuildingInstancePrototype
    {
        public Guid OwnerId { get; }

        public BuildingPrototype EntityPrototype { get; }

        public BuildingInstancePrototype(Guid ownerId, BuildingPrototype entityPrototype)
        {
            OwnerId = ownerId;
            EntityPrototype = entityPrototype;
        }
    }
}
