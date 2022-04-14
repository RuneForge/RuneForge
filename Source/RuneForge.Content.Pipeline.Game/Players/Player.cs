using System;
using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Players
{
    public class Player
    {
        [XmlAttribute(AttributeName = "Id")]
        public Guid Id { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Color")]
        public PlayerColor Color { get; set; }
    }
}
