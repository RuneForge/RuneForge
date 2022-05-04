using System;

namespace RuneForge.Data.Players
{
    [Serializable]
    public class PlayerColorDto
    {
        public uint MainColorPacked { get; set; }

        public uint EntityColorShadeAPacked { get; set; }
        public uint EntityColorShadeBPacked { get; set; }
        public uint EntityColorShadeCPacked { get; set; }
        public uint EntityColorShadeDPacked { get; set; }
    }
}
