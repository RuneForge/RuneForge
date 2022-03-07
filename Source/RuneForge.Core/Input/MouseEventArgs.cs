using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RuneForge.Core.Input
{
    public class MouseEventArgs : EventArgs
    {
        public int X { get; }

        public int Y { get; }

        public MouseButtons Button { get; }

        public int ScrollWheelValue { get; }

        public int HorizontalScrollWheelValue { get; }

        public MouseStateEx State { get; }

        public bool Handled { get; private set; }

        public Viewport Viewport { get; private set; }

        public Point Location => new Point(X, Y);

        public MouseEventArgs(int x, int y, MouseButtons button, int scrollWheelValue, int horizontalScrollWheelValue, MouseStateEx state)
        {
            //IL_0017: Unknown result type (might be due to invalid IL or missing references)
            //IL_0018: Unknown result type (might be due to invalid IL or missing references)
            //IL_0040: Unknown result type (might be due to invalid IL or missing references)
            //IL_0046: Unknown result type (might be due to invalid IL or missing references)
            X = x;
            Y = y;
            Button = button;
            ScrollWheelValue = scrollWheelValue;
            HorizontalScrollWheelValue = horizontalScrollWheelValue;
            State = state;
            Handled = false;
            Viewport = default(Viewport);
        }

        public void Handle()
        {
            Handled = true;
        }

        public void SetViewport(in Viewport viewport)
        {
            //IL_0003: Unknown result type (might be due to invalid IL or missing references)
            Viewport = viewport;
        }

        public Point GetLocationInsideViewport()
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_000d: Unknown result type (might be due to invalid IL or missing references)
            //IL_001d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0022: Unknown result type (might be due to invalid IL or missing references)
            //IL_002b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0030: Unknown result type (might be due to invalid IL or missing references)
            //IL_0033: Unknown result type (might be due to invalid IL or missing references)
            int x = X;
            Viewport viewport = Viewport;
            int num = x - viewport.X;
            int y = Y;
            viewport = Viewport;
            return new Point(num, y - viewport.Y);
        }

        public bool IsMouseInsideViewport()
        {
            //IL_0002: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            //IL_000b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0010: Unknown result type (might be due to invalid IL or missing references)
            //IL_0019: Unknown result type (might be due to invalid IL or missing references)
            //IL_001e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0027: Unknown result type (might be due to invalid IL or missing references)
            //IL_002c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0035: Unknown result type (might be due to invalid IL or missing references)
            //IL_003a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0049: Unknown result type (might be due to invalid IL or missing references)
            Point locationInsideViewport = GetLocationInsideViewport();
            Viewport viewport = Viewport;
            int x = viewport.X;
            viewport = Viewport;
            int y = viewport.Y;
            viewport = Viewport;
            int width = viewport.Width;
            viewport = Viewport;
            Rectangle val = default(Rectangle);
            val = new Rectangle(x, y, width, viewport.Height);
            return val.Contains(locationInsideViewport);
        }
    }
}
