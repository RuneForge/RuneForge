using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Entities.Components;

namespace RuneForge.Game.Entities.ComponentReaders
{
    public class AnimationAtlasComponentPrototypeReader : ComponentPrototypeReader<AnimationAtlasComponentPrototype>
    {
        public override AnimationAtlasComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            string animationAtlasAssetName = contentReader.ReadString();
            bool hasPlayerColor = contentReader.ReadBoolean();
            return new AnimationAtlasComponentPrototype(animationAtlasAssetName, hasPlayerColor);
        }
    }
}
