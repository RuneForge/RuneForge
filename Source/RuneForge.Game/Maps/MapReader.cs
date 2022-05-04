using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Buildings;
using RuneForge.Game.Components;
using RuneForge.Game.Components.Extensions;
using RuneForge.Game.Extensions;
using RuneForge.Game.Players;
using RuneForge.Game.Units;

namespace RuneForge.Game.Maps
{
    public class MapReader : ContentTypeReader<Map>
    {
        protected override Map Read(ContentReader reader, Map existingInstance)
        {
            string name = reader.ReadString();

            int width = reader.ReadInt32();
            int height = reader.ReadInt32();

            Guid humanPlayerId = reader.ReadGuid();
            Guid neutralPassivePlayerId = reader.ReadGuid();

            List<PlayerPrototype> playerPrototypes = ReadPlayers(reader);

            List<UnitInstancePrototype> unitInstancePrototypes = ReadUnitInstancePrototypes(reader, out Dictionary<string, UnitPrototype> unitPrototypes);
            List<BuildingInstancePrototype> buildingInstancePrototypes = ReadBuildingInstancePrototypes(reader, out Dictionary<string, BuildingPrototype> buildingPrototypes);

            Dictionary<string, MapDecorationPrototype> decorationPrototypes = ReadDecorationPrototypes(reader);

            MapTileset tileset = ReadTileset(reader, decorationPrototypes);

            List<MapLandscapeCell> landscapeCells = ReadLandscapeCellData(reader, width, height);
            List<MapDecorationCell> decorationCells = ReadDecorationCellData(reader, width, height);

            return new Map(
                name,
                width,
                height,
                humanPlayerId,
                neutralPassivePlayerId,
                tileset,
                landscapeCells,
                decorationCells,
                playerPrototypes,
                unitPrototypes.Values.ToArray(),
                buildingPrototypes.Values.ToArray(),
                unitInstancePrototypes,
                buildingInstancePrototypes
                );
        }

        private static List<PlayerPrototype> ReadPlayers(ContentReader reader)
        {
            int playerPrototypesCount = reader.ReadInt32();
            List<PlayerPrototype> playerPrototypes = new List<PlayerPrototype>();
            for (int i = 0; i < playerPrototypesCount; i++)
            {
                Guid id = reader.ReadGuid();
                string name = reader.ReadString();

                Color mainColor = reader.ReadColor();

                Color entityColorShadeA = reader.ReadColor();
                Color entityColorShadeB = reader.ReadColor();
                Color entityColorShadeC = reader.ReadColor();
                Color entityColorShadeD = reader.ReadColor();

                int componentPrototypesCount = reader.ReadInt32();
                List<ComponentPrototype> componentPrototypes = new List<ComponentPrototype>();
                for (int j = 0; j < componentPrototypesCount; j++)
                {
                    ComponentPrototype componentPrototype = reader.ReadComponentPrototype();
                    componentPrototypes.Add(componentPrototype);
                }

                PlayerColor color = new PlayerColor(mainColor, entityColorShadeA, entityColorShadeB, entityColorShadeC, entityColorShadeD);
                playerPrototypes.Add(new PlayerPrototype(id, name, color, componentPrototypes));
            }

            return playerPrototypes;
        }

