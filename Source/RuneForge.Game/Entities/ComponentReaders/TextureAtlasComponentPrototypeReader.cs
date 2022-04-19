using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Entities.Components;

namespace RuneForge.Game.Entities.ComponentReaders
{
    public class TextureAtlasComponentPrototypeReader : ComponentPrototypeReader<TextureAtlasComponentPrototype>
    {
        public override TextureAtlasComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            string textureAtlasAssetName = contentReader.ReadString();
            bool hasPlayerColor = contentReader.ReadBoolean();
            return new TextureAtlasComponentPrototype(textureAtlasAssetName, hasPlayerColor);
        }
    }
}
