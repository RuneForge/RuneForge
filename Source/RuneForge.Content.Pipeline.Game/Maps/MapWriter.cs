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
        }
    }
}
