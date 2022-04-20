using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
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
