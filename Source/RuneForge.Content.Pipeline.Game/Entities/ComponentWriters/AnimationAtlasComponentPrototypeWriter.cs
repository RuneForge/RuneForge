using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Entities.Components;

namespace RuneForge.Content.Pipeline.Game.Entities.ComponentWriters
{
    public class AnimationAtlasComponentPrototypeWriter : ComponentPrototypeWriter<AnimationAtlasComponentPrototype>
    {
        public override void WriteComponentPrototype(ContentWriter contentWriter, AnimationAtlasComponentPrototype componentPrototype)
        {
            contentWriter.Write(componentPrototype.AnimationAtlasAssetName);
            contentWriter.Write(componentPrototype.HasPlayerColor);
        }
    }
}
