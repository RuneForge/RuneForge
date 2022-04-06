using System.Collections.Generic;
using System.Linq;

using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Game.Maps
{
    public class Map
    {
        public const int CellWidth = 32;
        public const int CellHeight = 32;

        private readonly MapCell[] m_cells;

        public string Name { get; }

        public int Width { get; }
        public int Height { get; }

        public MapTileset Tileset { get; }

        public Map(string name, int width, int height, MapTileset tileset, IList<MapCell> cells)
        {
            m_cells = cells.ToArray();

            Name = name;

            Width = width;
            Height = height;

            Tileset = tileset;
        }

        public void ResolveMapCellTypes(IMapCellTypeResolver mapCellTypeResolver)
        {
            for (int i = 0; i < m_cells.Length; i++)
            {
                GetCoordinatesByIndex(i, out int x, out int y);
                if (mapCellTypeResolver.TryResolveMapCellType(x, y, this, out MapCellType type))
                    SetCell(x, y, new MapCell(m_cells[i].Tier, type));
            }
        }

        public MapCell GetCell(int x, int y)
        {
            return m_cells[GetIndexByCoordinates(x, y)];
        }

        private void SetCell(int x, int y, MapCell mapCell)
        {
            m_cells[GetIndexByCoordinates(x, y)] = mapCell;
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
