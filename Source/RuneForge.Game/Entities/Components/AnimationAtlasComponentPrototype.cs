namespace RuneForge.Game.Entities.Components
{
    public class AnimationAtlasComponentPrototype : ComponentPrototype
    {
        public string AnimationAtlasAssetName { get; }

        public bool HasPlayerColor { get; }

        public AnimationAtlasComponentPrototype(string animationAtlasAssetName, bool hasPlayerColor)
        {
            AnimationAtlasAssetName = animationAtlasAssetName;
            HasPlayerColor = hasPlayerColor;
        }
    }
}
