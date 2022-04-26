using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.PathGenerators;
using RuneForge.Game.PathGenerators.Interfaces;
using RuneForge.Game.Units;

namespace RuneForge.Game.Orders.Implementations
{
    public class GatherResourcesOrder : Order
    {
        private const string c_moveAnimationName = "Move";

        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IBuildingService m_buildingService;
        private readonly IPathGenerator m_pathGenerator;

        public Building ResourceSource { get; }

        public bool CancellationRequested { get; private set; }

        public GatherResourcesOrder(Entity entity, Building resourceSource, bool cancellationRequested, IGameSessionContext gameSessionContext, IBuildingService buildingService, IPathGenerator pathGenerator)
            : base(entity)
        {
            m_gameSessionContext = gameSessionContext;
            m_buildingService = buildingService;
            m_pathGenerator = pathGenerator;
            ResourceSource = resourceSource;
            CancellationRequested = cancellationRequested;
        }

        public override void Update(GameTime gameTime, out bool stateChanged)
        {
            stateChanged = false;
            if (State == OrderState.InProgress)
            {
                MovementComponent movementComponent = Entity.GetComponentOfType<MovementComponent>();
                UnitShelterOccupantComponent shelterOccupantComponent = Entity.GetComponentOfType<UnitShelterOccupantComponent>();
                if (CancellationRequested && (!movementComponent.MovementScheduled || movementComponent.PathBlocked) && (!shelterOccupantComponent.InsideShelter))
                {
                    CancelInternal();
                    stateChanged = true;
                }
                else if (!CancellationRequested)
                {
                    if (!shelterOccupantComponent.InsideShelter && (!movementComponent.MovementScheduled || movementComponent.PathBlocked))
                    {
                        ResourceSourceComponent resourceSourceComponent = ResourceSource.GetComponentOfType<ResourceSourceComponent>();
                        ResourceContainerComponent buildingResourceContainerComponent = ResourceSource.GetComponentOfType<ResourceContainerComponent>();
                        ResourceContainerComponent unitResourceContainerComponent = Entity.GetComponentOfType<ResourceContainerComponent>();

                        Entity destinationEntity;
                        bool shouldGatherResources = unitResourceContainerComponent.GetResourceAmount(resourceSourceComponent.ResourceType) == 0;
                        if (shouldGatherResources && buildingResourceContainerComponent.GetResourceAmount(resourceSourceComponent.ResourceType) > 0)
                        {
                            destinationEntity = ResourceSource;
                        }
                        else if (shouldGatherResources)
                        {
                            destinationEntity = null;
                        }
                        else
                        {
                            Unit unit = (Unit)Entity;
                            destinationEntity = m_gameSessionContext.Buildings.FirstOrDefault(building =>
                            {
                                return building.Owner == unit.Owner
                                    && building.TryGetComponentOfType(out ResourceStorageComponent resourceStorageComponent)
                                    && (resourceStorageComponent.AcceptedResourceTypes & resourceSourceComponent.ResourceType) == resourceSourceComponent.ResourceType;
                            });
                        }

                        if (destinationEntity == null)
                        {
                            CancelInternal();
                            stateChanged = true;
                        }
                        else
                        {
                            Unit unit = (Unit)Entity;
                            LocationComponent unitLocationComponent = unit.GetComponentOfType<LocationComponent>();
                            LocationComponent buildingLocationComponent = destinationEntity.GetComponentOfType<LocationComponent>();
                            m_pathGenerator.GeneratePath(unitLocationComponent.LocationCells, buildingLocationComponent.GetSurroundingCells(), movementComponent.MovementType, out PathType pathType, out Queue<Point> path);
                            if (path.Count == 0)
                            {
                                StopAnimation();
                                stateChanged = true;
                                UnitShelterComponent shelterComponent = destinationEntity.GetComponentOfType<UnitShelterComponent>();
                                if (shelterComponent.Occupants.Count < shelterComponent.OccupantsLimit)
                                {
                                    shelterComponent.AddOccupant(unit);
                                    shelterOccupantComponent.EnteredFrom = unitLocationComponent.LocationCells;
                                    shelterOccupantComponent.InsideShelter = true;
                                    shelterOccupantComponent.TimeSinceEntering = TimeSpan.Zero;
                                    m_buildingService.RegisterBuildingChanges(ResourceSource.Id);
                                    stateChanged = true;
                                }
                            }
                            else if (!CancellationRequested)
                            {
                                movementComponent.OriginCell = unitLocationComponent.LocationCells;
                                movementComponent.DestinationCell = path.Dequeue();
                                movementComponent.MovementScheduled = true;
                                movementComponent.MovementInProgress = false;
                                movementComponent.PathBlocked = false;
                                StartMoveAnimation(false);
                                stateChanged = true;
                            }
                        }
                    }
                    if ((movementComponent.MovementScheduled && !movementComponent.PathBlocked) || movementComponent.MovementInProgress)
                    {
                        UpdateMoveAnimation(gameTime.ElapsedGameTime);
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

        private void UpdateMoveAnimation(TimeSpan elapsedTime)
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
