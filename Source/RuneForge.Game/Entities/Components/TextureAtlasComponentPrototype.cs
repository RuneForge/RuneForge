using RuneForge.Game.Entities.Attributes;
using RuneForge.Game.Entities.ComponentReaders;

namespace RuneForge.Game.Entities.Components
{
    [ComponentPrototypeReader(typeof(TextureAtlasComponentPrototypeReader))]
    public class TextureAtlasComponentPrototype : ComponentPrototype
    {
        public string TextureAtlasAssetName { get; }

        public bool HasPlayerColor { get; }

        public TextureAtlasComponentPrototype(string textureAtlasAssetName, bool hasPlayerColor)
        {
            TextureAtlasAssetName = textureAtlasAssetName;
            HasPlayerColor = hasPlayerColor;
        }
    }
}
