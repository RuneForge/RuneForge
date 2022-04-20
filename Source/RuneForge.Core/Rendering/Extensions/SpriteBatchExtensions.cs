using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.TextureAtlases;

namespace RuneForge.Core.Rendering.Extensions
{
    public static class SpriteBatchExtensions
    {
        public static void DrawTextureRegionUsingExternalTexture(
            this SpriteBatch spriteBatch,
            TextureRegion2D textureRegion,
            Texture2D externalTexture,
            Rectangle destinationRectangle,
            Color color
            )
        {
            spriteBatch.Draw(externalTexture, destinationRectangle, textureRegion.Bounds, color);
        }
    }
}
