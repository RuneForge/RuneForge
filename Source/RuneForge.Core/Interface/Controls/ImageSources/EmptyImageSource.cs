using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.Interface.Controls.ImageSources
{
    public class EmptyImageSource : ImageSource
    {
        public override int Width { get; } = 0;

        public override int Height { get; } = 0;

        public override void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color)
        {
            throw new NotSupportedException();
        }
    }
}
