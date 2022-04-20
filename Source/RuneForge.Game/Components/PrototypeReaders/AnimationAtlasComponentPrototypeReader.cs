using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
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
