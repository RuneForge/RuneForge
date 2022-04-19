using System.ComponentModel;
using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Entities.Attributes;
using RuneForge.Content.Pipeline.Game.Entities.ComponentWriters;

namespace RuneForge.Content.Pipeline.Game.Entities.Components
{
    [ComponentPrototypeWriter(typeof(LocationComponentPrototypeWriter))]
    public class LocationComponentPrototype : ComponentPrototype
    {
        private const int c_xDefaultValue = -1;
        private const int c_yDefaultValue = -1;
        private const int c_widthDefaultValue = -1;
        private const int c_heightDefaultValue = -1;
        private const string c_runtimeTypeName = "RuneForge.Game.Entities.Components.LocationComponentPrototype, RuneForge.Game";

        [XmlIgnore]
        public int? X { get; set; }
        [XmlIgnore]
        public int? Y { get; set; }

        [XmlIgnore]
        public int? Width { get; set; }
        [XmlIgnore]
        public int? Height { get; set; }

        [DefaultValue(c_xDefaultValue)]
        [XmlAttribute(AttributeName = "X")]
        public int XSerializable
        {
            get => X.Value;
            set
            {
                if (value != c_xDefaultValue)
                    X = value;
            }
        }
        public bool ShouldSerializeX
        {
            get => X.HasValue;
        }
        [DefaultValue(c_yDefaultValue)]
        [XmlAttribute(AttributeName = "Y")]
        public int YSerializable
        {
            get => Y.Value;
            set
            {
                if (value != c_yDefaultValue)
                    Y = value;
            }
        }
        public bool ShouldSerializeY
        {
            get => Y.HasValue;
        }

        [DefaultValue(c_widthDefaultValue)]
        [XmlAttribute(AttributeName = "Width")]
        public int WidthSerializable
        {
            get => Width.Value;
            set
            {
                if (value != c_widthDefaultValue)
                    Width = value;
            }
        }
        public bool ShouldSerializeWidth
        {
            get => Width.HasValue;
        }
        [DefaultValue(c_heightDefaultValue)]
        [XmlAttribute(AttributeName = "Height")]
        public int HeightSerializable
        {
            get => Height.Value;
            set
            {
                if (value != c_heightDefaultValue)
                    Height = value;
            }
        }
        public bool ShouldSerializeHeight
        {
            get => Height.HasValue;
        }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
