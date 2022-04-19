namespace RuneForge.Game.Entities.Components
{
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
