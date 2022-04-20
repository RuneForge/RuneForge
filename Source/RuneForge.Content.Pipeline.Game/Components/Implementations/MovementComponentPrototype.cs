using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Components.Implementations
{
    public class MovementComponentPrototype : ComponentPrototype
    {
        private const string c_runtimeTypeName = "RuneForge.Game.Components.Implementations.MovementComponentPrototype, RuneForge.Game";

        [XmlAttribute(AttributeName = "MovementSpeed")]
        public float MovementSpeed { get; set; }

        public override string GetRuntimeTypeName()
        {
            return c_runtimeTypeName;
        }
    }
}
