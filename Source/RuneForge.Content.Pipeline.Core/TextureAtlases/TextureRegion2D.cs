using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Core.TextureAtlases
{
    public class TextureRegion2D
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "X")]
        public int X { get; set; }

        [XmlAttribute(AttributeName = "Y")]
        public int Y { get; set; }

        [XmlAttribute(AttributeName = "Width")]
        public int Width { get; set; }

        [XmlAttribute(AttributeName = "Height")]
        public int Height { get; set; }
    }
}
