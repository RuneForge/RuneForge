using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

using RuneForge.Game.Components.Entities;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.PathGenerators.Interfaces;

namespace RuneForge.Game.PathGenerators
{
    public class BreadthFirstPathGenerator : IPathGenerator
    {
        private static readonly Point[] s_neighborCellOffsets = new Point[8]
        {
            new Point(+0, -1),
            new Point(+0, +1),
            new Point(-1, +0),
            new Point(+1, +0),
            new Point(-1, -1),
            new Point(+1, -1),
            new Point(-1, +1),
            new Point(+1, +1),
        };

        private readonly IGameSessionContext m_gameSessionContext;

        public BreadthFirstPathGenerator(IGameSessionContext gameSessionContext)
        {
            m_gameSessionContext = gameSessionContext;
        }

        public void GeneratePath(Point origin, Point destination, MovementFlags requiredMovementFlags, out PathType pathType, out Queue<Point> path)
        {
            Map map = m_gameSessionContext.Map;

            destination.X = Math.Max(Math.Min(destination.X, map.Width - 1), 0);
            destination.Y = Math.Max(Math.Min(destination.Y, map.Height - 1), 0);

            HashSet<Point> occupiedCells = new HashSet<Point>();
            foreach (Entity entity in m_gameSessionContext.Units.Concat<Entity>(m_gameSessionContext.Buildings))
            {
                if (entity.TryGetComponentOfType(out LocationComponent locationComponent))
                {
                    foreach (Point occupiedCell in locationComponent.GetOccupiedCells())
                        occupiedCells.Add(occupiedCell);
                }
                if (entity.TryGetComponentOfType(out MovementComponent movementComponent) && movementComponent.MovementInProgress)
                {
                    occupiedCells.Add(movementComponent.DestinationCell);
                }
            }

            int GetIndex(int x, int y) => (y * map.Width) + x;
            int[] visitedCells = new int[map.Width * map.Height];
            Array.Fill(visitedCells, -1);

            visitedCells[GetIndex(origin.X, origin.Y)] = 0;

            Queue<Point> cellsToVisit = new Queue<Point>();
            cellsToVisit.Enqueue(origin);

            while (cellsToVisit.Count > 0)
            {
                Point currentCell = cellsToVisit.Dequeue();

                foreach (Point neighborCellOffset in s_neighborCellOffsets)
                {
                    Point nextCell = currentCell + neighborCellOffset;

                    if (nextCell.X < 0 || nextCell.Y < 0 || nextCell.X >= map.Width || nextCell.Y >= map.Height)
                        continue;

                    int visitedCellValue = visitedCells[GetIndex(nextCell.X, nextCell.Y)];
                    if (visitedCellValue > -1 && visitedCellValue <= visitedCells[GetIndex(currentCell.X, currentCell.Y)] + 1)
                        continue;

                    if (occupiedCells.Contains(nextCell))
                        continue;

                    int actualMovementFlags = (int)requiredMovementFlags;
                    actualMovementFlags &= (int)map.GetLandscapeCellMovementFlags(nextCell.X, nextCell.Y);
                    if (map.GetDecorationCell(nextCell.X, nextCell.Y).Tier > MapDecorationCellTier.None)
                        actualMovementFlags &= ~(int)map.GetDecorationCellMovementFlags(nextCell.X, nextCell.Y);

                    if (actualMovementFlags == 0)
                        continue;

                    visitedCells[GetIndex(nextCell.X, nextCell.Y)] = visitedCells[GetIndex(currentCell.X, currentCell.Y)] + 1;
                    cellsToVisit.Enqueue(nextCell);
                }
            }

            path = new Queue<Point>();
            pathType = PathType.PathToDestination;
            if (visitedCells[GetIndex(destination.X, destination.Y)] == -1)
            {
                pathType = PathType.PathToClosestReachableCell;

                Point reachableDestination = destination;
                while (reachableDestination != origin)
                {
                    reachableDestination.X += Math.Sign(origin.X - reachableDestination.X);
                    reachableDestination.Y += Math.Sign(origin.Y - reachableDestination.Y);

                    if (visitedCells[GetIndex(reachableDestination.X, reachableDestination.Y)] > -1)
                        break;
                }

                destination = reachableDestination;
                if (reachableDestination == origin)
                    pathType = PathType.NoPath;
            }

            Stack<Point> reversedPath = new Stack<Point>();
            while (destination != origin)
            {
                reversedPath.Push(destination);

                foreach (Point neighborCellOffset in s_neighborCellOffsets)
                {
                    Point nextCell = destination + neighborCellOffset;

                    if (nextCell.X < 0 || nextCell.Y < 0 || nextCell.X >= map.Width || nextCell.Y >= map.Height)
                        continue;

                    if (visitedCells[GetIndex(nextCell.X, nextCell.Y)] == visitedCells[GetIndex(destination.X, destination.Y)] - 1)
                    {
                        destination = nextCell;
                        break;
                    }
                }
            }

            while (reversedPath.Count > 0)
                path.Enqueue(reversedPath.Pop());
        }
    }
}
