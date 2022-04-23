using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Buildings;
using RuneForge.Content.Pipeline.Game.Players;
using RuneForge.Content.Pipeline.Game.Units;

namespace RuneForge.Content.Pipeline.Game.Maps
{
    [XmlRoot(ElementName = "Map")]
    public class Map
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Width")]
        public int Width { get; set; }
        [XmlElement(ElementName = "Height")]
        public int Height { get; set; }

        [XmlElement(ElementName = "HumanPlayerId")]
        public Guid HumanPlayerId { get; set; }

        [XmlElement(ElementName = "Tileset")]
        public MapTileset Tileset { get; set; }

        [XmlArray(ElementName = "PlayerPrototypes")]
        [XmlArrayItem(ElementName = "PlayerPrototype")]
        public List<PlayerPrototype> PlayerPrototypes { get; set; }

        [XmlArray(ElementName = "DecorationPrototypes")]
        [XmlArrayItem(ElementName = "DecorationPrototype")]
        public List<MapDecorationPrototype> DecorationPrototypes { get; set; }

        [XmlArray(ElementName = "UnitInstancePrototypes")]
        [XmlArrayItem(ElementName = "UnitInstancePrototype")]
        public List<UnitInstancePrototype> UnitInstancePrototypes { get; set; }
        [XmlArray(ElementName = "BuildingInstancePrototypes")]
        [XmlArrayItem(ElementName = "BuildingInstancePrototype")]
        public List<BuildingInstancePrototype> BuildingInstancePrototypes { get; set; }

        [XmlArray(ElementName = "LandscapeCells")]
        [XmlArrayItem(ElementName = "LandscapeCell")]
        public List<MapLandscapeCell> LandscapeCells { get; set; }
        [XmlArray(ElementName = "DecorationCells")]
        [XmlArrayItem(ElementName = "DecorationCell")]
        public List<MapDecorationCell> DecorationCells { get; set; }
    }
}
