using Microsoft.Xna.Framework;

using RuneForge.Game.Components.Entities;

namespace RuneForge.Game.Components.Implementations
{
    public class MovementComponent : Component
    {
        public float MovementSpeed { get; }

        public MovementFlags MovementType { get; }

        public int OriginCellX { get; set; }
        public int OriginCellY { get; set; }

        public int DestinationCellX { get; set; }
        public int DestinationCellY { get; set; }

        public bool MovementScheduled { get; set; }
        public bool MovementInProgress { get; set; }
        public bool PathBlocked { get; set; }

        public MovementComponent(float movementSpeed, MovementFlags movementType)
        {
            MovementSpeed = movementSpeed;
            MovementType = movementType;
            OriginCellX = -1;
            OriginCellY = -1;
            DestinationCellX = -1;
            DestinationCellY = -1;
            MovementScheduled = false;
            MovementInProgress = false;
            PathBlocked = false;
        }

        public Point OriginCell
        {
            get => new Point(OriginCellX, OriginCellY);
            set => (OriginCellX, OriginCellY) = value;
        }

        public Point DestinationCell
        {
            get => new Point(DestinationCellX, DestinationCellY);
            set => (DestinationCellX, DestinationCellY) = value;
        }
    }
}
