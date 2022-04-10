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

                writer.Write(tileset.CellPrototypes.Count);
                foreach (MapTilesetCellPrototype cellPrototype in tileset.CellPrototypes)
                {
                    writer.Write((int)cellPrototype.Tier);
                    writer.Write((int)cellPrototype.Type);

                    writer.Write(cellPrototype.TextureRegionName);
                }
                writer.Write(tileset.DecorationPrototypes.Count);
                foreach (MapTilesetDecorationPrototype decorationPrototype in tileset.DecorationPrototypes)
                {
                    writer.Write((int)decorationPrototype.Tier);
                    writer.Write((int)decorationPrototype.Type);

                    writer.Write(decorationPrototype.TextureRegionName);
                }
            }

            List<MapCell> cells = map.Cells;
            if (cells == null || cells.Count == 0 || cells.Count != map.Width * map.Height)
                throw new InvalidOperationException("The map is empty or its cell data is corrupted.");
            else
            {
                foreach (MapCell cell in cells)
                {
                    writer.Write((int)cell.Tier);
                    writer.Write((int)cell.Type);
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
