using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.PathGenerators;
using RuneForge.Game.PathGenerators.Interfaces;

namespace RuneForge.Game.Orders.Implementations
{
    public class AttackOrder : Order
    {
        private const string c_moveAnimationName = "Move";
        private const string c_attackAnimationName = "Attack";

        private readonly IPathGenerator m_pathGenerator;

        public Entity TargetEntity { get; }

        public bool CompletingRequested { get; private set; }
        public bool CancellationRequested { get; private set; }

        public AttackOrder(Entity entity, Entity targetEntity, IPathGenerator pathGenerator)
            : this(entity, targetEntity, OrderState.Scheduled, false, false, pathGenerator)
        {
        }
        public AttackOrder(Entity entity, Entity targetEntity, OrderState orderState, bool completingRequested, bool cancellationRequested, IPathGenerator pathGenerator)
            : base(entity)
        {
            m_pathGenerator = pathGenerator;
            TargetEntity = targetEntity;
            State = orderState;
            CompletingRequested = completingRequested;
            CancellationRequested = cancellationRequested;
        }

        public override void Update(GameTime gameTime, out bool stateChanged)
        {
            stateChanged = false;
            if (State == OrderState.InProgress)
            {
                if (Entity.TryGetComponentOfType(out MeleeCombatComponent meleeCombatComponent))
                {
                    if ((TargetEntity.TryGetComponentOfType(out HealthComponent healthComponent) && healthComponent.Health == 0)
                        || (TargetEntity.TryGetComponentOfType(out DurabilityComponent durabilityComponent) && durabilityComponent.Durability == 0))
                    {
                        CompletingRequested = true;
                        stateChanged = true;
                    }

                    MovementComponent movementComponent = Entity.GetComponentOfType<MovementComponent>();
                    if (CompletingRequested && (!movementComponent.MovementScheduled || movementComponent.PathBlocked) && (!meleeCombatComponent.CycleInProgress))
                    {
                        CompleteInternal();
                        stateChanged = true;
                    }
                    if (CancellationRequested && (!movementComponent.MovementScheduled || movementComponent.PathBlocked) && (!meleeCombatComponent.CycleInProgress))
                    {
                        CancelInternal();
                        stateChanged = true;
                    }
                    else if (!CompletingRequested && !CancellationRequested)
                    {
                        if ((!movementComponent.MovementScheduled) && !meleeCombatComponent.CycleInProgress)
                        {
                            LocationComponent locationComponent = Entity.GetComponentOfType<LocationComponent>();
                            LocationComponent targetLocationComponent = TargetEntity.GetComponentOfType<LocationComponent>();
                            m_pathGenerator.GeneratePath(locationComponent.LocationCells, targetLocationComponent.GetSurroundingCells(), movementComponent.MovementType, out PathType pathType, out Queue<Point> path);
                            if (pathType == PathType.PathToDestination && path.Count == 0)
                            {
                                StopAnimation();
                                meleeCombatComponent.Reset();
                                meleeCombatComponent.TargetEntity = TargetEntity;
                                meleeCombatComponent.CycleInProgress = true;
                                StartAttackAnimation(false);
                                stateChanged = true;
                            }
                            else if (path.Count > 0)
                            {
                                StopAnimation();
                                movementComponent.OriginCell = locationComponent.LocationCells;
                                movementComponent.DestinationCell = path.Dequeue();
                                movementComponent.MovementScheduled = true;
                                movementComponent.MovementInProgress = false;
                                movementComponent.PathBlocked = false;
                                StartMoveAnimation(false);
                                stateChanged = true;
                            }
                            else
                            {
                                StopAnimation();
                                stateChanged = true;
                            }
                        }

                        if ((movementComponent.MovementScheduled && !movementComponent.PathBlocked) || meleeCombatComponent.CycleInProgress)
                        {
                            UpdateAnimation(gameTime.ElapsedGameTime);
                            stateChanged = true;
                        }
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

        private void CompleteInternal()
        {
            CompletingRequested = true;
            base.Complete();
            StopAnimation();
        }

        private void CancelInternal()
        {
            CancellationRequested = true;
            base.Cancel();
            StopAnimation();
        }

        private void StartMoveAnimation(bool requestReset)
        {
            if (!Entity.TryGetComponentOfType(out AnimationStateComponent animationStateComponent))
                return;
            animationStateComponent.AnimationName = c_moveAnimationName;
            animationStateComponent.ElapsedTime = TimeSpan.Zero;
            animationStateComponent.ResetRequested = requestReset;
        }

        private void StartAttackAnimation(bool requestReset)
        {
            if (!Entity.TryGetComponentOfType(out AnimationStateComponent animationStateComponent))
                return;
            animationStateComponent.AnimationName = c_attackAnimationName;
            animationStateComponent.ElapsedTime = TimeSpan.Zero;
            animationStateComponent.ResetRequested = requestReset;
        }

        private void UpdateAnimation(TimeSpan elapsedTime)
        {
            if (!Entity.TryGetComponentOfType(out AnimationStateComponent animationStateComponent))
                return;
            animationStateComponent.ElapsedTime = elapsedTime;
        }

        private void StopAnimation()
        {
            if (!Entity.TryGetComponentOfType(out AnimationStateComponent animationStateComponent))
                return;
            animationStateComponent.AnimationName = null;
            animationStateComponent.ElapsedTime = TimeSpan.Zero;
            animationStateComponent.ResetRequested = true;
        }
    }
}
