using System.Collections.Generic;

using Microsoft.Xna.Framework;

using RuneForge.Game.Components.Entities;

namespace RuneForge.Game.PathGenerators.Interfaces
{
    public interface IPathGenerator
    {
        public void GeneratePath(Point origin, Point destination, MovementFlags requiredMovementFlags, out PathType pathType, out Queue<Point> path);
        public void GeneratePath(Point origin, IList<Point> destinations, MovementFlags requiredMovementFlags, out PathType pathType, out Queue<Point> path);

        public bool IsCellFree(Point cell, MovementFlags requiredMovementFlags);
    }
}
