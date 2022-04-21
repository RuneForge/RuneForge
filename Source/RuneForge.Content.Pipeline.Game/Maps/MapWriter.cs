using System;
using System.Collections.Generic;
using System.Globalization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Buildings;
using RuneForge.Content.Pipeline.Game.Components;
using RuneForge.Content.Pipeline.Game.Components.Extensions;
using RuneForge.Content.Pipeline.Game.Extensions;
using RuneForge.Content.Pipeline.Game.Players;
using RuneForge.Content.Pipeline.Game.Units;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    [ContentTypeWriter]
    public class MapWriter : ContentTypeWriter<Map>
    {
        private const string c_runtimeReaderTypeName = "RuneForge.Game.Maps.MapReader, RuneForge.Game";

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return c_runtimeReaderTypeName;
        }

        protected override void Write(ContentWriter writer, Map map)
        {
            writer.Write(map.Name);

            writer.Write(map.Width);
            writer.Write(map.Height);

            WritePlayerPrototypes(writer, map);

            WriteUnitInstancePrototypes(writer, map);
            WriteBuildingInstancePrototypes(writer, map);

            WriteDecorationPrototypes(writer, map);

            WriteTileset(writer, map);

            WriteLandscapeCellData(writer, map);
            WriteDecorationCellData(writer, map);
        }

        private static void WritePlayerPrototypes(ContentWriter writer, Map map)
        {
            List<PlayerPrototype> playerPrototypes = map.PlayerPrototypes;
            if (playerPrototypes == null || playerPrototypes.Count == 0)
                throw new InvalidOperationException("The map should have at least 1 player prototype.");
            else
            {
                writer.Write(playerPrototypes.Count);
                foreach (PlayerPrototype playerPrototype in playerPrototypes)
                {
                    writer.Write(playerPrototype.Id);
                    writer.Write(playerPrototype.Name);

                    PlayerColor playerColor = playerPrototype.Color;
                    if (playerColor == null)
                        throw new InvalidOperationException("Each player prototype should have an assigned color.");
                    else
                    {
                        writer.Write(new Color(uint.Parse(playerPrototype.Color.MainColor, NumberStyles.HexNumber)));

                        writer.Write(new Color(uint.Parse(playerPrototype.Color.EntityColorShadeA, NumberStyles.HexNumber)));
                        writer.Write(new Color(uint.Parse(playerPrototype.Color.EntityColorShadeB, NumberStyles.HexNumber)));
                        writer.Write(new Color(uint.Parse(playerPrototype.Color.EntityColorShadeC, NumberStyles.HexNumber)));
                        writer.Write(new Color(uint.Parse(playerPrototype.Color.EntityColorShadeD, NumberStyles.HexNumber)));
                    }
                }
            }
        }

        private static void WriteUnitInstancePrototypes(ContentWriter writer, Map map)
        {
            List<UnitInstancePrototype> unitInstancePrototypes = map.UnitInstancePrototypes;
            if (unitInstancePrototypes == null)
                throw new InvalidOperationException("The map should have at least an empty list of unit instance prototypes.");
            else
            {
                writer.Write(unitInstancePrototypes.Count);
                foreach (UnitInstancePrototype unitInstancePrototype in unitInstancePrototypes)
                {
                    writer.Write(unitInstancePrototype.OwnerId);
                    writer.Write(unitInstancePrototype.EntityPrototypeAssetName);

                    List<ComponentPrototype> componentPrototypeOverrides = unitInstancePrototype.ComponentPrototypeOverrides;
                    if (componentPrototypeOverrides == null)
                        throw new InvalidOperationException("The unit instance prototype should have at least an empty list of component prototype overrides.");
                    else
                    {
                        writer.Write(componentPrototypeOverrides.Count);
                        foreach (ComponentPrototype componentPrototypeOverride in componentPrototypeOverrides)
                            writer.Write(componentPrototypeOverride);
                    }
                }
            }
        }

        private static void WriteBuildingInstancePrototypes(ContentWriter writer, Map map)
        {
            List<BuildingInstancePrototype> buildingInstancePrototypes = map.BuildingInstancePrototypes;
            if (buildingInstancePrototypes == null)
                throw new InvalidOperationException("The map should have at least an empty list of building instance prototypes.");
            else
            {
                writer.Write(buildingInstancePrototypes.Count);
                foreach (BuildingInstancePrototype buildingInstancePrototype in buildingInstancePrototypes)
                {
                    writer.Write(buildingInstancePrototype.OwnerId);
                    writer.Write(buildingInstancePrototype.EntityPrototypeAssetName);

                    List<ComponentPrototype> componentPrototypeOverrides = buildingInstancePrototype.ComponentPrototypeOverrides;
                    if (componentPrototypeOverrides == null)
                        throw new InvalidOperationException("The building instance prototype should have at least an empty list of component prototype overrides.");
                    else
                    {
                        writer.Write(componentPrototypeOverrides.Count);
                        foreach (ComponentPrototype componentPrototypeOverride in componentPrototypeOverrides)
                            writer.Write(componentPrototypeOverride);
                    }
                }
            }
        }

        private static void WriteDecorationPrototypes(ContentWriter writer, Map map)
        {
            List<MapDecorationPrototype> decorationPrototypes = map.DecorationPrototypes;
            if (decorationPrototypes == null)
                throw new InvalidOperationException("The map should have at least an empty list of decoration prototypes.");
            else
            {
                writer.Write(decorationPrototypes.Count);
                foreach (MapDecorationPrototype decorationPrototype in decorationPrototypes)
                {
                    writer.Write(decorationPrototype.Name);
                }
            }
        }

        private static void WriteTileset(ContentWriter writer, Map map)
        {
            MapTileset tileset = map.Tileset;
            if (tileset == null)
                throw new InvalidOperationException("The map has no associated tileset.");
            else
            {
                writer.Write(tileset.TextureAtlasName);

                writer.Write(tileset.LandscapeCellPrototypes.Count);
                foreach (MapTilesetLandscapeCellPrototype landscapeCellPrototype in tileset.LandscapeCellPrototypes)
                {
                    writer.Write((int)landscapeCellPrototype.Tier);
                    writer.Write((int)landscapeCellPrototype.Type);

                    writer.Write((int)landscapeCellPrototype.MovementFlags);
                    writer.Write((int)landscapeCellPrototype.BuildingFlags);

                    writer.Write(landscapeCellPrototype.TextureRegionName);
                }
                writer.Write(tileset.DecorationCellPrototypes.Count);
                foreach (MapTilesetDecorationCellPrototype decorationCellPrototype in tileset.DecorationCellPrototypes)
                {
                    writer.Write((int)decorationCellPrototype.Tier);
                    writer.Write((int)decorationCellPrototype.Type);

                    writer.Write((int)decorationCellPrototype.MovementFlags);
                    writer.Write((int)decorationCellPrototype.BuildingFlags);

                    writer.Write(decorationCellPrototype.PrototypeName);

                    writer.Write(decorationCellPrototype.TextureRegionName);
                }
            }
        }

        private static void WriteLandscapeCellData(ContentWriter writer, Map map)
        {
            List<MapLandscapeCell> landscapeCells = map.LandscapeCells;
            if (landscapeCells == null || landscapeCells.Count == 0 || landscapeCells.Count != map.Width * map.Height)
                throw new InvalidOperationException("The map is empty or its landscape cell data is corrupted.");
            else
            {
                foreach (MapLandscapeCell landscapeCell in landscapeCells)
                {
                    writer.Write((int)landscapeCell.Tier);
                    writer.Write((int)landscapeCell.Type);
                }
            }
        }

        private static void WriteDecorationCellData(ContentWriter writer, Map map)
        {
            List<MapDecorationCell> decorationCells = map.DecorationCells;
            if (decorationCells == null || decorationCells.Count == 0 || decorationCells.Count != map.Width * map.Height)
                throw new InvalidOperationException("The map is empty or its decoration cell data is corrupted.");
            else
            {
                foreach (MapDecorationCell decorationCell in decorationCells)
                {
                    writer.Write((int)decorationCell.Tier);
                    writer.Write((int)decorationCell.Type);
                }
            }
        }
    }
}
