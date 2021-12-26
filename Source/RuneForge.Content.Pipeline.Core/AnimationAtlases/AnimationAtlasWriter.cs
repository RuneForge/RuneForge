using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace RuneForge.Content.Pipeline.Core.AnimationAtlases
{
    [ContentTypeWriter]
    public class AnimationAtlasWriter : ContentTypeWriter<AnimationAtlas>
    {
        private const string c_runtimeReaderTypeName = "RuneForge.Core.AnimationAtlases.AnimationAtlasReader, RuneForge.Core";

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return c_runtimeReaderTypeName;
        }

        protected override void Write(ContentWriter writer, AnimationAtlas animationAtlas)
        {
            writer.Write(animationAtlas.TextureAssetName);

            writer.Write(animationAtlas.Animations.Count);
            foreach (Animation2D animation in animationAtlas.Animations)
            {
                writer.Write(animation.Name);

                writer.Write(animation.Looped);
                writer.Write(animation.Reversed);

                writer.Write(animation.AnimationRegions.Count);
                foreach (AnimationRegion2D animationRegion in animation.AnimationRegions)
                {
                    writer.Write(animationRegion.Name);

                    writer.Write(animationRegion.X);
                    writer.Write(animationRegion.Y);
                    writer.Write(animationRegion.Width);
                    writer.Write(animationRegion.Height);

                    writer.Write(animationRegion.FrameTimeMilliseconds);
                }
            }
        }
    }
}
