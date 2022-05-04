using System;

namespace RuneForge.Data.Components
{
    [Serializable]
    public class LocationComponentDto : ComponentDto
    {
        public float X { get; set; }
        public float Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
