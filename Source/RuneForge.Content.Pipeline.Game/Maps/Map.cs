using System.Collections.Generic;
using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    [XmlRoot(ElementName = "Map")]
    public class Map
    {
        [XmlElement(ElementName = "Width")]
        public int Width { get; set; }
        [XmlElement(ElementName = "Height")]
        public int Height { get; set; }

        [XmlElement(ElementName = "Tileset")]
        public MapTileset Tileset { get; set; }

        [XmlArray(ElementName = "Cells")]
        [XmlArrayItem(ElementName = "Cell")]
        public List<MapCell> Cells { get; set; }

        [XmlArray(ElementName = "Decorations")]
        [XmlArrayItem(ElementName = "Decoration")]
        public List<MapDecoration> Decorations { get; set; }
    }
}
