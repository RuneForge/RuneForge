using System;

namespace RuneForge.Game.Maps
{
    [Flags]
    public enum Directions
    {
        None = 0x00,
        North = 0x01,
        South = 0x02,
        West = 0x04,
        East = 0x08,
        NorthWest = 0x10,
        NorthEast = 0x20,
        SouthWest = 0x40,
        SouthEast = 0x80,
    }
}
