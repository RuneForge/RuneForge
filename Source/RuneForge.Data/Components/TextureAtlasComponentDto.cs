using System;

namespace RuneForge.Data.Components
{
    [Serializable]
    public class TextureAtlasComponentDto : ComponentDto
    {
        public string TextureAtlasAssetName { get; set; }

        public bool HasPlayerColor { get; set; }
    }
}
