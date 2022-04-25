using System;

using Microsoft.Xna.Framework;

namespace RuneForge.Game.Components.Implementations
{
    public class UnitShelterOccupantComponent : Component
    {
        public bool InsideShelter { get; set; }

        public TimeSpan TimeSinceEntering { get; set; }

        public int EnteredFromX { get; set; }
        public int EnteredFromY { get; set; }

        public UnitShelterOccupantComponent()
        {
            InsideShelter = false;
            TimeSinceEntering = TimeSpan.Zero;
            EnteredFromX = 0;
            EnteredFromY = 0;
        }

        public Point EnteredFrom
        {
            get => new Point(EnteredFromX, EnteredFromY);
            set => (EnteredFromX, EnteredFromY) = value;
        }
    }
}
