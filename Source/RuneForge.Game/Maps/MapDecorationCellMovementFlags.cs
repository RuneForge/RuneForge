using System;

namespace RuneForge.Game.Maps
{
    [Flags]
    public enum MapDecorationCellMovementFlags
    {
        None = 0x00,
        LandMovementBlocked = 0x01,
        CoastalMovementBlocked = 0x02,
        WaterMovementBlocked = 0x04,
        AirMovementBlocked = 0x08,
    }
}
