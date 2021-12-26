using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.TextureAtlases;

namespace RuneForge.Core.AnimationAtlases
{
    public class AnimationRegion2D : TextureRegion2D
    {
        public TimeSpan FrameTime { get; }

        public AnimationRegion2D(string name, Texture2D texture, TimeSpan frameTime)
            : this(name, texture, 0, 0, texture.Width, texture.Height, frameTime)
        {
        }
        public AnimationRegion2D(string name, Texture2D texture, int x, int y, int width, int height, TimeSpan frameTime)
            : base(name, texture, x, y, width, height)
        {
            FrameTime = frameTime;
        }
        public AnimationRegion2D(string name, Texture2D texture, Point location, Point size, TimeSpan frameTime)
            : this(name, texture, location.X, location.Y, size.X, size.Y, frameTime)
        {
        }
        public AnimationRegion2D(string name, Texture2D texture, Rectangle bounds, TimeSpan frameTime)
            : this(name, texture, bounds.X, bounds.Y, bounds.Width, bounds.Height, frameTime)
        {
        }
    }
}
