using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.TextureAtlases
{
    public static class TextureRegion2DExtensions
    {
        public static void Draw(
            this SpriteBatch spriteBatch,
            TextureRegion2D textureRegion,
            Rectangle destinationRectangle,
            Color color
            )
        {
            spriteBatch.Draw(textureRegion.Texture, destinationRectangle, textureRegion.Bounds, color);
        }

        public static void Draw(
            this SpriteBatch spriteBatch,
            TextureRegion2D textureRegion,
            Rectangle destinationRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            SpriteEffects effects,
            float layerDepth
            )
        {
            spriteBatch.Draw(textureRegion.Texture, destinationRectangle, textureRegion.Bounds, color, rotation, origin, effects, layerDepth);
        }

        public static void Draw(
            this SpriteBatch spriteBatch,
            TextureRegion2D textureRegion,
            Vector2 location,
            Color color
            )
        {
            spriteBatch.Draw(textureRegion.Texture, location, textureRegion.Bounds, color);
        }

        public static void Draw(
            this SpriteBatch spriteBatch,
            TextureRegion2D textureRegion,
            Vector2 location,
            Color color,
            float rotation,
            Vector2 origin,
            float scale,
            SpriteEffects effects,
            float layerDepth
            )
        {
            spriteBatch.Draw(textureRegion.Texture, location, textureRegion.Bounds, color, rotation, origin, scale, effects, layerDepth);
        }

        public static void Draw(
            this SpriteBatch spriteBatch,
            TextureRegion2D textureRegion,
            Vector2 location,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth
            )
        {
            spriteBatch.Draw(textureRegion.Texture, location, textureRegion.Bounds, color, rotation, origin, scale, effects, layerDepth);
        }
    }
}
