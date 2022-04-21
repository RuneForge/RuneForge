using RuneForge.Content.Pipeline.Game.Components.Attributes;
using RuneForge.Content.Pipeline.Game.Components.PrototypeWriters;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    [ComponentPrototypeWriter(typeof(AnimationStateComponentPrototypeWriter))]
    public class AnimationStateComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.AnimationStateComponentPrototype, RuneForge.Game";

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
