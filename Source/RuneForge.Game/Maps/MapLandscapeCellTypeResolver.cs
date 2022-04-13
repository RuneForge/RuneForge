using System;
using System.Collections.Generic;

using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Game.Maps
{
    public class MapLandscapeCellTypeResolver : IMapLandscapeCellTypeResolver
    {
        private static readonly List<(MapLandscapeCellTypes, Directions)> s_landscapeCellPatterns = new List<(MapLandscapeCellTypes, Directions)>()
        {
            (MapLandscapeCellTypes.Center, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast | Directions.SouthWest | Directions.SouthEast),
            (MapLandscapeCellTypes.InnerCornerNorthWest, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthEast | Directions.SouthWest | Directions.SouthEast),
            (MapLandscapeCellTypes.InnerCornerNorthEast, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.SouthWest | Directions.SouthEast),
            (MapLandscapeCellTypes.InnerCornerSouthWest, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast | Directions.SouthEast),
            (MapLandscapeCellTypes.InnerCornerSouthEast, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast | Directions.SouthWest),
            (MapLandscapeCellTypes.EdgeNorth, Directions.South | Directions.West | Directions.East | Directions.SouthWest | Directions.SouthEast),
            (MapLandscapeCellTypes.EdgeSouth, Directions.North | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast),
            (MapLandscapeCellTypes.EdgeWest, Directions.North | Directions.South | Directions.East | Directions.NorthEast | Directions.SouthEast),
            (MapLandscapeCellTypes.EdgeEast, Directions.North | Directions.South | Directions.West | Directions.NorthWest | Directions.SouthWest),
            (MapLandscapeCellTypes.OuterCornerNorthWest, Directions.South | Directions.East | Directions.SouthEast),
            (MapLandscapeCellTypes.OuterCornerNorthEast, Directions.South | Directions.West | Directions.SouthWest),
            (MapLandscapeCellTypes.OuterCornerSouthWest, Directions.North | Directions.East | Directions.NorthEast),
            (MapLandscapeCellTypes.OuterCornerSouthEast, Directions.North | Directions.West | Directions.NorthWest),
        };

        public MapLandscapeCellTypes ResolveLandscapeCellType(int x, int y, Map map)
        {
            if (!TryResolveLandscapeCellType(x, y, map, out MapLandscapeCellTypes type))
                throw new InvalidOperationException("Unable to resolve the type of the cell.");
            else
                return type;
        }

        public bool TryResolveLandscapeCellType(int x, int y, Map map, out MapLandscapeCellTypes type)
        {
            Directions adjacentCells = GetAdjacentLandscapeCells(x, y, map);
            foreach ((MapLandscapeCellTypes landscapeCellType, Directions mask) in s_landscapeCellPatterns)
            {
                if ((adjacentCells & mask) == mask)
                {
                    type = landscapeCellType;
                    return true;
                }
            }
            type = MapLandscapeCellTypes.Center;
            return false;
        }

        private Directions GetAdjacentLandscapeCells(int x, int y, Map map)
        {
            Directions result = Directions.None;
            MapLandscapeCellTier landscapeCellTier = map.GetLandscapeCell(x, y).Tier;

            result |= y > 0 ? map.GetLandscapeCell(x, y - 1).Tier < landscapeCellTier ? Directions.None : Directions.North : Directions.North;
            result |= y < map.Height - 1 ? map.GetLandscapeCell(x, y + 1).Tier < landscapeCellTier ? Directions.None : Directions.South : Directions.South;
            result |= x > 0 ? map.GetLandscapeCell(x - 1, y).Tier < landscapeCellTier ? Directions.None : Directions.West : Directions.West;
            result |= x < map.Width - 1 ? map.GetLandscapeCell(x + 1, y).Tier < landscapeCellTier ? Directions.None : Directions.East : Directions.East;

            result |= y > 0 && x > 0 ? map.GetLandscapeCell(x - 1, y - 1).Tier < landscapeCellTier ? Directions.None : Directions.NorthWest : Directions.NorthWest;
            result |= y > 0 && x < map.Width - 1 ? map.GetLandscapeCell(x + 1, y - 1).Tier < landscapeCellTier ? Directions.None : Directions.NorthEast : Directions.NorthEast;
            result |= y < map.Height - 1 && x > 0 ? map.GetLandscapeCell(x - 1, y + 1).Tier < landscapeCellTier ? Directions.None : Directions.SouthWest : Directions.SouthWest;
            result |= y < map.Height - 1 && x < map.Width - 1 ? map.GetLandscapeCell(x + 1, y + 1).Tier < landscapeCellTier ? Directions.None : Directions.SouthEast : Directions.SouthEast;

            return result;
        }
    }
}
