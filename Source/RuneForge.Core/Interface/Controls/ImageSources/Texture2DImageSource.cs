using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.Interface.Controls.ImageSources
{
    public class Texture2DImageSource : ImageSource
    {
        private readonly Texture2D m_texture;

        public override int Width => m_texture.Width;

        public override int Height => m_texture.Height;

        public override Point Size => new Point(m_texture.Width, m_texture.Height);

        public Texture2DImageSource(Texture2D texture)
        {
            m_texture = texture ?? throw new ArgumentNullException("texture");
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            spriteBatch.Draw(m_texture, destinationRectangle, color);
        }
    }
}
