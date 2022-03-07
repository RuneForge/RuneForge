using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.TextureAtlases;

namespace RuneForge.Core.Interface.Controls.Helpers
{
    internal static class GraphicsControlDrawingHelpers
    {
        public static void DrawRectangle(this SpriteBatch spriteBatch, TextureRegion2DProvider textureRegionProvider, Rectangle rectangle, Color color)
        {
            spriteBatch.DrawRectangleInternal(rectangle, textureRegionProvider, null, color);
        }

        public static void DrawFilledRectangle(this SpriteBatch spriteBatch, TextureRegion2DProvider factoryMethod, TextureRegion2D backgroundTextureRegion, Rectangle rectangle, Color color)
        {
            spriteBatch.DrawRectangleInternal(rectangle, factoryMethod, backgroundTextureRegion, color);
        }

        private static void DrawRectangleInternal(this SpriteBatch spriteBatch, Rectangle rectangle, TextureRegion2DProvider textureRegionProvider, TextureRegion2D backgroundTextureRegion, Color color)
        {
            Vector4 frameWidth = CalculateFrameWidth(textureRegionProvider);
            spriteBatch.Draw(textureRegionProvider(Alignment.TopLeft), GetCornerRectangle(Alignment.TopLeft, rectangle, frameWidth), color);
            spriteBatch.Draw(textureRegionProvider(Alignment.TopRight), GetCornerRectangle(Alignment.TopRight, rectangle, frameWidth), color);
            spriteBatch.Draw(textureRegionProvider(Alignment.BottomLeft), GetCornerRectangle(Alignment.BottomLeft, rectangle, frameWidth), color);
            spriteBatch.Draw(textureRegionProvider(Alignment.BottomRight), GetCornerRectangle(Alignment.BottomRight, rectangle, frameWidth), color);
            spriteBatch.DrawHorizontalLine(textureRegionProvider(Alignment.Top), GetSideRectangle(Alignment.Top, rectangle, frameWidth), color);
            spriteBatch.DrawHorizontalLine(textureRegionProvider(Alignment.Bottom), GetSideRectangle(Alignment.Bottom, rectangle, frameWidth), color);
            spriteBatch.DrawVerticalLine(textureRegionProvider(Alignment.Left), GetSideRectangle(Alignment.Left, rectangle, frameWidth), color);
            spriteBatch.DrawVerticalLine(textureRegionProvider(Alignment.Right), GetSideRectangle(Alignment.Right, rectangle, frameWidth), color);
            if (backgroundTextureRegion != null)
            {
                Rectangle rectangle2 = new Rectangle(rectangle.X + (int)frameWidth.Y, rectangle.Y + (int)frameWidth.W, rectangle.Width - (int)frameWidth.Y - (int)frameWidth.Z, rectangle.Height - (int)frameWidth.W - (int)frameWidth.X);
                spriteBatch.FillRectangle(backgroundTextureRegion, rectangle2, color);
            }
        }

        private static void DrawHorizontalLine(this SpriteBatch spriteBatch, TextureRegion2D textureRegion, Rectangle rectangle, Color color)
        {
            for (int i = 0; i < rectangle.Width; i += textureRegion.Width)
            {
                int x = Math.Min(rectangle.Width - i, textureRegion.Width);
                spriteBatch.Draw(textureRegion, new Rectangle(new Point(rectangle.X + i, rectangle.Y), new Point(x, textureRegion.Height)), color);
            }
        }

        private static void DrawVerticalLine(this SpriteBatch spriteBatch, TextureRegion2D textureRegion, Rectangle rectangle, Color color)
        {
            for (int i = 0; i < rectangle.Height; i += textureRegion.Height)
            {
                int y = Math.Min(rectangle.Height - i, textureRegion.Height);
                spriteBatch.Draw(textureRegion, new Rectangle(new Point(rectangle.X, rectangle.Y + i), new Point(textureRegion.Width, y)), color);
            }
        }

