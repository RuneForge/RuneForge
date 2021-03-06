using System.Collections.Generic;
using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    public class MapTileset
    {
        [XmlElement(ElementName = "TextureAtlasName")]
        public string TextureAtlasName { get; set; }

        [XmlArray(ElementName = "LandscapeCellPrototypes")]
        [XmlArrayItem(ElementName = "LandscapeCellPrototype")]
        public List<MapTilesetLandscapeCellPrototype> LandscapeCellPrototypes { get; set; }

        [XmlArray(ElementName = "DecorationCellPrototypes")]
        [XmlArrayItem(ElementName = "DecorationCellPrototype")]
        public List<MapTilesetDecorationCellPrototype> DecorationCellPrototypes { get; set; }
    }
}
