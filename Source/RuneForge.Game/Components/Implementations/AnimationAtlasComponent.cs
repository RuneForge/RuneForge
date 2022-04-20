namespace RuneForge.Game.Components.Implementations
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
