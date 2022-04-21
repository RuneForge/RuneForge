using System.Xml.Serialization;

using RuneForge.Content.Pipeline.Game.Components.Attributes;
using RuneForge.Content.Pipeline.Game.Components.PrototypeWriters;
using RuneForge.Game.Components.Entities;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    [ComponentPrototypeWriter(typeof(MovementComponentPrototypeWriter))]
    public class MovementComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.MovementComponentPrototype, RuneForge.Game";

        [XmlAttribute(AttributeName = "MovementSpeed")]
        public float MovementSpeed { get; set; }

        [XmlAttribute(AttributeName = "MovementType")]
        public MovementFlags MovementType { get; set; }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
