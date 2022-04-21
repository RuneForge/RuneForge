using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Implementations;

namespace RuneForge.Game.Components.PrototypeReaders
{
    public class AnimationStateComponentPrototypeReader : ComponentPrototypeReader<AnimationStateComponentPrototype>
    {
        public override AnimationStateComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            return new AnimationStateComponentPrototype();
        }
    }
}
