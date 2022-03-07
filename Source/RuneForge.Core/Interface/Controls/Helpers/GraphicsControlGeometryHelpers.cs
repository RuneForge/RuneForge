using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.Interface.Controls.Helpers
{
    internal static class GraphicsControlGeometryHelpers
    {
        public static Viewport CreateChildViewport(in Viewport viewport, int x, int y, int width, int height)
        {
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            //IL_000c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0011: Unknown result type (might be due to invalid IL or missing references)
            //IL_0014: Unknown result type (might be due to invalid IL or missing references)
            return CreateChildViewport(in viewport, new Rectangle(x, y, width, height));
        }

        public static Viewport CreateChildViewport(in Viewport viewport, Rectangle rectangle)
        {
            //IL_0002: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            //IL_0010: Unknown result type (might be due to invalid IL or missing references)
            //IL_0019: Unknown result type (might be due to invalid IL or missing references)
            //IL_001e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0027: Unknown result type (might be due to invalid IL or missing references)
            //IL_002f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0036: Unknown result type (might be due to invalid IL or missing references)
            //IL_003b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0044: Unknown result type (might be due to invalid IL or missing references)
            //IL_0051: Unknown result type (might be due to invalid IL or missing references)
            //IL_0058: Unknown result type (might be due to invalid IL or missing references)
            //IL_005d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0066: Unknown result type (might be due to invalid IL or missing references)
            //IL_0078: Unknown result type (might be due to invalid IL or missing references)
            //IL_007d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0087: Unknown result type (might be due to invalid IL or missing references)
            //IL_008c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0095: Unknown result type (might be due to invalid IL or missing references)
            //IL_009a: Unknown result type (might be due to invalid IL or missing references)
            //IL_009e: Unknown result type (might be due to invalid IL or missing references)
            Viewport val = viewport;
            int num = val.X + rectangle.X;
            val = viewport;
            int num2 = val.Y + rectangle.Y;
            int width = rectangle.Width;
            val = viewport;
            int num3 = Math.Min(width, val.Width - rectangle.X);
            int height = rectangle.Height;
            val = viewport;
            int num4 = Math.Min(height, val.Height - rectangle.Y);
            val = viewport;
            float minDepth = val.MinDepth;
            val = viewport;
            return new Viewport(num, num2, num3, num4, minDepth, val.MaxDepth);
        }

        public static void CalculateLocationInsideRectangle(Alignment alignment, Point size, in Rectangle rectangle, out Point location)
        {
            //IL_002e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0050: Unknown result type (might be due to invalid IL or missing references)
            //IL_0093: Unknown result type (might be due to invalid IL or missing references)
            //IL_00b5: Unknown result type (might be due to invalid IL or missing references)
            Alignment alignment2 = alignment & (Alignment.Top | Alignment.Bottom);
            Alignment alignment3 = alignment & (Alignment.Left | Alignment.Right);
            location = default(Point);
            if (1 == 0)
            {
            }
            int y = alignment2 switch
            {
                Alignment.Top => 0,
                Alignment.Bottom => rectangle.Height - size.Y,
                Alignment.Top | Alignment.Bottom => (int)((1f * rectangle.Height / 2f) - (1f * size.Y / 2f)),
                _ => 0,
            };
            if (1 == 0)
            {
            }
            location.Y = y;
            if (1 == 0)
            {
            }
            y = alignment3 switch
            {
                Alignment.Left => 0,
                Alignment.Right => rectangle.Width - size.X,
                Alignment.Left | Alignment.Right => (int)((1f * rectangle.Width / 2f) - (1f * size.X / 2f)),
                _ => 0,
            };
            if (1 == 0)
            {
            }
            location.X = y;
        }

        public static void CalculateLocationInsideRectangle(Alignment alignment, Vector2 size, in Rectangle rectangle, out Vector2 location)
        {
            //IL_0033: Unknown result type (might be due to invalid IL or missing references)
            //IL_0050: Unknown result type (might be due to invalid IL or missing references)
            //IL_0099: Unknown result type (might be due to invalid IL or missing references)
            //IL_00b6: Unknown result type (might be due to invalid IL or missing references)
            Alignment alignment2 = alignment & (Alignment.Top | Alignment.Bottom);
            Alignment alignment3 = alignment & (Alignment.Left | Alignment.Right);
            if (1 == 0)
            {
            }
            float y = alignment2 switch
            {
                Alignment.Top => 0f,
                Alignment.Bottom => rectangle.Height - size.Y,
                Alignment.Top | Alignment.Bottom => (1f * rectangle.Height / 2f) - (size.Y / 2f),
                _ => 0f,
            };
            if (1 == 0)
            {
            }
            location.Y = y;
            if (1 == 0)
            {
            }
            y = alignment3 switch
            {
                Alignment.Left => 0f,
                Alignment.Right => rectangle.Width - size.X,
                Alignment.Left | Alignment.Right => (1f * rectangle.Width / 2f) - (size.X / 2f),
                _ => 0f,
            };
            if (1 == 0)
            {
            }
            location.X = y;
        }
    }
}
