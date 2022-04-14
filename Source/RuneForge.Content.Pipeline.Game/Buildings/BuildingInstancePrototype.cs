using System;
using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Buildings
{
    public class BuildingInstancePrototype
    {
        [XmlElement(ElementName = "OwnerId")]
        public Guid OwnerId { get; set; }

        [XmlElement(ElementName = "EntityPrototypeAssetName")]
        public string EntityPrototypeAssetName { get; set; }
    }
}
