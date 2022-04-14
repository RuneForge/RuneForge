using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using RuneForge.Game.Maps.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Units;

namespace RuneForge.Game.Maps
{
    public class Map
    {
        public const int CellWidth = 32;
        public const int CellHeight = 32;

        private readonly MapLandscapeCell[] m_landscapeCells;
        private readonly MapDecorationCell[] m_decorationCells;

        public string Name { get; }

        public int Width { get; }
        public int Height { get; }

        public MapTileset Tileset { get; }

        public ReadOnlyCollection<PlayerPrototype> PlayerPrototypes { get; }

        public ReadOnlyCollection<UnitInstancePrototype> UnitInstancePrototypes { get; }

        public Map(
            string name,
            int width,
            int height,
            MapTileset tileset,
            IList<MapLandscapeCell> landscapeCells,
            IList<MapDecorationCell> decorationCells,
            IList<PlayerPrototype> playerPrototypes,
            IList<UnitInstancePrototype> unitInstancePrototypes
            )
        {
            m_landscapeCells = landscapeCells.ToArray();
            m_decorationCells = decorationCells.ToArray();

            Name = name;
            Width = width;
            Height = height;
            Tileset = tileset;
            PlayerPrototypes = new ReadOnlyCollection<PlayerPrototype>(playerPrototypes);
            UnitInstancePrototypes = new ReadOnlyCollection<UnitInstancePrototype>(unitInstancePrototypes);
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
        public void ResolveDecorationCellTypes(IMapDecorationCellTypeResolver decorationCellTypeResolver)
        {
            for (int i = 0; i < m_landscapeCells.Length; i++)
            {
                GetCoordinatesByIndex(i, out int x, out int y);
                if (!decorationCellTypeResolver.TryResolveDecorationCellType(x, y, this, out MapDecorationCellTypes type))
                    type = MapDecorationCellTypes.Destroyed;
                SetDecorationCell(x, y, new MapDecorationCell(m_decorationCells[i].Tier, type));
            }
        }
        public void CreateMapDecorations(IMapDecorationFactory mapDecorationFactory, IMapDecorationService mapDecorationService)
        {
            for (int i = 0; i < m_landscapeCells.Length; i++)
            {
                GetCoordinatesByIndex(i, out int x, out int y);
                MapDecorationCell cell = GetDecorationCell(x, y);
                if (Tileset.TryGetDecorationCellPrototype(cell.Tier, cell.Type, out MapTilesetDecorationCellPrototype cellPrototype))
                {
                    MapDecorationPrototype decorationPrototype = cellPrototype.EntityPrototype;
                    MapDecoration decoration = mapDecorationFactory.CreateFromPrototype(x, y, decorationPrototype);
                    mapDecorationService.AddMapDecoration(decoration);
                    SetDecorationCell(x, y, new MapDecorationCell(cell.Tier, cell.Type, decoration));
                }
            }
        }

        public MapLandscapeCell GetLandscapeCell(int x, int y)
        {
            return m_landscapeCells[GetIndexByCoordinates(x, y)];
        }
        public MapDecorationCell GetDecorationCell(int x, int y)
        {
            return m_decorationCells[GetIndexByCoordinates(x, y)];
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
        public MapDecorationCellMovementFlags GetDecorationCellMovementFlags(int x, int y)
        {
            MapDecorationCell cell = GetDecorationCell(x, y);
            MapTilesetDecorationCellPrototype cellPrototype = Tileset.GetDecorationCellPrototype(cell.Tier, cell.Type);
            return cellPrototype.MovementFlags;
        }
        public MapDecorationCellBuildingFlags GetDecorationCellBuildingFlags(int x, int y)
        {
            MapDecorationCell cell = GetDecorationCell(x, y);
            MapTilesetDecorationCellPrototype cellPrototype = Tileset.GetDecorationCellPrototype(cell.Tier, cell.Type);
            return cellPrototype.BuildingFlags;
        }

        private void SetLandscapeCell(int x, int y, MapLandscapeCell cell)
        {
            m_landscapeCells[GetIndexByCoordinates(x, y)] = cell;
        }
        private void SetDecorationCell(int x, int y, MapDecorationCell cell)
        {
            m_decorationCells[GetIndexByCoordinates(x, y)] = cell;
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
