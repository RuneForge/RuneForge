using RuneForge.Data.Components;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class AnimationAtlasComponentFactory : ComponentFactory<AnimationAtlasComponent, AnimationAtlasComponentPrototype, AnimationAtlasComponentDto>
    {
        public override AnimationAtlasComponent CreateComponentFromPrototype(AnimationAtlasComponentPrototype componentPrototype, AnimationAtlasComponentPrototype componentPrototypeOverride)
        {
            return new AnimationAtlasComponent(componentPrototype.AnimationAtlasAssetName, componentPrototype.HasPlayerColor);
        }

        public override AnimationAtlasComponent CreateComponentFromDto(AnimationAtlasComponentDto componentDto)
        {
            return new AnimationAtlasComponent(componentDto.AnimationAtlasAssetName, componentDto.HasPlayerColor);
        }
    }
}
