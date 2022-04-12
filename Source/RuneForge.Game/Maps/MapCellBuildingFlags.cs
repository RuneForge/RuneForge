using System;

namespace RuneForge.Game.Maps
{
    [Flags]
    public enum MapCellBuildingFlags
    {
        None = 0x00,
        LandBuildingAllowed = 0x01,
        CoastalBuildingAllowed = 0x02,
        WaterBuildingAllowed = 0x04,
    }
}
