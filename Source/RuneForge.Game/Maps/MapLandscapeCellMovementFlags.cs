using System;

namespace RuneForge.Game.Maps
{
    [Flags]
    public enum MapLandscapeCellMovementFlags
    {
        None = 0x00,
        LandMovementAllowed = 0x01,
        CoastalMovementAllowed = 0x02,
        WaterMovementAllowed = 0x04,
        AirMovementAllowed = 0x08,
    }
}
