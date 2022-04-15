using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Entities.Interfaces;

namespace RuneForge.Game.Entities
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
