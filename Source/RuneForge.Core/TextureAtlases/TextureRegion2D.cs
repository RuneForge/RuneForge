using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.TextureAtlases
{
    public class TextureRegion2D
    {
        public string Name { get; }

        public Texture2D Texture { get; }

        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public TextureRegion2D(string name, Texture2D texture)
            : this(name, texture, 0, 0, texture.Width, texture.Height)
        {
        }
        public TextureRegion2D(string name, Texture2D texture, int x, int y, int width, int height)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            X = x >= 0 && x < texture.Width ? x : throw new ArgumentException("X location component is outside of the texture bounds.");
            Y = y >= 0 && y < texture.Height ? y : throw new ArgumentException("Y location component is outside of the texture bounds.");
            Width = (width >= 0 && width <= texture.Width)
                ? width : throw new ArgumentException("Texture region's width is either negative or greater than width of the texture.");
            Height = (height >= 0 && height <= texture.Height)
                ? height : throw new ArgumentException("Texture region's height is either negative or greater than height of the texture.");
        }
        public TextureRegion2D(string name, Texture2D texture, Point location, Point size)
            : this(name, texture, location.X, location.Y, size.X, size.Y)
        {
        }
        public TextureRegion2D(string name, Texture2D texture, Rectangle bounds)
            : this(name, texture, bounds.X, bounds.Y, bounds.Width, bounds.Height)
        {
        }

        public Point Location => new Point(X, Y);
        public Point Size => new Point(Width, Height);
        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);
    }
}
