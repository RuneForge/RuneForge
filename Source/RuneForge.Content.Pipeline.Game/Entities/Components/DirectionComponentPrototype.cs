using System.ComponentModel;
using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Entities.Attributes;
using RuneForge.Content.Pipeline.Game.Entities.ComponentWriters;
using RuneForge.Game.Maps;

namespace RuneForge.Content.Pipeline.Game.Entities.Components
{
    [ComponentPrototypeWriter(typeof(DirectionComponentPrototypeWriter))]
    public class DirectionComponentPrototype : ComponentPrototype
    {
        private const Directions c_directionDefaultValue = Directions.None;
        private const string c_runtimeTypeName = "RuneForge.Game.Entities.Components.DirectionComponentPrototype, RuneForge.Game";

        [XmlIgnore]
        public Directions? Direction { get; set; }

        [DefaultValue(c_directionDefaultValue)]
        [XmlAttribute(AttributeName = "Direction")]
        public Directions DirectionSerializable
        {
            get => Direction.Value;
            set
            {
                if (value != c_directionDefaultValue)
                    Direction = value;
            }
        }
        public bool ShouldSerializeDirection
        {
            get => Direction.HasValue;
        }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
