using System;

namespace RuneForge.Data.Components
{
    [Serializable]
    public class AnimationStateComponentDto : ComponentDto
    {
        public string AnimationName { get; set; }

        public TimeSpan ElapsedTime { get; set; }

        public bool ResetRequested { get; set; }
    }
}
