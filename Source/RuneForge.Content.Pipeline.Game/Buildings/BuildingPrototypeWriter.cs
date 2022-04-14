using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace RuneForge.Content.Pipeline.Game.Buildings
{
    [ContentTypeWriter]
    public class BuildingPrototypeWriter : ContentTypeWriter<BuildingPrototype>
    {
        private const string c_runtimeReaderTypeName = "RuneForge.Game.Buildings.BuildingPrototypeReader, RuneForge.Game";

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return c_runtimeReaderTypeName;
        }

        protected override void Write(ContentWriter writer, BuildingPrototype buildingPrototype)
        {
            writer.Write(buildingPrototype.Name);
        }
    }
}
