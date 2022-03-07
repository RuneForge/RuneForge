using System;

namespace RuneForge.Core.Interface.Controls
{
    [Flags]
    public enum Alignment
    {
        None = 0x00,
        Top = 0x01,
        Bottom = 0x02,
        Left = 0x04,
        Right = 0x08,
        TopLeft = 0x05,
        TopRight = 0x09,
        BottomLeft = 0x06,
        BottomRight = 0x0A,
        Center = 0x0F
    }
}
