using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Entities.Attributes;
using RuneForge.Content.Pipeline.Game.Entities.ComponentWriters;

namespace RuneForge.Content.Pipeline.Game.Entities.Components
{
    [ComponentPrototypeWriter(typeof(LocationComponentPrototypeWriter))]
    public class LocationComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Entities.Components.LocationComponentPrototype, RuneForge.Game";

        [XmlAttribute(AttributeName = "X")]
        public int X { get; set; }
        [XmlAttribute(AttributeName = "Y")]
        public int Y { get; set; }

        [XmlAttribute(AttributeName = "Width")]
        public int Width { get; set; }
        [XmlAttribute(AttributeName = "Height")]
        public int Height { get; set; }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
