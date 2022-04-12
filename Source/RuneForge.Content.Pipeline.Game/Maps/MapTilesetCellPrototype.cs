﻿using System.Xml.Serialization;

using RuneForge.Game.Maps;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    public class MapTilesetCellPrototype
    {
        [XmlAttribute(AttributeName = "Tier")]
        public MapCellTier Tier { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public MapCellTypes Type { get; set; }

        [XmlElement(ElementName = "TextureRegionName")]
        public string TextureRegionName { get; set; }
    }
}
