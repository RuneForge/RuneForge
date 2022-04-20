using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Factories;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentFactory(typeof(TextureAtlasComponentFactory))]
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
