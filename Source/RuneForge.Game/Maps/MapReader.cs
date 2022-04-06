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
                MapCellType type = (MapCellType)reader.ReadInt32();

                cells.Add(new MapCell(tier, type));
            }

            return new Map(mapAssetName, width, height, tileset, cells);
        }

        private MapTileset ReadTileset(ContentReader reader)
        {
            string textureAtlasName = reader.ReadString();

            int cellPrototypesCount = reader.ReadInt32();
            Dictionary<(MapCellTier, MapCellType), MapTilesetCellPrototype> cellPrototypes = new Dictionary<(MapCellTier, MapCellType), MapTilesetCellPrototype>();
            for (int i = 0; i < cellPrototypesCount; i++)
            {
                MapCellTier tier = (MapCellTier)reader.ReadInt32();
                MapCellType type = (MapCellType)reader.ReadInt32();

                string textureRegionName = reader.ReadString();

                cellPrototypes.Add((tier, type), new MapTilesetCellPrototype()
                {
                    TextureRegionName = textureRegionName,
                });
            }

            return new MapTileset(textureAtlasName, cellPrototypes);
        }
    }
}