        private static List<UnitInstancePrototype> ReadUnitInstancePrototypes(ContentReader reader, out Dictionary<string, UnitPrototype> unitPrototypes)
        {
            int unitInstancePrototypesCount = reader.ReadInt32();
            List<UnitInstancePrototype> unitInstancePrototypes = new List<UnitInstancePrototype>();
            unitPrototypes = new Dictionary<string, UnitPrototype>();
            for (int i = 0; i < unitInstancePrototypesCount; i++)
            {
                Guid ownerId = reader.ReadGuid();
                string entityPrototypeAssetName = reader.ReadString();
                if (!unitPrototypes.TryGetValue(entityPrototypeAssetName, out UnitPrototype unitPrototype))
                {
                    unitPrototype = reader.ContentManager.Load<UnitPrototype>(entityPrototypeAssetName);
                    unitPrototypes.Add(entityPrototypeAssetName, unitPrototype);
                }

                int componentPrototypeOverridesCount = reader.ReadInt32();
                List<ComponentPrototype> componentPrototypeOverrides = new List<ComponentPrototype>();
                for (int j = 0; j < componentPrototypeOverridesCount; j++)
                {
                    ComponentPrototype componentPrototypeOverride = reader.ReadComponentPrototype();
                    componentPrototypeOverrides.Add(componentPrototypeOverride);
                }

                unitInstancePrototypes.Add(new UnitInstancePrototype(ownerId, unitPrototype, componentPrototypeOverrides));
            }

            return unitInstancePrototypes;
        }
        private static List<BuildingInstancePrototype> ReadBuildingInstancePrototypes(ContentReader reader, out Dictionary<string, BuildingPrototype> buildingPrototypes)
        {
            int buildingInstancePrototypesCount = reader.ReadInt32();
            List<BuildingInstancePrototype> buildingInstancePrototypes = new List<BuildingInstancePrototype>();
            buildingPrototypes = new Dictionary<string, BuildingPrototype>();
            for (int i = 0; i < buildingInstancePrototypesCount; i++)
            {
                Guid ownerId = reader.ReadGuid();
                string entityPrototypeAssetName = reader.ReadString();
                if (!buildingPrototypes.TryGetValue(entityPrototypeAssetName, out BuildingPrototype buildingPrototype))
                {
                    buildingPrototype = reader.ContentManager.Load<BuildingPrototype>(entityPrototypeAssetName);
                    buildingPrototypes.Add(entityPrototypeAssetName, buildingPrototype);
                }

                int componentPrototypeOverridesCount = reader.ReadInt32();
                List<ComponentPrototype> componentPrototypeOverrides = new List<ComponentPrototype>();
                for (int j = 0; j < componentPrototypeOverridesCount; j++)
                {
                    ComponentPrototype componentPrototypeOverride = reader.ReadComponentPrototype();
                    componentPrototypeOverrides.Add(componentPrototypeOverride);
                }

                buildingInstancePrototypes.Add(new BuildingInstancePrototype(ownerId, buildingPrototype, componentPrototypeOverrides));
            }

            return buildingInstancePrototypes;
        }

        private static Dictionary<string, MapDecorationPrototype> ReadDecorationPrototypes(ContentReader reader)
        {
            int decorationPrototypesCount = reader.ReadInt32();
            Dictionary<string, MapDecorationPrototype> decorationPrototypes = new Dictionary<string, MapDecorationPrototype>();
            for (int i = 0; i < decorationPrototypesCount; i++)
            {
                string name = reader.ReadString();

                decorationPrototypes.Add(name, new MapDecorationPrototype(name));
            }

            return decorationPrototypes;
        }

        private static MapTileset ReadTileset(ContentReader reader, Dictionary<string, MapDecorationPrototype> decorationPrototypes)
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

                string decorationPrototypeName = reader.ReadString();
                MapDecorationPrototype decorationPrototype = decorationPrototypes[decorationPrototypeName];

                string textureRegionName = reader.ReadString();

                decorationCellPrototypes.Add((tier, type), new MapTilesetDecorationCellPrototype(movementFlags, buildingFlags, decorationPrototype, textureRegionName));
            }

            return new MapTileset(textureAtlasName, landscapeCellPrototypes, decorationCellPrototypes);
        }

        private static List<MapLandscapeCell> ReadLandscapeCellData(ContentReader reader, int width, int height)
        {
            int landscapeCellsCount = width * height;
            List<MapLandscapeCell> landscapeCells = new List<MapLandscapeCell>();
            for (int i = 0; i < landscapeCellsCount; i++)
            {
                MapLandscapeCellTier tier = (MapLandscapeCellTier)reader.ReadInt32();
                MapLandscapeCellTypes type = (MapLandscapeCellTypes)reader.ReadInt32();

                landscapeCells.Add(new MapLandscapeCell(tier, type));
            }

            return landscapeCells;
        }

        private static List<MapDecorationCell> ReadDecorationCellData(ContentReader reader, int width, int height)
        {
            int decorationCellsCount = width * height;
            List<MapDecorationCell> decorationCells = new List<MapDecorationCell>();
            for (int i = 0; i < decorationCellsCount; i++)
            {
                MapDecorationCellTier tier = (MapDecorationCellTier)reader.ReadInt32();
                MapDecorationCellTypes type = (MapDecorationCellTypes)reader.ReadInt32();

                decorationCells.Add(new MapDecorationCell(tier, type));
            }

            return decorationCells;
        }
    }
}
