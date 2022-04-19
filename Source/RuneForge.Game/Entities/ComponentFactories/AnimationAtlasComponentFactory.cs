using RuneForge.Game.Entities.Components;

namespace RuneForge.Game.Entities.ComponentFactories
{
    public class AnimationAtlasComponentFactory : ComponentFactory<AnimationAtlasComponent, AnimationAtlasComponentPrototype>
    {
        public override AnimationAtlasComponent CreateComponentFromPrototype(AnimationAtlasComponentPrototype componentPrototype, AnimationAtlasComponentPrototype componentPrototypeOverride)
        {
            return new AnimationAtlasComponent(componentPrototype.AnimationAtlasAssetName, componentPrototype.HasPlayerColor);
        }
    }
}
