using System;

using RuneForge.Core.TextureAtlases;

namespace RuneForge.Core.Interface.Controls.Helpers
{
    internal static class GraphicsControlHelpers
    {
        public static TextureRegion2D GetTextureRegion(this TextureAtlas textureAtlas, string textureRegionName)
        {
            return textureAtlas.TextureRegions[textureRegionName];
        }

        public static TextureRegion2D GetTextureRegion(this TextureAtlas textureAtlas, string textureRegionNameTemplate, Alignment alignment)
        {
            string textureRegionName = string.Format(textureRegionNameTemplate, alignment);
            return GetTextureRegion(textureAtlas, textureRegionName);
        }

        public static TextureRegion2D GetFrameTextureRegion(this TextureAtlas textureAtlas, string sideTextureRegionNameTemplate, string cornerTextureRegionNameTemplate, Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.Top:
                case Alignment.Bottom:
                case Alignment.Left:
                case Alignment.Right:
                    return GetTextureRegion(textureAtlas, sideTextureRegionNameTemplate, alignment);
                case Alignment.TopLeft:
                case Alignment.BottomLeft:
                case Alignment.TopRight:
                case Alignment.BottomRight:
                    return GetTextureRegion(textureAtlas, cornerTextureRegionNameTemplate, alignment);
                default:
                    throw new Exception("The requested texture region does not represent a part of a frame.");
            }
        }
    }
}
