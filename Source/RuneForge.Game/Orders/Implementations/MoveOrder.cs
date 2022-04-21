using System.Collections.Generic;

using Microsoft.Xna.Framework;

using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.PathGenerators;
using RuneForge.Game.PathGenerators.Interfaces;

namespace RuneForge.Game.Orders.Implementations
{
    public class MoveOrder : Order
    {
        private readonly IPathGenerator m_pathGenerator;

        public int DestinationX { get; }
        public int DestinationY { get; }

        public bool CancellationRequested { get; private set; }

        public MoveOrder(Entity entity, int destinationX, int destinationY, bool cancellationRequested, IPathGenerator pathGenerator)
            : base(entity)
        {
            m_pathGenerator = pathGenerator;
            DestinationX = destinationX;
            DestinationY = destinationY;
            CancellationRequested = cancellationRequested;
        }

        public override void Update(GameTime gameTime, out bool stateChanged)
        {
            stateChanged = false;
            if (State == OrderState.InProgress)
            {
                MovementComponent movementComponent = Entity.GetComponentOfType<MovementComponent>();

                if (CancellationRequested && (!movementComponent.MovementScheduled || movementComponent.PathBlocked))
                {
                    base.Cancel();
                }
                if (!movementComponent.MovementScheduled || movementComponent.PathBlocked)
                {
                    LocationComponent locationComponent = Entity.GetComponentOfType<LocationComponent>();
                    m_pathGenerator.GeneratePath(locationComponent.LocationCells, new Point(DestinationX, DestinationY), movementComponent.MovementType, out PathType pathType, out Queue<Point> path);
                    if (pathType == PathType.NoPath || path.Count == 0)
                    {
                        base.Complete();
                        stateChanged = true;
                    }
                    else
                    {
                        movementComponent.OriginCell = locationComponent.LocationCells;
                        movementComponent.DestinationCell = path.Dequeue();
                        movementComponent.MovementScheduled = true;
                        movementComponent.MovementInProgress = false;
                        movementComponent.PathBlocked = false;
                        stateChanged = true;
                    }
                }
            }

            base.Update(gameTime, out bool stateChangedInternal);
            stateChanged |= stateChangedInternal;
        }

        public override void Cancel()
        {
            CancellationRequested = true;
        }

        public override void Complete()
        {
        }
    }
}
