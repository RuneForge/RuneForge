using System;

namespace RuneForge.Data.Components
{
    public class MeleeCombatComponentDto : ComponentDto
    {
        public decimal AttackPower { get; set; }

        public TimeSpan CycleTime { get; set; }

        public TimeSpan ActionTime { get; set; }

        public string TargetEntityId { get; set; }

        public TimeSpan TimeElapsed { get; set; }

        public bool CycleInProgress { get; set; }

        public bool ActionTaken { get; set; }
    }
}
