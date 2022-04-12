using System;

namespace RuneForge.Game.Maps
{
    [Flags]
    public enum MapCellTypes
    {
        Center = 0x0000,
        EdgeNorth = 0x0001,
        EdgeSouth = 0x0002,
        EdgeWest = 0x0004,
        EdgeEast = 0x0008,
        OuterCornerNorthWest = 0x0010,
        OuterCornerNorthEast = 0x0020,
        OuterCornerSouthWest = 0x0040,
        OuterCornerSouthEast = 0x0080,
        InnerCornerNorthWest = 0x0100,
        InnerCornerNorthEast = 0x0200,
        InnerCornerSouthWest = 0x0400,
        InnerCornerSouthEast = 0x0800,
    }
}
