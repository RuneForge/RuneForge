using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;

namespace RuneForge.Game.Maps
{
    public class MapReader : ContentTypeReader<Map>
    {
        protected override Map Read(ContentReader reader, Map existingInstance)
        {
            string mapAssetName = reader.AssetName;

            int width = reader.ReadInt32();
            int height = reader.ReadInt32();

            MapTileset tileset = ReadTileset(reader);

            int landscapeCellsCount = width * height;
            List<MapLandscapeCell> landscapeCells = new List<MapLandscapeCell>();
            for (int i = 0; i < landscapeCellsCount; i++)
            {
                MapLandscapeCellTier tier = (MapLandscapeCellTier)reader.ReadInt32();
                MapLandscapeCellTypes type = (MapLandscapeCellTypes)reader.ReadInt32();

                landscapeCells.Add(new MapLandscapeCell(tier, type));
            }
            int decorationCellsCount = width * height;
            List<MapDecorationCell> decorationCells = new List<MapDecorationCell>();
            for (int i = 0; i < decorationCellsCount; i++)
            {
                MapDecorationCellTier tier = (MapDecorationCellTier)reader.ReadInt32();
                MapDecorationCellTypes type = (MapDecorationCellTypes)reader.ReadInt32();

                decorationCells.Add(new MapDecorationCell(tier, type));
            }

            return new Map(mapAssetName, width, height, tileset, landscapeCells, decorationCells);
        }

        private MapTileset ReadTileset(ContentReader reader)
        {
            string textureAtlasName = reader.ReadString();

            int landscapeCellPrototypesCount = reader.ReadInt32();
            Dictionary<(MapLandscapeCellTier, MapLandscapeCellTypes), MapTilesetLandscapeCellPrototype> landscapeCellPrototypes = new Dictionary<(MapLandscapeCellTier, MapLandscapeCellTypes), MapTilesetLandscapeCellPrototype>();
            for (int i = 0; i < landscapeCellPrototypesCount; i++)
            {
                MapLandscapeCellTier tier = (MapLandscapeCellTier)reader.ReadInt32();
                MapLandscapeCellTypes type = (MapLandscapeCellTypes)reader.ReadInt32();

                MapLandscapeCellMovementFlags movementFlags = (MapLandscapeCellMovementFlags)reader.ReadInt32();
                MapLandscapeCellBuildingFlags buildingFlags = (MapLandscapeCellBuildingFlags)reader.ReadInt32();

                string textureRegionName = reader.ReadString();

                landscapeCellPrototypes.Add((tier, type), new MapTilesetLandscapeCellPrototype(movementFlags, buildingFlags, textureRegionName));
            }
            int decorationCellPrototypesCount = reader.ReadInt32();
            Dictionary<(MapDecorationCellTier, MapDecorationCellTypes), MapTilesetDecorationCellPrototype> decorationCellPrototypes = new Dictionary<(MapDecorationCellTier, MapDecorationCellTypes), MapTilesetDecorationCellPrototype>();
            for (int i = 0; i < decorationCellPrototypesCount; i++)
            {
                MapDecorationCellTier tier = (MapDecorationCellTier)reader.ReadInt32();
                MapDecorationCellTypes type = (MapDecorationCellTypes)reader.ReadInt32();

                MapDecorationCellMovementFlags movementFlags = (MapDecorationCellMovementFlags)reader.ReadInt32();
                MapDecorationCellBuildingFlags buildingFlags = (MapDecorationCellBuildingFlags)reader.ReadInt32();

                string textureRegionName = reader.ReadString();

                decorationCellPrototypes.Add((tier, type), new MapTilesetDecorationCellPrototype(movementFlags, buildingFlags, textureRegionName));
            }

            return new MapTileset(textureAtlasName, landscapeCellPrototypes, decorationCellPrototypes);
        }
    }
}
