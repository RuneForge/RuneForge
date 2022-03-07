using System;
using System.Collections.Generic;

using RuneForge.Core.TextureAtlases;

namespace RuneForge.Core.Interface.Controls.Helpers
{
    internal static class GraphicsControlValidationHelpers
    {
        public static void ValidateGeometry(this TextureAtlas textureAtlas, RectangularFrameValidationData validationData)
        {
            (Alignment, Alignment)[] validationPairs = validationData.ValidationPairs;
            for (int i = 0; i < validationPairs.Length; i++)
            {
                (Alignment, Alignment) tuple = validationPairs[i];
                Alignment item = tuple.Item1;
                Alignment item2 = tuple.Item2;
                TextureRegion2D textureRegion = textureAtlas.GetTextureRegion(validationData.SideTextureRegionNameTemplate, item);
                TextureRegion2D textureRegion2 = textureAtlas.GetTextureRegion(validationData.CornerTextureRegionNameTemplate, item2);
                ThrowIfFalse(GetTextureRegionLength(textureRegion, item) == GetTextureRegionLength(textureRegion2, item));
            }
        }

        private static int GetTextureRegionLength(TextureRegion2D textureRegion, Alignment side)
        {
            if (side == Alignment.Top || side == Alignment.Bottom)
            {
                return textureRegion.Height;
            }
            if (side == Alignment.Left || side == Alignment.Right)
            {
                return textureRegion.Width;
            }
            return 0;
        }

        private static void ThrowIfFalse(bool condition)
        {
            if (!condition)
            {
                throw new Exception("One of the texture atlas validation rules failed. Check content related to graphics user interface.");
            }
        }
    }
}
