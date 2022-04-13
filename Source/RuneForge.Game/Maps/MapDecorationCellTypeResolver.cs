using System;
using System.Collections.Generic;

using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Game.Maps
{
    public class MapDecorationCellTypeResolver : IMapDecorationCellTypeResolver
    {
        private static readonly List<(MapDecorationCellTypes, Directions)> s_decorationCellPatterns = new List<(MapDecorationCellTypes, Directions)>()
        {
            (MapDecorationCellTypes.Center, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast | Directions.SouthWest | Directions.SouthEast),
            (MapDecorationCellTypes.InnerCornerNorthWest, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthEast | Directions.SouthWest | Directions.SouthEast),
            (MapDecorationCellTypes.InnerCornerNorthEast, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.SouthWest | Directions.SouthEast),
            (MapDecorationCellTypes.InnerCornerSouthWest, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast | Directions.SouthEast),
            (MapDecorationCellTypes.InnerCornerSouthEast, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast | Directions.SouthWest),
            (MapDecorationCellTypes.EdgeNorth, Directions.South | Directions.West | Directions.East | Directions.SouthWest | Directions.SouthEast),
            (MapDecorationCellTypes.EdgeSouth, Directions.North | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast),
            (MapDecorationCellTypes.EdgeWest, Directions.North | Directions.South | Directions.East | Directions.NorthEast | Directions.SouthEast),
            (MapDecorationCellTypes.EdgeEast, Directions.North | Directions.South | Directions.West | Directions.NorthWest | Directions.SouthWest),
            (MapDecorationCellTypes.OuterCornerNorthWest, Directions.South | Directions.East | Directions.SouthEast),
            (MapDecorationCellTypes.OuterCornerNorthEast, Directions.South | Directions.West | Directions.SouthWest),
            (MapDecorationCellTypes.OuterCornerSouthWest, Directions.North | Directions.East | Directions.NorthEast),
            (MapDecorationCellTypes.OuterCornerSouthEast, Directions.North | Directions.West | Directions.NorthWest),
        };

        public MapDecorationCellTypes ResolveDecorationCellType(int x, int y, Map map)
        {
            if (!TryResolveDecorationCellType(x, y, map, out MapDecorationCellTypes type))
                throw new InvalidOperationException("Unable to resolve the type of the decoration.");
            else
                return type;
        }

        public bool TryResolveDecorationCellType(int x, int y, Map map, out MapDecorationCellTypes type)
        {
            Directions adjacentDecorationCells = GetAdjacentDecorationCells(x, y, map);
            foreach ((MapDecorationCellTypes decorationCellType, Directions mask) in s_decorationCellPatterns)
            {
                if ((adjacentDecorationCells & mask) == mask)
                {
                    type = decorationCellType;
                    return true;
                }
            }
            type = MapDecorationCellTypes.Center;
            return false;
        }

        private Directions GetAdjacentDecorationCells(int x, int y, Map map)
        {
            Directions result = Directions.None;
            MapDecorationCellTier decorationCellTier = map.GetDecorationCell(x, y).Tier;

            result |= y > 0 ? map.GetDecorationCell(x, y - 1).Tier != decorationCellTier ? Directions.None : Directions.North : Directions.North;
            result |= y < map.Height - 1 ? map.GetDecorationCell(x, y + 1).Tier != decorationCellTier ? Directions.None : Directions.South : Directions.South;
            result |= x > 0 ? map.GetDecorationCell(x - 1, y).Tier != decorationCellTier ? Directions.None : Directions.West : Directions.West;
            result |= x < map.Width - 1 ? map.GetDecorationCell(x + 1, y).Tier != decorationCellTier ? Directions.None : Directions.East : Directions.East;

            result |= y > 0 && x > 0 ? map.GetDecorationCell(x - 1, y - 1).Tier != decorationCellTier ? Directions.None : Directions.NorthWest : Directions.NorthWest;
            result |= y > 0 && x < map.Width - 1 ? map.GetDecorationCell(x + 1, y - 1).Tier != decorationCellTier ? Directions.None : Directions.NorthEast : Directions.NorthEast;
            result |= y < map.Height - 1 && x > 0 ? map.GetDecorationCell(x - 1, y + 1).Tier != decorationCellTier ? Directions.None : Directions.SouthWest : Directions.SouthWest;
            result |= y < map.Height - 1 && x < map.Width - 1 ? map.GetDecorationCell(x + 1, y + 1).Tier != decorationCellTier ? Directions.None : Directions.SouthEast : Directions.SouthEast;

            return result;
        }
    }
}
