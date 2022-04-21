using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.Factories
{
    public class AnimationStateComponentFactory : ComponentFactory<AnimationStateComponent, AnimationStateComponentPrototype>
    {
        public override AnimationStateComponent CreateComponentFromPrototype(AnimationStateComponentPrototype componentPrototype, AnimationStateComponentPrototype componentPrototypeOverride)
        {
            return new AnimationStateComponent();
        }
    }
}
