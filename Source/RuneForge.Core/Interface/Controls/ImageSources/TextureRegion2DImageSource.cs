using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.TextureAtlases;

namespace RuneForge.Core.Interface.Controls.ImageSources
{
    public class TextureRegion2DImageSource : ImageSource
    {
        private readonly TextureRegion2D m_textureRegion;

        public override int Width => m_textureRegion.Width;

        public override int Height => m_textureRegion.Height;

        public override Point Size => m_textureRegion.Size;

        public TextureRegion2DImageSource(TextureRegion2D textureRegion)
        {
            m_textureRegion = textureRegion ?? throw new ArgumentNullException("textureRegion");
        }

        public static TextureRegion2DImageSource FromTextureAtlas(TextureAtlas textureAtlas, string textureRegionName)
        {
            if (textureAtlas == null)
            {
                throw new ArgumentNullException("textureAtlas");
            }
            if (textureRegionName == null)
            {
                throw new ArgumentNullException("textureRegionName");
            }
            return new TextureRegion2DImageSource(textureAtlas.TextureRegions[textureRegionName]);
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            spriteBatch.Draw(m_textureRegion, destinationRectangle, color);
        }
    }
}
