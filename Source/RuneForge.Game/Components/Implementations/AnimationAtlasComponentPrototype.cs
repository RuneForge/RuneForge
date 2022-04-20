using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Factories;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentFactory(typeof(AnimationAtlasComponentFactory))]
    [ComponentPrototypeReader(typeof(AnimationAtlasComponentPrototypeReader))]
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
