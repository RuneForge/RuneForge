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
            int decorationsCount = width * height;
            List<MapDecoration> decorations = new List<MapDecoration>();
            for (int i = 0; i < decorationsCount; i++)
            {
                MapDecorationTier tier = (MapDecorationTier)reader.ReadInt32();
                MapDecorationTypes type = (MapDecorationTypes)reader.ReadInt32();

                decorations.Add(new MapDecoration(tier, type));
            }

            return new Map(mapAssetName, width, height, tileset, landscapeCells, decorations);
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

            return new MapTileset(textureAtlasName, landscapeCellPrototypes, decorationPrototypes);
        }
    }
}
