using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.TextureAtlases
{
    public class TextureAtlasReader : ContentTypeReader<TextureAtlas>
    {
        protected override TextureAtlas Read(ContentReader reader, TextureAtlas existingInstance)
        {
            string textureAtlasAssetName = reader.AssetName;

            string textureAssetName = reader.ReadString();
            Texture2D texture = reader.ContentManager.Load<Texture2D>(textureAssetName);

            int textureRegionsCount = reader.ReadInt32();
            Dictionary<string, TextureRegion2D> textureRegions = new Dictionary<string, TextureRegion2D>();
            for (int i = 0; i < textureRegionsCount; i++)
            {
                string textureRegionName = reader.ReadString();

                Rectangle textureRegionBounds = new Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());

                textureRegions.Add(textureRegionName, new TextureRegion2D(textureRegionName, texture, textureRegionBounds));
            }

            return new TextureAtlas(textureAtlasAssetName, texture, textureRegions);
        }
    }
}
