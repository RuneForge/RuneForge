namespace RuneForge.Game.Entities.Components
{
    public class TextureAtlasComponent : Component
    {
        public string TextureAtlasAssetName { get; }

        public bool HasPlayerColor { get; }

        public TextureAtlasComponent(string textureAtlasAssetName, bool hasPlayerColor)
        {
            TextureAtlasAssetName = textureAtlasAssetName;
            HasPlayerColor = hasPlayerColor;
        }
    }
}
