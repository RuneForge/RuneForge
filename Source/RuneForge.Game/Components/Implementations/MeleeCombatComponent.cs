using System;

using RuneForge.Game.Entities;

namespace RuneForge.Game.Components.Implementations
{
    public class MeleeCombatComponent : Component
    {
        public decimal AttackPower { get; }

        public TimeSpan CycleTime { get; }

        public TimeSpan ActionTime { get; }

        public Entity TargetEntity { get; set; }

        public TimeSpan TimeElapsed { get; set; }

        public bool CycleInProgress { get; set; }

        public bool ActionTaken { get; set; }

        public MeleeCombatComponent(decimal attackPower, TimeSpan cycleTime, TimeSpan actionTime)
        {
            AttackPower = attackPower;
            CycleTime = cycleTime;
            ActionTime = actionTime;
            TimeElapsed = TimeSpan.Zero;
            CycleInProgress = false;
            ActionTaken = false;
        }

        public void Reset()
        {
            TargetEntity = null;
            TimeElapsed = TimeSpan.Zero;
            CycleInProgress = false;
            ActionTaken = false;
        }
    }
}
