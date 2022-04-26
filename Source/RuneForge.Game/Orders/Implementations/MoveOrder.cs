using System;
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
        private const string c_moveAnimationName = "Move";

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
                    StopMoveAnimation();
                    stateChanged = true;
                }
                if (!movementComponent.MovementScheduled || movementComponent.PathBlocked)
                {
                    LocationComponent locationComponent = Entity.GetComponentOfType<LocationComponent>();
                    m_pathGenerator.GeneratePath(locationComponent.LocationCells, new Point(DestinationX, DestinationY), movementComponent.MovementType, out PathType pathType, out Queue<Point> path);
                    if (pathType == PathType.NoPath || path.Count == 0)
                    {
                        base.Complete();
                        StopMoveAnimation();
                        stateChanged = true;
                    }
                    else if (!CancellationRequested)
                    {
                        movementComponent.OriginCell = locationComponent.LocationCells;
                        movementComponent.DestinationCell = path.Dequeue();
                        movementComponent.MovementScheduled = true;
                        movementComponent.MovementInProgress = false;
                        movementComponent.PathBlocked = false;
                        StartMoveAnimation(false);
                        stateChanged = true;
                    }
                }
                if ((movementComponent.MovementScheduled && !movementComponent.PathBlocked) || movementComponent.MovementInProgress)
                {
                    UpdateMoveAnimation(gameTime.ElapsedGameTime);
                    stateChanged = true;
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

        private void StartMoveAnimation(bool requestReset)
        {
            if (!Entity.TryGetComponentOfType(out AnimationStateComponent animationStateComponent))
                return;
            animationStateComponent.AnimationName = c_moveAnimationName;
            animationStateComponent.ElapsedTime = TimeSpan.Zero;
            animationStateComponent.ResetRequested = requestReset;
        }

        private void UpdateMoveAnimation(TimeSpan elapsedTime)
        {
            if (!Entity.TryGetComponentOfType(out AnimationStateComponent animationStateComponent))
                return;
            animationStateComponent.ElapsedTime = elapsedTime;
        }

        private void StopMoveAnimation()
        {
            if (!Entity.TryGetComponentOfType(out AnimationStateComponent animationStateComponent))
                return;
            animationStateComponent.AnimationName = null;
            animationStateComponent.ElapsedTime = TimeSpan.Zero;
            animationStateComponent.ResetRequested = true;
        }
    }
}
