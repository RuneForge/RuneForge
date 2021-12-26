using System.Collections.Generic;
using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Core.AnimationAtlases
{
    public class Animation2D
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Looped")]
        public bool Looped { get; set; }

        [XmlAttribute(AttributeName = "Reversed")]
        public bool Reversed { get; set; }

        [XmlArray(ElementName = "AnimationRegions")]
        [XmlArrayItem(ElementName = "AnimationRegion")]
        public List<AnimationRegion2D> AnimationRegions { get; set; }
    }
}
