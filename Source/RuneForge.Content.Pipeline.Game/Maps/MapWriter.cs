using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

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
                writer.Write(tileset.DecorationPrototypes.Count);
                foreach (MapTilesetDecorationPrototype decorationPrototype in tileset.DecorationPrototypes)
                {
                    writer.Write((int)decorationPrototype.Tier);
                    writer.Write((int)decorationPrototype.Type);

                    writer.Write((int)decorationPrototype.BuildingFlags);
                    writer.Write((int)decorationPrototype.MovementFlags);

                    writer.Write(decorationPrototype.TextureRegionName);
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
            List<MapDecoration> decorations = map.Decorations;
            if (decorations == null || decorations.Count == 0 || decorations.Count != map.Width * map.Height)
                throw new InvalidOperationException("The map is empty or its decoration data is corrupted.");
            else
            {
                foreach (MapDecoration decoration in decorations)
                {
                    writer.Write((int)decoration.Tier);
                    writer.Write((int)decoration.Type);
                }
            }
        }
    }
}
