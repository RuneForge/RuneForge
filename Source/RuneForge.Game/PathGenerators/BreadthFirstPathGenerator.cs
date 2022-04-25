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
            GeneratePath(origin, new Point[1] { destination }, requiredMovementFlags, out pathType, out path);
        }

        public void GeneratePath(Point origin, IList<Point> destinations, MovementFlags requiredMovementFlags, out PathType pathType, out Queue<Point> path)
        {
            Map map = m_gameSessionContext.Map;

            destinations = new List<Point>(destinations);
            for (int i = destinations.Count - 1; i >= 0; i--)
            {
                Point destination = destinations[i];
                if (destinations.Count > 1 && (destination.X < 0 || destination.Y < 0 || destination.X >= map.Width || destination.Y >= map.Height))
                    destinations.RemoveAt(i);
                else
                {
                    destination.X = Math.Max(Math.Min(destination.X, map.Width - 1), 0);
                    destination.Y = Math.Max(Math.Min(destination.Y, map.Height - 1), 0);
                    destinations[i] = destination;
                }
            }

            HashSet<Point> occupiedCells = GetCellsOccupiedByEntities();

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

                    int actualMovementFlags = CalculateMovementFlags(requiredMovementFlags, map, nextCell);
                    if (actualMovementFlags == 0)
                        continue;

                    visitedCells[GetIndex(nextCell.X, nextCell.Y)] = visitedCells[GetIndex(currentCell.X, currentCell.Y)] + 1;
                    cellsToVisit.Enqueue(nextCell);
                }
            }

            path = new Queue<Point>();
            pathType = PathType.PathToDestination;

            int minCost = int.MaxValue;
            Point finalDestination = new Point();
            foreach (Point destination in destinations)
            {
                int cost = visitedCells[GetIndex(destination.X, destination.Y)];
                if (cost >= 0 && cost < minCost)
                {
                    minCost = cost;
                    finalDestination = destination;
                }
            }
            if (minCost == int.MaxValue)
                finalDestination = destinations[m_gameSessionContext.RandomNumbersGenerator.Next(destinations.Count)];

            if (visitedCells[GetIndex(finalDestination.X, finalDestination.Y)] == -1)
            {
                pathType = PathType.PathToClosestReachableCell;

                Point reachableDestination = finalDestination;
                while (reachableDestination != origin)
                {
                    reachableDestination.X += Math.Sign(origin.X - reachableDestination.X);
                    reachableDestination.Y += Math.Sign(origin.Y - reachableDestination.Y);

                    if (visitedCells[GetIndex(reachableDestination.X, reachableDestination.Y)] > -1)
                        break;
                }

                finalDestination = reachableDestination;
                if (reachableDestination == origin)
                    pathType = PathType.NoPath;
            }

            Stack<Point> reversedPath = new Stack<Point>();
            while (finalDestination != origin)
            {
                reversedPath.Push(finalDestination);

                foreach (Point neighborCellOffset in s_neighborCellOffsets)
                {
                    Point nextCell = finalDestination + neighborCellOffset;

                    if (nextCell.X < 0 || nextCell.Y < 0 || nextCell.X >= map.Width || nextCell.Y >= map.Height)
                        continue;

                    if (visitedCells[GetIndex(nextCell.X, nextCell.Y)] == visitedCells[GetIndex(finalDestination.X, finalDestination.Y)] - 1)
                    {
                        finalDestination = nextCell;
                        break;
                    }
                }
            }

            while (reversedPath.Count > 0)
                path.Enqueue(reversedPath.Pop());
        }

        public bool IsCellFree(Point cell, MovementFlags requiredMovementFlags)
        {
            Map map = m_gameSessionContext.Map;
            HashSet<Point> occupiedCells = GetCellsOccupiedByEntities();
            if (occupiedCells.Contains(cell))
                return false;

            int actualMovementFlags = CalculateMovementFlags(requiredMovementFlags, map, cell);
            if (actualMovementFlags == 0)
                return false;

            return true;
        }

        public bool TryGetClosestFreeCell(Point cell, IList<Point> possibleCells, MovementFlags requiredMovementFlags, out Point result)
        {
            Map map = m_gameSessionContext.Map;
            HashSet<Point> occupiedCells = GetCellsOccupiedByEntities();

            result = Point.Zero;
            int minDistance = int.MaxValue;
            foreach (Point possibleCell in possibleCells)
            {
                if (possibleCell.X < 0 || possibleCell.Y < 0 || possibleCell.X >= map.Width || possibleCell.Y >= map.Height)
                    continue;

                if (occupiedCells.Contains(possibleCell))
                    continue;

                int actualMovementFlags = CalculateMovementFlags(requiredMovementFlags, map, cell);
                if (actualMovementFlags == 0)
                    continue;

                int distance = Math.Abs(cell.X - possibleCell.X) + Math.Abs(cell.Y - possibleCell.Y);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = possibleCell;
                }
            }
            return minDistance < int.MaxValue;
        }

        private static int CalculateMovementFlags(MovementFlags requiredMovementFlags, Map map, Point nextCell)
        {
            int actualMovementFlags = (int)requiredMovementFlags;
            actualMovementFlags &= (int)map.GetLandscapeCellMovementFlags(nextCell.X, nextCell.Y);
            if (map.GetDecorationCell(nextCell.X, nextCell.Y).Tier > MapDecorationCellTier.None)
                actualMovementFlags &= ~(int)map.GetDecorationCellMovementFlags(nextCell.X, nextCell.Y);
            return actualMovementFlags;
        }

        private HashSet<Point> GetCellsOccupiedByEntities()
        {
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

            return occupiedCells;
        }
    }
}
