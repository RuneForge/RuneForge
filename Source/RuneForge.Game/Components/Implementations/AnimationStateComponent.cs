using System;

namespace RuneForge.Game.Components.Implementations
{
    public class AnimationStateComponent : Component
    {
        public string AnimationName { get; set; }

        public TimeSpan ElapsedTime { get; set; }

        public bool ResetRequested { get; set; }
    }
}
