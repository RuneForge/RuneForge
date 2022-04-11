using Microsoft.Xna.Framework;

namespace RuneForge.Game.Players
{
    public struct PlayerColor
    {
        public Color MainColor { get; }

        public Color EntityColorShadeA { get; }
        public Color EntityColorShadeB { get; }
        public Color EntityColorShadeC { get; }
        public Color EntityColorShadeD { get; }

        public PlayerColor(Color mainColor, Color entityColorShadeA, Color entityColorShadeB, Color entityColorShadeC, Color entityColorShadeD)
        {
            MainColor = mainColor;
            EntityColorShadeA = entityColorShadeA;
            EntityColorShadeB = entityColorShadeB;
            EntityColorShadeC = entityColorShadeC;
            EntityColorShadeD = entityColorShadeD;
        }
    }
}
