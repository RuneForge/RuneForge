using System;

namespace RuneForge.Data.Components
{
    [Serializable]
    public class AnimationAtlasComponentDto : ComponentDto
    {
        public string AnimationAtlasAssetName { get; set; }

        public bool HasPlayerColor { get; set; }
    }
}
