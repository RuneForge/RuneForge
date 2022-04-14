using System;
using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Units
{
    public class UnitInstancePrototype
    {
        [XmlElement(ElementName = "OwnerId")]
        public Guid OwnerId { get; set; }

        [XmlElement(ElementName = "EntityPrototypeAssetName")]
        public string EntityPrototypeAssetName { get; set; }
    }
}
