using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace RuneForge.Content.Pipeline.Core.TextureAtlases
{
    [ContentTypeWriter]
    public class TextureAtlasWriter : ContentTypeWriter<TextureAtlas>
    {
        private const string c_runtimeReaderTypeName = "RuneForge.Core.TextureAtlases.TextureAtlasReader, RuneForge.Core";

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return c_runtimeReaderTypeName;
        }

        protected override void Write(ContentWriter writer, TextureAtlas textureAtlas)
        {
            writer.Write(textureAtlas.TextureAssetName);

            writer.Write(textureAtlas.TextureRegions.Count);
            foreach (TextureRegion2D textureRegion in textureAtlas.TextureRegions)
            {
                writer.Write(textureRegion.Name);

                writer.Write(textureRegion.X);
                writer.Write(textureRegion.Y);
                writer.Write(textureRegion.Width);
                writer.Write(textureRegion.Height);
            }
        }
    }
}
