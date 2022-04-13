using System.Collections.Generic;
using System.Linq;

using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Game.Maps
{
    public class Map
    {
        public const int CellWidth = 32;
        public const int CellHeight = 32;

        private readonly MapLandscapeCell[] m_landscapeCells;
        private readonly MapDecoration[] m_decorations;

        public string Name { get; }

        public int Width { get; }
        public int Height { get; }

        public MapTileset Tileset { get; }

        public Map(string name, int width, int height, MapTileset tileset, IList<MapLandscapeCell> landscapeCells, IList<MapDecoration> decorations)
        {
            m_landscapeCells = landscapeCells.ToArray();
            m_decorations = decorations.ToArray();

            Name = name;

            Width = width;
            Height = height;

            Tileset = tileset;
        }

        public void ResolveLandscapeCellTypes(IMapLandscapeCellTypeResolver landscapeCellTypeResolver)
        {
            for (int i = 0; i < m_landscapeCells.Length; i++)
            {
                GetCoordinatesByIndex(i, out int x, out int y);
                if (landscapeCellTypeResolver.TryResolveLandscapeCellType(x, y, this, out MapLandscapeCellTypes type))
                    SetLandscapeCell(x, y, new MapLandscapeCell(m_landscapeCells[i].Tier, type));
            }
        }
        public void ResolveMapDecorationTypes(IMapDecorationTypeResolver mapDecorationTypeResolver)
        {
            for (int i = 0; i < m_landscapeCells.Length; i++)
            {
                GetCoordinatesByIndex(i, out int x, out int y);
                if (!mapDecorationTypeResolver.TryResolveMapDecorationType(x, y, this, out MapDecorationTypes type))
                    type = MapDecorationTypes.Destroyed;
                SetDecoration(x, y, new MapDecoration(m_decorations[i].Tier, type));
            }
        }

        public MapLandscapeCell GetLandscapeCell(int x, int y)
        {
            return m_landscapeCells[GetIndexByCoordinates(x, y)];
        }
        public MapDecoration GetDecoration(int x, int y)
        {
            return m_decorations[GetIndexByCoordinates(x, y)];
        }

        public MapLandscapeCellMovementFlags GetLandscapeCellMovementFlags(int x, int y)
        {
            MapLandscapeCell cell = GetLandscapeCell(x, y);
            MapTilesetLandscapeCellPrototype cellPrototype = Tileset.GetLandscapeCellPrototype(cell.Tier, cell.Type);
            return cellPrototype.MovementFlags;
        }
        public MapLandscapeCellBuildingFlags GetLandscapeCellBuildingFlags(int x, int y)
        {
            MapLandscapeCell cell = GetLandscapeCell(x, y);
            MapTilesetLandscapeCellPrototype cellPrototype = Tileset.GetLandscapeCellPrototype(cell.Tier, cell.Type);
            return cellPrototype.BuildingFlags;
        }
        public MapDecorationMovementFlags GetDecorationMovementFlags(int x, int y)
        {
            MapDecoration decoration = GetDecoration(x, y);
            MapTilesetDecorationPrototype decorationPrototype = Tileset.GetDecorationPrototype(decoration.Tier, decoration.Type);
            return decorationPrototype.MovementFlags;
        }
        public MapDecorationBuildingFlags GetDecorationBuildingFlags(int x, int y)
        {
            MapDecoration decoration = GetDecoration(x, y);
            MapTilesetDecorationPrototype decorationPrototype = Tileset.GetDecorationPrototype(decoration.Tier, decoration.Type);
            return decorationPrototype.BuildingFlags;
        }

        private void SetLandscapeCell(int x, int y, MapLandscapeCell mapCell)
        {
            m_landscapeCells[GetIndexByCoordinates(x, y)] = mapCell;
        }
        private void SetDecoration(int x, int y, MapDecoration mapDecoration)
        {
            m_decorations[GetIndexByCoordinates(x, y)] = mapDecoration;
        }

        private void GetCoordinatesByIndex(int index, out int x, out int y)
        {
            (x, y) = (index % Width, index / Width);
        }
        private int GetIndexByCoordinates(int x, int y)
        {
            return x + (y * Width);
        }
    }
}
