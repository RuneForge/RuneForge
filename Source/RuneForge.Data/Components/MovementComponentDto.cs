using System;

namespace RuneForge.Data.Components
{
    [Serializable]
    public class MovementComponentDto : ComponentDto
    {
        public float MovementSpeed { get; }

        public int OriginCellX { get; set; }
        public int OriginCellY { get; set; }

        public int DestinationCellX { get; set; }
        public int DestinationCellY { get; set; }

        public bool MovementScheduled { get; set; }
        public bool MovementInProgress { get; set; }
        public bool PathBlocked { get; set; }
    }
}
