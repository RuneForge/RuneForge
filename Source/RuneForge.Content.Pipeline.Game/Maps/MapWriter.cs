using System;
using System.Collections.Generic;
using System.Globalization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Players;

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
            writer.Write(map.Width);
            writer.Write(map.Height);

            List<Player> players = map.Players;
            if (players == null || players.Count == 0)
                throw new InvalidOperationException("The map should have at least 1 player.");
            else
            {
                writer.Write(players.Count);
                foreach (Player player in players)
                {
                    writer.Write(player.Id.ToByteArray());
                    writer.Write(player.Name);

                    PlayerColor playerColor = player.Color;
                    if (playerColor == null)
                        throw new InvalidOperationException("Each player should have an assigned color.");
                    else
                    {
                        writer.Write(new Color(uint.Parse(player.Color.MainColor, NumberStyles.HexNumber)));

                        writer.Write(new Color(uint.Parse(player.Color.EntityColorShadeA, NumberStyles.HexNumber)));
                        writer.Write(new Color(uint.Parse(player.Color.EntityColorShadeB, NumberStyles.HexNumber)));
                        writer.Write(new Color(uint.Parse(player.Color.EntityColorShadeC, NumberStyles.HexNumber)));
                        writer.Write(new Color(uint.Parse(player.Color.EntityColorShadeD, NumberStyles.HexNumber)));
                    }
                }
            }

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

                    writer.Write((int)landscapeCellPrototype.BuildingFlags);
                    writer.Write((int)landscapeCellPrototype.MovementFlags);

                    writer.Write(landscapeCellPrototype.TextureRegionName);
                }
                writer.Write(tileset.DecorationCellPrototypes.Count);
                foreach (MapTilesetDecorationCellPrototype decorationCellPrototype in tileset.DecorationCellPrototypes)
                {
                    writer.Write((int)decorationCellPrototype.Tier);
                    writer.Write((int)decorationCellPrototype.Type);

                    writer.Write((int)decorationCellPrototype.BuildingFlags);
                    writer.Write((int)decorationCellPrototype.MovementFlags);

                    writer.Write(decorationCellPrototype.PrototypeName);

                    writer.Write(decorationCellPrototype.TextureRegionName);
                }
            }

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
