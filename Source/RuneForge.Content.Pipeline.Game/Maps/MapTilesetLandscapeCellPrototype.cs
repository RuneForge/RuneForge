using System.Xml.Serialization;

using RuneForge.Game.Maps;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    public class MapTilesetLandscapeCellPrototype
    {
        [XmlAttribute(AttributeName = "Tier")]
        public MapLandscapeCellTier Tier { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public MapLandscapeCellTypes Type { get; set; }

        [XmlElement(ElementName = "MovementFlags")]
        public MapLandscapeCellMovementFlags MovementFlags { get; set; }
        [XmlElement(ElementName = "BuildingFlags")]
        public MapLandscapeCellBuildingFlags BuildingFlags { get; set; }

        [XmlElement(ElementName = "TextureRegionName")]
        public string TextureRegionName { get; set; }
    }
}
