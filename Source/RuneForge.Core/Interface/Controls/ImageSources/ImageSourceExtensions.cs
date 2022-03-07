using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.Interface.Controls.ImageSources
{
    public static class ImageSourceExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, ImageSource imageSource, Rectangle destinationRectangle, Color color)
        {
            //IL_0003: Unknown result type (might be due to invalid IL or missing references)
            //IL_0004: Unknown result type (might be due to invalid IL or missing references)
            imageSource.Draw(spriteBatch, destinationRectangle, color);
        }
    }
}
