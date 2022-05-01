using RuneForge.Content.Pipeline.Game.Components.Attributes;
using RuneForge.Content.Pipeline.Game.Components.PrototypeWriters;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    [ComponentPrototypeWriter(typeof(MeleeCombatComponentPrototypeWriter))]
    public class MeleeCombatComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.MeleeCombatComponentPrototype, RuneForge.Game";

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
