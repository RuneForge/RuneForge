using System;

namespace RuneForge.Game.Components.Entities
{
    [Flags]
    public enum MovementFlags
    {
        None = 0x00,
        LandMovementRequired = 0x01,
        CoastalMovementRequired = 0x02,
        WaterMovementRequired = 0x04,
        AirMovementRequired = 0x08,
    }
}
