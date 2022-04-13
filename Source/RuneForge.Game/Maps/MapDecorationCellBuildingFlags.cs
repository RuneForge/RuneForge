using System;

namespace RuneForge.Game.Maps
{
    [Flags]
    public enum MapDecorationCellBuildingFlags
    {
        None = 0x00,
        LandBuildingBlocked = 0x01,
        CoastalBuildingBlocked = 0x02,
        WaterBuildingBlocked = 0x04,
    }
}
