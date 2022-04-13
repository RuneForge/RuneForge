using System.Xml.Serialization;

using RuneForge.Game.Maps;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    public class MapTilesetDecorationCellPrototype
    {
        [XmlAttribute(AttributeName = "Tier")]
        public MapDecorationCellTier Tier { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public MapDecorationCellTypes Type { get; set; }

        [XmlElement(ElementName = "MovementFlags")]
        public MapDecorationCellMovementFlags MovementFlags { get; set; }
        [XmlElement(ElementName = "BuildingFlags")]
        public MapDecorationCellBuildingFlags BuildingFlags { get; set; }

        [XmlElement(ElementName = "TextureRegionName")]
        public string TextureRegionName { get; set; }
    }
}
