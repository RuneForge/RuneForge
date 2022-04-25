using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Components;
using RuneForge.Content.Pipeline.Game.Components.Implementations;

namespace RuneForge.Content.Pipeline.Game.Players
{
    public class PlayerPrototype
    {
        [XmlAttribute(AttributeName = "Id")]
        public Guid Id { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Color")]
        public PlayerColor Color { get; set; }

        [XmlArray(ElementName = "ComponentPrototypes")]
        [XmlArrayItem(ElementName = "ResourceContainerComponentPrototype", Type = typeof(ResourceContainerComponentPrototype))]
        public List<ComponentPrototype> ComponentPrototypes { get; set; }
    }
}
