using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace RuneForge.Content.Pipeline.Game.Units
{
    [ContentTypeWriter]
    public class UnitPrototypeWriter : ContentTypeWriter<UnitPrototype>
    {
        private const string c_runtimeReaderTypeName = "RuneForge.Game.Units.UnitPrototypeReader, RuneForge.Game";

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return c_runtimeReaderTypeName;
        }

        protected override void Write(ContentWriter writer, UnitPrototype unitPrototype)
        {
            writer.Write(unitPrototype.Name);
        }
    }
}
