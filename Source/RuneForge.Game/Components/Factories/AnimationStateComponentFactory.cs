using RuneForge.Data.Components;
using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    [ComponentDto(typeof(AnimationStateComponentDto))]
    public class AnimationStateComponentFactory : ComponentFactory<AnimationStateComponent, AnimationStateComponentPrototype, AnimationStateComponentDto>
    {
        public override AnimationStateComponent CreateComponentFromPrototype(AnimationStateComponentPrototype componentPrototype, AnimationStateComponentPrototype componentPrototypeOverride)
        {
            return new AnimationStateComponent();
        }

        public override AnimationStateComponent CreateComponentFromDto(AnimationStateComponentDto componentDto)
        {
            return new AnimationStateComponent()
            {
                AnimationName = componentDto.AnimationName,
                ElapsedTime = componentDto.ElapsedTime,
                ResetRequested = componentDto.ResetRequested,
            };
        }
    }
}
