using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

using RuneForge.Game.Maps;

using Map = RuneForge.Content.Pipeline.Game.Maps.Map;
using MapDecorationCell = RuneForge.Content.Pipeline.Game.Maps.MapDecorationCell;
using MapLandscapeCell = RuneForge.Content.Pipeline.Game.Maps.MapLandscapeCell;
using MapTileset = RuneForge.Content.Pipeline.Game.Maps.MapTileset;
using MapTilesetDecorationCellPrototype = RuneForge.Content.Pipeline.Game.Maps.MapTilesetDecorationCellPrototype;
using MapTilesetLandscapeCellPrototype = RuneForge.Content.Pipeline.Game.Maps.MapTilesetLandscapeCellPrototype;

namespace RuneForge.Tools.MapConverter
{
    internal class Program
    {
        private static readonly int s_grassColorArgb = Color.Green.ToArgb();
        private static readonly int s_mudColorArgb = Color.Yellow.ToArgb();
        private static readonly int s_waterColorArgb = Color.Blue.ToArgb();
        private static readonly int s_treesColorArgb = Color.DarkGreen.ToArgb();
        private static readonly int s_rocksColorArgb = Color.DarkKhaki.ToArgb();

        private static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 1)
                throw new InvalidOperationException("The program requires one and only argument providing a name of the source file.");

            string sourceFileName = args.Single();

            Bitmap bitmap;
            using (FileStream fileStream = new FileStream(sourceFileName, FileMode.Open))
                bitmap = new Bitmap(Image.FromStream(fileStream));

            int width = bitmap.Width;
            int height = bitmap.Height;

            MapLandscapeCell[] landscapeCells = new MapLandscapeCell[width * height];
            MapDecorationCell[] decorationCells = new MapDecorationCell[width * height];

            for (int i = 0; i < width * height; i++)
            {
                int x = i % width;
                int y = i / width;

                int colorArgb = bitmap.GetPixel(x, y).ToArgb();

                MapLandscapeCellTier landscapeCellTier = MapLandscapeCellTier.None;
                MapDecorationCellTier decorationCellTier = MapDecorationCellTier.None;
                if (colorArgb == s_grassColorArgb)
                    landscapeCellTier = MapLandscapeCellTier.Grass;
                if (colorArgb == s_mudColorArgb)
                    landscapeCellTier = MapLandscapeCellTier.Mud;
                if (colorArgb == s_waterColorArgb)
                    landscapeCellTier = MapLandscapeCellTier.Water;
                if (colorArgb == s_treesColorArgb)
                    (landscapeCellTier, decorationCellTier) = (MapLandscapeCellTier.Grass, MapDecorationCellTier.Trees);
                if (colorArgb == s_rocksColorArgb)
                    (landscapeCellTier, decorationCellTier) = (MapLandscapeCellTier.Mud, MapDecorationCellTier.Rocks);

                landscapeCells[i] = new MapLandscapeCell() { Tier = landscapeCellTier, Type = MapLandscapeCellTypes.Center };
                decorationCells[i] = new MapDecorationCell() { Tier = decorationCellTier, Type = MapDecorationCellTypes.Center };
            }

            Map map = new Map()
            {
                Width = width,
                Height = height,
                Tileset = new MapTileset()
                {
                    TextureAtlasName = "TextureAtlasName",
                    LandscapeCellPrototypes = new List<MapTilesetLandscapeCellPrototype>()
                    {
                        new MapTilesetLandscapeCellPrototype() { Tier = MapLandscapeCellTier.Grass, Type = MapLandscapeCellTypes.Center, TextureRegionName = "TextureRegionName" },
                    },
                    DecorationCellPrototypes = new List<MapTilesetDecorationCellPrototype>()
                    {
                        new MapTilesetDecorationCellPrototype() { Tier = MapDecorationCellTier.Trees, Type = MapDecorationCellTypes.Center, TextureRegionName = "TextureRegionName" },
                    },
                },
                LandscapeCells = landscapeCells.ToList(),
                DecorationCells = decorationCells.ToList(),
            };

            string outputFileName = $"{Path.GetFileNameWithoutExtension(sourceFileName)}.xml";

            using StreamWriter streamWriter = new StreamWriter(outputFileName);
            using XmlWriter xmlWriter = XmlWriter.Create(streamWriter, new XmlWriterSettings() { Indent = true });
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            serializer.Serialize(xmlWriter, map);
        }
    }
}
