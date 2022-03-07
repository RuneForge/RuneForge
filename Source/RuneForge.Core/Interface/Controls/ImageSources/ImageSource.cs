using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.Interface.Controls.ImageSources
{
    public abstract class ImageSource
    {
        public abstract int Width { get; }

        public abstract int Height { get; }

        public virtual Point Size => new Point(Width, Height);

        public abstract void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color);
    }
}
