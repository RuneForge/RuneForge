using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace RuneForge.Content.Pipeline.Game.Entities.Interfaces
{
    public interface IComponentPrototypeWriter
    {
        public void WriteComponentPrototype(ContentWriter contentWriter, ComponentPrototype componentPrototype);
    }

    public interface IComponentPrototypeWriter<TComponentPrototype> : IComponentPrototypeWriter
        where TComponentPrototype : ComponentPrototype
    {
        public void WriteComponentPrototype(ContentWriter contentWriter, TComponentPrototype componentPrototype);
    }
}
