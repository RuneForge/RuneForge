using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class AnimationAtlasComponentFactory : ComponentFactory<AnimationAtlasComponent, AnimationAtlasComponentPrototype>
    {
        public override AnimationAtlasComponent CreateComponentFromPrototype(AnimationAtlasComponentPrototype componentPrototype, AnimationAtlasComponentPrototype componentPrototypeOverride)
        {
            return new AnimationAtlasComponent(componentPrototype.AnimationAtlasAssetName, componentPrototype.HasPlayerColor);
        }
    }
}
