namespace RuneForge.Game.Entities.Components
{
    public class AnimationAtlasComponent : Component
    {
        public string AnimationAtlasAssetName { get; }

        public bool HasPlayerColor { get; }

        public AnimationAtlasComponent(string animationAtlasAssetName, bool hasPlayerColor)
        {
            AnimationAtlasAssetName = animationAtlasAssetName;
            HasPlayerColor = hasPlayerColor;
        }
    }
}
