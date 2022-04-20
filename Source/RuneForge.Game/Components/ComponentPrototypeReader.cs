using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Components.Interfaces;

namespace RuneForge.Game.Components
{
    public abstract class ComponentPrototypeReader<TComponentPrototype> : IComponentPrototypeReader<TComponentPrototype>
        where TComponentPrototype : ComponentPrototype
    {
        protected ComponentPrototypeReader()
        {
        }

        public abstract TComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader);

        #region IComponentPrototypeReader Implicit Implementation

        ComponentPrototype IComponentPrototypeReader.ReadComponentPrototype(ContentReader contentReader)
        {
            return ReadTypedComponentPrototype(contentReader);
        }

        #endregion
    }
}
