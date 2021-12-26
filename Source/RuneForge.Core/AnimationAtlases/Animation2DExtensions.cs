using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.AnimationAtlases
{
    public static class Animation2DExtensions
    {
        public static void Draw(
            this SpriteBatch spriteBatch,
            Animation2D animation,
            Rectangle destinationRectangle,
            Color color
            )
        {
            AnimationRegion2D animationRegion = animation.GetCurrentAnimationRegion();
            spriteBatch.Draw(animationRegion.Texture, destinationRectangle, animationRegion.Bounds, color);
        }

        public static void Draw(
            this SpriteBatch spriteBatch,
            Animation2D animation,
            Rectangle destinationRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            SpriteEffects effects,
            float layerDepth
            )
        {
            AnimationRegion2D animationRegion = animation.GetCurrentAnimationRegion();
            spriteBatch.Draw(animationRegion.Texture, destinationRectangle, animationRegion.Bounds, color, rotation, origin, effects, layerDepth);
        }

        public static void Draw(
            this SpriteBatch spriteBatch,
            Animation2D animation,
            Vector2 location,
            Color color
            )
        {
            AnimationRegion2D animationRegion = animation.GetCurrentAnimationRegion();
            spriteBatch.Draw(animationRegion.Texture, location, animationRegion.Bounds, color);
        }

        public static void Draw(
            this SpriteBatch spriteBatch,
            Animation2D animation,
            Vector2 location,
            Color color,
            float rotation,
            Vector2 origin,
            float scale,
            SpriteEffects effects,
            float layerDepth
            )
        {
            AnimationRegion2D animationRegion = animation.GetCurrentAnimationRegion();
            spriteBatch.Draw(animationRegion.Texture, location, animationRegion.Bounds, color, rotation, origin, scale, effects, layerDepth);
        }

        public static void Draw(
            this SpriteBatch spriteBatch,
            Animation2D animation,
            Vector2 location,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth
            )
        {
            AnimationRegion2D animationRegion = animation.GetCurrentAnimationRegion();
            spriteBatch.Draw(animationRegion.Texture, location, animationRegion.Bounds, color, rotation, origin, scale, effects, layerDepth);
        }
    }
}
