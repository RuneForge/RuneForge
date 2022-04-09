using System;
using System.Collections.Generic;

using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Game.Maps
{
    public class MapCellTypeResolver : IMapCellTypeResolver
    {
        private readonly List<(MapCellType, Directions)> s_mapCellPatterns = new List<(MapCellType, Directions)>()
        {
            (MapCellType.Center, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast | Directions.SouthWest | Directions.SouthEast),
            (MapCellType.InnerCornerNorthWest, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthEast | Directions.SouthWest | Directions.SouthEast),
            (MapCellType.InnerCornerNorthEast, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.SouthWest | Directions.SouthEast),
            (MapCellType.InnerCornerSouthWest, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast | Directions.SouthEast),
            (MapCellType.InnerCornerSouthEast, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast | Directions.SouthWest),
            (MapCellType.EdgeNorth, Directions.South | Directions.West | Directions.East | Directions.SouthWest | Directions.SouthEast),
            (MapCellType.EdgeSouth, Directions.North | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast),
            (MapCellType.EdgeWest, Directions.North | Directions.South | Directions.East | Directions.NorthEast | Directions.SouthEast),
            (MapCellType.EdgeEast, Directions.North | Directions.South | Directions.West | Directions.NorthWest | Directions.SouthWest),
            (MapCellType.OuterCornerNorthWest, Directions.South | Directions.East | Directions.SouthEast),
            (MapCellType.OuterCornerNorthEast, Directions.South | Directions.West | Directions.SouthWest),
            (MapCellType.OuterCornerSouthWest, Directions.North | Directions.East | Directions.NorthEast),
            (MapCellType.OuterCornerSouthEast, Directions.North | Directions.West | Directions.NorthWest),
        };

        public MapCellType ResolveMapCellType(int x, int y, Map map)
        {
            if (!TryResolveMapCellType(x, y, map, out MapCellType type))
                throw new InvalidOperationException("Unable to resolve the type of the cell.");
            else
                return type;
        }

        public bool TryResolveMapCellType(int x, int y, Map map, out MapCellType type)
        {
            Directions adjacentCells = GetAdjacentCells(x, y, map);
            foreach ((MapCellType mapCellType, Directions mask) in s_mapCellPatterns)
            {
                if ((adjacentCells & mask) == mask)
                {
                    type = mapCellType;
                    return true;
                }
            }
            type = MapCellType.Center;
            return false;
        }

        private Directions GetAdjacentCells(int x, int y, Map map)
        {
            Directions result = Directions.None;
            MapCellTier mapCellTier = map.GetCell(x, y).Tier;

            result |= y > 0 ? map.GetCell(x, y - 1).Tier < mapCellTier ? Directions.None : Directions.North : Directions.North;
            result |= y < map.Height - 1 ? map.GetCell(x, y + 1).Tier < mapCellTier ? Directions.None : Directions.South : Directions.South;
            result |= x > 0 ? map.GetCell(x - 1, y).Tier < mapCellTier ? Directions.None : Directions.West : Directions.West;
            result |= x < map.Width - 1 ? map.GetCell(x + 1, y).Tier < mapCellTier ? Directions.None : Directions.East : Directions.East;

            result |= y > 0 && x > 0 ? map.GetCell(x - 1, y - 1).Tier < mapCellTier ? Directions.None : Directions.NorthWest : Directions.NorthWest;
            result |= y > 0 && x < map.Width - 1 ? map.GetCell(x + 1, y - 1).Tier < mapCellTier ? Directions.None : Directions.NorthEast : Directions.NorthEast;
            result |= y < map.Height - 1 && x > 0 ? map.GetCell(x - 1, y + 1).Tier < mapCellTier ? Directions.None : Directions.SouthWest : Directions.SouthWest;
            result |= y < map.Height - 1 && x < map.Width - 1 ? map.GetCell(x + 1, y + 1).Tier < mapCellTier ? Directions.None : Directions.SouthEast : Directions.SouthEast;

            return result;
        }
    }
}
