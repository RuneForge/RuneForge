using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components.Interfaces;

namespace RuneForge.Content.Pipeline.Game.Components
{
    public abstract class ComponentPrototypeWriter<TComponentPrototype> : IComponentPrototypeWriter<TComponentPrototype>
        where TComponentPrototype : ComponentPrototype
    {
        protected ComponentPrototypeWriter()
        {
        }

        public abstract void WriteComponentPrototype(ContentWriter contentWriter, TComponentPrototype componentPrototype);

        public void WriteComponentPrototype(ContentWriter contentWriter, ComponentPrototype componentPrototype)
        {
            WriteComponentPrototype(contentWriter, (TComponentPrototype)componentPrototype);
        }
    }
}
