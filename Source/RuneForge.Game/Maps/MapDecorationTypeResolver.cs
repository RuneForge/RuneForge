using System;
using System.Collections.Generic;

using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Game.Maps
{
    public class MapDecorationTypeResolver : IMapDecorationTypeResolver
    {
        private static readonly List<(MapDecorationTypes, Directions)> s_mapDecorationPatterns = new List<(MapDecorationTypes, Directions)>()
        {
            (MapDecorationTypes.Center, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast | Directions.SouthWest | Directions.SouthEast),
            (MapDecorationTypes.InnerCornerNorthWest, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthEast | Directions.SouthWest | Directions.SouthEast),
            (MapDecorationTypes.InnerCornerNorthEast, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.SouthWest | Directions.SouthEast),
            (MapDecorationTypes.InnerCornerSouthWest, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast | Directions.SouthEast),
            (MapDecorationTypes.InnerCornerSouthEast, Directions.North | Directions.South | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast | Directions.SouthWest),
            (MapDecorationTypes.EdgeNorth, Directions.South | Directions.West | Directions.East | Directions.SouthWest | Directions.SouthEast),
            (MapDecorationTypes.EdgeSouth, Directions.North | Directions.West | Directions.East | Directions.NorthWest | Directions.NorthEast),
            (MapDecorationTypes.EdgeWest, Directions.North | Directions.South | Directions.East | Directions.NorthEast | Directions.SouthEast),
            (MapDecorationTypes.EdgeEast, Directions.North | Directions.South | Directions.West | Directions.NorthWest | Directions.SouthWest),
            (MapDecorationTypes.OuterCornerNorthWest, Directions.South | Directions.East | Directions.SouthEast),
            (MapDecorationTypes.OuterCornerNorthEast, Directions.South | Directions.West | Directions.SouthWest),
            (MapDecorationTypes.OuterCornerSouthWest, Directions.North | Directions.East | Directions.NorthEast),
            (MapDecorationTypes.OuterCornerSouthEast, Directions.North | Directions.West | Directions.NorthWest),
        };

        public MapDecorationTypes ResolveMapDecorationType(int x, int y, Map map)
        {
            if (!TryResolveMapDecorationType(x, y, map, out MapDecorationTypes type))
                throw new InvalidOperationException("Unable to resolve the type of the decoration.");
            else
                return type;
        }

        public bool TryResolveMapDecorationType(int x, int y, Map map, out MapDecorationTypes type)
        {
            Directions adjacentCells = GetAdjacentCells(x, y, map);
            foreach ((MapDecorationTypes mapDecorationType, Directions mask) in s_mapDecorationPatterns)
            {
                if ((adjacentCells & mask) == mask)
                {
                    type = mapDecorationType;
                    return true;
                }
            }
            type = MapDecorationTypes.Center;
            return false;
        }

        private Directions GetAdjacentCells(int x, int y, Map map)
        {
            Directions result = Directions.None;
            MapDecorationTier mapDecorationTier = map.GetDecoration(x, y).Tier;

            result |= y > 0 ? map.GetDecoration(x, y - 1).Tier != mapDecorationTier ? Directions.None : Directions.North : Directions.North;
            result |= y < map.Height - 1 ? map.GetDecoration(x, y + 1).Tier != mapDecorationTier ? Directions.None : Directions.South : Directions.South;
            result |= x > 0 ? map.GetDecoration(x - 1, y).Tier != mapDecorationTier ? Directions.None : Directions.West : Directions.West;
            result |= x < map.Width - 1 ? map.GetDecoration(x + 1, y).Tier != mapDecorationTier ? Directions.None : Directions.East : Directions.East;

            result |= y > 0 && x > 0 ? map.GetDecoration(x - 1, y - 1).Tier != mapDecorationTier ? Directions.None : Directions.NorthWest : Directions.NorthWest;
            result |= y > 0 && x < map.Width - 1 ? map.GetDecoration(x + 1, y - 1).Tier != mapDecorationTier ? Directions.None : Directions.NorthEast : Directions.NorthEast;
            result |= y < map.Height - 1 && x > 0 ? map.GetDecoration(x - 1, y + 1).Tier != mapDecorationTier ? Directions.None : Directions.SouthWest : Directions.SouthWest;
            result |= y < map.Height - 1 && x < map.Width - 1 ? map.GetDecoration(x + 1, y + 1).Tier != mapDecorationTier ? Directions.None : Directions.SouthEast : Directions.SouthEast;

            return result;
        }
    }
}
