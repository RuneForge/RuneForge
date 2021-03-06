using System;

namespace RuneForge.Data.Maps
{
    [Serializable]
    public class MapDecorationDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
