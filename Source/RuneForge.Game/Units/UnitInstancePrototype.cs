using System;

namespace RuneForge.Game.Units
{
    public class UnitInstancePrototype
    {
        public Guid OwnerId { get; }

        public UnitPrototype EntityPrototype { get; }

        public UnitInstancePrototype(Guid ownerId, UnitPrototype entityPrototype)
        {
            OwnerId = ownerId;
            EntityPrototype = entityPrototype;
        }
    }
}
