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

            int cellsCount = width * height;
            List<MapCell> cells = new List<MapCell>();
            for (int i = 0; i < cellsCount; i++)
            {
                MapCellTier tier = (MapCellTier)reader.ReadInt32();
                MapCellTypes type = (MapCellTypes)reader.ReadInt32();

                cells.Add(new MapCell(tier, type));
            }
            int decorationsCount = width * height;
            List<MapDecoration> decorations = new List<MapDecoration>();
            for (int i = 0; i < decorationsCount; i++)
            {
                MapDecorationTier tier = (MapDecorationTier)reader.ReadInt32();
                MapDecorationTypes type = (MapDecorationTypes)reader.ReadInt32();

                decorations.Add(new MapDecoration(tier, type));
            }

            return new Map(mapAssetName, width, height, tileset, cells, decorations);
        }

        private MapTileset ReadTileset(ContentReader reader)
        {
            string textureAtlasName = reader.ReadString();

            int cellPrototypesCount = reader.ReadInt32();
            Dictionary<(MapCellTier, MapCellTypes), MapTilesetCellPrototype> cellPrototypes = new Dictionary<(MapCellTier, MapCellTypes), MapTilesetCellPrototype>();
            for (int i = 0; i < cellPrototypesCount; i++)
            {
                MapCellTier tier = (MapCellTier)reader.ReadInt32();
                MapCellTypes type = (MapCellTypes)reader.ReadInt32();

                MapCellMovementFlags movementFlags = (MapCellMovementFlags)reader.ReadInt32();
                MapCellBuildingFlags buildingFlags = (MapCellBuildingFlags)reader.ReadInt32();

                string textureRegionName = reader.ReadString();

                cellPrototypes.Add((tier, type), new MapTilesetCellPrototype(movementFlags, buildingFlags, textureRegionName));
            }
            int decorationPrototypesCount = reader.ReadInt32();
            Dictionary<(MapDecorationTier, MapDecorationTypes), MapTilesetDecorationPrototype> decorationPrototypes = new Dictionary<(MapDecorationTier, MapDecorationTypes), MapTilesetDecorationPrototype>();
            for (int i = 0; i < decorationPrototypesCount; i++)
            {
                MapDecorationTier tier = (MapDecorationTier)reader.ReadInt32();
                MapDecorationTypes type = (MapDecorationTypes)reader.ReadInt32();

                MapDecorationMovementFlags movementFlags = (MapDecorationMovementFlags)reader.ReadInt32();
                MapDecorationBuildingFlags buildingFlags = (MapDecorationBuildingFlags)reader.ReadInt32();

                string textureRegionName = reader.ReadString();

                decorationPrototypes.Add((tier, type), new MapTilesetDecorationPrototype(movementFlags, buildingFlags, textureRegionName));
            }

            return new MapTileset(textureAtlasName, cellPrototypes, decorationPrototypes);
        }
    }
}
