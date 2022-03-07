using System;

namespace RuneForge.Core.Input
{
    [Flags]
    public enum ModifierKeys
    {
        None = 0x00,
        Alt = 0x01,
        Control = 0x02,
        Shift = 0x04,
    }
}
