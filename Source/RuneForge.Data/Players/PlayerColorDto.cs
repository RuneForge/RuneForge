using System;

using Microsoft.Xna.Framework;

namespace RuneForge.Data.Players
{
    [Serializable]
    public class PlayerColorDto
    {
        public Color MainColor { get; set; }

        public Color EntityColorShadeA { get; set; }
        public Color EntityColorShadeB { get; set; }
        public Color EntityColorShadeC { get; set; }
        public Color EntityColorShadeD { get; set; }
    }
}
