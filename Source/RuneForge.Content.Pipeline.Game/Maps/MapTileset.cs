using System.Collections.Generic;
using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    public class MapTileset
    {
        [XmlElement(ElementName = "TextureAtlasName")]
        public string TextureAtlasName { get; set; }

        [XmlArray(ElementName = "CellPrototypes")]
        [XmlArrayItem(ElementName = "CellPrototype")]
        public List<MapTilesetCellPrototype> CellPrototypes { get; set; }

        [XmlArray(ElementName = "DecorationPrototypes")]
        [XmlArrayItem(ElementName = "DecorationPrototype")]
        public List<MapTilesetDecorationPrototype> DecorationPrototypes { get; set; }
    }
}