        private static void FillRectangle(this SpriteBatch spriteBatch, TextureRegion2D textureRegion, Rectangle rectangle, Color color)
        {
            for (int i = 0; i < rectangle.Width; i += textureRegion.Width)
            {
                for (int j = 0; j < rectangle.Height; j += textureRegion.Height)
                {
                    int x = Math.Min(rectangle.Width - i, textureRegion.Width);
                    int y = Math.Min(rectangle.Height - j, textureRegion.Height);
                    spriteBatch.Draw(textureRegion, new Rectangle(new Point(rectangle.X + i, rectangle.Y + j), new Point(x, y)), color);
                }
            }
        }

        private static Vector4 CalculateFrameWidth(TextureRegion2DProvider textureRegionProvider)
        {
            Vector4 result = default(Vector4);
            result.W = textureRegionProvider(Alignment.Top).Height;
            result.X = textureRegionProvider(Alignment.Bottom).Height;
            result.Y = textureRegionProvider(Alignment.Left).Width;
            result.Z = textureRegionProvider(Alignment.Right).Width;
            return result;
        }

        private static Rectangle GetCornerRectangle(Alignment alignment, Rectangle rectangle, Vector4 frameWidth)
        {
            if (1 == 0)
            {
            }
            Point point = alignment switch
            {
                Alignment.TopLeft => new Point(rectangle.X, rectangle.Y),
                Alignment.TopRight => new Point(rectangle.X + rectangle.Width - (int)frameWidth.Z, rectangle.Y),
                Alignment.BottomLeft => new Point(rectangle.X, rectangle.Y + rectangle.Height - (int)frameWidth.X),
                Alignment.BottomRight => new Point(rectangle.X + rectangle.Width - (int)frameWidth.Z, rectangle.Y + rectangle.Height - (int)frameWidth.X),
                _ => Point.Zero,
            };
            if (1 == 0)
            {
            }
            Point location = point;
            if (1 == 0)
            {
            }
            point = alignment switch
            {
                Alignment.TopLeft => new Point((int)frameWidth.Y, (int)frameWidth.W),
                Alignment.TopRight => new Point((int)frameWidth.Z, (int)frameWidth.W),
                Alignment.BottomLeft => new Point((int)frameWidth.Y, (int)frameWidth.X),
                Alignment.BottomRight => new Point((int)frameWidth.Z, (int)frameWidth.X),
                _ => Point.Zero,
            };
            if (1 == 0)
            {
            }
            Point size = point;
            return new Rectangle(location, size);
        }

        private static Rectangle GetSideRectangle(Alignment alignment, Rectangle rectangle, Vector4 frameWidth)
        {
            if (1 == 0)
            {
            }
            Point point = alignment switch
            {
                Alignment.Top => new Point(rectangle.X + (int)frameWidth.Y, rectangle.Y),
                Alignment.Bottom => new Point(rectangle.X + (int)frameWidth.Y, rectangle.Y + rectangle.Height - (int)frameWidth.X),
                Alignment.Left => new Point(rectangle.X, rectangle.Y + (int)frameWidth.W),
                Alignment.Right => new Point(rectangle.X + rectangle.Width - (int)frameWidth.Z, rectangle.Y + (int)frameWidth.W),
                _ => Point.Zero,
            };
            if (1 == 0)
            {
            }
            Point location = point;
            if (1 == 0)
            {
            }
            point = alignment switch
            {
                Alignment.Top => new Point(rectangle.Width - (int)frameWidth.Y - (int)frameWidth.Z, (int)frameWidth.W),
                Alignment.Bottom => new Point(rectangle.Width - (int)frameWidth.Y - (int)frameWidth.Z, (int)frameWidth.X),
                Alignment.Left => new Point((int)frameWidth.Y, rectangle.Height - (int)frameWidth.W - (int)frameWidth.X),
                Alignment.Right => new Point((int)frameWidth.Z, rectangle.Height - (int)frameWidth.W - (int)frameWidth.X),
                _ => Point.Zero,
            };
            if (1 == 0)
            {
            }
            Point size = point;
            return new Rectangle(location, size);
        }
    }
}
