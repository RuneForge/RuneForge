using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Systems.Implementations
{
    public class MovementSystem : System
    {
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IUnitService m_unitService;
        private readonly HashSet<Point> m_occupiedCells;

        public MovementSystem(IGameSessionContext gameSessionContext, IUnitService unitService)
        {
            m_gameSessionContext = gameSessionContext;
            m_unitService = unitService;
            m_occupiedCells = new HashSet<Point>();
        }

        public override void Update(GameTime gameTime)
        {
            m_occupiedCells.Clear();
            foreach (Entity entity in m_gameSessionContext.Units.Concat<Entity>(m_gameSessionContext.Buildings))
            {
                if (!entity.HasComponentOfType<MovementComponent>())
                    continue;

                if (!entity.HasComponentOfType<LocationComponent>())
                    throw new InvalidOperationException($"An entity has the {nameof(MovementComponent)} component but misses the {nameof(LocationComponent)}.");

                switch (entity)
                {
                    case Unit unit:
                        ProcessUnitMovement(unit);
                        break;
                }
            }
            base.Update(gameTime);
        }

        private void ProcessUnitMovement(Unit unit)
        {
            bool changesIntroduced = false;
            MovementComponent movementComponent = unit.GetComponentOfType<MovementComponent>();
            LocationComponent locationComponent = unit.GetComponentOfType<LocationComponent>();

            if (movementComponent.MovementScheduled && !movementComponent.MovementInProgress)
            {
                changesIntroduced = true;
                if (m_occupiedCells.Contains(movementComponent.DestinationCell))
                    movementComponent.PathBlocked = true;
                else
                {
                    movementComponent.MovementInProgress = true;
                    m_occupiedCells.Add(movementComponent.DestinationCell);
                }

                if (unit.TryGetComponentOfType(out DirectionComponent directionComponent))
                    directionComponent.Direction = GetDirection(movementComponent.OriginCell, movementComponent.DestinationCell);
            }

            if (movementComponent.MovementInProgress)
            {
                changesIntroduced = true;

                float destinationX = movementComponent.DestinationCellX * Map.CellWidth;
                float destinationY = movementComponent.DestinationCellY * Map.CellHeight;
                float deltaX = destinationX - locationComponent.X;
                float deltaY = destinationY - locationComponent.Y;

                if (MathF.Abs(deltaX) <= movementComponent.MovementSpeed)
                    locationComponent.X = destinationX;
                else
                    locationComponent.X += movementComponent.MovementSpeed * MathF.Sign(deltaX);

                if (MathF.Abs(deltaY) <= movementComponent.MovementSpeed)
                    locationComponent.Y = destinationY;
                else
                    locationComponent.Y += movementComponent.MovementSpeed * MathF.Sign(deltaY);

                if (locationComponent.X == destinationX && locationComponent.Y == destinationY)
                {
                    movementComponent.MovementScheduled = false;
                    movementComponent.MovementInProgress = false;
                }
            }

            if (changesIntroduced)
                m_unitService.RegisterUnitChanges(unit.Id);
        }

        private Directions GetDirection(Point originCell, Point destinationCell)
        {
            int deltaCellX = destinationCell.X - originCell.X;
            int deltaCellY = destinationCell.Y - originCell.Y;
            if (deltaCellX == 0 && deltaCellY == -1)
                return Directions.North;
            if (deltaCellX == 0 && deltaCellY == +1)
                return Directions.South;
            if (deltaCellX == -1 && deltaCellY == 0)
                return Directions.West;
            if (deltaCellX == +1 && deltaCellY == 0)
                return Directions.East;
            if (deltaCellX == -1 && deltaCellY == -1)
                return Directions.NorthWest;
            if (deltaCellX == +1 && deltaCellY == -1)
                return Directions.NorthEast;
            if (deltaCellX == -1 && deltaCellY == +1)
                return Directions.SouthWest;
            if (deltaCellX == +1 && deltaCellY == +1)
                return Directions.SouthEast;
            return Directions.None;
        }
    }
}
