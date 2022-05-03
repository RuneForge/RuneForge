using System;

using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Factories;
using RuneForge.Game.Components.PrototypeReaders;

namespace RuneForge.Game.Components.Implementations
{
    [ComponentFactory(typeof(MeleeCombatComponentFactory))]
    [ComponentPrototypeReader(typeof(MeleeCombatComponentPrototypeReader))]
    public class MeleeCombatComponentPrototype : ComponentPrototype
    {
        public decimal AttackPower { get; }

        public TimeSpan CycleTime { get; }

        public TimeSpan ActionTime { get; }

        public MeleeCombatComponentPrototype(decimal attackPower, TimeSpan cycleTime, TimeSpan actionTime)
        {
            AttackPower = attackPower;
            CycleTime = cycleTime;
            ActionTime = actionTime;
        }
    }
}
